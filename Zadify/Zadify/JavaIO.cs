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
using Android.Util;
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
                using (var stream = context.OpenFileOutput(fileName, FileCreationMode.Private))
                {
                    var xmlSerializer = new XmlSerializer(typeof (T));

                    xmlSerializer.Serialize(stream, data);
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error("SaveDataError", e.Message + e.StackTrace);
                return false;
            }
        }

        public static T LoadData<T>(Context context, string fileName)
        {
            var file = context.GetFileStreamPath(fileName);

            if (file.Exists())
            {
                using (var openStream = context.OpenFileInput(fileName))
                {
                    using (var reader = new StreamReader(openStream))
                    {
                        try
                        {
                            var serializer = new XmlSerializer(typeof (T));

                            var loadedObject = serializer.Deserialize(reader);

                            return (T) loadedObject;
                        }
                        catch (Exception e)
                        {
                            Log.Error("LoadDataError", e.Message + e.StackTrace);
                            return default(T);
                        }
                    }
                }
            }
            else
            {
                Log.Error("LoadDataFileNotFound", fileName);
                throw new Java.IO.FileNotFoundException("Could not find file " + fileName);
            }
        }


    }
}