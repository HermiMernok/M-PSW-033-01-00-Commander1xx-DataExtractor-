﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataExtractorWindow
{
    public class DateTimeCheck
    {
        public enum Status
        {
            Unknown = 0,
            Ok      = 1,
            Error_1 = 2,
            Error_2 = 3,
            Error_3 = 4,
            Error_4 = 5
        }

        public static uint CheckDateTimeStamp(byte[] timestamp, out DateTime DateTimeStamp)
        {
            DateTime _DateTimeStamp;
            DateTimeStamp = DateTime.Now;

            byte[] DefaultEntry = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            byte[] EmptyEntry = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            try
            {
                if (timestamp.Length != 6)
                    throw new ApplicationException($"Expected timestamp length 6 bytes, got {timestamp.Length} bytes.");

                if (!timestamp.Zip(DefaultEntry, (a, b) => (a == b)).Any(p => !p))
                    return (uint)Status.Error_3;
                else if (!timestamp.Zip(EmptyEntry, (a, b) => (a == b)).Any(p => !p))
                    return (uint)Status.Error_4;
                // FIXME: Gerotek - Time Update
                //string DateTimeString = "20" + timestamp[0].ToString("X") + "/" +   // Year
                //                               timestamp[1].ToString("X") + "/" +   // Month
                //                               timestamp[2].ToString("X") + " " +   // Day
                //                               timestamp[3].ToString("X") + ":" +   // Hour
                //                               timestamp[4].ToString("X") + ":" +   //min
                //                               timestamp[5].ToString("X");          //sec

                string DateTimeString = "2018/" +   // Year
                                        timestamp[1].ToString("X") + "/" +   // Month
                                        timestamp[2].ToString("X") + " " +   // Day
                                        timestamp[3].ToString("X") + ":" +   // Hour
                                        timestamp[4].ToString("X") + ":" +   //min
                                        timestamp[5].ToString("X") + "." +     //sec
                                        timestamp[0].ToString();         


                if (DateTime.TryParse(DateTimeString, out _DateTimeStamp))
                {
                    DateTimeStamp = _DateTimeStamp;

                    if (_DateTimeStamp < Convert.ToDateTime("16/01/01 00:00:00.000"))
                    {
                        return (uint)Status.Error_2;
                    }

                    return (uint)Status.Ok;

                }

                return (uint)Status.Error_1;

            }
            catch (Exception ex) { return (uint)Status.Error_1; }
        }
    }
}
