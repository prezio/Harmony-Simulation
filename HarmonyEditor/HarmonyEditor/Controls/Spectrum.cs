using PeriodicChords;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarmonyEditor
{
    public class Spectrum : DraggableComponent
    {
        public Chord CurChord { get; set; }
        public bool Rotated { get; set; }
        public bool FreqNotes { get; set; } // true - frequencies
        // false - notes

        public Spectrum()
            : base()
        {
            Rotated = false;
            AllowDrag = false;
            Selected = false;
        }

        public Spectrum(bool rotated, bool freqnotes, Chord chord, int width, int height)
        {
            Rotated = rotated;
            CurChord = chord;
            Width = width;
            Height = height;
            FreqNotes = freqnotes;
            AllowDrag = true;
            Selected = false;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (CurChord == null)
            {
                return;
            }

            Color backColor, lineColor;
            if (Selected)
            {
                backColor = Color.SandyBrown;
                lineColor = Color.Black;
            }
            else
            {
                backColor = Color.LightPink;
                lineColor = Color.Black;
            }

            pe.Graphics.FillRegion(new SolidBrush(backColor), new Region(ClientRectangle));
            double max = FreqNotes ? AppConfiguration.GetFreqMax() : AppConfiguration.GetNoteMax();
            double min = FreqNotes ? AppConfiguration.GetFreqMin() : AppConfiguration.GetNoteMin();
            double[] peaks = FreqNotes ? CurChord.Frequencies : CurChord.Notes;

            if (peaks != null && Rotated == false)
            {
                double range = max - min;
                foreach (double peak in peaks)
                {
                    double w = ClientRectangle.X + (peak - min) / range * ClientRectangle.Width;
                    pe.Graphics.DrawLine(new Pen(lineColor), (float)w, (float)ClientRectangle.Y, (float)w, (float)ClientRectangle.Bottom);
                }
            }
            else if (peaks != null && Rotated == true)
            {
                double range = max - min;
                foreach (double peak in peaks)
                {
                    double h = ClientRectangle.X + (peak - min) / range * ClientRectangle.Height;
                    pe.Graphics.DrawLine(new Pen(lineColor), (float)0, (float)h, (float)ClientRectangle.Width, (float)h);
                }
            }
        }
    }
}
