using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Data;

namespace General_Desktop_Application.Models
{
    class M_State
    {
        // Builders
        public M_State(string stValue, string stConnectionString, string stVoid)
        {
            while (true)
            {
                Uuid = Guid.NewGuid().ToString();

                if (D_State.Select("SELECT * FROM build_general.state WHERE(stat_uuid = '" + Uuid + "');", stConnectionString).Count == 0)
                    break;
            }

            Value = stValue;
        }
        public M_State(string stValue)
        {
            while (true)
            {
                Uuid = Guid.NewGuid().ToString();

                if (D_State.Select("SELECT * FROM build_general.state WHERE(stat_uuid = '" + Uuid + "');").Count == 0)
                    break;
            }

            Value = stValue;
        }
        public M_State(string stUuid, string stValue)
        {
            Uuid = stUuid;
            Value = stValue;
        }

        // Properties
        public string Uuid { set; get; }
        public string Value { set; get; }
        public List<M_City> Cities { get { return D_City.Select("SELECT * FROM build_general.city WHERE(stat_uuid = '" + Uuid + "') ORDER BY stat_value DESC;"); } }
    }
}