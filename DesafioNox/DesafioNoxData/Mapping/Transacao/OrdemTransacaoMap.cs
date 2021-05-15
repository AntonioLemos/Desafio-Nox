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
    public class OrdemTransacaoMap : EntityTypeConfiguration<OrdemTransacao>
    {
        public OrdemTransacaoMap()
        {
            ToTable("tbOrdemTransacao");
            HasKey(x => x.IdOrdemTransacao);
            Property(x => x.TipoOrdemTransacao).HasMaxLength(25).IsRequired();         
        }
        

    }
}
