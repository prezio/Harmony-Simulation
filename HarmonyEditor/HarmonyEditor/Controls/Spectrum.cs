using PeriodicChords;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarmonyEditor
{
    public partial class Spectrum : Control
    {
        private bool _isDragging = false;
        private int _mX = 0;
        private int _mY = 0;
        private int _DDradius = 40;

        public bool AllowDrag { get; set; }
        public Chord CurChord { get; set; }
        public bool Rotated { get; set; }
        public bool Selected { get; set; }
        public bool FreqNotes { get; set; } // true - frequencies
                                            // false - notes

        public Spectrum()
        {
            InitializeComponent();
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

        /*protected override void OnGotFocus(EventArgs e)
        {
            this.BackColor = _selColor;
            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            this.BackColor = Color.LightPink;
            base.OnLostFocus(e);
        }
        protected override void OnClick(EventArgs e)
        {
            this.Focus();
            base.OnClick(e);
        }*/
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _mX = e.X;
            _mY = e.Y;
            this._isDragging = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!_isDragging)
            {
                if (e.Button == MouseButtons.Left && _DDradius > 0 && this.AllowDrag)
                {
                    int num1 = _mX - e.X;
                    int num2 = _mY - e.Y;
                    if (((num1 * num1) + (num2 * num2)) > _DDradius)
                    {
                        DoDragDrop(this, DragDropEffects.All);
                        _isDragging = true;
                        return;
                    }
                }
                base.OnMouseMove(e);
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isDragging = false;
            base.OnMouseUp(e);
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
                foreach(double peak in peaks)
                {
                    double w = ClientRectangle.X+(peak - min)/range*ClientRectangle.Width;
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
