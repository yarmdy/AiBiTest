using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;

namespace AiBi.Test.Bll
{
    public partial class HomeReq 
    {
        public class Login :BaseReq{ 
            public string Account { get; set; }
            public string Password { get; set; }
            public string VerificationCode { get; set; }

            public string ReturnUrl { get; set; }

            
        }
        
    }
}
