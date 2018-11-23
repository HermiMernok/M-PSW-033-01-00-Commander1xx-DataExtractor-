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

        string Search1_Text = "";
        string Search2_Text = "";
        string Search3_Text = "";
        string Search4_Text = "";
        string Search5_Text = "";

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
           

            string[] Search_Text = { "", "" };
          


            if (Filter_Text.Contains("&"))
            {
                Search_Text = Filter_Text.Split(new char[] { '&' }, 2);
                Search1_Text = Search_Text[0];
                Search2_Text = Search_Text[1];
                Search3_Text = Search_Text[0];
                Search4_Text = Search_Text[1];

                if (Search1_Text.LastIndexOf(' ') == Search1_Text.Length - 1)
                {
                    Search1_Text.Remove(Search1_Text.LastIndexOf(' '), 1);
                }

                if (Search2_Text.IndexOf(' ') == 1)
                {
                    Search2_Text.Remove(1, 1);
                }
            }
            else if (Filter_Text.Contains("|"))
            {

                Search_Text = Filter_Text.Split(new char[] { '|' }, 2);
                Search3_Text = Search_Text[0];
                Search4_Text = Search_Text[1];

                if (Search3_Text.LastIndexOf(' ') == Search3_Text.Length - 1)
                {
                    Search3_Text.Remove(Search3_Text.LastIndexOf(' '), 1);
                }

                if (Search4_Text.IndexOf(' ') == 1)
                {
                    Search4_Text.Remove(1, 1);
                }
            }
            else
            {

                Search5_Text = Filter_Text;
            }


            //Dispatcher.Invoke(((Action)(() => Filter_CollectionView.Filter = filter_Item))));
            
           

          //  ReportProgress(Percentage_Complete);
        }

        public bool filter_Item(object item2)
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
            Log_Entry p = item2 as Log_Entry;

            //filter selected events
            if (!Events_Selected.Contains("Select All"))
            {
                foreach (var item in Events_Selected)
                {
                    if (p.EventDescription == item.ToString())
                    {
                        Value = true;
                    }
                }
            }
            else
            {
                Value = true;
            }

            //filter date
            //if(DateTime.ParseExact(p.Date, "D", null) < filter_Start_Date || DateTime.ParseExact(p.Date, "D", null) > filter_End_Date)
            if (p.DateTimeStamp < filter_Start_Date || p.DateTimeStamp > filter_End_Date)
            {
                return false;
            }


            if (!p.EventInformation.CaseInsensitiveContains(Search1_Text) || !p.EventInformation.CaseInsensitiveContains(Search2_Text))
            {
                return false;
            }

            if (!p.EventInformation.CaseInsensitiveContains(Search3_Text) && !p.EventInformation.CaseInsensitiveContains(Search4_Text))
            {
                return false;
            }

            if (!p.EventInformation.CaseInsensitiveContains(Search5_Text))
            {
                return false;
            }

            if (!GeneralFunctions.RawData_Search(p.RawData, RawDataFilter_Text))
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
