using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeBank.Models
{
    public class CodeBankContext : DbContext
    {
        public CodeBankContext(DbContextOptions<CodeBankContext> options)
            : base(options)
        {
        }

        public DbSet<CodeBank.Models.Code> Code { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, 
            //# you should move it out of source code.See http://go.microsoft.com/fwlink/?LinkId=723263 
            //# for guidance on storing connection strings.

           // optionsBuilder.UseMySQL("server=localhost;database=laravel;user=root;password=pakistan");
        }
    }
}
