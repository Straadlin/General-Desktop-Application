using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Models
{
    class User
    {
        public System.Guid user_uuid__uniqueidentifier { get; set; }
        public string user_username__nvarchar { get; set; }
        public string user_email__nvarchar { get; set; }
        public string user_cellphone__nvarchar { get; set; }
        public string user_password__nvarchar { get; set; }
        public string user_firstname__nvarchar { get; set; }
        public string user_lastname__nvarchar { get; set; }
        public byte user_rol__tinyint { get; set; }
        public string user_extradata__nvarchar { get; set; }
        public Nullable<System.Guid> reso_uuid_picture__uniqueidentifier { get; set; }
        public Nullable<System.Guid> date_uuid_birthdate__uniqueidentifier { get; set; }
        public Nullable<System.Guid> city_uuid__uniqueidentifier { get; set; }
        public Nullable<System.Guid> sess_uuid_used__uniqueidentifier { get; set; }
        public System.Guid sess_uuid_created__uniqueidentifier { get; set; }
        public Nullable<System.Guid> user_uuid_root__uniqueidentifier { get; set; }
        public Nullable<System.Guid> sess_uuid_deleted__uniqueidentifier { get; set; }
    }
}