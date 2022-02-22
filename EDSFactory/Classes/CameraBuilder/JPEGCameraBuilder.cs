using AForge.Video;
using AForge.Video.FFMPEG;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDSFactory
{
    public class JPEGCameraBuilder : CameraBuilder
    {  
        public override void CreateCameraStream()
        {
            m_camera.m_stream = new JPEGStream();
        }

        public override void CreateCameraSource(string entryCamCameraURL)
        {
            m_camera.SetCameraSource(entryCamCameraURL);
        }

        public override void CreateCameraUserName(string entryCamUserName)
        {
            m_camera.SetCameraUserName(entryCamUserName);
        }

        public override void CreateCameraPassword(string entryCamPassword)
        {
            m_camera.SetCameraPassword(entryCamPassword);
        }

        public override void CreateBeforeAndAfterTime(int beforeAndAfterSecond)
        {
            m_camera.SetBeforeAndAfterTime(beforeAndAfterSecond); 
        }

        public override void CreateBuffer()
        {
            m_camera.SetBuffer(new List<KeyValuePair<DateTime, byte[]>>());
        }
    }
}
