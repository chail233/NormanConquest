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

        // 播放
        public void Play(bool loop = false)
        {
            Stop();
            isLooping = loop;
            string path = filePath;
            string openCmd = $"open \"{path}\" type mpegvideo alias {alias}";
            int err = mciSendString(openCmd, null, 0, IntPtr.Zero);
            if (err != 0)
            {
                // 可选：打错误信息
                return;
            }

            string playCmd = loop
                ? $"play {alias} from 0 repeat"
                : $"play {alias} from 0";

            mciSendString(playCmd, null, 0, IntPtr.Zero);
            isPlaying = true;
        }
        // 停止播放
        public void Stop()
        {
            if (isPlaying)
            {
                if (!string.IsNullOrEmpty(alias))
                {
                    mciSendString($"stop {alias}", null, 0, IntPtr.Zero);
                    mciSendString($"close {alias}", null, 0, IntPtr.Zero);
                }
                isPlaying = false;
            }
        }

        // 释放资源
        public void Dispose()
        {
            Stop();
        }
    }
}
