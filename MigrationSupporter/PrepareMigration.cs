using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ST3.Data;
using ST3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ST3.MigrationSupporter
{
    public static class PrepareMigration
    {
        public static void MigrateDb(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                if (!context.Shelters.Any())
                {
                    context.Shelters.AddRange(

                        new Shelter() { Name = "Dom Doroty", State = "Podkarpacie" },
                        new Shelter() { Name = "Dogo House", State = "Podkarpacie" }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
