using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExample;
public class DoggosContext : DbContext
{
    public DoggosContext(string connectionString) : base(GetOptions(connectionString))
    {
             
    }

    private static DbContextOptions GetOptions(string connectionString)
    {
        return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
    }

    public DbSet<Doggo> Doggos { get; set; }
    public DbSet<DoggoInfo> DoggoInfos { get; set; }
}
