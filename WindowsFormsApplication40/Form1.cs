//----------------------------------------------------------------------------
//  Copyright (C) 2004-2019 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;

using System.Threading;

namespace WindowsFormsApplication40
{
    public partial class Form1 : Form
    {
        private VideoCapture _capture = null;
        private Mat _frame;
        private double FrameRate;

        //private Image<Bgr, byte> frame = new Image<Bgr, byte>("你好.jpg");
        //参考链接 https://blog.csdn.net/cuoban/article/details/50555953
        public Form1()
        {
            InitializeComponent();
            //使用显卡处理图像数据效率会很多,如果你的设备支持,最好打开,使用CvInvoke.HaveOpenCLCompatibleGpuDevice能返回是否支持.
            // 配置CvInvoke.UseOpenCL能让OpenCV 启用或者停用 GPU运算
            //CvInvoke.UseOpenCL = CvInvoke.HaveOpenCLCompatibleGpuDevice;
            CvInvoke.UseOpenCL = false;
            try
            {
               // Application.Idle += Application_Idle;
                //Application.Idle += new EventHandler(Application_Idle);
                //构造一个摄像头实例,如果调用本地摄像机则括号里面为空
               // _capture = new VideoCapture(@"C:\Users\Administrator\Desktop\video\vtest.avi");              
               // double TotalFrames = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);
                //FrameRate = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);//帧率
               

            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
          
 

        }

        private void Application_Idle(object sender, EventArgs e)
        {
            try
            {

                _frame = _capture.QueryFrame();
                if (_frame != null)
                {
                    System.Threading.Thread.Sleep((int)(1000.0 / FrameRate - 5));
                    pictureBox1.Image = _frame.Bitmap;

                    double time_index = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosMsec);
                    label1.Text = "Time: " + TimeSpan.FromMilliseconds(time_index).ToString().Substring(0, 8);
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }

       
        //Dispose意为释放，释放组件所占用的内存。
        //C#特性，为提高运行效率，自动会释放已使用过且不再需要使用的组件来减少程序的CPU使用率。
        //默认会在程序运行一段时间后自动加载该Dispose方法，或者可以显式的自行调用此方法。
        private void ReleaseData()
        {
            if (_capture != null)

                _capture.Dispose();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if(op.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    
                     _capture = new VideoCapture(op.FileName);
                    double TotalFrames = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);
                    FrameRate = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);//帧率
                    Application.Idle += new EventHandler(Application_Idle);

                }                
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }


            }
        }
    }
}
