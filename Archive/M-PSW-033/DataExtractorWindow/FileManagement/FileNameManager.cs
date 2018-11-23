using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataExtractorWindow
{
    class FileNameManager
    {
        DateTimeCheck DateTime_Check = new DateTimeCheck();
        uint DateTime_Status = (uint)DateTimeCheck.Status.Unknown;

        public bool Get_FileName_Information(string File_Name, out string Binded_ID, out DateTime File_Date)
        {
            // ---- Define the sizes of the identifiers  ----
            int BindedID_Size = 7;
            int Separator_Size = 1;

            // ---- Search for the binded string idendifier  ----
            int Search_Index1 = File_Name.IndexOf("UnitID-");
            // ---- Search for the end of the binded identifier marked with '_' ----
            int Search_Index2 = File_Name.IndexOf("_", Search_Index1);

            // ---- Copy the binded idendifier out of the filename ----
            char[] Copy_Section = new char[Search_Index2 - (Search_Index1 + BindedID_Size)];
            File_Name.CopyTo((Search_Index1 + BindedID_Size), Copy_Section, 0, Search_Index2 - (Search_Index1 + BindedID_Size));

            // ---- Convert binded idendifier to string ----
            Binded_ID = new string(Copy_Section);

            // ---- Search for the date that should always follow the binded idendifier ----
            Search_Index1 = Search_Index2;
            // ---- Search for the end of the date marked with '_' ----
            Search_Index2 = File_Name.IndexOf("_", (Search_Index1 + Separator_Size));

            // ---- Copy the date out of the filename ----
            Copy_Section = new char[Search_Index2 - (Search_Index1 + Separator_Size)];
            File_Name.CopyTo((Search_Index1 + Separator_Size), Copy_Section, 0, Search_Index2 - (Search_Index1 + Separator_Size));

            // ---- Create a date byte array to check if it is a valid DateTime ----
            byte[] Date_Time_Stamp = new byte[6];

            // ---- Split the date string into bytes (0, 1, 2) ----
            string[] Date = (new string(Copy_Section)).Split('-');
            Date_Time_Stamp[0] = GeneralFunctions.ToByteArray(Date[2])[1];
            Date_Time_Stamp[1] = GeneralFunctions.ToByteArray(Date[1])[0];
            Date_Time_Stamp[2] = GeneralFunctions.ToByteArray(Date[0])[0];

            // ---- Search for the time that should always follow the date ----
            Search_Index1 = Search_Index2;
            // ---- Search for the end of the date marked with '.' (file type) ----
            Search_Index2 = File_Name.IndexOf(".", (Search_Index1 + Separator_Size));

            // ---- Copy the time out of the filename ----
            Copy_Section = new char[Search_Index2 - (Search_Index1 + Separator_Size)];
            File_Name.CopyTo((Search_Index1 + Separator_Size), Copy_Section, 0, Search_Index2 - (Search_Index1 + Separator_Size));

            // ---- Split the date string into bytes (0, 1, 2) ----
            string[] Time = (new string(Copy_Section)).Split('-');
            Date_Time_Stamp[3] = GeneralFunctions.ToByteArray(Time[0])[0];
            Date_Time_Stamp[4] = GeneralFunctions.ToByteArray(Time[1])[0];
            Date_Time_Stamp[5] = GeneralFunctions.ToByteArray(Time[2])[0];

            // ---- Check if the time stamp read from the file name is valid ----
            DateTime_Status = DateTime_Check.CheckDateTimeStamp(Date_Time_Stamp, out File_Date);

            // ---- Return true that the date of the file is valid ----
            if (DateTime_Status == (uint)DateTimeCheck.Status.Ok)
            {
                return true;
            }
            // ---- Return false that the date of the file is invalid ----
            else
            {
                return false;
            }
        }

        public uint Get_DateTime_Status()
        {
            return (uint)DateTime_Status;
        }
    }
}
