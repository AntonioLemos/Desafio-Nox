using DesafioNoxData.Mapping.Trasacao;
using DesafioNoxModels;
using System.Data.Entity;

namespace DesafioNoxData.DataContexts
{
    public class DesafioNoxDataContext : DbContext
    {
        public DesafioNoxDataContext(): base("DesafioNoxConnectionString")
        {
            Database.SetInitializer<DesafioNoxDataContext>(new DesafioNoxDataContextInit());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<OrdemTransacao> OrdemTransacao { get; set; }
        public DbSet<StatusTransacao> StatusTransacao { get; set; }
        public DbSet<Transacao> Transacao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new OrdemTransacaoMap());
            modelBuilder.Configurations.Add(new StatusTransacaoMap());
            modelBuilder.Configurations.Add(new TransacaoMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class DesafioNoxDataContextInit : DropCreateDatabaseIfModelChanges<DesafioNoxDataContext> 
    {
        protected override void Seed(DesafioNoxDataContext context)
        {
            context.Usuario.Add(new Usuario {  EmailUsuario = "disafionoxteste@email.com", SaldoUsuarioReal = 10000, SaldoUsuario = 10000 });

            context.OrdemTransacao.Add(new OrdemTransacao { IdOrdemTransacao = 1, TipoOrdemTransacao = "COMPRA" });
            context.OrdemTransacao.Add(new OrdemTransacao { IdOrdemTransacao = 2, TipoOrdemTransacao = "VENDA" });
 
            context.StatusTransacao.Add(new StatusTransacao { IdStatusTransacao = 1, StatusTransacaoDescricao = "ABERTA" });
            context.StatusTransacao.Add(new StatusTransacao { IdStatusTransacao = 2, StatusTransacaoDescricao = "EXECUTADA" });
            context.StatusTransacao.Add(new StatusTransacao { IdStatusTransacao = 3, StatusTransacaoDescricao = "REJEITADA" });            

            context.SaveChanges();

            base.Seed(context);
        }

    }
}
