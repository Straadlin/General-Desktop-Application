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
    using System.Collections.Generic;
    
    public partial class principalcompany
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public principalcompany()
        {
            this.principalcompany1 = new HashSet<principalcompany>();
        }
    
        public System.Guid prco_uuid__uniqueidentifier { get; set; }
        public string prco_rfc__varchar { get; set; }
        public string prco_name__varchar { get; set; }
        public string prco_address__varchar { get; set; }
        public string prco_phone__varchar { get; set; }
        public string prco_email__varchar { get; set; }
        public string prco_facebook__varchar { get; set; }
        public bool prco_developmentmode__bit { get; set; }
        public byte[] prco_timebetweenbackups__timestamp { get; set; }
        public Nullable<System.Guid> reso_uuid_logo__uniqueidentifier { get; set; }
        public Nullable<System.Guid> city_uuid__uniqueidentifier { get; set; }
        public System.Guid sess_uuid_created__uniqueidentifier { get; set; }
        public Nullable<System.Guid> prco_uuid_root__uniqueidentifier { get; set; }
    
        public virtual city city { get; set; }
        public virtual resource resource { get; set; }
        public virtual session session { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<principalcompany> principalcompany1 { get; set; }
        public virtual principalcompany principalcompany2 { get; set; }
    }
}
