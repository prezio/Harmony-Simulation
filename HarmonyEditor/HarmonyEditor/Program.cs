using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;

namespace HarmonyEditor
{
    static class Program
    {
        public static OutputDevice outDevice;
        //public static Sequence sequence;
        public static Sequencer sequencer;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       
        [STAThread]
        static void Main()
        {
            //Program.sequence = new Sanford.Multimedia.Midi.Sequence();
            Program.sequencer = new Sanford.Multimedia.Midi.Sequencer();
            sequencer.ChannelMessagePlayed += ChannelMessagePlayed;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            Application.Run(new MainWindow());
        }
        static  void ChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            

            outDevice.Send(e.Message);
            
        }
    }
}
