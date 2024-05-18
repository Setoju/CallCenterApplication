using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenterApplication
{
    public class CallCenter : INotifyPropertyChanged
    {
        private ObservableCollection<Operator> _operators;
        private ObservableCollection<Caller> _callsQueue;

        public CallCenter()
        {
            _operators = new ObservableCollection<Operator>();
            _callsQueue = new ObservableCollection<Caller>();
        }

        public ObservableCollection<Operator> Operators
        {
            get { return _operators; }
        }

        public ObservableCollection<Caller> CallsQueue
        {
            get { return _callsQueue; }
        }

        public void AddOperator(Operator @operator)
        {
            _operators.Add(@operator);
            OnPropertyChanged(nameof(Operators));
        }

        public void RemoveOperator(Operator @operator)
        {
            _operators.Remove(@operator);
            OnPropertyChanged(nameof(Operators));
        }

        public void ReceiveCall(Caller call)
        {
            _callsQueue.Add(call);
            if (CanSendCall(call))
            {
                SendCall(call);
            }
            OnPropertyChanged(nameof(CallsQueue));
        }

        public void RemoveReceivedCall(Caller call)
        {
            _callsQueue.Remove(call);
            OnPropertyChanged(nameof(CallsQueue));
        }

        private bool CanSendCall(Caller call)
        {
            foreach (Operator op in _operators)
            {
                if (!op.IsOnCall && call.Languages.Keys.Intersect(op.Languages.Keys).Any() && op.Skills.ContainsKey(call.CallType))
                {
                    return true;
                }
            }

            return false;
        }

        public async void SendCall(Caller call)
        {
            Operator bestOperator = null;
            int bestSkillLevel = int.MaxValue;
            TimeSpan longestIdleTime = TimeSpan.Zero;

            foreach (Operator op in _operators)
            {
                if (!op.IsOnCall && call.Languages.Keys.Intersect(op.Languages.Keys).Any() && op.Skills.ContainsKey(call.CallType))
                {
                    if (op.Skills[call.CallType] < bestSkillLevel || (op.Skills[call.CallType] == bestSkillLevel && op.IdleTime.Elapsed > longestIdleTime))
                    {
                        bestOperator = op;
                        bestSkillLevel = op.Skills[call.CallType];
                        longestIdleTime = op.IdleTime.Elapsed;
                    }
                }
            }

            if (bestOperator != null)
            {
                await bestOperator.RespondToCall(call, this);
            }
        }

        public void CheckQueue()
        {
            if (_callsQueue.Count > 0)
            {
                List<Caller> callsCopy = new List<Caller>(_callsQueue);

                foreach (Caller call in callsCopy)
                {
                    if (CanSendCall(call))
                    {
                        SendCall(call);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}