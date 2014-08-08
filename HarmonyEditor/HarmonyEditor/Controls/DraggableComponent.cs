using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarmonyEditor
{
    public partial class DraggableComponent : Control
    {
        protected bool _isDragging = false;
        protected int _mX = 0;
        protected int _mY = 0;
        protected int _DDradius = 40;

        public bool AllowDrag { get; set; }
        public bool Selected { get; set; }

        public DraggableComponent()
        {
            InitializeComponent();
        }

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
    }
}
