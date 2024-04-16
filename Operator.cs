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

        public void RespondToCall(Caller call, CallCenter callCenter)
        {
            callCenter.RemoveCallFromQueue(call);
            _idleTimer.Reset();

            int unavailableTime = call.CallComplexity / _performance;

            MessageBox.Show("Working...");

            Thread.Sleep(unavailableTime * 1000);

            MessageBox.Show("Done!");            

            Idle();
        }
    }
}
