using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orbita.Entity;
 

namespace orbita.Configurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.HasKey(u => u.RA);
            builder.Property(u => u.Id).HasColumnType("INT");
            builder.Property(u => u.RA).HasColumnType("VARCHAR(100)");          
            builder.Property(u => u.Name).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.Email).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(u => u.CPF).HasColumnType("VARCHAR(100)").IsRequired();       
        }
    }
}
