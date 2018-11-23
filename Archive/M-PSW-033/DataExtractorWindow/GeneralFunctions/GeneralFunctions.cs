using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtractorWindow
{
    class GeneralFunctions
    {
        public static byte[] ToByteArray(String HexString)
        {
            int NumberChars = HexString.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            }
            return bytes;
        }
        
        public static int ConvertToInt32(byte hexCodedNumber)
        {
            string binaryString = BitConverter.ToString(new byte[] { hexCodedNumber });
            int number = (int)Convert.ToByte(binaryString, 10);
            return number;
        }
        
        public static byte[] GetStringToBytes(string value)
        {
            byte[] bytes = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                bytes = Encoding.UTF8.GetBytes(value.Substring(i * 2, 2));

            }
            return bytes;
        }

        public static bool RawData_Search(string RawData_Text, string Filter_Text)
        {
            int index = 0;
            foreach (char item in Filter_Text)
            {
                if (item == '$' || item == '-')
                {
                    RawData_Text = RawData_Text.Remove(index, 1);
                    RawData_Text = RawData_Text.Insert(index, item.ToString());
                }
                index++;
            }

            if (RawData_Text == Filter_Text)
            {
                return true;
            }

            return false;
        }

    }
}
