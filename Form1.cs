using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace scaler
{
    public partial class Form1 : Form
    {
        private Boolean flag = false ;
        private Graphics g;
        private int x, y;
        public int cc = 0;
        private double len,lenu,lenl;
        private Color c;
        private int xx ,yy ;
        private Capture capture;        //takes images from camera as image frames
        private bool captureInProgress;
        public Form1()
        {
            InitializeComponent();
        }

        private void ReleaseData()
        {
            if (capture != null)
                capture.Dispose();
        }
        
        private void ProcessFrame(object sender, EventArgs arg)
        {
            //Image<Bgr, Byte> ImageFrame = capture.QueryFrame();
            
            g.DrawRectangle(new Pen(Color.GreenYellow), 100,60, 170, 170);
            imageBox1.Image = capture.QueryFrame();
           
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                
                try
                {
                    capture = new Capture();
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }


            if (capture != null)
            {
                if (captureInProgress)
                {  //if camera is getting frames then stop the capture and set button Text
                    // "Start" for resuming capture
                    button1.Text = "Start!"; //
                   
                    Application.Idle -= ProcessFrame;
                }
                else
                {
                    //if camera is NOT getting frames then start the capture and set button
                    // Text to "Stop" for pausing capture
                    button1.Text = "Capture";
                    Application.Idle += ProcessFrame;
                    g = imageBox1.CreateGraphics();
                    g.DrawRectangle(new Pen(Color.GreenYellow), 100, 60, 170, 170);
                }

                captureInProgress = !captureInProgress;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void imageBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (cc == 0)
            {
                x = e.X;
                y = e.Y;
                lenu =  Math.Sqrt(((184 - x) * (184 - x)) + ((61 - y) * (61 - y)));
                
                g.DrawLine(new Pen(Color.Gold), xx, yy, x, y);
                g.DrawEllipse(new Pen(Color.Blue), x, y, 5, -5);
                cc = 1;
            }
            if(cc == 1 )
            {
                
                xx = e.X;
                yy = e.Y;
                lenl = Math.Sqrt(((184 - xx) * (184 - xx)) + ((231 - yy) * (231 - yy)));
                
                g.DrawLine(new Pen(Color.Gold), xx, yy, x, y);
                g.DrawEllipse(new Pen(Color.Red), xx, yy, 5, -5);
                label1.Visible = true;
                len = Math.Sqrt(((xx - x) * (xx - x)) + ((yy - y) * (yy - y)));
                label1.Text  = Convert.ToString(len);
                button2.Visible = true;
               
            }
               
            
        }

        private void imageBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                flag = true;
            }
            xx = e.X;
            yy = e.Y;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            len = len / 40;
            //MessageBox.Show("img len " + Convert.ToString(len));
            //double lu = ((lenu* 15) / (lenu - 15)*5);
            //double ll = ((lenl * 15) / (lenl - 15)*5) ;
            lenu = lenu / 5; 
            lenl = lenl / 5;
            //MessageBox.Show("ln u : " + lenu);
            //MessageBox.Show("ln l : " + lenl);
            double m ,l;
            if (lenu > lenl)
            {
                l = lenu * 5;
                m = lenu;
            }
            else
            {
                l = lenl * 5;
                m = lenl;
            }
            //m = 1 / (0.2 - (1 / l));
            //MessageBox.Show(Convert.ToString("image dis " + m));
            //MessageBox.Show("distance of object "+Convert.ToString(l));
            double mag = m / l ;
            //MessageBox.Show(Convert.ToString("mag : " + mag));
            double h = len / mag ;
            //MessageBox.Show(" height  " + Convert.ToString(h));
            label2.Visible = true; label3.Visible = true; label4.Visible = true; label5.Visible = true;
            len = Math.Round(len, 2); l = Math.Round(l, 2); h = Math.Round(h, 2);
            label4.Text = "Height of Image: " + Convert.ToString(len) + " cm";
            label2.Text = "Distance of object: " + Convert.ToString(l) + " cm";
            label3.Text = "Height of object: " + Convert.ToString(h) + " cm";
            label5.Text = "Error +-2cm ";
            //double
           // MessageBox.Show(Convert.ToString(l));
        }
    }
}
