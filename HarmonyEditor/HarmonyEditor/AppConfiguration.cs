using System;
using System.Collections.Generic;
using System.Linq;

namespace HarmonyEditor
{
    public static class AppConfiguration
    {
        public static double GetFreqMin()
        {
            double result;
            double.TryParse(System.Configuration.ConfigurationManager.AppSettings["FrequencyMin"], out result);
            return result;
        }
        public static double GetFreqMax()
        {
            double result;
            double.TryParse(System.Configuration.ConfigurationManager.AppSettings["FrequencyMax"], out result);
            return result;
        }
        public static double GetNoteMin()
        {
            double result;
            double.TryParse(System.Configuration.ConfigurationManager.AppSettings["NoteMin"], out result);
            return result;
        }
        public static double GetNoteMax()
        {
            double result;
            double.TryParse(System.Configuration.ConfigurationManager.AppSettings["NoteMax"], out result);
            return result;
        }
    }
}
