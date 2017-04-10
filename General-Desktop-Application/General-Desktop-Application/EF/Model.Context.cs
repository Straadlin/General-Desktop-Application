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
        //public straad_generaldesktopapplication_pcpcpcpc_001Entities()
        //    : base("name=straad_generaldesktopapplication_pcpcpcpc_001Entities")
        //{
        //}

        public straad_generaldesktopapplication_pcpcpcpc_001Entities()
            : base(General_Desktop_Application.Classes.Preferences.Connectionstring)
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
    
        public virtual ObjectResult<string> proc_systemserver_getDatetime(ObjectParameter dateTime)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_systemserver_getDatetime", dateTime);
        }
    
        public virtual ObjectResult<proc_principalcompany_findAllBranches_Result> proc_principalcompany_findAllBranches(Nullable<System.Guid> prco_uuid__uniqueidentifier______Current)
        {
            var prco_uuid__uniqueidentifier______CurrentParameter = prco_uuid__uniqueidentifier______Current.HasValue ?
                new ObjectParameter("prco_uuid__uniqueidentifier______Current", prco_uuid__uniqueidentifier______Current) :
                new ObjectParameter("prco_uuid__uniqueidentifier______Current", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_principalcompany_findAllBranches_Result>("proc_principalcompany_findAllBranches", prco_uuid__uniqueidentifier______CurrentParameter);
        }
    
        public virtual ObjectResult<proc_user_findAllBranches_Result> proc_user_findAllBranches(Nullable<System.Guid> user_uuid__uniqueidentifier______Current)
        {
            var user_uuid__uniqueidentifier______CurrentParameter = user_uuid__uniqueidentifier______Current.HasValue ?
                new ObjectParameter("user_uuid__uniqueidentifier______Current", user_uuid__uniqueidentifier______Current) :
                new ObjectParameter("user_uuid__uniqueidentifier______Current", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_user_findAllBranches_Result>("proc_user_findAllBranches", user_uuid__uniqueidentifier______CurrentParameter);
        }
    }
}
