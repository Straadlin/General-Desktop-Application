using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_Classroom
    {
        // Atributtes
        string _stUuid;
        string _stValue;

        // Builder
        public M_Classroom(string stUuid, string stValue)
        {
            _stUuid = stUuid;
            _stValue = stValue;
        }

        // Properties
        public string Uuid { set { _stUuid = value; } get { return _stUuid; } }
        public string Value { set { _stValue = value; } get { return _stValue; } }
    }
}