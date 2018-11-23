using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProximityDetectionSystemInfo;


namespace DataExtractorWindow
{
    

    class DataLogManager
    {
        public Action<int> ReportProgressDelegate { get; set;}

        private void ReportProgress(int percent)
        {
            if (ReportProgressDelegate != null)
            { 
                ReportProgressDelegate(percent);
            }
        }
        
        

        

        #region Public Variables
        public UInt32 Total_Log_Entries = 0;
        public byte Total_Bytes_in_Entry = 16;
        public UInt32 Log_Summary_Count = 1;
        public DateTime Oldest_Entry = DateTime.Now;
        public DateTime Newest_Entry = new DateTime();
        public UInt32 Log_Entry_Counter = 1;
        public UInt32 Log_Event_Total = 0;
        public UInt32 Log_Parameter_Total = 0;
        public UInt32 Log_CAS_Events = 0;
        public UInt32 Log_Discard_Event_Total = 0;
        public UInt32 Log_Discard_Event_Counter = 0;
        public UInt32 Log_Discard_DateTime_Counter = 0;
        public UInt32 Log_Error_DateTime_Counter = 0;
        public UInt32 Log_Default_DateTime_Counter = 0;
        public UInt32 Log_Clear_DateTime_Counter = 0;

        public List<Log_Entry> Temp_list = new List<Log_Entry>();
        public List<EventSummaryInformation> EventID_CountList = new List<EventSummaryInformation>();

        public DateTime DateTimeStamp;
        public MainWindow MainApp;
 

        #endregion
        //        DateTimeCheck DateTime_Check = new DateTimeCheck();

        //  public Data_Manager(MainWindow Main_Window)
        //    {
        //       MainApp = new MainWindow();
        //       }




        public void Create_EventList (Enum EventID_Enum, Enum EventTotal_Enum)
        {
            // ---- Get the size of the enum ----
            int enumlen = Enum.GetNames(EventID_Enum.GetType()).Length;
            UInt32 i = 0;
            // ---- Create a list with the size of the enum ----
            for (i = 0; i < enumlen; i++)
            {
                // ---- Add entry for each index of enum, with name and description from enum ----
                EventID_CountList.Add(new EventSummaryInformation
                {
                    Event_ID = i,
                    Event_Count = 0,
                    Event_Name = Log_Entry.GetEnumDisplayName(EventID_Enum, i),
                    Event_Description = Log_Entry.GetEnumDescription(EventID_Enum, i),
                    Event_Group = Log_Entry.GetEnumCategory(EventID_Enum, i),
                });
            }

            // ---- Get the size of the enum ----
            enumlen = Enum.GetNames(EventTotal_Enum.GetType()).Length;

            // ---- Create a list with the size of the enum ----
            for (UInt32 j = i; j < (i + enumlen); j++)
            {
                // ---- Add entry for each index of enum, with name and description from enum ----
                EventID_CountList.Add(new EventSummaryInformation
                {
                    Event_ID = j,
                    Event_Count = 0,
                    Event_Name = Log_Entry.GetEnumDisplayName(EventTotal_Enum, (j - i)),
                    Event_Description = Log_Entry.GetEnumDescription(EventTotal_Enum, (j - i)),
                    Event_Group = Log_Entry.GetEnumCategory(EventTotal_Enum, (j - i)),
                });
            }

        }


        public void Read_File(string Log_Filename)
        {

            byte[] logBytes = { 0 };
            byte[] logChuncks = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] logTimeStamp = { 0, 0, 0, 0, 0, 0 };
            byte[] logData = { 0, 0, 0, 0, 0, 0, 0, 0 };
            UInt16 logID = 0;
            string Log_Info_raw = System.IO.File.ReadAllText(Log_Filename, Encoding.Default);
            int File_Length = (int)(new FileInfo(Log_Filename).Length);
            BinaryReader breader = new BinaryReader(File.OpenRead(Log_Filename));

            logBytes = breader.ReadBytes(File_Length);

            Create_EventList(PDS_EventID.EV_Power_Down, PDS_EventTotal.Total_Events);

            Log_Summary_Count = 1;

            int Percentage_Complete = 0;
            UInt32 UID = 0;
            UInt16 VID = 0;
            byte LicenceType = 0;
            UInt16 Direction = 0;
            string Event_Information = " ";

            DateTime ExpiryDate;

            Total_Log_Entries = (UInt32)((float)File_Length / (float)Total_Bytes_in_Entry);
            Log_Event_Total = Total_Log_Entries;


            for (int i = 0; i < (int)Total_Log_Entries - 1; i++)
            {
                for (int j = 0; j < Total_Bytes_in_Entry; j++)
                {
                    logChuncks[j] = logBytes[i * Total_Bytes_in_Entry + j];
                }

                // ---- Copy from the 16 byte logChunk the DateTimeStamp ----
                Buffer.BlockCopy(logChuncks, 0, logTimeStamp, 0, 6);
                // ---- Copy from the 16 byte logChunk the Data ----
                Buffer.BlockCopy(logChuncks, 8, logData, 0, 8);

                DateTime Event_DateTime;

                uint DateTime_Status = Validation.CheckTimeStamp(logTimeStamp, out Event_DateTime);

                if (DateTime_Status == (uint)Validation.Status.Ok)
                {
                    logID = BitConverter.ToUInt16(logChuncks, 6);

                    if ((logID > 0) && (logID < 376))
                    {
                        //theList.ElementAt(logID).Event_Count++;
                        EventID_CountList.Find(x => x.Event_ID == logID).Event_Count++;
                        
                        UID = 0;
                        VID = 0;
                        Direction = 0;
                        Event_Information = " ";

                        if ((logID >= 20) && (logID <= 343))
                        {
                            byte[] temp = { 0, 0, 0, 0, 0, 0, 0, 0 };
                            Buffer.BlockCopy(logData, 0, temp, 0, 8);
                            Array.Reverse(temp);

                            UID = BitConverter.ToUInt32(temp, 4);
                            VID = BitConverter.ToUInt16(temp, 2);
                            Direction = BitConverter.ToUInt16(temp, 0);

                            Event_Information = "UID: " + UID.ToString("X") + " " +
                                                "VID: " + VID.ToString() + " " +
                                                "Direction: " + Direction.ToString() + " deg";

                            Log_CAS_Events++;
                        }
                        else if (logID == (int)PDS_EventID.EV_Card_Inserted)
                        {

                            byte[] Expiry_Date = { 0, 0, 0, 0, 0, 0 };
                            UID = BitConverter.ToUInt32(logData, 0);
                            LicenceType = logData[4];
                            Buffer.BlockCopy(logData, 5, Expiry_Date, 0, 3);

                            if (Validation.CheckTimeStamp(Expiry_Date, out ExpiryDate) != (uint)Validation.Status.Ok)
                            {

                                Event_Information = "UID: " + UID.ToString("X") + " " +
                                                        "Licence Type: " + LicenceType.ToString() + " " +
                                                        "Expiry Date: " + ExpiryDate.ToShortDateString();
                            }
                            else
                            {
                                Event_Information = "UID: " + UID.ToString("X") + " " +
                                                        "Licence Type: " + LicenceType.ToString() + " " +
                                                        "Expiry Date: " + ExpiryDate.ToShortDateString() + " Date Invalid";
                            }

                        }
                        else
                        {
                            
                        }

                        Temp_list.Add(new Log_Entry
                        {
                            No = Log_Entry_Counter++,
                            DateTimeStamp = Event_DateTime,
                            EventID = logID,
                            _EventDescription = Convert.ToInt32(logID),
                            RawData = BitConverter.ToString(logData),
                            _UID = UID,
                            _VID = VID,
                            _EventInformation = Event_Information
                        });

                        if (Oldest_Entry > Event_DateTime)
                        {
                            Oldest_Entry = Event_DateTime;
                        }
                        if (Newest_Entry < Event_DateTime)
                        {
                            Newest_Entry = Event_DateTime;
                        }

                    }
                    else if ((logID >= 1000) && (logID <= 1245))
                    {
                        Log_Parameter_Total++;
                    }
                    else
                    {
                        Log_Discard_Event_Counter++;
                    }
                }



                else if (DateTime_Status == (uint)Validation.Status.Error_2)
                {
                    Log_Error_DateTime_Counter++;
                }
                else if (DateTime_Status == (uint)Validation.Status.Error_3)
                {
                    Log_Default_DateTime_Counter++;
                }
                else if (DateTime_Status == (uint)Validation.Status.Error_4)
                {
                    Log_Clear_DateTime_Counter++;
                }
                else

                {
                    Log_Discard_DateTime_Counter++;
                }

                if ((i % (Total_Log_Entries / 100)) == 0)
                {
                    Percentage_Complete++;
                    ReportProgress(Percentage_Complete);                   
                }

            }

            Log_Discard_Event_Total = Log_Discard_Event_Counter + Log_Discard_DateTime_Counter + Log_Default_DateTime_Counter + Log_Clear_DateTime_Counter;


        }

        protected virtual void Task_Progress_Update(EventArgs e)
        {

        }


    }
}
