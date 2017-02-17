using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_Date
    {
        // Atributtes
        string _stUuid;
        DateTime _oValue;

        // Builder
        public M_Date(string stUuid, DateTime oValue)
        {
            _stUuid = stUuid;
            _oValue = oValue;
        }

        // Properties
        public string Uuid { set { _stUuid = value; } get { return _stUuid; } }
        public DateTime Value { set { _oValue = value; } get { return _oValue; } }
    }
}