using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;

namespace AiBi.Test.Bll
{
    public partial class PlanReq
    {
        public class Page :PageReq{ 
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }
        
    }
}
