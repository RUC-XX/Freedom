﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Freedom
{
    public class UserViewModel
    {
        [Key]
        [Display(Name = "ID")]
        public int User_ID { set; get; }

        [Required(ErrorMessage="请填写用户名")]
        [Display(Name="用户名")]
        public string User_Name { set; get; }

        [Required(ErrorMessage = "请填写密码")]
        [Display(Name = "密码")]
        public string Password { set; get; }

        [Required(ErrorMessage = "请填写邮件地址")]
        [Display(Name = "邮件地址")]
        public string Email { set; get; }

        [Display(Name = "角色")]
        public string Role_Name { set; get; }

        [Required(ErrorMessage = "请填写新密码")]
        [Display(Name = "新密码")]
        public string NewPassword { set; get; }
    }
}
