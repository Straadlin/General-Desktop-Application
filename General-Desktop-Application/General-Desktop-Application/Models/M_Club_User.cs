using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_Club_User
    {
        // Atributtes
        string _stUuidClub;
        string _stUuidUser;

        // Builder
        public M_Club_User(string stUuidClub, string stUuidUser)
        {
            _stUuidClub = stUuidClub;
            _stUuidUser = stUuidUser;
        }

        // Properties
        public string UuidAppointment { set { _stUuidClub = value; } get { return _stUuidClub; } }
        public string UuidUser { set { _stUuidUser = value; } get { return _stUuidUser; } }
    }
}