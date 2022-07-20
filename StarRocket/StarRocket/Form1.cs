using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StarRocket
{
    public partial class Form1 : Form
    {
        public int f = 0;
        public int pos = 0;
        public int f2 = 0;
        public int f3 = 0;
        Random x1 = new Random();
        Bitmap off;
        List<rock> R = new List<rock>();
        List<star> S = new List<star>();
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Paint += Form1_paint;
            this.Load += Form1_loade;
            this.KeyDown += Form1_KeyDown;
            Timer t = new Timer();
            t.Start();
            t.Tick += T_Tick;
            t.Interval = 400;
        }
        private void T_Tick(object sender, EventArgs e)
        {
            int x = x1.Next(0, ClientSize.Width);
            star pnn2 = new star();
            pnn2.x = x;
            pnn2.y = 0;
            pnn2.w = 30;
            pnn2.h = 30;
            pnn2.ims = new Bitmap("2.bmp");
            S.Add(pnn2);
            for(int i=0;i<S.Count;i++)
            {
                S[i].y= S[i].y+ 5;

                if(f==1)
                {
                    S[i].x = S[i].x + 2;
                    f = 0;
                }
                else
                {
                    S[i].x = S[i].x - 2;
                    f = 1;
                }
                f2 = 0;
                if (f3 == 1&&f2==1)
                {
                    S[pos].w = 0;
                    S[pos].h = 0;
                }

            }
            Dubblebuffer(CreateGraphics());
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (R[0].x < (ClientSize.Width-120) )
            {
                if (e.KeyCode == Keys.Right)
                {
                    R[0].x = R[0].x + 20;
                }
            }
            if (R[0].x > 20)
            {
                if (e.KeyCode == Keys.Left)
                {
                    R[0].x = R[0].x - 20;
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                f2 = 1;
                for(int i=0;i<S.Count;i++)
                {
                    if(R[0].x<=S[i].x&& R[0].x+20 >= S[i].x)
                    {
                        f3 = 1;
                        pos = i;
                    }
                }
            }
                Dubblebuffer(this.CreateGraphics());
        }
        private void Form1_loade(object sender, EventArgs e)
        {
            rock pnn = new rock();
            pnn.x = ClientSize.Width / 2;
            pnn.y = ClientSize.Height-100;
            pnn.w = 100;
            pnn.h = 100;
            pnn.ir = new Bitmap("1.bmp");
            R.Add(pnn);
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
        }
        private void Form1_paint(object sender, PaintEventArgs e)
        {
            Dubblebuffer(e.Graphics);
        }
        void Dubblebuffer(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            Drowscene(g2);
            g.DrawImage(off, 0, 0);
        }
        void Drowscene(Graphics g)
        {
            g.Clear(Color.Black);
            R[0].ir.MakeTransparent(); 
            g.DrawImage(R[0].ir, R[0].x, R[0].y, R[0].w, R[0].h);
            for(int i=0; i<S.Count;i++)
            {
                S[i].ims.MakeTransparent();
                g.DrawImage(S[i].ims, S[i].x, S[i].y, S[i].w, S[i].h);
            }
                Brush b = new SolidBrush(Color.White);
            if (f2 == 1)
            {
                g.FillRectangle(b, R[0].x + 45, 0, 10, (ClientSize.Height - 100));
            }
        }
    }
    class star
    {
        public int x;
        public int y;
        public int w;
        public int h;
        public Bitmap ims;

    }
    class rock
    {
        public int x;
        public int y;
        public int w;
        public int h;
        public Bitmap ir;
    }
}
