using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_Appointment_User
    {
        // Atributtes
        string _stUuidAppointment;
        string _stUuidUser;

        // Builder
        public M_Appointment_User(string stUuidAppointment, string stUuidUser)
        {
            _stUuidAppointment = stUuidAppointment;
            _stUuidUser = stUuidUser;
        }

        // Properties
        public string UuidAppointment { set { _stUuidAppointment = value; } get { return _stUuidAppointment; } }
        public string UuidUser { set { _stUuidUser = value; } get { return _stUuidUser; } }
    }
}