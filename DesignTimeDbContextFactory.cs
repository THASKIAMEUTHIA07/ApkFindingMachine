using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FindingMachine.Models;

namespace FindingMachine
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            optionsBuilder.UseMySql(
                "server=localhost;port=3306;database=FindingMachineDB;user=root;password=140711",
                new MySqlServerVersion(new Version(8, 0, 21)) // 
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}