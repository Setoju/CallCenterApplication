using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenterApplication
{
    public class CallCenter
    {
        private List<Operator> _operators;
        private Queue<Caller> _callsQueue;
        private Timer _timer;

        public CallCenter(List<Operator> operators)
        {
            //_operators = new List<Operator>();
            _operators = operators;
            _callsQueue = new Queue<Caller>();            
            _timer = new Timer(CheckQueue, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        public List<Operator> Operators
        {
            get { return _operators; }            
        }    
        
        public Queue<Caller> CallsQueue
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
            bool callSended = SendCall(call);
            if (!callSended)
            {
                _callsQueue.Enqueue(call);
            }
        }

        public void GetCall(Caller call)
        {
            _callsQueue.Enqueue(call);
        }
        
        public bool SendCall(Caller call)
        {
            foreach (Operator op in _operators)
            {
                // Add finding more skilled operator
                if (call.Languages.Keys.Intersect(op.Languages.Keys).Any() && op.Skills.ContainsKey(call.CallType))
                {
                    op.RespondToCall(call, this);

                    return true;
                }
            }

            return false;
        }

        private void CheckQueue(object state)
        {
            if (_callsQueue.Count > 0)
            {
                List<Caller> callsToRemove = new List<Caller>();

                foreach (Caller call in _callsQueue)
                {
                    if (SendCall(call))
                    {
                        callsToRemove.Add(call);
                    }
                }

                foreach (Caller callToRemove in callsToRemove)
                {
                    RemoveCallFromQueue(callToRemove);
                }
            }
        }

        public void RemoveCallFromQueue(Caller call)
        {
            _callsQueue = new Queue<Caller>(_callsQueue.Where(c => c != call));
        }
    }
}
