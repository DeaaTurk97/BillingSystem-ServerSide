using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.DTOs.billingSystem
{
    public class PhoneBookDTO : BaseDTO
    {
        //public string PhoneNumber { get; set; }
        //public string PhoneName { get; set; }
        //public int PersonalUserId { get; set; }
        //public string UserName { get; set; }
        //public string? TypePhoneNumber { get; set; }
        //public string? statusNumberName { get; set; }

        public string PhoneNumber { get; set; }
        public string PhoneName { get; set; }
        public int PersonalUserId { get; set; }
        public string UserName { get; set; }
        public int TypePhoneNumberId { get; set; }
        public string? TypePhoneNumberName { get; set; }
        public int StatusNumberId { get; set; }
        public string? StatusNumberName { get; set; }
        public int StatusAdminId { get; set; }
    }
}
