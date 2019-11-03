using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6
{
    [Route("/admin/api")]
    public class ApiController
    {
        private readonly ApplicationDbContext dbContext;

        public ApiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("test")]
        public async Task<object> Test()
        {
            try
            {
                await dbContext.Database.MigrateAsync();

                dbContext.Items.Add(new Items { Name = "test" });
                await dbContext.SaveChangesAsync();

                return await dbContext.Items.ToListAsync();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpGet("test2")]
        public async Task<object> Test2()
        {
            return await dbContext.Items.CountAsync();
        }
    }
}
