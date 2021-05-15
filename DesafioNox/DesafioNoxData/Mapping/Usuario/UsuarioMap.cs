using DesafioNoxModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DesafioNoxData.Mapping.Trasacao
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("tbUsuario");

            Property(x => x.IdUsuario)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasKey(x => x.IdUsuario);
            Property(x => x.EmailUsuario).HasMaxLength(64).IsRequired();
            Property(x => x.SaldoUsuarioReal).HasPrecision(26,10).IsRequired();
            Property(x => x.SaldoUsuario).HasPrecision(26,10).IsRequired();

        }


    }
}
