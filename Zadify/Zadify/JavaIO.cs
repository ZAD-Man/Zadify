using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Zadify
{
    public class JavaIO
    {
        public static bool SaveData<T>(Context context, string fileName, T data)
        {
            try
            {
                using (Stream stream = context.OpenFileOutput(fileName, FileCreationMode.Private))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof (T));

                    xmlSerializer.Serialize(stream, data);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static T LoadData<T>(Context context, string fileName)
        {
            Java.IO.File file = context.GetFileStreamPath(fileName);

            if (file.Exists())
            {
                using (Stream openStream = context.OpenFileInput(fileName))
                {
                    using (StreamReader reader = new StreamReader(openStream))
                    {
                        try
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof (T));

                            var loadedObject = serializer.Deserialize(reader);

                            return (T) loadedObject;
                        }
                        catch (Exception ex)
                        {
                            return default(T);
                        }
                    }
                }
            }
            else
            {
                throw new Java.IO.FileNotFoundException("Could not find file " + fileName);
            }
        }
    }
}