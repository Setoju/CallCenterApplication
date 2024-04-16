using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterApplication
{
    public class Caller : Person
    {
        private string _callType;
        private int _callComplexity;        

        public Caller(string name, Dictionary<string, int> languages, string callType, int callComplexity) : base(name, languages)
        {
            _callType = callType;
            _callComplexity = callComplexity;
            _languages = languages;
        }

        public void MakeCall(CallCenter callCenter)
        {
            callCenter.ReceiveCall(this);
        }        

        public string CallType
        {
            get { return _callType; }
        }

        public Dictionary<string, int> CallLanguages
        {
            get { return _languages; }
        }

        public int CallComplexity
        {
            get { return _callComplexity; }
        }
    }
}
