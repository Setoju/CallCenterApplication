using CallCenterApplication.Operators;
using System;
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
        private Queue<Call> _callsQueue;
        private Timer _timer;

        public CallCenter(List<Operator> operators)
        {
            //_operators = new List<Operator>();
            _operators = operators;
            _callsQueue = new Queue<Call>();            
            _timer = new Timer(CheckQueue, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        public List<Operator> Operators
        {
            get { return _operators; }            
        }    
        
        public Queue<Call> CallsQueue
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

        public void ReceiveCall(Call call)
        {
            bool callSended = SendCall(call);
            if (!callSended)
            {
                _callsQueue.Enqueue(call);
            }
        }
        
        public bool SendCall(Call call)
        {
            foreach (Operator op in _operators)
            {
                // Add finding more skilled operator
                if (op.Languages.ContainsKey(call.CallLanguage) && op.Skills.ContainsKey(call.CallType))
                {
                    op.RespondToCall(call);

                    return true;
                }
            }

            return false;
        }

        private void CheckQueue(object state)
        {
            if (_callsQueue.Count > 0)
            {
                List<Call> callsToRemove = new List<Call>();

                foreach (Call call in _callsQueue)
                {
                    if (SendCall(call))
                    {
                        callsToRemove.Add(call);
                    }
                }

                foreach (Call callToRemove in callsToRemove)
                {
                    _callsQueue = new Queue<Call>(_callsQueue.Where(c => c != callToRemove));
                }
            }
        }
    }
}
