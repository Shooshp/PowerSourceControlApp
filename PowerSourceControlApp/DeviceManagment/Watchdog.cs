using System;
using System.Timers;

namespace PowerSourceControlApp.DeviceManagment
{
    public class Watchdog : IDisposable
    {
        private Timer _timer;

        public event EventHandler TimeToDie;

        public delegate void EventHandler();

        public Watchdog(int time)
        {
            _timer = new Timer(time);
            _timer.Elapsed += TimeIsUp;
            _timer.Enabled = false;
        }

        public void Start()
        {
            _timer.Enabled = true;
        }

        private void TimeIsUp(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            TimeToDie?.Invoke();
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    _timer.Elapsed -= TimeIsUp;
                }
            }
            finally
            {
                _timer.Dispose();
            }
        }
    }
}
