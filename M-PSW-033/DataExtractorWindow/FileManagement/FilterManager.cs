using DataExtractorWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace Log_Viewer.FileManagement
{

    class FilterManager
    {
        public DateTime filter_Start_Date = new DateTime();
        public DateTime filter_End_Date = new DateTime();
        public ICollectionView Filter_CollectionView;
        public String Filter_Text = "";
        public String RawDataFilter_Text = ""; 
        public bool Check_All_Selected = false;
        public IList<String> Events_Selected;
        public UInt32 Total_Filtered_Entries = 0;
        public UInt32 Total_Log_Entries = 0;

        public string[] Filter_And_String_Array;
        public string[] Filter_Or_String_Array;
        public string Single_Filter = "";

        public Action<int> ReportProgressDelegate { get; set; }

        private void ReportProgress(int percent)
        {
            if (ReportProgressDelegate != null)
            {
                ReportProgressDelegate(percent);
            }
        }

        public void Filter()
        {
            // ReportProgress(Percentage_Complete);
            ReportProgress(0);
            Filter_And_String_Array = null;
            Filter_Or_String_Array = null;
            Single_Filter = "";
            if (Filter_Text.Contains("&"))
            {
                string[] Search_Text = Filter_Text.Split(new char[] { '&' });

                int count = 0;
                foreach (string Item in Search_Text)
                {
                    if (Item.Length != 0)
                    {
                        if (Item.LastIndexOf(' ') == Item.Length - 1)
                        {
                            Search_Text[count] = Item.Remove(Item.LastIndexOf(' '), 1);
                        }

                        if (Item.IndexOf(' ') == 0)
                        {
                            Search_Text[count] = Item.Remove(0, 1);
                        }
                    }
                    count++;
                }
                Filter_And_String_Array = Search_Text;
            }
            else if (Filter_Text.Contains("|"))
            {
                string[] Search_Text = Filter_Text.Split(new char[] { '|' });
                int count = 0;
                foreach (string Item in Search_Text)
                {

                    if (Item.Length != 0)
                    {
                        if (Item.LastIndexOf(' ') == Item.Length - 1)
                        {
                            Search_Text[count] = Item.Remove(Item.LastIndexOf(' '), 1);                            
                        }

                        if (Item.IndexOf(' ') == 0)
                        {
                            Search_Text[count] = Item.Remove(0, 1);
                        }
                    }
                    count++;
                }
                Filter_Or_String_Array = Search_Text;

            }
            else
            {
                Single_Filter = Filter_Text;
            }


        Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Filter_CollectionView.Filter = Filter_Item;
            }));
            ReportProgress(100);
        }

        public bool Filter_Item(object item2)
        {



        int Count = 0;
            int Percentage_Complete = 0;
            bool Value = false;

            Count++;
            if ((Count % (Total_Log_Entries / 100)) == 0)
            {
                Percentage_Complete++;
                ReportProgress(Percentage_Complete);
            }
            // Data_Management1.Log_Summary_Count++;

            Value = false;
            LogEntry p = item2 as LogEntry;

            //filter selected events
            if (!Events_Selected.Contains("Select All"))
            {
                foreach (var item in Events_Selected)
                {
                    if (p.EventName == item.ToString())
                    {
                        Value = true;
                    }
                }
            }
            else
            {
                Value = true;
            }

            if (Filter_And_String_Array != null)
            {
                foreach (string item in Filter_And_String_Array)
                {
                    if (!p.EventInformation.CaseInsensitiveContains(item))
                    {
                        return false;
                    }
                }
            }

            bool _Check_OR = false;

            if (Filter_Or_String_Array != null)
            {               
                foreach (string item in Filter_Or_String_Array)
                {
                    if (p.EventInformation.CaseInsensitiveContains(item))
                    {
                        _Check_OR = true;
                    }
                }
                if (!_Check_OR)
                {
                    return _Check_OR;
                }
            }     

            if (Single_Filter != "")
            {
                if (!p.EventInformation.CaseInsensitiveContains(Single_Filter))
                {
                    return false;
                }
            }

            if (!GeneralFunctions.RawData_Search(p.RawDataDisplay, RawDataFilter_Text))
            {
                return false;
            }

            if (Value == true)
            {
                Total_Filtered_Entries++;
            }

            return Value;
        }
    }
}
