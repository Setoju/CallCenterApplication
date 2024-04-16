using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenterApplication
{
    public abstract class Person
    {
        protected string _name;
        protected Dictionary<string, int> _languages;

        public Person(string name, Dictionary<string, int> languages)
        {
            _name = name;
            _languages = languages;
        }

        public string Name
        {
            get { return _name; }
        }

        public Dictionary<string, int> Languages{
            get { return _languages; }
        }
    }
}
