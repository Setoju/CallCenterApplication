using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CallCenterApplication
{
    public class Operator : Person
    {
        private Dictionary<string, int> _skillSet;
        private int _performance;
        private Stopwatch _idleTimer;
        private bool _onCall = false;

        public Operator(string name, Dictionary<string, int> languages, Dictionary<string, int> skillSet, int performance)
        : base(name, languages)
        {
            _skillSet = skillSet;
            _performance = performance;
            _idleTimer = new Stopwatch();
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
        public bool IsOnCall
        {
            get { return _onCall; }
        }

        public async Task RespondToCall(Caller call, CallCenter callCenter)
        {
            lock (callCenter.CallsQueue)
            {
                callCenter.RemoveReceivedCall(call);
            }
            _onCall = true;
            _idleTimer.Reset();

            int unavailableTime = call.CallComplexity / _performance;
            MessageBox.Show("Working...");
            await Task.Delay(unavailableTime * 1000);
            MessageBox.Show("Done!");  
            
            _onCall = false;

            callCenter.CheckQueue();
            Idle();
        }
    }
}
