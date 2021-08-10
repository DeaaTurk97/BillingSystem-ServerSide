﻿using System;

namespace Acorna.Core.Models.Security
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GroupId { get; set; }
        public int LanguageId { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
