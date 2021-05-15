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
    public class StatusTransacaoMap : EntityTypeConfiguration<StatusTransacao>
    {
        public StatusTransacaoMap()
        {
            ToTable("tbStatusTransacao");
            HasKey(x => x.IdStatusTransacao);
            Property(x => x.StatusTransacaoDescricao).HasMaxLength(25).IsRequired();
        }


    }
}
