using MwangiFinancial.Models;
using MwangiFinancial.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.ViewModels
{
    public class UserProfileViewModel
    {
        public IndexViewModel IndexViewModel { get; set; }
        public UserViewModel UserViewModel { get; set; }
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        public BankAccountType AccountType { get; set; }
    }
}