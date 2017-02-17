using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_Class_User
    {
        // Atributtes
        string _stUuidClass;
        string _stUuidUser;

        // Builder
        public M_Class_User(string stUuidClass, string stUuidUser)
        {
            _stUuidClass = stUuidClass;
            _stUuidUser = stUuidUser;
        }

        // Properties
        public string UuidAppointment { set { _stUuidClass = value; } get { return _stUuidClass; } }
        public string UuidUser { set { _stUuidUser = value; } get { return _stUuidUser; } }
    }
}