﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class straad_generaldesktopapplication_pcpcpcpc_001Entities : DbContext
    {
        public straad_generaldesktopapplication_pcpcpcpc_001Entities()
            : base("name=straad_generaldesktopapplication_pcpcpcpc_001Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<city> cities { get; set; }
        public virtual DbSet<country> countries { get; set; }
        public virtual DbSet<preference> preferences { get; set; }
        public virtual DbSet<state> states { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<date> dates { get; set; }
        public virtual DbSet<resource> resources { get; set; }
        public virtual DbSet<session> sessions { get; set; }
        public virtual DbSet<principalcompany> principalcompanies { get; set; }
        public virtual DbSet<version> versions { get; set; }
    
        public virtual ObjectResult<string> proc_systemserver_getdatetime(ObjectParameter dateTime)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_systemserver_getdatetime", dateTime);
        }
    
        public virtual ObjectResult<Nullable<System.Guid>> proc_systemserver_verifyloginuser(string userNameEmailOrCellphone, string password)
        {
            var userNameEmailOrCellphoneParameter = userNameEmailOrCellphone != null ?
                new ObjectParameter("userNameEmailOrCellphone", userNameEmailOrCellphone) :
                new ObjectParameter("userNameEmailOrCellphone", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<System.Guid>>("proc_systemserver_verifyloginuser", userNameEmailOrCellphoneParameter, passwordParameter);
        }
    
        public virtual int proc_user_insert(Nullable<System.Guid> user_uuid__uniqueidentifier, string user_username__varchar, string user_email__varchar, string user_cellphone__varchar, string user_password__varchar, string user_firstname__varchar, string user_lastname__varchar, Nullable<byte> user_roleaccess__tinyint, string user_extradata__varchar, Nullable<System.Guid> reso_uuid_picture__uniqueidentifier, Nullable<System.Guid> date_uuid_birthdate__uniqueidentifier, Nullable<System.Guid> city_uuid__uniqueidentifier, Nullable<System.Guid> sess_uuid_used__uniqueidentifier, Nullable<System.Guid> sess_uuid_created__uniqueidentifier, Nullable<System.Guid> user_uuid_root__uniqueidentifier, Nullable<System.Guid> sess_uuid_deleted__uniqueidentifier)
        {
            var user_uuid__uniqueidentifierParameter = user_uuid__uniqueidentifier.HasValue ?
                new ObjectParameter("user_uuid__uniqueidentifier", user_uuid__uniqueidentifier) :
                new ObjectParameter("user_uuid__uniqueidentifier", typeof(System.Guid));
    
            var user_username__varcharParameter = user_username__varchar != null ?
                new ObjectParameter("user_username__varchar", user_username__varchar) :
                new ObjectParameter("user_username__varchar", typeof(string));
    
            var user_email__varcharParameter = user_email__varchar != null ?
                new ObjectParameter("user_email__varchar", user_email__varchar) :
                new ObjectParameter("user_email__varchar", typeof(string));
    
            var user_cellphone__varcharParameter = user_cellphone__varchar != null ?
                new ObjectParameter("user_cellphone__varchar", user_cellphone__varchar) :
                new ObjectParameter("user_cellphone__varchar", typeof(string));
    
            var user_password__varcharParameter = user_password__varchar != null ?
                new ObjectParameter("user_password__varchar", user_password__varchar) :
                new ObjectParameter("user_password__varchar", typeof(string));
    
            var user_firstname__varcharParameter = user_firstname__varchar != null ?
                new ObjectParameter("user_firstname__varchar", user_firstname__varchar) :
                new ObjectParameter("user_firstname__varchar", typeof(string));
    
            var user_lastname__varcharParameter = user_lastname__varchar != null ?
                new ObjectParameter("user_lastname__varchar", user_lastname__varchar) :
                new ObjectParameter("user_lastname__varchar", typeof(string));
    
            var user_roleaccess__tinyintParameter = user_roleaccess__tinyint.HasValue ?
                new ObjectParameter("user_roleaccess__tinyint", user_roleaccess__tinyint) :
                new ObjectParameter("user_roleaccess__tinyint", typeof(byte));
    
            var user_extradata__varcharParameter = user_extradata__varchar != null ?
                new ObjectParameter("user_extradata__varchar", user_extradata__varchar) :
                new ObjectParameter("user_extradata__varchar", typeof(string));
    
            var reso_uuid_picture__uniqueidentifierParameter = reso_uuid_picture__uniqueidentifier.HasValue ?
                new ObjectParameter("reso_uuid_picture__uniqueidentifier", reso_uuid_picture__uniqueidentifier) :
                new ObjectParameter("reso_uuid_picture__uniqueidentifier", typeof(System.Guid));
    
            var date_uuid_birthdate__uniqueidentifierParameter = date_uuid_birthdate__uniqueidentifier.HasValue ?
                new ObjectParameter("date_uuid_birthdate__uniqueidentifier", date_uuid_birthdate__uniqueidentifier) :
                new ObjectParameter("date_uuid_birthdate__uniqueidentifier", typeof(System.Guid));
    
            var city_uuid__uniqueidentifierParameter = city_uuid__uniqueidentifier.HasValue ?
                new ObjectParameter("city_uuid__uniqueidentifier", city_uuid__uniqueidentifier) :
                new ObjectParameter("city_uuid__uniqueidentifier", typeof(System.Guid));
    
            var sess_uuid_used__uniqueidentifierParameter = sess_uuid_used__uniqueidentifier.HasValue ?
                new ObjectParameter("sess_uuid_used__uniqueidentifier", sess_uuid_used__uniqueidentifier) :
                new ObjectParameter("sess_uuid_used__uniqueidentifier", typeof(System.Guid));
    
            var sess_uuid_created__uniqueidentifierParameter = sess_uuid_created__uniqueidentifier.HasValue ?
                new ObjectParameter("sess_uuid_created__uniqueidentifier", sess_uuid_created__uniqueidentifier) :
                new ObjectParameter("sess_uuid_created__uniqueidentifier", typeof(System.Guid));
    
            var user_uuid_root__uniqueidentifierParameter = user_uuid_root__uniqueidentifier.HasValue ?
                new ObjectParameter("user_uuid_root__uniqueidentifier", user_uuid_root__uniqueidentifier) :
                new ObjectParameter("user_uuid_root__uniqueidentifier", typeof(System.Guid));
    
            var sess_uuid_deleted__uniqueidentifierParameter = sess_uuid_deleted__uniqueidentifier.HasValue ?
                new ObjectParameter("sess_uuid_deleted__uniqueidentifier", sess_uuid_deleted__uniqueidentifier) :
                new ObjectParameter("sess_uuid_deleted__uniqueidentifier", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_user_insert", user_uuid__uniqueidentifierParameter, user_username__varcharParameter, user_email__varcharParameter, user_cellphone__varcharParameter, user_password__varcharParameter, user_firstname__varcharParameter, user_lastname__varcharParameter, user_roleaccess__tinyintParameter, user_extradata__varcharParameter, reso_uuid_picture__uniqueidentifierParameter, date_uuid_birthdate__uniqueidentifierParameter, city_uuid__uniqueidentifierParameter, sess_uuid_used__uniqueidentifierParameter, sess_uuid_created__uniqueidentifierParameter, user_uuid_root__uniqueidentifierParameter, sess_uuid_deleted__uniqueidentifierParameter);
        }
    
        public virtual ObjectResult<proc_user_select_Result> proc_user_select(Nullable<System.Guid> user_uuid__uniqueidentifier)
        {
            var user_uuid__uniqueidentifierParameter = user_uuid__uniqueidentifier.HasValue ?
                new ObjectParameter("user_uuid__uniqueidentifier", user_uuid__uniqueidentifier) :
                new ObjectParameter("user_uuid__uniqueidentifier", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_user_select_Result>("proc_user_select", user_uuid__uniqueidentifierParameter);
        }
    
        public virtual ObjectResult<proc_user_selectAll_Result> proc_user_selectAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_user_selectAll_Result>("proc_user_selectAll");
        }
    }
}
