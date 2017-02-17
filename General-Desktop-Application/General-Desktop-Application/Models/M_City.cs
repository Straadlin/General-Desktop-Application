using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Data;

namespace General_Desktop_Application.Models
{
    class M_City
    {
        // Builders
        public M_City(string stValue, string stUuidState, string stConnectionString, string stVoid)
        {
            while (true)
            {
                Uuid = Guid.NewGuid().ToString();

                if (D_City.Select("SELECT * FROM build_general.city WHERE(city_uuid = '" + Uuid + "');", stConnectionString).Count == 0)
                    break;
            }

            Value = stValue;
            UuidState = stUuidState;
        }
        public M_City(string stValue, string stUuidState)
        {
            while (true)
            {
                Uuid = Guid.NewGuid().ToString();

                if (D_City.Select("SELECT * FROM build_general.city WHERE(city_uuid = '" + Uuid + "');").Count == 0)
                    break;
            }

            Value = stValue;
            UuidState = stUuidState;
        }
        public M_City(string stUuid, string stValue, string stUuidState)
        {
            Uuid = stUuid;
            Value = stValue;
            UuidState = stUuidState;
        }

        // Properties
        public string Uuid { set; get; }
        public string Value { set; get; }
        public string UuidState { set; get; }
        public M_State State { get { return D_State.Select("SELECT * FROM build_general.state WHERE(stat_uuid = '" + UuidState + "');")[0]; } }
    }
}