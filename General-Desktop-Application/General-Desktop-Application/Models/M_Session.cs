using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class M_Session
    {
        // Builder
        public M_Session(string stUuid, DateTime objStartTime, DateTime? objLastActivity, string stIpBatch01, string stIpBatch02, string stIpBatch03, string stIpBatch04, string stUuidUser, string stUuidDate)
        {
            Uuid = stUuid;
            StartTime = objStartTime;
            LastActivity = objLastActivity;
            IpBatch01 = stIpBatch01;
            IpBatch02 = stIpBatch02;
            IpBatch03 = stIpBatch03;
            IpBatch04 = stIpBatch04;
            UuidUser = stUuidUser;
            UuidDate = stUuidDate;
        }

        // Properties
        public string Uuid { set; get; }
        public DateTime StartTime { set; get; }
        public DateTime? LastActivity { set; get; }
        public string IpBatch01 { set; get; }
        public string IpBatch02 { set; get; }
        public string IpBatch03 { set; get; }
        public string IpBatch04 { set; get; }
        public string UuidUser { set; get; }
        public string UuidDate { set; get; }
    }
}