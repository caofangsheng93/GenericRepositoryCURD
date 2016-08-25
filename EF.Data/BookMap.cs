using EF.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data
{
    public class BookMap:EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            //配置主键
            this.HasKey(s => s.ID);

            //配置字段
            this.Property(s => s.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(s => s.Author).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            this.Property(s => s.AddedDate).IsRequired();
            this.Property(s => s.IP).IsOptional();
            this.Property(s => s.ISBN).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            this.Property(s => s.ModifiedDate).IsOptional();
            this.Property(s => s.PublishedDate).IsRequired();
            this.Property(s => s.Title).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();

            //配置表名
            this.ToTable("Books");
        }
    }
}
