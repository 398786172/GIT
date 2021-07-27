using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCV.OCVTest
{
    public partial class BateryViewController : UserControl
    {
        public BateryViewController()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        public Color Value1Color
        {
            set
            {
                if (this.IsHandleCreated)
                {

                    Invoke(new EventHandler((ojb, par) =>
                    {
                        labV.BackColor = value;
                    }));
                }
            }
        }
        public string Value1
        {
            set
            {
                if (this.IsHandleCreated)
                {

                    Invoke(new EventHandler((ojb, par) =>
                    {
                        labV.Text = value;
                    }));
                }
            }
        }
        public Color Value2Color
        {
            set
            {
                if (this.IsHandleCreated)
                {

                    Invoke(new EventHandler((ojb, par) =>
                    {
                        labIR.BackColor = value;
                    }));
                }
            }
        }
        public string Value2
        {
            set
            {
                if (this.IsHandleCreated)
                {

                    Invoke(new EventHandler((ojb, par) =>
                    {
                        labIR.Text = value;
                    }));
                }
            }
        }

        public Color AllColor
        {
            set
            {
                this.BackColor = value;
            }
        }
        public int Channle
        {
            set
            {
                if (this.IsHandleCreated)
                {
                    Invoke(new EventHandler((ojb, par) =>
                    {
                        labBatCode.Text = $"({value.ToString("000")})";
                    }));
                }
            }
        }


    }



}
