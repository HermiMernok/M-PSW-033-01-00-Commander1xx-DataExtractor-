using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using ProximityDetectionSystemInfo;
using DataExtractorWindow;

namespace DataExtractorWindow
{ 
    public class LogEntry
    {
        private UInt32 _Number;
        public UInt32 No
        {
            get { return _Number; }
            set
            {
                if (_Number != value)
                {
                    _Number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        private DateTime _DateTimeStamp;
        public DateTime DateTimeStamp
        {
            get { return _DateTimeStamp; }
            set
            {
                if (_DateTimeStamp != value)
                {
                    _DateTimeStamp = value;
                    OnPropertyChanged("DateTimeStamp");
                }
            }
        }

        private UInt16 _EventID;
        public UInt16 EventID
        {
            get { return _EventID; }
            set
            {
                if (_EventID != value)
                {
                    _EventID = value;
                    OnPropertyChanged("Log_ID");
                }
            }
        }

        public Int32 _EventName;
        public string EventName
        {
            get
            {
                if (Enum.IsDefined(typeof(PDS_EventID), _EventName))
                    return EnumFunctions.GetEnumDisplayName(PDS_EventID.None, (UInt32)_EventName);
                else
                    return "";

            }
        }

        public string EventDescription
        {
            get
            {
                if (Enum.IsDefined(typeof(PDS_EventID), _EventName))
                    return EnumFunctions.GetEnumDescription(PDS_EventID.None, (UInt32)_EventName);
                else
                    return "";

            }
        }

        private string _RawData;

        public string RawDataDisplay
        {
            get { return _RawData; }
            set
            {
                if (RawDataDisplay != value)
                {
                    _RawData = value;
                    OnPropertyChanged("Log_Data_String");
                }
            }
        }

        private byte[] _RawDataEntry = new byte[8];

        public byte[] RawDataEntry
        {
            get { return _RawDataEntry; }
            set
            {
                Buffer.BlockCopy(value, 0, _RawDataEntry, 0, 8);
                //if (RawDataEntry != value)
                //{
                //    _RawDataEntry = value;
                //    OnPropertyChanged("Log_Data_Hex");
                //}
            }
        }

        public string _EventInformation;
        public string EventInformation
        {
            get { return _EventInformation; }
            set
            {
                if (EventInformation != value)
                {
                    _EventInformation = value;
                    OnPropertyChanged("EventInformation");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

    }
}
