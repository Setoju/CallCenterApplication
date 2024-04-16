using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterApplication
{
    public class Call
    {
        private string _callType;
        private int _callComplexity;
        private string _callLanguage;

        public Call(string callType, int callComplexity, string callLanguage)
        {
            _callType = callType;
            _callComplexity = callComplexity;
            _callLanguage = callLanguage;
        }

        public void MakeCall(CallCenter callCenter)
        {
            callCenter.ReceiveCall(this);
        }        

        public string CallType
        {
            get { return _callType; }
        }

        public string CallLanguage
        {
            get { return _callLanguage; }
        }

        public int CallComplexity
        {
            get { return _callComplexity; }
        }
    }
}
