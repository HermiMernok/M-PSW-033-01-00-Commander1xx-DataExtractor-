using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataExtractorWindow
{
    class EnumFunctions
    {
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
