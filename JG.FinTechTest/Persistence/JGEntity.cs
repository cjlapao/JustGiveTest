using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JG.FinTechTest.Persistence
{
    public class JGEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
