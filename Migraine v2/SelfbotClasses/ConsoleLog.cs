namespace Migraine_v2.SelfbotClasses {
    public class ConsoleLog {
        public static void Log(string s) => LogInfo = LogInfo + s + "\n";
        public static string LogInfo = "";
    }
}
