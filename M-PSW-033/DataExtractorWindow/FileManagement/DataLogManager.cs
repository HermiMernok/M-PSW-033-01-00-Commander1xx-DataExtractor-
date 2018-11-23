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
        public int Total_Bytes_in_Entry = 16;
       
        public DateTime Oldest_Entry = DateTime.Now;
        public DateTime Newest_Entry = new DateTime();

        public UInt32 Log_Summary_Count = 1;

        public UInt32 Log_Entry_Counter = 1;
        public UInt32 Log_Event_Total = 0;
        public UInt32 Log_Parameter_Total = 0;
        public UInt32 Log_CAS_Events = 0;

        public List<LogEntry> Temp_list = new List<LogEntry>();
        public List<EventSummaryInformation> EventID_CountList = new List<EventSummaryInformation>();
        #endregion

        #region Create Event List
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
                    Event_Name = EnumFunctions.GetEnumDisplayName(EventID_Enum, i),
                    Event_Description = EnumFunctions.GetEnumDescription(EventID_Enum, i),
                    Event_Group = EnumFunctions.GetEnumCategory(EventID_Enum, i),
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
                    Event_Name = EnumFunctions.GetEnumDisplayName(EventTotal_Enum, (j - i)),
                    Event_Description = EnumFunctions.GetEnumDescription(EventTotal_Enum, (j - i)),
                    Event_Group = EnumFunctions.GetEnumCategory(EventTotal_Enum, (j - i)),
                });
            }
        }
        #endregion

        #region Parse Datalog
        public void Read_File(string Log_Filename)
        {

            byte[] logBytes = { 0 };
            byte[] logChuncks = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] logTimeStamp = { 0, 0, 0, 0, 0, 0 };
            byte[] logData = { 0, 0, 0, 0, 0, 0, 0, 0 };

            string Log_Info_raw = System.IO.File.ReadAllText(Log_Filename, Encoding.Default);
            int File_Length = (int)(new FileInfo(Log_Filename).Length);
            BinaryReader breader = new BinaryReader(File.OpenRead(Log_Filename));

            logBytes = breader.ReadBytes(File_Length);

            Create_EventList(PDS_EventID.Power_Down, PDS_EventTotal.Total_Events);

            Log_Summary_Count = 1;

            int Percentage_Complete = 0;
      
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

                // ---- Create event Date and Time Stamp ----
                DateTime Event_DateTime;

                // ---- Check if event Date and Time Stamp is valid ----
                uint DateTime_Status = DateTimeCheck.CheckDateTimeStamp(logTimeStamp, out Event_DateTime);

                // ---- If valid event Date and Time Stamp was detected ----
                if (DateTime_Status == (uint) DateTimeCheck.Status.Ok)
                {
                    
                    UInt16 EventID = BitConverter.ToUInt16(logChuncks, 6);
                    string LogGroup = "";
                    string Event_Information = "";

                    // ---- Copy the Event ID into variable ----
                    EventID = BitConverter.ToUInt16(logChuncks, 6);

                    // ---- Check if event event ID is defined in enum ----
                    if ((EventID > (int) PDS_EventID.None && (EventID <= (int) PDS_EventID.AdvisoryAction_End)))
                    {
                        // ---- Increment the counter of the index where the event ID was found ----
                        EventID_CountList.Find(x => x.Event_ID == EventID).Event_Count++;

                        // ---- Get string name of event group according to the event ID ----
                        LogGroup = EnumFunctions.GetEnumCategory(PDS_EventID.None, EventID);

                        // ---- Increment the counter of the index where the event group was found ----
                        EventID_CountList.Find(x => x.Event_Name == LogGroup).Event_Count++;
                   
                        // ---- Parse the event data according to the event ID ----
                        switch (EventID)
                        {
                            case (int)PDS_EventID.Revision_OK:
                            case (int)PDS_EventID.Revision_Fail:

                                uint ParameterRev = logData[0];
                                uint ParameterSubRev = logData[1];
                                uint FirmwareRev = logData[2];
                                uint FirmwareSubRev = logData[3];

                                Event_Information = "Parameter: V" + ParameterRev.ToString() + "." + ParameterSubRev.ToString() + " , " +
                                                    "Firmware: V" + FirmwareRev.ToString() + "." + FirmwareSubRev.ToString();

                                break;

                            case (int)PDS_EventID.Expansion_Module_1_OK:
                            case (int)PDS_EventID.Expansion_Module_2_OK:
                            case (int)PDS_EventID.Expansion_Module_3_OK:
                            case (int)PDS_EventID.Expansion_Module_4_OK:

                                uint Module_KindOK = logData[0];

                                Event_Information = "Module Kind: " + Module_KindOK.ToString();

                                break;

                            case (int)PDS_EventID.Expansion_Module_1_Fail:
                            case (int)PDS_EventID.Expansion_Module_2_Fail:
                            case (int)PDS_EventID.Expansion_Module_3_Fail:
                            case (int)PDS_EventID.Expansion_Module_4_Fail:

                                uint Module_KindFail = logData[0];
                                uint Module_FailID = logData[1];

                                Event_Information = "Module Kind: " + Module_KindFail.ToString() + " , " + 
                                                    "Failure ID : " + Module_FailID.ToString();

                                break;

                            case (int)PDS_EventID.PulseDeviceRF01_OK:
                            case (int)PDS_EventID.PulseDeviceRF02_OK:
                            case (int)PDS_EventID.PulseDeviceRF03_OK:
                            case (int)PDS_EventID.PulseDeviceRF04_OK:
                            case (int)PDS_EventID.PulseDeviceRF05_OK:
                            case (int)PDS_EventID.PulseDeviceRF01_Fail:
                            case (int)PDS_EventID.PulseDeviceRF02_Fail:
                            case (int)PDS_EventID.PulseDeviceRF03_Fail:
                            case (int)PDS_EventID.PulseDeviceRF04_Fail:
                            case (int)PDS_EventID.PulseDeviceRF05_Fail:
                            case (int)PDS_EventID.PulseDeviceCAN01_OK:
                            case (int)PDS_EventID.PulseDeviceCAN02_OK:
                            case (int)PDS_EventID.PulseDeviceCAN03_OK:
                            case (int)PDS_EventID.PulseDeviceCAN04_OK:
                            case (int)PDS_EventID.PulseDeviceCAN05_OK:
                            case (int)PDS_EventID.PulseDeviceCAN01_Fail:
                            case (int)PDS_EventID.PulseDeviceCAN02_Fail:
                            case (int)PDS_EventID.PulseDeviceCAN03_Fail:
                            case (int)PDS_EventID.PulseDeviceCAN04_Fail:
                            case (int)PDS_EventID.PulseDeviceCAN05_Fail:

                                UInt32 UnitFail_UID = BitConverter.ToUInt32(logData, 0);
                                uint UnitFail_SID = logData[4];
                                Event_Information = "UID: 0x" + UnitFail_UID.ToString("X") + " , " +
                                                    "SID: " + UnitFail_SID.ToString();

                                break;

                            case (int)PDS_EventID.Estop_Latch_Int:
                            case (int)PDS_EventID.EStop_Release_Int:
                            case (int)PDS_EventID.Estop_Latch_Ext:
                            case (int)PDS_EventID.Estop_Release_Ext:

                                double EstopSpeed = ((double) BitConverter.ToInt16(logData, 0)) / 10.0;
                                uint EstopActionPrevious = logData[6];
                                uint EstopActionCurrent = logData[7];
                                Int16 EstopEvent = BitConverter.ToInt16(logData, 0);

                                Event_Information = "Speed: " + EstopSpeed.ToString("##,#00.0") + " km/h , " +
                                                    "Action Active: " + EstopActionPrevious.ToString() + " , " +
                                                    "Action EStop: " + EstopActionCurrent.ToString();

                                break;

                            case (int)PDS_EventID.License_Processed_01:

                                UInt32 License_Processed_01_UID = BitConverter.ToUInt32(logData, 0);
                                uint License_Processed_01_Type = logData[4];
                                byte[] License_Processed_01_Date = { 0, 0, 0, 0, 0, 0};
                                Buffer.BlockCopy(logData, 5, License_Processed_01_Date, 0, 3);
                                DateTime License_Processed_01_DateTime;
                                uint License_Processed_01_DateTime_Status = DateTimeCheck.CheckDateTimeStamp(License_Processed_01_Date, out License_Processed_01_DateTime);

                                Event_Information = "UID: 0x" + License_Processed_01_UID.ToString("X") + " , " +
                                                    "Type: " + License_Processed_01_Type.ToString() + " , " +
                                                    "Exipre Date: " + License_Processed_01_DateTime.ToShortDateString();

                                break;

                            case (int)PDS_EventID.License_Processed_02:

                                UInt16 License_Processed_02_Client = BitConverter.ToUInt16(logData, 0);

                                Event_Information = "Client Code: " + License_Processed_02_Client.ToString();

                                break;

                            case (int)PDS_EventID.License_Processed_03:

                                string License_Processed_03_Name = System.Text.ASCIIEncoding.Default.GetString(logData);

                                Event_Information = "Name (1 - 8): " + License_Processed_03_Name;

                                break;

                            case (int)PDS_EventID.License_Processed_04:

                                string License_Processed_04_Name = System.Text.ASCIIEncoding.Default.GetString(logData, 0, 7);

                                Event_Information = "Name (9 - 15): " + License_Processed_04_Name;

                                break;

                            case (int)PDS_EventID.Licence_Warning:

                                UInt32 Licence_Warning_UID = BitConverter.ToUInt32(logData, 0);
                                uint Licence_Warning_Type = logData[4];
                                byte[] Licence_Warning_Date = { 0, 0, 0, 0, 0, 0 };
                                Buffer.BlockCopy(logData, 5, Licence_Warning_Date, 0, 3);
                                DateTime Licence_Warning_DateTime;
                                uint Licence_Warning_DateTime_Status = DateTimeCheck.CheckDateTimeStamp(Licence_Warning_Date, out Licence_Warning_DateTime);

                                Event_Information = "UID: 0x" + Licence_Warning_UID.ToString("X") + " , " +
                                                    "Type: " + Licence_Warning_Type.ToString() + " , " +
                                                    "Exipre Date: " + Licence_Warning_DateTime.ToShortDateString();

                                break;

                            case (int)PDS_EventID.License_Invalid:
                            case (int)PDS_EventID.License_Valid:

                                UInt32 License_Valid_UID = BitConverter.ToUInt32(logData, 0);
                                uint License_Valid_Type = logData[4];
                                byte[] License_Valid_Date = { 0, 0, 0, 0, 0, 0 };
                                Buffer.BlockCopy(logData, 5, License_Valid_Date, 0, 3);
                                DateTime License_Valid_DateTime;
                                uint License_Valid_DateTime_Status = DateTimeCheck.CheckDateTimeStamp(License_Valid_Date, out License_Valid_DateTime);
                                string License_Valid_Date_String = "Invalid Date";

                                if (License_Valid_DateTime_Status == 1)
                                {
                                    License_Valid_Date_String = License_Valid_DateTime.ToShortDateString();
                                }

                                Event_Information = "UID: 0x" + License_Valid_UID.ToString("X") + " , " +
                                                    "Type: " + License_Valid_Type.ToString() + " , " +
                                                    "Exipre Date: " + License_Valid_Date_String;

                                break;

                            case (int)PDS_EventID.LowSpeedZone:
                            case (int)PDS_EventID.MeduimSpeedZone:
                            case (int)PDS_EventID.HighSpeedZone:

                                double ZoneChangeSpeed = ((double) BitConverter.ToInt16(logData, 0)) / 10.0;
                                uint ZoneChangePrevious = logData[2];
                                uint ZoneChange = logData[4];

                                Event_Information = "Speed: " + ZoneChangeSpeed.ToString("##,#00.0") + " km/h , " +
                                                    "Speed Zone Previous: " + ZoneChangePrevious.ToString() + " , " +
                                                    "Speed Zone Activate: " + ZoneChange.ToString();

                                break;

                            case (int)PDS_EventID.Speed_OK:
                            case (int)PDS_EventID.Speed_OK_End:
                            case (int)PDS_EventID.Speed_Warning:
                            case (int)PDS_EventID.Speed_Warning_End:
                            case (int)PDS_EventID.Speed_Overspeed:
                            case (int)PDS_EventID.Speed_Overspeed_End:

                                double SpeedDisplay = ((double) BitConverter.ToInt16(logData, 0)) / 10.0;
                                uint SpeedDisplayZone = logData[2];
                                uint SpeedDisplayWarning = logData[3];
                                uint SpeedDisplayOver = logData[4];

                                Event_Information = "Speed: " + SpeedDisplay.ToString("##,#00.0") + " km/h , " +
                                                    "Speed Zone: " + SpeedDisplayZone.ToString() + " , " +
                                                    "Speed Warning: " + SpeedDisplayWarning.ToString("##00") + " km/h , " +
                                                    "Speed Over: " + SpeedDisplayOver.ToString("##00") + " km/h";

                                break;

                            case (int)PDS_EventID.Operational_Workzone_01:

                                uint Operational_Workzone_01_Raduis = logData[4];

                                Event_Information = "Raduis: " + Operational_Workzone_01_Raduis.ToString() + " m";

                                break;

                            case (int)PDS_EventID.Operational_Workzone_02:

                                double Operational_Workzone_02_LAT = ((double)BitConverter.ToInt32(logData, 0) * Math.Pow(10, -7));
                                double Operational_Workzone_02_LON = ((double)BitConverter.ToInt32(logData, 4) * Math.Pow(10, -7));

                                Event_Information = "Unit Latitude: " + Operational_Workzone_02_LAT.ToString() + " deg , " +
                                                    "Unit Longitude: " + Operational_Workzone_02_LON.ToString() + " deg";

                                break;

                            case (int)PDS_EventID.SystemAction:

                                double SystemActionSpeed = ((double)BitConverter.ToInt16(logData, 0)) / 10.0;
                                uint SystemActionPrevious = logData[2];
                                uint SystemActionCurrent = logData[3];
                                Int16 SystemActionEvent = BitConverter.ToInt16(logData, 4);

                                Event_Information = "Speed: " + SystemActionSpeed.ToString("##,#00.0") + " km/h , " +
                                                    "Action Previous: " + SystemActionPrevious.ToString() + " , " +
                                                    "Action Current: " + SystemActionCurrent.ToString() + " , " +
                                                    "Event Activate: " + SystemActionEvent.ToString();

                                break;

                            case (int)PDS_EventID.PDS_01:
                            case (int)PDS_EventID.PDS_01_End:

                                UInt32 PDS_01_BID = BitConverter.ToUInt32(logData, 0);
                                uint PDS_01_Kind = logData[4];
                                uint PDS_01_Group = logData[5];
                                uint PDS_01_Type = logData[6];
                                Int32 PDS_01_Width = logData[7] * 10;

                                Event_Information = "Threat ID: 0x" + PDS_01_BID.ToString("X") + " , " +
                                                    "Threat Kind: " + PDS_01_Kind.ToString() + " , " +
                                                    "Threat Group: " + PDS_01_Group.ToString() + " , " +
                                                    "Threat Type: " + PDS_01_Type.ToString() + " , " +
                                                    "Display Width: " + PDS_01_Width.ToString() + " deg ";

                                break;

                            case (int)PDS_EventID.PDS_02:
                            case (int)PDS_EventID.PDS_02_End:

                                uint PDS_02_Sector = logData[0];
                                uint PDS_02_Zone = logData[1];
                                double PDS_02_Speed = ((double) BitConverter.ToInt16(logData, 2)) / 10.0;
                                Int16 PDS_02_Distance = BitConverter.ToInt16(logData, 4);
                                double PDS_02_Heading = BitConverter.ToInt16(logData, 6) / 100.00;

                                Event_Information = "Display Sector: " + PDS_02_Sector.ToString() + " , " +
                                                    "Display Zone: " + PDS_02_Zone.ToString() + " , " +
                                                    "Threat Speed: " + PDS_02_Speed.ToString("##,#00.0") + " km/h , " +
                                                    "Threat Distance: " + PDS_02_Distance.ToString() + " m , " +
                                                    "Threat Heading: " + PDS_02_Heading.ToString() + " deg ";

                                break;

                            case (int)PDS_EventID.PDS_03:
                            case (int)PDS_EventID.PDS_03_End:

                                double PDS_03_LAT = ((double) BitConverter.ToInt32(logData, 0) * Math.Pow(10, -7));
                                double PDS_03_LON = ((double) BitConverter.ToInt32(logData, 4) * Math.Pow(10, -7));

                                Event_Information = "Threat Latitude: " + PDS_03_LAT.ToString() + " deg , " +
                                                    "Threat Longitude: " + PDS_03_LON.ToString() + " deg";

                                break;

                            case (int)PDS_EventID.PDS_04:
                            case (int)PDS_EventID.PDS_04_End:

                                uint PDS_04_HorizontalAcc = logData[0];
                                uint ProximityZoneBrake = logData[1];

                                Int16 ProximityZonePOI = BitConverter.ToInt16(logData, 2);

                                uint PDS_04_DisplayPosition = logData[4];

                                uint ProximityZoneCritical = logData[5];
                                uint ProximityZoneWarning = logData[6];
                                uint ProximityZonePresence = logData[7];

                                Event_Information = "Threat Accuracy: " + PDS_04_HorizontalAcc.ToString() + " m , " +
                                                    "Brake Distance: " + ProximityZoneBrake.ToString() + " m , " +
                                                    "Critical Distance: " + ProximityZoneCritical.ToString() + " m , " +
                                                    "Warning Distance: " + ProximityZoneWarning.ToString() + " m , " +
                                                    "Presence Distance: " + ProximityZonePresence.ToString() + " m , " +
                                                    "Threat Priority: " + PDS_04_DisplayPosition.ToString() + " , " +
                                                    "Threat POI Distance: " + ProximityZonePOI.ToString();

                                break;

                            case (int)PDS_EventID.PDS_05:
                            case (int)PDS_EventID.PDS_05_End:

                                double PDS_05_Speed = ((double) BitConverter.ToInt16(logData, 0)) / 10.0;
                                double PDS_05_Heading = BitConverter.ToUInt16(logData, 2) / 100.00;

                                uint PDS_05_HorizontalAcc = logData[4];
                                uint PDS_05_Length = logData[5];
                                uint PDS_05_Width = logData[6];
                                uint PDS_05_BrakeDistance = logData[7];


                                Event_Information = "Unit Speed: " + PDS_05_Speed.ToString("##,#00.0") + " km/h , " +
                                                    "Unit Heading: " + PDS_05_Heading.ToString("###,#0.00") + " deg , " +
                                                    "Unit Accuracy: " + PDS_05_HorizontalAcc.ToString() + " m , " +
                                                    "Threat Length: " + PDS_05_Length.ToString() + " m , " +
                                                    "Threat Width: " + PDS_05_Width.ToString() + " m , " +
                                                    "Threat Brake Distance: " + PDS_05_BrakeDistance.ToString() + " m";

                                break;

                            case (int)PDS_EventID.PDS_06:
                            case (int)PDS_EventID.PDS_06_End:

                                double PDS_06_LAT = ((double) BitConverter.ToInt32(logData, 0) * Math.Pow(10, -7));
                                double PDS_06_LON = ((double) BitConverter.ToInt32(logData, 4) * Math.Pow(10, -7));

                                Event_Information = "Unit Latitude: " + PDS_06_LAT.ToString() + " deg , " +
                                                    "Unit Longitude: " + PDS_06_LON.ToString() + " deg";

                                break;

                            case (int)PDS_EventID.PDS_07:
                            case (int)PDS_EventID.PDS_07_End:

                                double PDS_07_LAT = ((double)BitConverter.ToInt32(logData, 0) * Math.Pow(10, -7));
                                double PDS_07_LON = ((double)BitConverter.ToInt32(logData, 4) * Math.Pow(10, -7));

                                Event_Information = "POI Latitude: " + PDS_07_LAT.ToString() + " deg , " +
                                                    "POI Longitude: " + PDS_07_LON.ToString() + " deg";

                                break;

                            case (int)PDS_EventID.Confirm_Emergency_Stop:
                            case (int)PDS_EventID.Confirm_Controlled_Stop:
                            case (int)PDS_EventID.Confirm_SlowDown:
                            case (int)PDS_EventID.Confirm_Bypass:
                            case (int)PDS_EventID.Confirm_ApplySetPoint:
                            case (int)PDS_EventID.Confirm_StandDown:

                                uint ConfirmRegisterFormat = logData[0];
                                uint ConfirmStatus = logData[1];
                                UInt32 ConfirmValue = BitConverter.ToUInt32(logData, 2);

                                Event_Information = "Register Format: 0x" + ConfirmRegisterFormat.ToString("X") + " , " +
                                                    "Confirm Status: " + ConfirmStatus.ToString() + " , " +
                                                    "Confirm Value: " + ConfirmValue.ToString();

                                break;

                            case (int)PDS_EventID.Input_01_On:
                            case (int)PDS_EventID.Input_02_On:
                            case (int)PDS_EventID.Input_03_On:
                            case (int)PDS_EventID.Input_04_On:
                            case (int)PDS_EventID.Input_05_On:
                            case (int)PDS_EventID.Input_06_On:

                                double Input_LAT = ((double)BitConverter.ToInt32(logData, 0) * Math.Pow(10, -7));
                                double Input_LON = ((double)BitConverter.ToInt32(logData, 4) * Math.Pow(10, -7));

                                Event_Information = "Input Latitude: " + Input_LAT.ToString() + " deg , " +
                                                    "Input Longitude: " + Input_LON.ToString() + " deg";

                                break;

                            default:

                                Event_Information = "No Information";

                                break;
                        }

                        // ---- Add the parsed information to the list (binded to datagrid) ----
                        Temp_list.Add(new LogEntry
                        {
                            No = Log_Entry_Counter++,
                            DateTimeStamp = Event_DateTime,
                            EventID = EventID,
                            _EventName = Convert.ToInt32(EventID),
                            RawDataDisplay = BitConverter.ToString(logData),
                            RawDataEntry = logData,
                            _EventInformation = Event_Information
                         });

                        // ---- Keep record of the oldest and most recent date and time stamp for filter control ----
                        if (Oldest_Entry > Event_DateTime)
                        {
                            Oldest_Entry = Event_DateTime;
                        }
                        if (Newest_Entry < Event_DateTime)
                        {
                            Newest_Entry = Event_DateTime;
                        }
                    }
                    else if ((EventID >= 1000) && (EventID <= 1245))
                    {
                        Log_Parameter_Total++;
                    }
                    else
                    {
                        EventID_CountList.Find(x => x.Event_Name == "Event ID Failure").Event_Count++;
                    }
                }
                // ---- If the Date and Time stamp is faulty increment counter ----
                else if (DateTime_Status == (uint)DateTimeCheck.Status.Error_2)
                {
                    EventID_CountList.Find(x => x.Event_Name == "Invalid Timestamp Failure").Event_Count++;
                }
                // ---- If the Date and Time stamp is empty increment counter ----
                else if (DateTime_Status == (uint)DateTimeCheck.Status.Error_3)
                {
                    EventID_CountList.Find(x => x.Event_Name == "Default Log Entry").Event_Count++;
                }
                // ---- If the Date and Time stamp is default increment counter ----
                else if (DateTime_Status == (uint)DateTimeCheck.Status.Error_4)
                {
                    EventID_CountList.Find(x => x.Event_Name == "Empty Log Entry").Event_Count++;
                }
                // ---- If the Date and Time stamp is faulty increment counter ----
                else
                {
                    EventID_CountList.Find(x => x.Event_Name == "Invalid Timestamp Failure").Event_Count++;
                }

                if ((i % (Total_Log_Entries / 100)) == 0)
                {
                    Percentage_Complete++;
                    ReportProgress(Percentage_Complete);                   
                }
            }

            // ---- Add the event counts to the list (binded to datagrid) ----
            EventID_CountList.Find(x => x.Event_Name == "Datalog Entries").Event_Count = Log_Event_Total;
            EventID_CountList.Find(x => x.Event_Name == "Discarded Events").Event_Count =   EventID_CountList.Find(x => x.Event_Name == "Event ID Failure").Event_Count +
                                                                                            EventID_CountList.Find(x => x.Event_Name == "Invalid Timestamp Failure").Event_Count +
                                                                                            EventID_CountList.Find(x => x.Event_Name == "Default Log Entry").Event_Count +
                                                                                            EventID_CountList.Find(x => x.Event_Name == "Empty Log Entry").Event_Count;
        }

        public void Close_File()
        {
            EventID_CountList.Clear();
            Temp_list.Clear();
        }

        #endregion
    }
}
