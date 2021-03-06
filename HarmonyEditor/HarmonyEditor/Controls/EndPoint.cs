﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarmonyEditor
{
    public class EndPoint : DraggableComponent
    {

        public EndPoint(int width, int height)
            : base()
        {
            Selected = false;
            Width = width;
            Height = height;
            AllowDrag = true;
        }
        public EndPoint()
        {
            Selected = false;
            AllowDrag = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Color backColor;
            if (Selected)
                backColor = Color.Brown;
            else
                backColor = Color.Black;

            pe.Graphics.FillRegion(new SolidBrush(backColor), new Region(ClientRectangle));
        }
    }
}
