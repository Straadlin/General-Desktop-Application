//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace General_Desktop_Application.EF
{
    using System;
    
    public partial class proc_user_select_Result
    {
        public System.Guid user_uuid__uniqueidentifier { get; set; }
        public string user_username__varchar { get; set; }
        public string user_email__varchar { get; set; }
        public string user_cellphone__varchar { get; set; }
        public byte[] user_password__varbinary { get; set; }
        public string user_firstname__varchar { get; set; }
        public string user_lastname__varchar { get; set; }
        public byte user_roleaccess__tinyint { get; set; }
        public string user_extradata__varchar { get; set; }
        public Nullable<System.Guid> reso_uuid_picture__uniqueidentifier { get; set; }
        public Nullable<System.Guid> date_uuid_birthdate__uniqueidentifier { get; set; }
        public Nullable<System.Guid> city_uuid__uniqueidentifier { get; set; }
        public Nullable<System.Guid> sess_uuid_used__uniqueidentifier { get; set; }
        public System.Guid sess_uuid_created__uniqueidentifier { get; set; }
        public Nullable<System.Guid> user_uuid_root__uniqueidentifier { get; set; }
        public Nullable<System.Guid> sess_uuid_deleted__uniqueidentifier { get; set; }
    }
}
