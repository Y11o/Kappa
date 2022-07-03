using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Kappa
{
    public partial class Form1 : Form
    {
        double alpfa = 250;
        double radFrom = 15;
        double radTo = 100;
        double rad; double radGain = 5;
        double initT = 0, lastT = 6.3;
        double step = 0.1, arg;
        double x, y;
        int speed = 100;
        int currPoint = 0;
        bool radFlag = true;
        Point[] points;
        Pen trajPen = new Pen(Color.Salmon, 1);
        Pen circlePen = new Pen(Color.Black, 1);
        SolidBrush filler = new SolidBrush(Color.FromArgb(0));
        bool type = true;
        int repeat = 1;
        int trajStep = 1;
        int startQuater = 1;

        public Form1()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex) {
                case 0:
                    speed = 400; break;
                case 1:
                    speed = 200; break;
                case 2:
                    speed = 135; break;
                case 3:
                    speed = 100; break;
                case 4:
                    speed = 80; break;
                case 5:
                    speed = 65; break;
                case 6:
                    speed = 50; break;            
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex) {
                case 0:
                    circlePen.Color = Color.Black; break;
                case 1:
                    circlePen.Color = Color.Gray; break;
                case 2:
                    circlePen.Color = Color.Red; break;
                case 3:
                    circlePen.Color = Color.Green; break;
                case 4:
                    circlePen.Color = Color.Blue; break;
                case 5:
                    circlePen.Color = Color.Orange; break;
                case 6:
                    circlePen.Color = Color.Yellow; break;
                case 7:
                    circlePen.Color = Color.Brown; break;
                case 8:
                    circlePen.Color = Color.Coral; break;
                case 9:
                    circlePen.Color = Color.Cyan; break;
                case 10:
                    circlePen.Color = Color.Magenta; break;
                case 11:
                    circlePen.Color = Color.PaleGoldenrod; break;
                case 12:
                    circlePen.Color = Color.Purple; break;
                case 13:
                    circlePen.Color = Color.Salmon; break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    trajPen.Color = Color.Black; break;
                case 1:
                    trajPen.Color = Color.Gray; break;
                case 2:
                    trajPen.Color = Color.Red; break;
                case 3:
                    trajPen.Color = Color.Green; break;
                case 4:
                    trajPen.Color = Color.Blue; break;
                case 5:
                    trajPen.Color = Color.Orange; break;
                case 6:
                    trajPen.Color = Color.Yellow; break;
                case 7:
                    trajPen.Color = Color.Brown; break;
                case 8:
                    trajPen.Color = Color.Coral; break;
                case 9:
                    trajPen.Color = Color.Cyan; break;
                case 10:
                    trajPen.Color = Color.Magenta; break;
                case 11:
                    trajPen.Color = Color.PaleGoldenrod; break;
                case 12:
                    trajPen.Color = Color.Purple; break;
                case 13:
                    trajPen.Color = Color.Salmon; break;
                case 14:
                    trajPen.Color = Color.FromArgb(255, Color.Transparent); break;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            radGain = Convert.ToInt32(textBox4.Text);
        }

        private void Paint_Graphics(double x, double y, Point[] points, int alpfa, int rad, int currPoint) {
            Graphics graphics = pictureBox1.CreateGraphics();
            int h = pictureBox1.Height / 2;
            int w = pictureBox1.Width / 2;
            graphics.TranslateTransform(w, h);
            graphics.ScaleTransform(1, -1);
            graphics.Clear(BackColor);
            graphics.DrawLine(Pens.Black, new Point(-w, 0), new Point(w, 0)); //X
            graphics.DrawLine(Pens.Black, new Point(0, -h), new Point(0, h)); //Y
            graphics.DrawLine(Pens.Blue, new Point(-alpfa, -h), new Point(-alpfa, h)); //ASIMP
            graphics.DrawLine(Pens.Blue, new Point(alpfa, -h), new Point(alpfa, h));    //ASIMP
            graphics.DrawLines(trajPen, points);  //TRAJECTORY
            if (trajStep != 1 && points.Length != currPoint)
            {
                if (currPoint % trajStep == 0)
                {
                    graphics.DrawEllipse(circlePen, Convert.ToInt32(x) - Convert.ToInt32(rad / 2), Convert.ToInt32(y) - Convert.ToInt32(rad / 2), rad * 2, rad * 2);
                    graphics.FillEllipse(filler, Convert.ToInt32(x) - Convert.ToInt32(rad / 2) + circlePen.Width, Convert.ToInt32(y) - Convert.ToInt32(rad / 2) + circlePen.Width, (rad - circlePen.Width) * 2, (rad - circlePen.Width) * 2);
                }
            }
            else {
                graphics.DrawEllipse(circlePen, Convert.ToInt32(x) - Convert.ToInt32(rad / 2), Convert.ToInt32(y) - Convert.ToInt32(rad / 2), rad * 2, rad * 2);
                graphics.FillEllipse(filler, Convert.ToInt32(x) - Convert.ToInt32(rad / 2) + circlePen.Width, Convert.ToInt32(y) - Convert.ToInt32(rad / 2) + circlePen.Width, (rad - circlePen.Width) * 2, (rad - circlePen.Width) * 2);
            }
        }

        private void Paint_Graphics(double x, double y, Point[] points, int alpfa) {
            Graphics graphics = pictureBox1.CreateGraphics();
            int h = pictureBox1.Height / 2;
            int w = pictureBox1.Width / 2;
            graphics.TranslateTransform(w, h);
            graphics.ScaleTransform(1, -1);
            graphics.Clear(BackColor);
            graphics.DrawLine(Pens.Black, new Point(-w, 0), new Point(w, 0)); //X
            graphics.DrawLine(Pens.Black, new Point(0, -h), new Point(0, h)); //Y
            graphics.DrawLine(Pens.Blue, new Point(-alpfa, -h), new Point(-alpfa, h)); //ASIMP
            graphics.DrawLine(Pens.Blue, new Point(alpfa, -h), new Point(alpfa, h));    //ASIMP
            graphics.DrawLines(trajPen, points);  //TRAJECTORY
        }

        private void Paint_Circle(int currPoint, int x, int y, int rad, int alpfa, Point[] points)
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            int h = pictureBox1.Height / 2;
            int w = pictureBox1.Width / 2;
            graphics.TranslateTransform(w, h);
            graphics.ScaleTransform(1, -1);
            graphics.Clear(BackColor);
            graphics.DrawLine(Pens.Black, new Point(-w, 0), new Point(w, 0)); //X
            graphics.DrawLine(Pens.Black, new Point(0, -h), new Point(0, h)); //Y
            graphics.DrawLine(Pens.Blue, new Point(-alpfa, -h), new Point(-alpfa, h)); //ASIMP
            graphics.DrawLine(Pens.Blue, new Point(alpfa, -h), new Point(alpfa, h)); //ASIMP
            graphics.DrawLines(trajPen, points);
            if (trajStep != 1 && points.Length != currPoint)
            {
                if (currPoint % trajStep == 0)
                {
                    graphics.DrawEllipse(circlePen, x - rad / 2, y - rad / 2, rad * 2, rad * 2);
                    graphics.FillEllipse(filler, (x - rad / 2 + circlePen.Width), (y - rad / 2 + circlePen.Width), (rad - circlePen.Width) * 2, (rad - circlePen.Width) * 2);
                }
            }
            else {
                graphics.DrawEllipse(circlePen, x - rad / 2, y - rad / 2, rad * 2, rad * 2);
                graphics.FillEllipse(filler, x - rad / 2 + circlePen.Width, y - rad / 2 + circlePen.Width, (rad - circlePen.Width) * 2, (rad - circlePen.Width) * 2);
            }
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            repeat = Convert.ToInt32(textBox5.Text);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            trajPen.Width = Convert.ToInt32(textBox6.Text);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            circlePen.Width = Convert.ToInt32(textBox7.Text);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            trajStep = Convert.ToInt32(textBox8.Text);
            
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox5.SelectedIndex) {
                case 0:
                    startQuater = 1; break;
                case 1:
                    startQuater = 2; break;
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox6.SelectedIndex) {
                case 0:
                    trajPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid; break;
                case 1:
                    trajPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; break;
                case 2:
                    trajPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot; break;
                case 3:
                    trajPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot; break;
                case 4:
                    trajPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot; break;
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox7.SelectedIndex) {
                case 0:
                    circlePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid; break;
                case 1:
                    circlePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash; break;
                case 2:
                    circlePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot; break;
                case 3:
                    circlePen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot; break;
                case 4:
                    circlePen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot; break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label19.Text = "Control";
            Form2 newForm = new Form2();
            newForm.Show();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox8.SelectedIndex) {
                case 0:
                    filler.Color = Color.Black; break;
                case 1:
                    filler.Color = Color.Gray; break;
                case 2:
                    filler.Color = Color.Red; break;
                case 3:
                    filler.Color = Color.Green; break;
                case 4:
                    filler.Color = Color.Blue; break;
                case 5:
                    filler.Color = Color.Orange; break;
                case 6:
                    filler.Color = Color.Yellow; break;
                case 7:
                    filler.Color = Color.Brown; break;
                case 8:
                    filler.Color = Color.Coral; break;
                case 9:
                    filler.Color = Color.Cyan; break;
                case 10:
                    filler.Color = Color.Magenta; break;
                case 11:
                    filler.Color = Color.PaleGoldenrod; break;
                case 12:
                    filler.Color = Color.Purple; break;
                case 13:
                    filler.Color = Color.Salmon; break;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    type = true; break;
                case 1:
                    type = false; break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label19.Text = "Set parameters \nand click Draw";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label19.Text = "Kappa";
            lastT *= repeat;
            alpfa = Convert.ToDouble(textBox1.Text);
            radFrom = Convert.ToDouble(textBox2.Text);
            radTo = Convert.ToDouble(textBox3.Text);
            rad = radFrom;

            if (startQuater == 1) {
                arg = initT;
                if (radFrom == radTo) radGain = 0;
                points = new Point[Convert.ToInt32(lastT / step + 1)];
                if (type)
                {
                    while (arg <= lastT)
                    {
                        if (rad >= radTo) radFlag = false;
                        if (rad <= radFrom) radFlag = true;
                        x = alpfa * Math.Sin(arg);
                        y = alpfa * Math.Sin(arg) * Math.Tan(arg);
                        points[currPoint] = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                        Paint_Graphics(x, y, points, Convert.ToInt32(alpfa), Convert.ToInt32(rad), currPoint);
                        arg += step;
                        Thread.Sleep(speed);
                        currPoint++;
                        if (radFlag) rad += radGain; else rad -= radGain;
                    }
                    currPoint = 0;
                }
                else
                {
                    while (arg <= lastT)
                    {
                        x = alpfa * Math.Sin(arg);
                        y = alpfa * Math.Sin(arg) * Math.Tan(arg);
                        points[currPoint] = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                        Paint_Graphics(x, y, points, Convert.ToInt32(alpfa));
                        arg += step;
                        Thread.Sleep(speed);
                        currPoint++;
                    }
                    currPoint = 0;
                    arg = initT;
                    while (arg <= lastT)
                    {
                        if (rad >= radTo) radFlag = false;
                        if (rad <= radFrom) radFlag = true;
                        x = alpfa * Math.Sin(arg);
                        y = alpfa * Math.Sin(arg) * Math.Tan(arg);
                        points[currPoint] = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                        Paint_Circle(currPoint, Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(rad), Convert.ToInt32(alpfa), points);
                        arg += step;
                        Thread.Sleep(speed);
                        currPoint++;
                        if (radFlag) rad += radGain; else rad -= radGain;
                    }
                    currPoint = 0;
                }
            }

            if (startQuater == 2)
            {
                arg = lastT;
                if (radFrom == radTo) radGain = 0;
                points = new Point[Convert.ToInt32(lastT / step + 1)];
                if (type)
                {
                    while (arg >= initT)
                    {
                        if (rad >= radTo) radFlag = false;
                        if (rad <= radFrom) radFlag = true;
                        x = alpfa * Math.Sin(arg);
                        y = alpfa * Math.Sin(arg) * Math.Tan(arg);
                        points[currPoint] = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                        Paint_Graphics(x, y, points, Convert.ToInt32(alpfa), Convert.ToInt32(rad), currPoint);
                        arg -= step;
                        Thread.Sleep(speed);
                        currPoint++;
                        if (radFlag) rad += radGain; else rad -= radGain;
                    }
                    currPoint = 0;
                }
                else
                {
                    while (arg >= initT)
                    {
                        x = alpfa * Math.Sin(arg);
                        y = alpfa * Math.Sin(arg) * Math.Tan(arg);
                        points[currPoint] = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                        Paint_Graphics(x, y, points, Convert.ToInt32(alpfa));
                        arg -= step;
                        Thread.Sleep(speed);
                        currPoint++;
                    }
                    currPoint = 0;
                    arg = lastT;
                    while (arg >= initT)
                    {
                        if (rad >= radTo) radFlag = false;
                        if (rad <= radFrom) radFlag = true;
                        x = alpfa * Math.Sin(arg);
                        y = alpfa * Math.Sin(arg) * Math.Tan(arg);
                        points[currPoint] = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                        Paint_Circle(currPoint, Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(rad), Convert.ToInt32(alpfa), points);
                        arg -= step;
                        Thread.Sleep(speed);
                        currPoint++;
                        if (radFlag) rad += radGain; else rad -= radGain;
                    }
                    currPoint = 0;
                }
            }
        }
    }
}
