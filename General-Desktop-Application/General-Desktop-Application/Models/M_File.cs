using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_File
    {
        // Atributtes
        string _stUuid;
        string _stName;
        byte _byExtension;
        string _stDescription;
        byte[] _staValue;

        // Builder
        public M_File(string stUuid, string stName, byte byExtension, string stDescription, byte[] staValue)
        {
            _stUuid = stUuid;
            _stName = stName;
            _byExtension = byExtension;
            _stDescription = stDescription;
            _staValue = staValue;
        }

        // Properties
        public string Uuid { set { _stUuid = value; } get { return _stUuid; } }
        public string Name { set { _stName = value; } get { return _stName; } }
        public byte Extension { set { _byExtension = value; } get { return _byExtension; } }
        public string Description { set { _stDescription = value; } get { return _stDescription; } }
        public byte[] Value { set { _staValue = value; } get { return _staValue; } }
    }
}