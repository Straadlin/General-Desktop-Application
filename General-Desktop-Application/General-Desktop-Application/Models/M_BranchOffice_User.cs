using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_BranchOffice_User
    {
        // Atributtes
        string _stUuidBranchOffice;
        string _stUuidUser;

        // Builder
        public M_BranchOffice_User(string stUuidBranchOffice, string stUuidUser)
        {
            _stUuidBranchOffice = stUuidBranchOffice;
            _stUuidUser = stUuidUser;
        }

        // Properties
        public string UuidAppointment { set { _stUuidBranchOffice = value; } get { return _stUuidBranchOffice; } }
        public string UuidUser { set { _stUuidUser = value; } get { return _stUuidUser; } }
    }
}