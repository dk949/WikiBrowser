#define MOD
using System;
using Terraria.ModLoader;

namespace WikiBrowser {
    public class Logging {
        // Portable way o logging, commend out the #define MOD to enable Console.WriteLine()
        public static void Log(string message, LogType type) {
#if MOD
            switch (type) {
                case LogType.Info:
                    ModContent.GetInstance<WikiBrowser>().Logger.Info(message);
                    break;
                case LogType.Warn:
                    ModContent.GetInstance<WikiBrowser>().Logger.Warn(message);
                    break;
                case LogType.Error:
                    ModContent.GetInstance<WikiBrowser>().Logger.Error(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
#else
            Console.WriteLine(message);
#endif
        }
    }

    public enum LogType {
        Info,
        Warn,
        Error
    }
}