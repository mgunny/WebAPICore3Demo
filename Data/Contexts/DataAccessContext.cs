using EmpSubbieWebAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmpSubbieWebAPI.Data.Contexts
{
    public class DataAccessContext : DbContext
    {
        // Constructor
        public DataAccessContext(DbContextOptions<DataAccessContext> options) : base(options)
        {
        }

        [NotMapped]
        public DbSet<FormForUser> FormForUser { get; set; }


    }
}
