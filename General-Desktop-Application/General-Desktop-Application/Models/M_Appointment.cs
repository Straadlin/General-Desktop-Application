using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_Appointment
    {
        // Atributtes
        string _stUuid;
        DateTime _oTime;
        bool _boCancel;
        string _stUuidDate;
        string _stUuidBreanchOffice;
        string _stUuidAppointmentRoot;
        string _stUuidSessionUsed;
        string _stUuidSessionCreated;
        string _stUuidSessionDeleted;

        // Builder
        public M_Appointment(string stUuid, DateTime oTime, bool boCancel, string stUuidDate, string stUuidBreanchOffice, string stUuidAppointmentRoot, string stUuidSessionUsed, string stUuidSessionCreated, string stUuidSessionDeleted)
        {
            _stUuid = stUuid;
            _oTime = oTime;
            _boCancel = boCancel;
            _stUuidDate = stUuidDate;
            _stUuidBreanchOffice = stUuidBreanchOffice;
            _stUuidAppointmentRoot = stUuidAppointmentRoot;
            _stUuidSessionUsed = stUuidSessionUsed;
            _stUuidSessionCreated = stUuidSessionCreated;
            _stUuidSessionDeleted = stUuidSessionDeleted;
        }

        // Properties
        public string Uuid { set { _stUuid = value; } get { return _stUuid; } }
        public DateTime Time { set { _oTime = value; } get { return _oTime; } }
        public bool Cancel { set { _boCancel = value; } get { return _boCancel; } }
        public string UuidDate { set { _stUuidDate = value; } get { return _stUuidDate; } }
        public string UuidBreanchOffice { set { _stUuidBreanchOffice = value; } get { return _stUuidBreanchOffice; } }
        public string UuidAppointmentRoot { set { _stUuidAppointmentRoot = value; } get { return _stUuidAppointmentRoot; } }
        public string UuidSessionUsed { set { _stUuidSessionUsed = value; } get { return _stUuidSessionUsed; } }
        public string UuidSessionCreated { set { _stUuidSessionCreated = value; } get { return _stUuidSessionCreated; } }
        public string UuidSessionDeleted { set { _stUuidSessionDeleted = value; } get { return _stUuidSessionDeleted; } }
    }
}