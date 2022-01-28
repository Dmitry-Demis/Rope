using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Rope.Properties;

namespace Rope.BindingEnums
{
    public class EnumDescriptionTypeConverter : EnumConverter
    {
        public EnumDescriptionTypeConverter(Type type)
            : base(type)
        {
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    FieldInfo fi = value.GetType().GetField(value.ToString());
                    if (fi != null)
                    {
                        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        ResourceManager MyResourceClass =
                            new ResourceManager(typeof(Resources));

                        ResourceSet resourceSet =
                            MyResourceClass.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                        string result = null;
                        //foreach (DictionaryEntry entry in resourceSet)
                        //{
                        //    string resourceKey = entry.Key.ToString();
                        //    object resource = entry.Value;
                        //    if (attributes[0].Description == resourceKey)
                        //    {
                        //        result = resource.ToString();
                        //    }
                        //}

                       // return result;
                        return ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : value.ToString();
                    }
                }

                return string.Empty;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}