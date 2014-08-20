using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PopuloApplication
{
    public static class Melody
    {
        private static Task _taskPlaying = null;
        private static CancellationTokenSource _tokenCancelPlay;

        public static bool IsPlaying
        {
            get
            {
                if (_taskPlaying == null)
                    return false;
                return _taskPlaying.Status == TaskStatus.Running;
            }
        }
        public static void StartPlaying()
        {
            _tokenCancelPlay = new CancellationTokenSource();
            _taskPlaying = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            Thread.Sleep(10);
                            _tokenCancelPlay.Token.ThrowIfCancellationRequested();
                        }
                    }
                    catch (Exception)
                    {
                    }
                }, _tokenCancelPlay.Token);
        }
        public static void StopPlaying()
        {
            _tokenCancelPlay.Cancel();
            _taskPlaying.Wait();
        }
    }
}
