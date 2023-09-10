//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tudong.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            this.GAMEs = new HashSet<GAME>();
            this.MUAGAMEs = new HashSet<MUAGAME>();
            this.MUAGAMEs1 = new HashSet<MUAGAME>();
        }
    
        public int id { get; set; }
        public string email { get; set; }
        public string tentaikhoan { get; set; }
        public string matKhau { get; set; }
        public string sdt { get; set; }
        public Nullable<int> idRole { get; set; }
        public Nullable<System.DateTime> ngaySinh { get; set; }
        public string FuName { get; set; }
        public string Avartar { get; set; }
        public Nullable<int> sodu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GAME> GAMEs { get; set; }
        public virtual IDROLE IDROLE1 { get; set; }
        public virtual IDROLE IDROLE2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUAGAME> MUAGAMEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUAGAME> MUAGAMEs1 { get; set; }
    }
}
