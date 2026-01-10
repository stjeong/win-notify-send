using System.Runtime.InteropServices;

namespace win_notify_send;

// nuget push win-notify-send.1.0.0.nupkg -Source https://www.nuget.org/api/v2/package
class Program
{
    [DllImport("wintoast.dll", CharSet = CharSet.Unicode)]
    public static extern int fnToastSimple(string appId, string appName, string xmlText);

    [MTAThread]
    static int Main(string[] args)
    {
        if (OperatingSystem.IsWindows() == false)
        {
            Console.WriteLine("This program only runs on Windows.");
            return 1;
        }

        if (args.Length == 0 || args.Length > 2)
        {
            Console.WriteLine("Usage: win-notify-send <message>");
            Console.WriteLine("       win-notify-send <title> <message>");
            return 1;
        }

        string title;
        string message;

        if (args.Length == 1)
        {
            title = "win-notify-send";
            message = args[0];
        }
        else
        {
            title = args[0];
            message = args[1];
        }

        string toastMessage = $"""
<toast>
    <visual>
        <binding template="ToastGeneric">
            <text>{title}</text>
            <text>{message}</text>
        </binding>
    </visual>
</toast>
""";

        string appName = "win-notify-send";

        // 단축 아이콘 생성 디렉터리: %USERPROFILE%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs
        String shortcutPath =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Windows\Start Menu\Programs\{appName}.lnk";

        if (!System.IO.File.Exists(shortcutPath))
        {
            // 최초 실행인 경우, AppId 등록 및 바로가기 아이콘 생성 후 5초 정도 대기
            fnToastSimple($"kr.pe.sysnet.www.{appName}", appName, toastMessage);
            Thread.Sleep(5000);
        }

        if (fnToastSimple($"kr.pe.sysnet.www.{appName}", appName, toastMessage) == 0)
        {
            Console.WriteLine("Failed to send toast notification.");
            return 2;
        }

        Console.WriteLine("Toast notification sent successfully.");
        return 0;
    }
}
