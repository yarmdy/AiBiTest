using System;
using System.Collections.Generic;

namespace AiBi.Dal.Model
{
    public partial class IdEntity:BaseEntity
    {
        public IdEntity()
        {
            
        }
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
    }
}
