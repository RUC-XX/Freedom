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
    
    public partial class Userprofile
    {
        public Userprofile()
        {
            this.webpages_OAuthMembership = new HashSet<webpages_OAuthMembership>();
            this.webpages_Roles = new HashSet<webpages_Roles>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual Order_Detail Order_Detail { get; set; }
        public virtual User User { get; set; }
        public virtual webpages_Membership webpages_Membership { get; set; }
        public virtual ICollection<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}
