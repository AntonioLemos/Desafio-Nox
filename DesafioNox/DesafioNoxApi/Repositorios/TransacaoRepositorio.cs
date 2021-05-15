using DesafioNoxApi.Repositorios.Interfaces;
using DesafioNoxData.DataContexts;
using DesafioNoxModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioNoxApi.Repositorios
{
    public class TransacaoRepositorio : ITransacaoRepositorio
    {
        private DesafioNoxDataContext db = new DesafioNoxDataContext();

        public async Task<dynamic> PostTransacao(Transacao transacao)
        {
            try
            {
                var result = db.Transacao.Add(transacao);
                await db.SaveChangesAsync();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Transacao>> GetTransacoesPorData(int usuarioId, DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                IEnumerable<Transacao> result = new List<Transacao>();

                if (dataInicial.Date.ToString().Contains("01/01/0001") && dataFinal.Date.ToString().Contains("01/01/0001"))
                {
                    result = (from t in db.Transacao.Include("tbUsuario").Include("tbOrdemTransacao").Include("tbStatusTransacao")
                              where t.IdUsuario == usuarioId
                              select new
                              {
                                  IdTransacao = t.IdTransacao,
                                  ValorTransacao = Math.Round((t.ValorTransacao + (decimal)0.00005), 4),
                                  IdUsuario = t.IdUsuario,
                                  Usuario = t.Usuario,
                                  IdOrdemTransacao = t.IdOrdemTransacao,
                                  OrdemTransacao = t.OrdemTransacao,
                                  IdStatusTransacao = t.IdStatusTransacao,
                                  StatusTransacao = t.StatusTransacao,
                                  DataHoraTransacao = t.DataHoraTransacao
                              }).ToList().Select(x => new Transacao
                              {
                                  IdTransacao = x.IdTransacao,
                                  ValorTransacao = x.ValorTransacao,
                                  IdUsuario = x.IdUsuario,
                                  Usuario = new Usuario { IdUsuario = x.IdUsuario, EmailUsuario = x.Usuario.EmailUsuario, SaldoUsuarioReal = 0, SaldoUsuario = 0 },
                                  IdOrdemTransacao = x.IdOrdemTransacao,
                                  OrdemTransacao = x.OrdemTransacao,
                                  IdStatusTransacao = x.IdStatusTransacao,
                                  StatusTransacao = x.StatusTransacao,
                                  DataHoraTransacao = DateTime.SpecifyKind(x.DataHoraTransacao, DateTimeKind.Utc)
                              });

                    return result;
                }
                else if (!dataInicial.Date.ToString().Contains("01/01/0001") && dataFinal.Date.ToString().Contains("01/01/0001"))
                {
                    DateTime dateInicialPesquisa = dataInicial.ToUniversalTime();
                    result = (from t in db.Transacao.Include("tbUsuario").Include("tbOrdemTransacao").Include("tbStatusTransacao")
                              where t.IdUsuario == usuarioId && t.DataHoraTransacao > dateInicialPesquisa
                              select new
                              {
                                  IdTransacao = t.IdTransacao,
                                  ValorTransacao = Math.Round((t.ValorTransacao + (decimal)0.00005), 4),
                                  IdUsuario = t.IdUsuario,
                                  Usuario = t.Usuario,
                                  IdOrdemTransacao = t.IdOrdemTransacao,
                                  OrdemTransacao = t.OrdemTransacao,
                                  IdStatusTransacao = t.IdStatusTransacao,
                                  StatusTransacao = t.StatusTransacao,
                                  DataHoraTransacao = t.DataHoraTransacao
                              }).ToList().Select(x => new Transacao
                              {
                                  IdTransacao = x.IdTransacao,
                                  ValorTransacao = x.ValorTransacao,
                                  IdUsuario = x.IdUsuario,
                                  Usuario = new Usuario { IdUsuario = x.IdUsuario, EmailUsuario = x.Usuario.EmailUsuario, SaldoUsuarioReal = 0, SaldoUsuario = 0 },
                                  IdOrdemTransacao = x.IdOrdemTransacao,
                                  OrdemTransacao = x.OrdemTransacao,
                                  IdStatusTransacao = x.IdStatusTransacao,
                                  StatusTransacao = x.StatusTransacao,
                                  DataHoraTransacao = DateTime.SpecifyKind(x.DataHoraTransacao, DateTimeKind.Utc)
                              });

                    return result;

                }
                else if (!dataInicial.Date.ToString().Contains("01/01/0001") && !dataFinal.Date.ToString().Contains("01/01/0001"))
                {
                    DateTime dateInicialPesquisa = dataInicial.ToUniversalTime();
                    DateTime dateFinalPesquisa = dataFinal.ToUniversalTime();

                    result = (from t in db.Transacao.Include("tbUsuario").Include("tbOrdemTransacao").Include("tbStatusTransacao")
                              where t.IdUsuario == usuarioId
                                    && t.DataHoraTransacao > dateInicialPesquisa
                                    && t.DataHoraTransacao < dateFinalPesquisa
                              select new
                              {
                                  IdTransacao = t.IdTransacao,
                                  ValorTransacao = Math.Round((t.ValorTransacao + (decimal)0.00005), 4),
                                  IdUsuario = t.IdUsuario,
                                  Usuario = t.Usuario,
                                  IdOrdemTransacao = t.IdOrdemTransacao,
                                  OrdemTransacao = t.OrdemTransacao,
                                  IdStatusTransacao = t.IdStatusTransacao,
                                  StatusTransacao = t.StatusTransacao,
                                  DataHoraTransacao = t.DataHoraTransacao
                              }).ToList().Select(x => new Transacao
                              {
                                  IdTransacao = x.IdTransacao,
                                  ValorTransacao = x.ValorTransacao,
                                  IdUsuario = x.IdUsuario,
                                  Usuario = new Usuario { IdUsuario = x.IdUsuario, EmailUsuario = x.Usuario.EmailUsuario, SaldoUsuarioReal = 0, SaldoUsuario = 0 },
                                  IdOrdemTransacao = x.IdOrdemTransacao,
                                  OrdemTransacao = x.OrdemTransacao,
                                  IdStatusTransacao = x.IdStatusTransacao,
                                  StatusTransacao = x.StatusTransacao,
                                  DataHoraTransacao = DateTime.SpecifyKind(x.DataHoraTransacao, DateTimeKind.Utc)
                              });
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Dictionary<DateTime, Transacao>> GetTransacoesPorListaIds(int usuarioId, List<int> listIds)
        {
            try
            {
                var result = db.Transacao.Where(x => x.IdUsuario == usuarioId && listIds.Contains(x.IdTransacao)).OrderBy(x => x.DataHoraTransacao).ToDictionary(Field => Field.DataHoraTransacao, mc => mc);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public void AlterarStatusTransacaoUsuario(Dictionary<DateTime, Transacao> transacaoList, Usuario usuario)
        {
            try
            {
                foreach (var transacao in transacaoList.Values)
                {
                    db.Entry(transacao).State = EntityState.Modified;
                }

                db.Entry(usuario).State = EntityState.Modified;

                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public bool TransacaoOrdemExiste(int id)
        {
            return db.OrdemTransacao.Count(e => e.IdOrdemTransacao == id) > 0;
        }

        public bool TransacaoStatusExiste(int id)
        {
            return db.StatusTransacao.Count(e => e.IdStatusTransacao == id) > 0;
        }
    }
}