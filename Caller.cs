using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterApplication
{
    public class Caller : Person, INotifyPropertyChanged
    {
        private string _callType;
        private int _callComplexity;       
        private Stopwatch _waitingTime = new Stopwatch();

        public Caller(string name, Dictionary<string, int> languages, string callType, int callComplexity) : base(name, languages)
        {
            _callType = callType;
            _callComplexity = callComplexity;
            _languages = languages;

            _waitingTime.Start();
            StartWaitingTimer();
        }

        public void MakeCall(CallCenter callCenter)
        {
            callCenter.ReceiveCall(this);
        }
        private void StartWaitingTimer()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) =>
            {
                if (_waitingTime.Elapsed.TotalSeconds > 30)
                {
                    ViewModel._callCenter.RemoveReceivedCall(this);
                    timer.Stop();
                }
                OnPropertyChanged("WaitingTime");
            };
            timer.Start();
        }

        public string CallType
        {
            get { return _callType; }
        }
        public string WaitingTime
        {
            get { return _waitingTime.Elapsed.ToString(@"hh\:mm\:ss"); }
        }
        public Dictionary<string, int> CallLanguages
        {
            get { return _languages; }
        }

        public int CallComplexity
        {
            get { return _callComplexity; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
