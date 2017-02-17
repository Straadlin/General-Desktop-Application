using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Data;

namespace General_Desktop_Application.Models
{
    class M_User
    {
        // Builders
        public M_User(string stValue,
            string stEmail,
            string stPassword,
            string stFullName,
            bool boGender,
            string stLandLine,
            string stCellphoneNumber,
            string stComplement,
            byte byRole,
            bool boEnabled,
            byte? byMaximumHoursPeriod,
            byte? byMinimumHoursPeriod,
            string stUuidCity,
            string stUuidFileAvatar,
            string stUuidUserRoot,
            string stUuidSessionUsed,
            string stUuidSessionCreated,
            string stUuidSessionDeleted,
            string stConnectionString, string stVoid)
        {
            while (true)
            {
                Uuid = Guid.NewGuid().ToString();

                if (D_State.Select("SELECT * FROM build_general.user WHERE(stat_uuid = '" + Uuid + "');", stConnectionString).Count == 0)
                    break;
            }

            Value = stValue;
            Email = stEmail;
            Password = stPassword;
            FullName = stFullName;
            Gender = boGender;
            LandLine = stLandLine;
            CellphoneNumber = stCellphoneNumber;
            Complement = stComplement;
            Role = byRole;
            Enabled = boEnabled;
            MaximumHoursPeriod = byMaximumHoursPeriod;
            MinimumHoursPeriod = byMinimumHoursPeriod;
            UuidCity = stUuidCity;
            UuidFileAvatar = stUuidFileAvatar;
            UuidUserRoot = stUuidUserRoot;
            UuidSessionUsed = stUuidSessionUsed;
            UuidSessionCreated = stUuidSessionCreated;
            UuidSessionDeleted = stUuidSessionDeleted;
        }
        public M_User(string stValue,
            string stEmail,
            string stPassword,
            string stFullName,
            bool boGender,
            string stLandLine,
            string stCellphoneNumber,
            string stComplement,
            byte byRole,
            bool boEnabled,
            byte? byMaximumHoursPeriod,
            byte? byMinimumHoursPeriod,
            string stUuidCity,
            string stUuidFileAvatar,
            string stUuidUserRoot,
            string stUuidSessionUsed,
            string stUuidSessionCreated,
            string stUuidSessionDeleted)
        {
            while (true)
            {
                Uuid = Guid.NewGuid().ToString();

                if (D_State.Select("SELECT * FROM build_general.user WHERE(stat_uuid = '" + Uuid + "');").Count == 0)
                    break;
            }

            Value = stValue;
            Email = stEmail;
            Password = stPassword;
            FullName = stFullName;
            Gender = boGender;
            LandLine = stLandLine;
            CellphoneNumber = stCellphoneNumber;
            Complement = stComplement;
            Role = byRole;
            Enabled = boEnabled;
            MaximumHoursPeriod = byMaximumHoursPeriod;
            MinimumHoursPeriod = byMinimumHoursPeriod;
            UuidCity = stUuidCity;
            UuidFileAvatar = stUuidFileAvatar;
            UuidUserRoot = stUuidUserRoot;
            UuidSessionUsed = stUuidSessionUsed;
            UuidSessionCreated = stUuidSessionCreated;
            UuidSessionDeleted = stUuidSessionDeleted;
        }
        public M_User(string stUuid,
            string stValue,
            string stEmail,
            string stPassword,
            string stFullName,
            bool boGender,
            string stLandLine,
            string stCellphoneNumber,
            string stComplement,
            byte byRole,
            bool boEnabled,
            byte? byMaximumHoursPeriod,
            byte? byMinimumHoursPeriod,
            string stUuidCity,
            string stUuidFileAvatar,
            string stUuidUserRoot,
            string stUuidSessionUsed,
            string stUuidSessionCreated,
            string stUuidSessionDeleted)
        {
            Uuid = stUuid;
            Value = stValue;
            Email = stEmail;
            Password = stPassword;
            FullName = stFullName;
            Gender = boGender;
            LandLine = stLandLine;
            CellphoneNumber = stCellphoneNumber;
            Complement = stComplement;
            Role = byRole;
            Enabled = boEnabled;
            MaximumHoursPeriod = byMaximumHoursPeriod;
            MinimumHoursPeriod = byMinimumHoursPeriod;
            UuidCity = stUuidCity;
            UuidFileAvatar = stUuidFileAvatar;
            UuidUserRoot = stUuidUserRoot;
            UuidSessionUsed = stUuidSessionUsed;
            UuidSessionCreated = stUuidSessionCreated;
            UuidSessionDeleted = stUuidSessionDeleted;
        }

        // Properties
        public string Uuid { set; get; }
        public string Value { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string FullName { set; get; }
        public bool Gender { set; get; }
        public string LandLine { set; get; }
        public string CellphoneNumber { set; get; }
        public string Complement { set; get; }
        public byte Role { set; get; }
        public bool Enabled { set; get; }
        public byte? MaximumHoursPeriod { set { if (value != 0) MaximumHoursPeriod = value; } get { return MaximumHoursPeriod; } }//public byte? MaximumHoursPeriod { set; get; }
        public byte? MinimumHoursPeriod { set { if (value != 0) MinimumHoursPeriod = value; } get { return MinimumHoursPeriod; } }//public byte? MinimumHoursPeriod { set; get; }
        public string UuidCity { set; get; }
        public string UuidFileAvatar { set; get; }
        public string UuidUserRoot { set; get; }
        public string UuidSessionUsed { set; get; }
        public string UuidSessionCreated { set; get; }
        public string UuidSessionDeleted { set; get; }
    }
}