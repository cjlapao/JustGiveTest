using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JG.FinTechTest.Persistence;

namespace JG.FinTechTest.Models
{
    public class JGFinTechTestContext : DbContext
    {
        public JGFinTechTestContext (DbContextOptions<JGFinTechTestContext> options)
            : base(options)
        {
        }

        public DbSet<GiftAidDeclaration> GiftAidDeclaration { get; set; }
    }
}
