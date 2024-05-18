using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.ComponentModel;

namespace CallCenterApplication
{
    public class Operator : Person, INotifyPropertyChanged
    {
        private Dictionary<string, int> _skillSet;
        private int _performance;
        private Stopwatch _idleTimer;
        private bool _onCall = false;
        private DispatcherTimer _timer;

        public Operator(string name, Dictionary<string, int> languages, Dictionary<string, int> skillSet, int performance)
            : base(name, languages)
        {
            _skillSet = skillSet;
            _performance = performance;
            _idleTimer = new Stopwatch();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            Idle();
        }

        public Dictionary<string, int> Skills
        {
            get { return _skillSet; }
        }

        public void Idle()
        {
            _idleTimer.Start();
        }

        public Stopwatch IdleTime
        {
            get { return _idleTimer; }
        }

        public int Performance
        {
            get { return _performance; }
        }

        public bool IsOnCall
        {
            get { return _onCall; }
        }

        public string IdleTimeElapsed
        {
            get { return _idleTimer.Elapsed.ToString(@"hh\:mm\:ss"); }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            OnPropertyChanged("IdleTimeElapsed");
        }        

        public async Task RespondToCall(Caller call, CallCenter callCenter)
        {
            lock (callCenter.CallsQueue)
            {
                callCenter.RemoveReceivedCall(call);
            }
            _onCall = true;
            OnPropertyChanged("IsOnCall");
            _idleTimer.Reset();

            int unavailableTime = call.CallComplexity / _performance;
            await Task.Delay(unavailableTime * 1000);

            _onCall = false;
            OnPropertyChanged("IsOnCall");

            callCenter.CheckQueue();
            Idle();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}