using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{
    public static class ReaderExtension
    {
        public static int ReadInteger(this StreamReader reader)
        {
            return Convert.ToInt32(reader.ReadLine());
        }
        public static float ReadFloat(this StreamReader reader)
        {
            return Convert.ToSingle(reader.ReadLine());
        }
        public static Direction ReadDirection(this StreamReader reader)
        {
            string direct = reader.ReadLine();
            switch (direct)
            {
                case "South":
                    return Direction.South;

                case "North":
                    return Direction.North;

                case "East":
                    return Direction.East;
                default:
                    return Direction.West;
            }
        }

        public static JObject ReadJson(this StreamReader reader)
        {
            string s = reader.ReadToEnd();
            return JObject.Parse(s);
        }
    }
}
