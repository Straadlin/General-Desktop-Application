using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_Kardex
    {
        // Atributtes
        string _stUuid;
        byte _byLevel;
        byte _byChapter;
        byte _byType;
        string _stDescription;
        byte? _byMistake;
        string _stUuidUser;
        string _stUuidDate;
        string _stUuidKardexRoot;
        string _stUuidSessionUsed;
        string _stUuidSessionCreated;
        string _stUuidSessionDeleted;

        // Builder
        public M_Kardex(string stUuid, byte byLevel, byte byChapter, byte byType, string stDescription, byte? byMistake, string stUuidUser, string stUuidDate, string stUuidKardexRoot, string stUuidSessionUsed, string stUuidSessionCreated, string stUuidSessionDeleted)
        {
            _stUuid = stUuid;
            _byLevel = byLevel;
            _byChapter = byChapter;
            _byType = byType;
            _stDescription = stDescription;
            _byMistake = byMistake;
            _stUuidUser = stUuidUser;
            _stUuidDate = stUuidDate;
            _stUuidKardexRoot = stUuidKardexRoot;
            _stUuidSessionUsed = stUuidSessionUsed;
            _stUuidSessionCreated = stUuidSessionCreated;
            _stUuidSessionDeleted = stUuidSessionDeleted;
        }

        // Properties
        public string Uuid { set { _stUuid = value; } get { return _stUuid; } }
        public byte byLevel { set { _byLevel = value; } get { return _byLevel; } }
        public byte Chapter { set { _byChapter = value; } get { return _byChapter; } }
        public byte Type { set { _byType = value; } get { return _byType; } }
        public string Description { set { _stDescription = value; } get { return _stDescription; } }
        public byte? Mistake { set { _byMistake = value; } get { return _byMistake; } }
        public string UuidUser { set { _stUuidUser = value; } get { return _stUuidUser; } }
        public string UuidDate { set { _stUuidDate = value; } get { return _stUuidDate; } }
        public string UuidKardexRoot { set { _stUuidKardexRoot = value; } get { return _stUuidKardexRoot; } }
        public string UuidSessionUsed { set { _stUuidSessionUsed = value; } get { return _stUuidSessionUsed; } }
        public string UuidSessionCreated { set { _stUuidSessionCreated = value; } get { return _stUuidSessionCreated; } }
        public string UuidSessionDeleted { set { _stUuidSessionDeleted = value; } get { return _stUuidSessionDeleted; } }
    }
}