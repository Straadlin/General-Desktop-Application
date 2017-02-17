using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_Class
    {
        // Atributtes
        string _stUuid;
        string _stFolio;
        DateTime _oTime;
        string _stUuidUserTeacher;
        string _stUuidClassroom;
        string _stUuidDate;
        string _stUuidBranchOffice;
        string _stUuidClassroomRoot;
        string _stUuidSessionUsed;
        string _stUuidSessionCreated;
        string _stUuidSessionDeleted;

        // Builder
        public M_Class(string stUuid, string stFolio, DateTime oTime, string stUuidUserTeacher, string stUuidClassroom, string stUuidDate, string stUuidBranchOffice, string stUuidClassroomRoot, string stUuidSessionUsed, string stUuidSessionCreated, string stUuidSessionDeleted)
        {
            _stUuid = stUuid;
            _stFolio = stFolio;
            _oTime = oTime;
            _stUuidUserTeacher = stUuidUserTeacher;
            _stUuidClassroom = stUuidClassroom;
            _stUuidDate = stUuidDate;
            _stUuidBranchOffice = stUuidBranchOffice;
            _stUuidClassroomRoot = stUuidClassroomRoot;
            _stUuidSessionUsed = stUuidSessionUsed;
            _stUuidSessionCreated = stUuidSessionCreated;
            _stUuidSessionDeleted = stUuidSessionDeleted;
        }

        // Properties
        public string Uuid { set { _stUuid = value; } get { return _stUuid; } }
        public string Folio { set { _stFolio = value; } get { return _stFolio; } }
        public DateTime Time { set { _oTime = value; } get { return _oTime; } }
        public string UuidUserTeacher { set { _stUuidUserTeacher = value; } get { return _stUuidUserTeacher; } }
        public string UuidClassroom { set { _stUuidClassroom = value; } get { return _stUuidClassroom; } }
        public string UuidDate { set { _stUuidDate = value; } get { return _stUuidDate; } }
        public string UuidBranchOffice { set { _stUuidBranchOffice = value; } get { return _stUuidBranchOffice; } }
        public string UuidClassroomRoot { set { _stUuidClassroomRoot = value; } get { return _stUuidClassroomRoot; } }
        public string UuidSessionUsed { set { _stUuidSessionUsed = value; } get { return _stUuidSessionUsed; } }
        public string UuidSessionCreated { set { _stUuidSessionCreated = value; } get { return _stUuidSessionCreated; } }
        public string UuidSessionDeleted { set { _stUuidSessionDeleted = value; } get { return _stUuidSessionDeleted; } }
    }
}