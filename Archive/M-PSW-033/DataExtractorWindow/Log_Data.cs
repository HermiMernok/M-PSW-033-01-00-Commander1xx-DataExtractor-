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

namespace DataExtractorWindow
{
   
    public class Log_Entry
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
                if (EventID != value)
                {
                    _EventID = value;
                    OnPropertyChanged("Log_ID");
                }
            }
        }

        public Int32 _EventDescription;
        public string EventDescription
        {
            get
            {
                if (Enum.IsDefined(typeof(PDS_EventID), _EventDescription))                     
                    return (((PDS_EventID)(_EventDescription)).ToString()).Replace('_', ' ').Remove(0, 3);
                else                
                    return "";
                                
            }
        }

        private string _RawData;
        public string RawData
        {
            get { return _RawData; }
            set
            {
                if (RawData != value)
                {
                    _RawData = value;
                    OnPropertyChanged("Log_Data_String");
                }
            }
        }

        public UInt32 _UID;
        private string UID_Check;
        public string UID
        {
            
            get {
                    if (_UID == 0)
                    {
                        UID_Check = "N/A";
                    }
                    else
                    {
                        UID_Check = _UID.ToString("X");
                    }

                    return UID_Check;
                }
        }

        public UInt16 _VID;
        private string VID_Check;
        public string VID
        {
            get {
                    if (_VID == 0)
                    {
                        VID_Check = "N/A";
                    }
                    else
                    {
                        VID_Check = _VID.ToString();
                    }

                    return VID_Check;
                }
        }

        //public byte _LicenceType;
        //public string LicenceType
        //{
        //    get { return _LicenceType.ToString(); }
        //}

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

        public static string GetEnumDescription(Enum value, UInt32 i)
        {
            
            try
            {
                FieldInfo fi = value.GetType().GetField(value.GetType().GetEnumName(i));
                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

                if (attributes != null &&
                    attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.GetType().GetEnumName(i);
            }
            catch
            {
                return "Undefined";
            }

        }

        public static string GetEnumDisplayName(Enum value, UInt32 i)
        {
            
            try
            {
                //FieldInfo DisplayNameInfo = value.GetType().GetField(value.GetType().GetEnumName(i));
                //DisplayNameAttribute[] attributes =
                //    (DisplayNameAttribute[])DisplayNameInfo.GetCustomAttributes(
                //    typeof(DisplayNameAttribute),
                //    false);

                //if (attributes != null &&
                //    attributes.Length > 0)
                //    return attributes[0].DisplayName;
                //else
                //    return "No Event Name Assigned";

                return value.GetType().GetMember(value.GetType().GetEnumName(i))
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           .Name;
            }
            catch
            {
                return "No Event Name Assigned";
            }
        }


        public static string GetEnumCategory(Enum value, UInt32 i)
        {

            try
            {
                FieldInfo CategoryInfo = value.GetType().GetField(value.GetType().GetEnumName(i));
                CategoryAttribute[] attributes =
                    (CategoryAttribute[])CategoryInfo.GetCustomAttributes(
                    typeof(CategoryAttribute),
                    false);

                if (attributes != null &&
                    attributes.Length > 0)
                    return attributes[0].Category;
                else
                    return "No Event Group Assigned";
            }
            catch
            {
                return "Undefined";
            }

        }
    }
}
