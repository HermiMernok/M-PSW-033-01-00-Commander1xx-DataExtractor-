using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;
using System.Reflection;
using CustomObservableCollection;
using Log_Viewer.FileManagement;
using ProximityDetectionSystemInfo;

namespace DataExtractorWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainApp;
        #region Private Variables

        List<string> Log_Info = new List<string>();
        private RangeObservableCollection<Log_Entry> Log_Entry_List;
        private RangeObservableCollection<EventSummaryInformation> EventSummary_List;
        private RangeObservableCollection<Log_Summary> Unit_Info_List;
        DateTimeCheck DateTime_Check = new DateTimeCheck();
        string Log_Filename = "";
        public static BackgroundWorker BackgroundWorker_Read_File = new BackgroundWorker();
        BackgroundWorker BackgroundWorker_Filter = new BackgroundWorker();
        object _lock_Thread = new object();       
        private DataLogManager Data_Log_Management = new DataLogManager();
        private FileNameManager FileName_Management = new FileNameManager();
        FilterManager Filter_Manager;
        public string Binded_ID = "";
        #endregion
   
        public MainWindow()
        {
            InitializeComponent();
            MainApp = this;
             Filter_Manager = new FilterManager();
            //BitmapImage Meronk_Elektronik_Logo = new BitmapImage();
            //Meronk_Elektronik_Logo.UriSource = new Uri("pack://application:,,,/Resources/Icons/Mernok_Elektronik_Logo.png");

            //SplashScreen Meronk_Elektronik_Splash = new SplashScreen(Meronk_Elektronik_Logo.UriSource.ToString());



            PropertyInfo textEditorProperty = typeof(TextBox).GetProperty(
                  "TextEditor", BindingFlags.NonPublic | BindingFlags.Instance);

            object textEditor = textEditorProperty.GetValue(TextBox_RawDataFilter, null);

            // set _OvertypeMode on the TextEditor
            PropertyInfo overtypeModeProperty = textEditor.GetType().GetProperty(
                           "_OvertypeMode", BindingFlags.NonPublic | BindingFlags.Instance);

            overtypeModeProperty.SetValue(textEditor, true, null);

            Log_Entry_List = new RangeObservableCollection<Log_Entry>();
            EventSummary_List = new RangeObservableCollection<EventSummaryInformation>();
            Unit_Info_List = new RangeObservableCollection<Log_Summary>();

      
            List<string> Combobox_Events_String = new List<string>((Enum.GetValues(typeof(PDS_EventID)) as PDS_EventID[]).Select(PDS_EventID => PDS_EventID.ToString().Replace("_", " ").Remove(0, 3)));
            Combobox_Events_String.Insert(0, "Select All");

            CheckComboBox_Events.ItemsSource = Combobox_Events_String.ToArray();
            Select_All_CheckComboBox(CheckComboBox_Events, true);
            DataGrid_Log.AutoGenerateColumns = false;
            DataGrid_Log.ItemsSource = Log_Entry_List;
            DataGrid_Log.IsReadOnly = true;

            ProgressBar_TextBlock.Text = "Select file to upload (Crtl + O)";

            // ---- Create new List Collection View for Event Summary List ----
            ListCollectionView CV_Log_Summary = new ListCollectionView(EventSummary_List);
            CV_Log_Summary.GroupDescriptions.Add(new PropertyGroupDescription("Event_Group"));

            ListCollectionView CV_Unit_Info = new ListCollectionView(Unit_Info_List);
            CV_Unit_Info.GroupDescriptions.Add(new PropertyGroupDescription("Group"));

            BindingOperations.EnableCollectionSynchronization(Log_Entry_List, _lock_Thread);
            BindingOperations.EnableCollectionSynchronization(EventSummary_List, _lock_Thread);
            BindingOperations.EnableCollectionSynchronization(CV_Unit_Info, _lock_Thread);

            // ---- Set ItemSource of the datadgrid to List Collection View (CV_Log_Summary) ----
            DataGrid_Log_Summary.ItemsSource = CV_Log_Summary;
            DataGrid_Log_Summary.IsReadOnly = true;

            DataGrid_Unit_Information.ItemsSource = CV_Unit_Info;
            DataGrid_Unit_Information.IsReadOnly = true;

            Data_Log_Management.ReportProgressDelegate += BackgroundWorker_Read_File.ReportProgress;
            BackgroundWorker_Read_File.WorkerReportsProgress = true;
            BackgroundWorker_Read_File.DoWork += new DoWorkEventHandler(Process_Log_File);
            BackgroundWorker_Read_File.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);

            Filter_Manager.ReportProgressDelegate += BackgroundWorker_Filter.ReportProgress;
            BackgroundWorker_Filter.WorkerReportsProgress = true;
            BackgroundWorker_Filter.DoWork += new DoWorkEventHandler(Filter_Logs);
            BackgroundWorker_Filter.ProgressChanged += new ProgressChangedEventHandler(Background_Filter_ProgressChanged);
          
            Debug();
        }


        // Back on the 'UI' thread so we can update the progress bar
        public void Background_Filter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar_Log_Open.Value = e.ProgressPercentage;

            if (e.ProgressPercentage >= 100)
            {
                //Add_Summary_Entry("Total Filtered Entries", (Filter_Manager.Total_Filtered_Entries).ToString(), Log_Summary_List, "Filters");
                DataGrid_Log.ItemsSource = Filter_Manager.Filter_CollectionView;
                Enable_Filter_Interface(true);
            }
        }

        // Back on the 'UI' thread so we can update the progress bar
        public void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {           
             ProgressBar_Log_Open.Value = e.ProgressPercentage;
             ProgressBar_TextBlock.Text = "Opening datalog file " + ProgressBar_Log_Open.Value.ToString().PadLeft(3, '0') + " %";

            if (e.ProgressPercentage > 100)
            {
                Enable_Filter_Interface(true);
                Log_Entry_List.Clear();
                Log_Entry_List.AddRange(Data_Log_Management.Temp_list);
                Upload_Summary_Entries();
                Calender_Start_Date.SelectedDate = Data_Log_Management.Oldest_Entry;
                Calender_Start_Date.DisplayDate = Data_Log_Management.Oldest_Entry;
                DateTime Start_Time = Convert.ToDateTime("17/03/01 00:00:01");
                DateTime TimePicker_End_Time = Convert.ToDateTime("17/03/01 23:59:59");
                Calender_End_Date.SelectedDate = Data_Log_Management.Newest_Entry;
                Calender_End_Date.DisplayDate = Data_Log_Management.Newest_Entry;
                ProgressBar_TextBlock.Text = "Successfully opened datalog file" ;
            }
        }

        /// <summary>
        /// Open the application with a log file already loaded in to the datagrid.
        /// </summary>
        public void Debug()
        {
            
            Log_Filename = @"../../Resources/Sample_Log/DataLog_UnitID-514_03-08-2017_13-55-32.txt";
            BackgroundWorker_Read_File.RunWorkerAsync();

            Upload_Unit_Info_Entries(Log_Filename);            
            TabControl_Information.SelectedIndex = 2;
            Enable_Filter_Interface(true);


        }

        private void Upload_Unit_Info_Entries(string DataLog_FileName)
        {
            string Binded_ID = "------";
            DateTime File_Date;

            if (FileName_Management.Get_FileName_Information(DataLog_FileName, out Binded_ID, out File_Date))
            {
                Add_Summary_Entry("Unit ID", Binded_ID, Unit_Info_List, "Unit Details");
                Add_Summary_Entry("Extraction Date", File_Date.ToLongDateString(), Unit_Info_List, "Unit Details");
                Add_Summary_Entry("Extraction Time", File_Date.ToLongTimeString(), Unit_Info_List, "Unit Details");
            }
            else
            {
                Add_Summary_Entry("Unit ID", Binded_ID, Unit_Info_List, "Unit Details");
                Add_Summary_Entry("Extraction Date", "Invalid Date and Time on file name", Unit_Info_List, "Unit Details");
                Add_Summary_Entry("Extraction Time", "Invalid Date and Time on file name", Unit_Info_List, "Unit Details");
            }
        }

        private void Process_Log_File(object sender, DoWorkEventArgs e)
        {
            Data_Log_Management.Read_File(Log_Filename);      
        }

        private void Upload_Summary_Entries()
        {
            //Add_Summary_Entry("Total CAS Events", Data_Log_Management.Log_CAS_Events.ToString(), Log_Summary_List, "Total Events (" + Data_Log_Management.Log_Event_Total.ToString() + ")");
            //Add_Summary_Entry("Total Parameter Change Events", Data_Log_Management.Log_Parameter_Total.ToString(), Log_Summary_List, "Total Events (" + Data_Log_Management.Log_Event_Total.ToString() + ")");
            //Add_Summary_Entry("Total Error Events", Data_Log_Management.Log_Discard_Event_Total.ToString(), Log_Summary_List, "Total Events (" + Data_Log_Management.Log_Event_Total.ToString() + ")");
            //Add_Summary_Entry("Parameter Change Events", Data_Log_Management.Log_Parameter_Total.ToString(), Log_Summary_List, "Total Parameter Change Events (" + Data_Log_Management.Log_Parameter_Total.ToString() + ")");
            //Add_Summary_Entry("Event ID Errors", (Data_Log_Management.Log_Discard_Event_Counter).ToString(), Log_Summary_List, "Total Error Entries (" + Data_Log_Management.Log_Discard_Event_Total.ToString() + ")");
            //Add_Summary_Entry("Invalid Timestamp", (Data_Log_Management.Log_Discard_DateTime_Counter).ToString(), Log_Summary_List, "Total Error Entries (" + Data_Log_Management.Log_Discard_Event_Total.ToString() + ")");
            //Add_Summary_Entry("Errors Timestamp", (Data_Log_Management.Log_Error_DateTime_Counter).ToString(), Log_Summary_List, "Total Error Entries (" + Data_Log_Management.Log_Discard_Event_Total.ToString() + ")");
            //Add_Summary_Entry("Default Timestamp", (Data_Log_Management.Log_Default_DateTime_Counter).ToString(), Log_Summary_List, "Total Error Entries (" + Data_Log_Management.Log_Discard_Event_Total.ToString() + ")");
            //Add_Summary_Entry("Empty Timestamp", (Data_Log_Management.Log_Clear_DateTime_Counter).ToString(), Log_Summary_List, "Total Error Entries (" + Data_Log_Management.Log_Discard_Event_Total.ToString() + ")");

            EventSummary_List.AddRange(Data_Log_Management.EventID_CountList);
            
        }
        
        private void Button_Open_File_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            //openFileDialog.DefaultExt = "All files(*.*)";
            //openFileDialog.Filter = "Log Fils (*.Mer) | *.Mer | Text Files (*.txt) | *.txt | All files(*.*) | *.*";

            if (openFileDialog.ShowDialog() == true)
            {
                Log_Filename = openFileDialog.FileName;

                // Start the background worker
               // Read_Log_File(Log_Filename);
                Enable_Filter_Interface(true);
                if (!BackgroundWorker_Read_File.IsBusy)
                {
                    BackgroundWorker_Read_File.RunWorkerAsync();
                }
                else
                {

                }
                //Read_Log_File();
            }
        }

        /// <summary>
        /// Set user controls (enable/Disable)
        /// </summary>
        /// <param name="IsEnable"></param>
        private void Enable_Filter_Interface(bool IsEnable)
        {
            // === if IsEnable is true, enable user controls ===
            if (IsEnable)
            {
                Calender_End_Date.IsEnabled = true;
                Calender_Start_Date.IsEnabled = true;
                TimePicker_End_Time.IsEnabled = true;
                TimePicker_Start_Time.IsEnabled = true;
                TextBox_EventInformationFilter.IsEnabled = true;
                Button_Filter.IsEnabled = true;
                Button_Filter_Clear.IsEnabled = true;
            }
            // === if IsEnable is false, disable user controls ===
            else
            {
                Calender_End_Date.IsEnabled = false;
                Calender_Start_Date.IsEnabled = false;
                TimePicker_End_Time.IsEnabled = false;
                TimePicker_Start_Time.IsEnabled = false;
                TextBox_EventInformationFilter.IsEnabled = false;
                Button_Filter.IsEnabled = false;
                Button_Filter_Clear.IsEnabled = false;
            }
        }


        /// <summary>
        /// Save data logs to a .csv file with semicolon(;) as seperator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Save_File_Click(object sender, RoutedEventArgs e)
        {
            // === Open Save File Dialog ===
            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

            // == Default extension ===
            saveFileDialog1.DefaultExt = ".csv";
            // == filter types ===
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                // === open streamwrite to save file ===
                StreamWriter writer = new StreamWriter(saveFileDialog1.OpenFile());
                int counter = 0;

                // === write each entry from data log in to .csv file ===
                foreach (var Single_Log_Data in Log_Entry_List)
                {
                    counter++;

                    writer.WriteLine(Single_Log_Data.DateTimeStamp.Date + ";" +
                        Single_Log_Data.DateTimeStamp.TimeOfDay + ";" +
                        Single_Log_Data.EventID + ";" +
                        Single_Log_Data.EventDescription + ";" +
                        Single_Log_Data.RawData + ";" +
                        Single_Log_Data._UID.ToString("X") + ";" +
                        Single_Log_Data._VID //+ ";" +
                                             //Single_Log_Data._EventInformation.ToString()
                       );
                }
                
                // === dispose streamwrite ===
                writer.Dispose();
                // === close stramwrite ===
                writer.Close();

            }
        }

        /// <summary>
        /// filter button to start filtering the data logs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Filter_Click(object sender, RoutedEventArgs e)
        {
            // === start background worker to filter data === 
            Enable_Filter_Interface(false);
            
            Filter_Manager.filter_Start_Date = new DateTime(Calender_Start_Date.SelectedDate.Value.Year,
                                                            Calender_Start_Date.SelectedDate.Value.Month,
                                                            Calender_Start_Date.SelectedDate.Value.Day,
                                                            TimePicker_Start_Time.Value.Value.Hour,
                                                            TimePicker_Start_Time.Value.Value.Minute,
                                                            TimePicker_Start_Time.Value.Value.Second);

            Filter_Manager.filter_End_Date = new DateTime(Calender_End_Date.SelectedDate.Value.Year,
                                                             Calender_End_Date.SelectedDate.Value.Month,
                                                             Calender_End_Date.SelectedDate.Value.Day,
                                                             TimePicker_End_Time.Value.Value.Hour,
                                                             TimePicker_End_Time.Value.Value.Minute,
                                                             TimePicker_End_Time.Value.Value.Second);
           
            Filter_Manager.Filter_CollectionView = CollectionViewSource.GetDefaultView(DataGrid_Log.ItemsSource);
                      
            Filter_Manager.Total_Log_Entries = Data_Log_Management.Total_Log_Entries;
            Filter_Manager.Filter_Text = TextBox_EventInformationFilter.Text;
            Filter_Manager.RawDataFilter_Text = TextBox_RawDataFilter.Text;
            Filter_Manager.Events_Selected = CheckComboBox_Events.SelectedItems.Cast<String>().ToList();

            BackgroundWorker_Filter.RunWorkerAsync();

        }

        private void Filter_Logs(object sender, DoWorkEventArgs e)
        {
            Filter_Manager.Filter();
        }
       

   
        private void Button_Filter_Clear_Click(object sender, RoutedEventArgs e)
        {
            UInt32 Total_Filtered_Entries = 0;
            
            ICollectionView cv = CollectionViewSource.GetDefaultView(DataGrid_Log.ItemsSource);
            
            cv.Filter = o =>
                {
                    Total_Filtered_Entries++;
                    Log_Entry p = o as Log_Entry;
                    return true;
                };
            
            Clear_Filters();
           //Add_Summary_Entry("Total Filtered Entries", (Total_Filtered_Entries).ToString(), Log_Summary_List, "Filters");
        }
        /// <summary>
        /// Check if "Select All" entry is selected in CheckComboBox_Events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckComboBox_Events_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        {
            string selectedItem = e.Item as string; //cast to the type in the ItemsSource
            if (selectedItem == "Select All" && e.IsSelected)
            {
                Select_All_CheckComboBox(CheckComboBox_Events, true);
            }
            if (selectedItem == "Select All" && !e.IsSelected)
            {
                Select_All_CheckComboBox(CheckComboBox_Events, false);
            }
        }
        /// <summary>
        /// This function is used to select/unslect all the entries in a CheckCombobox.
        /// </summary>
        /// <param name="ChekComboBox_Temp"></param>
        /// <param name="Checked"></param>
        private void Select_All_CheckComboBox(Xceed.Wpf.Toolkit.CheckComboBox CheckComboBox_Temp, bool Checked)
        {
            // === If checked is true ===
            if (Checked)
            {

                // === check each checkbox in the checkCombobox ===
                foreach (var item in CheckComboBox_Temp.Items)
                {
                    if (!CheckComboBox_Temp.SelectedItems.Contains(item))
                    {
                        CheckComboBox_Temp.SelectedItems.Add(item);
                    }
                }
            }

            // === if check is false ===
            else
            {
                // === uncheck each checkbox in the checkcombobox ===
                foreach (var item in CheckComboBox_Temp.Items)
                {
                    if (CheckComboBox_Temp.SelectedItems.Contains(item))
                    {
                        CheckComboBox_Temp.SelectedItems.Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// CheckComboBox mouse Enter event
        ///  - add border to checkcombobox when mouse enters the user control(CheckComboBox)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckComboBox_Events_Mouse_Enter(object sender, MouseEventArgs e)
        {
            CheckComboBox_Events.BorderThickness = new Thickness(1.0);
        }

        /// <summary>
        /// CheckComboBox mouse exit event
        ///  - remove border to checkcombobox when mouse leaves the user control (CheckComboBox)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckComboBox_Events_Mouse_Leave(object sender, MouseEventArgs e)
        {
            CheckComboBox_Events.BorderThickness = new Thickness(0.0);
        }

        /// <summary>
        /// Close the Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Exit_Application_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// Remove mouse capture after clicking on a chalender
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }
               
        private void Menu_Item_About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow.MainWindow dlg = new AboutWindow.MainWindow();
            dlg.Owner = this;
            dlg.Top = (this.Top + (this.Height / 2)) - dlg.Height / 2;
            dlg.Left = (this.Left + (this.Width / 2)) - dlg.Width / 2;
            dlg.ShowDialog();

        }

        private void Add_Summary_Entry(string Log_Summary_Name, string Log_Summary_Value, ObservableCollection<Log_Summary> Current_List, string Current_Group)
        {
            if (!Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {

                    if (Current_List.Any(p => p.Name == Log_Summary_Name))
                    {
                        var oldItem = Current_List.FirstOrDefault(x => x.Name == Log_Summary_Name);

                        int Index = Current_List.IndexOf(oldItem);
                        Current_List.Remove(oldItem);

                        oldItem.Value = Log_Summary_Value;
                        oldItem.No = (uint)(Index + 1);
                        Current_List.Insert(Index, oldItem);
                    }
                    else
                    {
                        Current_List.Add(new Log_Summary
                        {
                            No = Data_Log_Management.Log_Summary_Count,
                            Name = Log_Summary_Name,
                            Value = Log_Summary_Value,
                            Group = Current_Group
                        });
                    }
                }));
            }
            else
            {
                if (Current_List.Any(p => p.Name == Log_Summary_Name))
                {
                    var oldItem = Current_List.FirstOrDefault(x => x.Name == Log_Summary_Name);
                    oldItem.Value = Log_Summary_Value;

                }
                else
                {
                    Current_List.Add(new Log_Summary
                    {
                        No = Data_Log_Management.Log_Summary_Count,
                        Name = Log_Summary_Name,
                        Value = Log_Summary_Value,
                        Group = Current_Group
                    });
                }
            }
            Data_Log_Management.Log_Summary_Count++;
        }

        private void Clear_Filters()
        {
            Calender_Start_Date.SelectedDate = Data_Log_Management.Oldest_Entry;
            Calender_Start_Date.DisplayDate = Data_Log_Management.Oldest_Entry;

            DateTime Start_Time = Convert.ToDateTime("17/03/01 00:00:01");
            DateTime TimePicker_End_Time = Convert.ToDateTime("17/03/01 23:59:59");

            Calender_End_Date.SelectedDate = Data_Log_Management.Newest_Entry;
            Calender_End_Date.DisplayDate = Data_Log_Management.Newest_Entry;

        }

        private void TextBox_KeyDown_Auto_Hypen(object sender, KeyEventArgs e)
        {
            TextBox Active_TextBox = sender as TextBox;
            string sVal = Active_TextBox.Text;


            sVal = sVal.Replace("-", "");

            string newst = Regex.Replace(sVal, ".{2}", "$0-");

        }

        /// <summary>
        /// Textbox textchanged event
        ///  - Condition textbox info to the following = $$-$$-$$-$$-$$-$$-$$-$$       
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_RawDataFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox Active_TextBox = sender as TextBox;
            char[] char_array = new char[23];
            string sVal = Active_TextBox.Text;

            sVal = sVal.Replace("-", "").ToUpper();


            int count = 0;
            for (int i = 0; i < 23; i++)
            {

                if (((i + 1) % 3) != 0)
                {

                    if (i < 23)
                    {

                        char_array[i] = sVal.ElementAt(count);
                    }
                    else
                    {
                        char_array[i] = '$';
                    }
                    count++;
                }
                else
                {
                    char_array[i] = '-';
                    if (Active_TextBox.SelectionStart == i)
                    {
                        Active_TextBox.SelectionStart = i + 1;
                    }
                }

            }
            
            int current_selected = Active_TextBox.SelectionStart;
            Active_TextBox.Text = new string(char_array);
            Active_TextBox.SelectionStart = current_selected;

        }

        /// <summary>
        /// On Preview Text input event - ensure that the text is only hex characters (0-9 A-F).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int hexNumber;
            // === ignore non hex characters ===
            e.Handled = !int.TryParse(e.Text, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.CurrentCulture, out hexNumber);
        }


        /// <summary>
        /// Textbox Preview Key down event 
        /// - Enable enter key to start filter
        /// - Substitude backspace and delete's space with a dolar sign ($)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_RawDataFilter_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox Active_TextBox = sender as TextBox;
            string sVal = Active_TextBox.Text;
            int current_selected = Active_TextBox.SelectionStart;

            // === check if enter is pressed ===
            if (e.Key == Key.Enter)
            {
                // === if enter is pressed filter data logs ===
                Button_Filter_Click(null, null);
            }

            // === check if backspace or delete is pressed ===
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = true;
                
                // === if textbox selection is not at zero
                if (Active_TextBox.SelectionStart != 0)
                {
                    // === replace deleted char with a dolar sign ($)
                    sVal = sVal.Remove(Active_TextBox.SelectionStart - 1, 1);
                    sVal = sVal.Insert(Active_TextBox.SelectionStart - 1, "$");
                    Active_TextBox.Text = sVal;
                    Active_TextBox.SelectionStart = current_selected - 1;
                }               
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewKeyDown_Filter_Key(object sender, KeyEventArgs e)
        {  
            // == check if enter is pressed ===
            if (e.Key == Key.Enter)
            {
                // === if enter is pressed filter data logs ===
                Button_Filter_Click(null, null);
            }
        }

        private EventSummaryInformation Selected_Summary;

        private void DataGrid_Log_Summary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid DataGrid_EventSummary = sender as DataGrid;
            var item = DataGrid_EventSummary.SelectedItem;

            //if (DataGrid_EventSummary is EventSummaryInformation)
            //{
                Selected_Summary = item as EventSummaryInformation;
                GroupBox_EventDescription.Header = Selected_Summary.Event_Name;
                GroupBox_EventDescription.Content = Selected_Summary.Event_Description;
            //}
        }
    }

}
