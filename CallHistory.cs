using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xaml;
using System.Text.Json.Serialization;
using System.IO;

namespace CallCenterApplication
{
    public class CallHistory
    {
        public string _operatorName { get; set; }
        public string _clientName { get; set; }
        public string _callType { get; set; }
        public List<string> _callLanguages { get; set; }
        public int _callTime { get; set; }

        public CallHistory(string operatorName, string clientName, string callType, List<string> callLanguages, int callTime)
        {
            _operatorName = operatorName;
            _clientName = clientName;
            _callType = callType;
            _callLanguages = callLanguages;
            _callTime = callTime;
        }

        public static void SerializeToFile(CallHistory callHistory)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(callHistory, options);
            File.AppendAllText(@"..\..\..\CallLog\log.json", jsonString);
        }
    }
}
