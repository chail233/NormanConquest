using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace NormanConquest
{
    public class AudioPlayer : IDisposable
    {
        private string filePath;
        private string alias;
        private bool isLooping;
        private bool isPlaying;
        // 导入外部函数 mciSendString 来控制多媒体设备
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string command, string returnString, int returnLength, IntPtr callback);

        public AudioPlayer(string filePath)
        {
            this.filePath = filePath;
            this.alias = Guid.NewGuid().ToString("N");
            this.isLooping = false;
            this.isPlaying = false;
        }

        // 播放（默认不循环）
        public void Play(bool loop = false)
        {
            Stop();
            isLooping = loop;
            string loopFlag = loop ? " REPEAT" : "";
            // 打开音频文件
            mciSendString($"open \"{filePath}\" alias {alias}", null, 0, IntPtr.Zero);
            // 播放
            mciSendString($"play {alias} from 0{loopFlag}", null, 0, IntPtr.Zero);
            isPlaying = true;
        }

        // 停止播放
        public void Stop()
        {
            if (isPlaying)
            {
                mciSendString($"stop {alias}", null, 0, IntPtr.Zero);
                mciSendString($"close {alias}", null, 0, IntPtr.Zero);
                isPlaying = false;
            }
        }

        // 设置音量 0-1000
        public void SetVolume(int volume)
        {
            volume = Math.Clamp(volume, 0, 1000);
            mciSendString($"setaudio {alias} volume to {volume}", null, 0, IntPtr.Zero);
        }

        // 释放资源
        public void Dispose()
        {
            Stop();
        }
    }
}
