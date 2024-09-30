using System.Diagnostics;

namespace Smart_Mirror_Version_2.Classes.Managers
{
    public static class ActionManager
    {
        public delegate void AppClick();
        public delegate void Event();
        public static void OpenApplication(string EXEName)
        {
            Process process = Process.Start(EXEName + ".exe");
        }
        public static void OpenWindowsApp(string AppsFolderLocation)
        {
            Process process = Process.Start("explorer.exe", "shell:AppsFolder\\" + AppsFolderLocation);
        }
        public static void OpenWebPage(string URL)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = URL,
                UseShellExecute = true
            });
        }
    }
}
