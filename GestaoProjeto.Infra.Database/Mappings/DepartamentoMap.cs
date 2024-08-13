using GestaoProjeto.Infra.Domain.Departamentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Database.Mappings
{
    public class DepartamentoMap : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            //nome da tabela
            builder.ToTable("Departamento");

            //chave primária
            builder.HasKey(x => x.Id);

            builder.Property(t => t.Nome)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
