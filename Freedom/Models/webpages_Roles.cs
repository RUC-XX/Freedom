//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Freedom.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class webpages_Roles
    {
        public webpages_Roles()
        {
            this.Userprofile = new HashSet<Userprofile>();
            this.User = new HashSet<User>();
        }
    
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    
        public virtual ICollection<Userprofile> Userprofile { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual webpages_Roles webpages_Roles1 { get; set; }
        public virtual webpages_Roles webpages_Roles2 { get; set; }
    }
}
