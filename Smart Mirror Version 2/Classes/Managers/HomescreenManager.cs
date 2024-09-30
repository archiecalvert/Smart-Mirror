using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Smart_Mirror_Version_2.Classes.Managers
{
    public class HomescreenManager
    {
        public static List<AppIcon> LoadIcons()
        {

            List<AppIcon> icons = new List<AppIcon>();
            string AppsDirectory = Directory.GetCurrentDirectory() + "//Content//Apps";
            foreach (string folder in Directory.GetDirectories(AppsDirectory))
            {
                string folderName = Path.GetFileName(folder);
                if (folderName == "Icons") continue;

            }
            return null;
        }

        /*                              
        1. OPEN WEBPAGE
        2. OPEN APPLICATION
        3. OPEN WINDOWS APPLICATION (ONE WITHOUT AN EXE LOCATION)
        */
        public static void CreateApp(string AppName, string IconTextureDirectory, int FunctionValue, string Data)
        {

            //Checks to see if the entered icon directory exists or not
            
            if(IconTextureDirectory.Substring(IconTextureDirectory.Length - 3) != "png") { throw new Exception("An error occured.\n The specified icon is not a png."); }

            string AppsDirectory = Directory.GetCurrentDirectory() + "//Content//Apps";
            if (Directory.Exists(AppsDirectory + "//" + AppName))
            {
                foreach(string file in Directory.GetFiles(AppsDirectory + "//" + AppName))
                {
                    File.Delete(file);
                }
                Directory.Delete(AppsDirectory + "//" + AppName);
            }
            Directory.CreateDirectory(AppsDirectory + "//" + AppName);
            //while (!Directory.Exists(AppsDirectory + "//" + AppName)) { }
            File.Copy(IconTextureDirectory, AppsDirectory + "//" + AppName + "//icon.png");
            File.Create(AppsDirectory + "//" + AppName + "//appdata.json").Close();
            //while (!Directory.Exists(AppsDirectory + "//" + AppName + "//appdata.json")) { }
            
            string appData;
            dynamic jsonObj = null;
            switch (FunctionValue)
            {
                case 1:
                    {
                        jsonObj = new WebPageJSON();
                        {
                            jsonObj.AppType = "Web Page";
                            jsonObj.AppName = AppName;
                            jsonObj.URL = Data;
                        };
                        break;
                    }
                case 2:
                    {
                        jsonObj = new EXEAppJSON();
                        {
                            jsonObj.AppType = "Executable";
                            jsonObj.AppName = AppName;
                            jsonObj.ExecutableName = Data;
                        };
                        break;
                    }
                case 3:
                    {
                        
                        jsonObj = new ApplicationJSON();
                        {
                            jsonObj.AppType = "Windows Store Application";
                            jsonObj.AppName = AppName;
                            jsonObj.AUMID = Data;
                        }
                        break;
                    }
                default:
                    throw new Exception("Function value not accepted. The value " + FunctionValue + " has no functionality assigned to it.");
            }
            
            appData = JsonConvert.SerializeObject(jsonObj);
            using (StreamWriter sw = new StreamWriter(AppsDirectory + "//" + AppName + "//appdata.json"))
            {
                sw.WriteLine(appData);
                sw.Close();
            }
            
        }
        public class WebPageJSON
        {
            public string AppType;
            public string AppName;
            public string URL;
        }
        public class EXEAppJSON
        {
            public string AppType;
            public string AppName;
            public string ExecutableName;
        }
        public class ApplicationJSON
        {
            public string AppType;
            public string AppName;
            public string AUMID;
        }

    }
}
