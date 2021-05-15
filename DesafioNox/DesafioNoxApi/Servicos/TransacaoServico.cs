using DesafioNox.Helper;
using DesafioNoxApi.Repositorios.Interfaces;
using DesafioNoxApi.Servicos.Interfaces;
using DesafioNoxModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioNoxApi.Servicos
{
    public class TransacaoServico : ITransacaoServico
    {
        protected readonly ITransacaoRepositorio _transacaoRepositorio;
        protected readonly IUsuarioRepositorio _usuarioRepositorio;

        public TransacaoServico(ITransacaoRepositorio transacaoRepositorio,
            IUsuarioRepositorio usuarioRepositorio)
        {
            _transacaoRepositorio = transacaoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<dynamic> PostTransacao(Transacao transacao)
        {
            var validate = await ValidateTransacao(transacao);

            return validate;
        }

        public async Task<MensagemResposta<IEnumerable<Transacao>>> GetTransacoesPesquisa(string dataBusca)
        {
            var validate = await ValidateBusca(dataBusca);

            return validate;
        }

        public async Task<MensagemResposta<IEnumerable<Transacao>>> PostStatus(string dataStatus)
        {
            var validate = await ValidateStatus(dataStatus);

            var dataInicial = Convert.ToDateTime("01/01/0001");
            var dataFinal = Convert.ToDateTime("01/01/0001");

            var transacaoResultList = await _transacaoRepositorio.GetTransacoesPorData(Convert.ToInt32(dataStatus.Split(';')[0]), dataInicial, dataFinal);

            validate.Resposta = transacaoResultList;

            return validate;
        }

        private async Task<MensagemResposta<IEnumerable<Transacao>>> ValidateStatus(string dataStatus)
        {
            var result = new MensagemResposta<IEnumerable<Transacao>>("OK", new List<Transacao>());

            int idUsuario;
            int novoStatus;
            List<int> listWhere = new List<int>();
            string[] listBusca = dataStatus.Split(';');

            Int32.TryParse(listBusca[0], out idUsuario);
            Int32.TryParse(listBusca[1], out novoStatus);

            for (int i = 2; i < listBusca.Length; i++)
                listWhere.Add(Convert.ToInt32(listBusca[i]));

            dynamic user = await _usuarioRepositorio.GetUsuarioId(idUsuario);

            if (user.IdUsuario == 0)
            {
                result.Mensagem = "Usuário inexistente!";
                return result;
            }

            decimal saldoUsuarioBase = (decimal)user.SaldoUsuario;
            decimal saldoUsuarioBaseReal = (decimal)user.SaldoUsuarioReal;

            Dictionary<DateTime, Transacao> resultTransacaoList = await _transacaoRepositorio.GetTransacoesPorListaIds(idUsuario, listWhere);

            if (resultTransacaoList.Count == 0)
            {
                result.Mensagem = "Selecione pelo menos uma transação!";
                return result;
            }

            var resultStatusExists = _transacaoRepositorio.TransacaoStatusExiste(novoStatus);

            if (!resultStatusExists)
            {
                result.Mensagem = "Status inexistente!";
                return result;
            }

            foreach (var item in resultTransacaoList)
            {
                if (item.Value.IdStatusTransacao == novoStatus)
                {
                    result.Mensagem = "Selecione um status diferente para a(s) transação(s)!";
                    return result;
                }
                else if (item.Value.IdStatusTransacao == TransacaoGlobals.STATUS_ABERTO)
                {
                    switch (novoStatus)
                    {
                        case TransacaoGlobals.STATUS_EXECUTADO:
                            saldoUsuarioBaseReal = saldoUsuarioBaseReal - item.Value.ValorTransacao;
                            item.Value.IdStatusTransacao = novoStatus;
                            break;
                        case TransacaoGlobals.STATUS_REJEITADO:
                            item.Value.IdStatusTransacao = novoStatus;
                            break;
                        default:
                            result.Mensagem = "Erro ao tentar mover status de EXECUTADO para STATUS DESCONHECIDO";
                            return result;
                    }
                }
                else if (item.Value.IdStatusTransacao == TransacaoGlobals.STATUS_REJEITADO)
                {
                    switch (novoStatus)
                    {
                        case TransacaoGlobals.STATUS_ABERTO:
                            result.Mensagem = "Erro ao tentar mover status de REJEITADO para ABERTO!";
                            return result;
                        case TransacaoGlobals.STATUS_EXECUTADO:
                            saldoUsuarioBaseReal = saldoUsuarioBaseReal - item.Value.ValorTransacao;
                            item.Value.IdStatusTransacao = novoStatus;
                            break;
                        default:
                            result.Mensagem = "Erro ao tentar mover status de EXECUTADO para STATUS DESCONHECIDO";
                            return result;
                    }
                }
                else if (item.Value.IdStatusTransacao == TransacaoGlobals.STATUS_EXECUTADO)
                {
                    switch (novoStatus)
                    {
                        case TransacaoGlobals.STATUS_ABERTO:
                            result.Mensagem = "Erro ao tentar mover status de EXECUTADO para ABERTO!";
                            return result;
                        case TransacaoGlobals.STATUS_REJEITADO:
                            saldoUsuarioBaseReal = saldoUsuarioBaseReal + item.Value.ValorTransacao;
                            item.Value.IdStatusTransacao = novoStatus;
                            break;
                        default:
                            result.Mensagem = "Erro ao tentar mover status de EXECUTADO para STATUS DESCONHECIDO";
                            return result;
                    }
                }
            }

            if (saldoUsuarioBaseReal < saldoUsuarioBase)
            {
                result.Mensagem = "Saldo insusficiente para alterar status da(s) transação(s)!";
                return result;
            }

            user.SaldoUsuario = saldoUsuarioBaseReal;
            user.SaldoUsuarioReal = saldoUsuarioBaseReal;

            _transacaoRepositorio.AlterarStatusTransacaoUsuario(resultTransacaoList, user);

            return result;
        }

        private async Task<MensagemResposta<IEnumerable<Transacao>>> ValidateBusca(string dataBusca)
        {
            var result = new MensagemResposta<IEnumerable<Transacao>>("OK", new List<Transacao>());

            int idUsuario;
            DateTime dataInicial, dataFinal;
            GetDataListaBusca(dataBusca, out idUsuario, out dataInicial, out dataFinal);

            dynamic user = await _usuarioRepositorio.GetUsuarioId(idUsuario);

            if (user.IdUsuario == 0)
            {
                result.Mensagem = "Usuário inexistente!";
                return result;
            }

            if ((dataInicial.Date.ToString().Contains("01/01/0001")) && !(dataFinal.Date.ToString().Contains("01/01/0001")))
            {
                result.Mensagem = "Selecione data de Inicio!";
                return result;
            }

            var transacaoResultList = await _transacaoRepositorio.GetTransacoesPorData(idUsuario, dataInicial, dataFinal);

            result.Resposta = transacaoResultList;

            return result;
        }

        private async Task<MensagemResposta<Transacao>> ValidateTransacao(Transacao transacao)
        {
            var result = new MensagemResposta<Transacao>("OK", transacao);

            dynamic user = await _usuarioRepositorio.GetUsuarioId(transacao.IdUsuario);

            if (user.IdUsuario == 0)
            {
                result.Mensagem = "Usuário inexistente!";
                return result;
            }
            else if (transacao.ValorTransacao > user.SaldoUsuario)
            {
                result.Mensagem = "Saldo insuficiente!";
                return result;
            }

            if (transacao.ValorTransacao <= 0)
            {
                result.Mensagem = "Valor da transação não pode ser 0!";
                return result;
            }

            var resultOrdemExists = _transacaoRepositorio.TransacaoOrdemExiste(transacao.IdOrdemTransacao);

            if (!resultOrdemExists)
            {
                result.Mensagem = "Ordem inexistente!";
                return result;
            }

            var dateUniversal = transacao.DataHoraTransacao.ToUniversalTime();
            var formatDataTransacao = DateTime.SpecifyKind(dateUniversal, DateTimeKind.Utc);

            transacao.DataHoraTransacao = formatDataTransacao;
            transacao.IdStatusTransacao = TransacaoGlobals.STATUS_ABERTO;

            var transacaoResult = await _transacaoRepositorio.PostTransacao(transacao);
            result.Resposta = transacaoResult;

            return result;
        }

        private static void GetDataListaBusca(string dataBusca, out int idUsuario, out DateTime dataInicial, out DateTime dataFinal)
        {
            string[] listBusca = dataBusca.Split(';');

            idUsuario = 0;
            dataInicial = Convert.ToDateTime("01/01/0001");
            dataFinal = Convert.ToDateTime("01/01/0001");

            for (int i = 0; i < listBusca.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        Int32.TryParse(listBusca[i].ToString(), out idUsuario);
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(listBusca[i].ToString()))
                        {
                            string[] dataInicioFormat = listBusca[i].ToString().Split('-');
                            DateTime.TryParse(dataInicioFormat[2] + "/" + dataInicioFormat[1] + "/" + dataInicioFormat[0], out dataInicial);
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(listBusca[i].ToString()))
                        {
                            string[] dataFinalFormat = listBusca[i].ToString().Split('-');
                            DateTime.TryParse(dataFinalFormat[2] + "/" + dataFinalFormat[1] + "/" + dataFinalFormat[0], out dataFinal);
                        }
                        break;
                    default: break;
                }
            }
        }


    }
}