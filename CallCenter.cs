using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CallCenterApplication
{
    public class CallCenter
    {
        private List<Operator> _operators;
        private List<Caller> _callsQueue;
        // It's much harder to implement the PriorityQueue because we often change the Queue
        // But we can't iterate through the PriorityQueue, so it makes methods too complicated

        public CallCenter()
        {
            _operators = new List<Operator>();
            _callsQueue = new List<Caller>();
        }

        public List<Operator> Operators
        {
            get { return _operators; }            
        }    
        
        public List<Caller> CallsQueue
        {
            get { return _callsQueue; }
        }

        public void AddOperator(Operator _operator)
        {
            _operators.Add(_operator);
        }

        public void RemoveOperator(Operator _operator)
        {
            _operators.Remove(_operator);
        }

        public void ReceiveCall(Caller call)
        {
            _callsQueue.Add(call);
            if (CanSendCall(call))
            {
                SendCall(call);
            }

        }

        public void RemoveReceivedCall(Caller call)
        {
            _callsQueue.Remove(call);
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
    }
}
