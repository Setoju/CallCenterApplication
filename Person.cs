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
        protected List<string> _languages;

        public Person(string name, List<string> languages)
        {
            _name = name;
            _languages = languages;
        }

        public string Name
        {
            get { return _name; }
        }

        public List<string> Languages {
            get { return _languages; }
        }        
    }
}
