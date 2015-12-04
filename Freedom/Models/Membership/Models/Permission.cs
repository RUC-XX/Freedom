﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freedom
{
    [Table("webpages_Permission")]
    public class Permission
    {
        public Permission()
        {
            PermissionsInRoles = new List<PermissionsInRoles>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name="权限ID")]
        public int PermissionId { get; set; }

        [Display(Name = "名称")]
        public string PermissionName { get; set; }

        [ForeignKey("PermissionId")]
        public ICollection<PermissionsInRoles> PermissionsInRoles { set; get; }
    }
}
