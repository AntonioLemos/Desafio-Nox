using DesafioNoxModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioNoxData.Mapping.Trasacao
{
    public class TransacaoMap : EntityTypeConfiguration<Transacao>
    {
        public TransacaoMap()
        {
            ToTable("tbTransacao");

            Property(x => x.IdTransacao)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasKey(x => x.IdTransacao);
            Property(x => x.ValorTransacao).HasPrecision(26, 10).IsRequired();

            HasRequired(x => x.Usuario);
            HasRequired(x => x.OrdemTransacao);
            HasRequired(x => x.StatusTransacao);

        }
    }
}
