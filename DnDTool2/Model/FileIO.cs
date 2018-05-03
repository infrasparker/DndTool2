using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public class FileIO
    {
        public static void WriteJson(string name, object obj)
        {
            WriteJson("", name, obj);
        }

        public static void WriteJson(string path, string name, object obj)
        {
            string s = BaseDirectory();
            string[] directories = path.Split('\\');
            string prev = "";
            foreach (string d in directories)
            {
                prev += "\\" + d;
                Directory.CreateDirectory(@"" + s + prev);
            }
            using (StreamWriter file = File.CreateText(@"" + s + path + "\\" + name + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, obj);
            }
        }

        public static T LoadJson<T>(string path, string name)
        {
            using (StreamReader file = File.OpenText(@"" + BaseDirectory() + path + "\\" + name + ".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (T)serializer.Deserialize(file, typeof(T));
            }
        }

        public static T LoadJson<T>(string path)
        {
            using (StreamReader file = File.OpenText(@"" + path))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (T)serializer.Deserialize(file, typeof(T));
            }
        }

        public static void Delete(string path, string name)
        {
            File.Delete(@"" + BaseDirectory() + path + "\\" + name);
        }

        public static string BaseDirectory()
        {
            string s = AppDomain.CurrentDomain.BaseDirectory;
            return s.Substring(0, s.LastIndexOf("DnDTool2")) + "DnDTool2";
        }

        public static string[] GetFiles(string path)
        {
            return Directory.GetFiles(BaseDirectory() + path);
        }
    }
}
