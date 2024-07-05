using Microsoft.EntityFrameworkCore;
using TechNation.Context;
using TechNation.Enum;
using TechNation.Models;
using TechNation.Repository.Interface;

namespace TechNation.Repository
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private readonly AddDbContext _addDbContext;

        public NotaFiscalRepository (AddDbContext addDbContext)
        {
            _addDbContext = addDbContext;
        }

        public NotaFiscal Adicionar(NotaFiscal notaFiscal)
        {
            try
            {
                _addDbContext.NotaEmitida.Add(notaFiscal);
                _addDbContext.SaveChanges();
                return (notaFiscal);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Criar a nota fiscal com ID {notaFiscal.Id}: {ex.Message}", ex);
            }
        }
              
        public NotaFiscal Atualizar(NotaFiscal notaFiscal)
        {
            try
            {
                _addDbContext.Entry(notaFiscal).State = EntityState.Modified;
                _addDbContext.SaveChanges();
                return (notaFiscal);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Atualizar a nota fiscal com ID {notaFiscal.Id}: {ex.Message}", ex);
            }
        }

        public NotaFiscal Excluir(int id)
        {
            try
            {
                var notaFiscal = _addDbContext.NotaEmitida.Find(id);
                if (notaFiscal != null)
                {
                    _addDbContext.NotaEmitida.Remove(notaFiscal);
                    _addDbContext.SaveChanges();
                    return notaFiscal;
                }
                throw new Exception($"Nota fiscal com ID {id} não encontrada.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir a nota fiscal com ID {id}: {ex.Message}", ex);
            }
        }
        public NotaFiscal ObterPorId(int id)
        {
            return _addDbContext.NotaEmitida.Find(id);
        }

        public IEnumerable<NotaFiscal> ObterTodos()
        {
            return _addDbContext.NotaEmitida.ToList();
        }
        public IEnumerable<NotaFiscal> Filtro(DateTime? mesEmissao, DateTime? mesCobranca, DateTime? mesPagamento, string statusNota)
        {
            var query = _addDbContext.NotaEmitida.AsQueryable();

            if (mesEmissao.HasValue)
            {
                query = query.Where(n => n.DataEmissao.Year == mesEmissao.Value.Year && n.DataEmissao.Month == mesEmissao.Value.Month);
            }

            if (mesCobranca.HasValue)
            {
                query = query.Where(n => n.DataCobranca.HasValue && n.DataCobranca.Value.Year == mesCobranca.Value.Year && n.DataCobranca.Value.Month == mesCobranca.Value.Month);
            }

            if (mesPagamento.HasValue)
            {
                query = query.Where(n => n.DataPagamento.HasValue && n.DataPagamento.Value.Year == mesPagamento.Value.Year && n.DataPagamento.Value.Month == mesPagamento.Value.Month);
            }

            if (!string.IsNullOrEmpty(statusNota))
            {
                if (System.Enum.TryParse(statusNota, out StatusNota status))
                {
                    query = query.Where(n => n.Status == status);
                }
                else
                {
                    throw new ArgumentException("Status da nota inválido", nameof(statusNota));
                }
            }

            return query.ToList();
        }

        public IEnumerable<decimal> ObterEvolucaoInadimplenciaMensal(int ano)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.DataEmissao.Year == ano && n.Status == StatusNota.PagamentoEmAtraso)
                .GroupBy(n => n.DataEmissao.Month)
                .Select(g => g.Sum(n => n.ValorNota))
                .ToList();
        }

        public IEnumerable<decimal> ObterEvolucaoReceitaMensal(int ano)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.DataPagamento.HasValue && n.DataPagamento.Value.Year == ano)
                .GroupBy(n => n.DataPagamento.Value.Month)
                .Select(g => g.Sum(n => n.ValorNota))
                .ToList();
        }


        public IEnumerable<NotaFiscal> ObterPorMesCobranca(int mes, int ano)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.DataCobranca.HasValue && n.DataCobranca.Value.Month == mes && n.DataCobranca.Value.Year == ano)
                .ToList();
        }

        public IEnumerable<NotaFiscal> ObterPorMesEmissao(int mes, int ano)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.DataEmissao.Month == mes && n.DataEmissao.Year == ano)
                .ToList();
        }

        public IEnumerable<NotaFiscal> ObterPorMesPagamento(int mes, int ano)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.DataPagamento.HasValue && n.DataPagamento.Value.Month == mes && n.DataPagamento.Value.Year == ano)
                .ToList();
        }

        public IEnumerable<NotaFiscal> ObterPorStatus(StatusNota status)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.Status == status)
                .ToList();
        }

        public decimal ObterTotalNotasAVencer(int mes, int ano)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.DataEmissao.Month == mes && n.DataEmissao.Year == ano && n.Status == StatusNota.Emitida)
                .Sum(n => n.ValorNota);
        }

        public decimal ObterTotalNotasEmitidas(int mes, int ano)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.DataEmissao.Month == mes && n.DataEmissao.Year == ano)
                .Sum(n => n.ValorNota);
        }

        public decimal ObterTotalNotasNaoCobradas(int mes, int ano)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.DataEmissao.Month == mes && n.DataEmissao.Year == ano && !n.DataCobranca.HasValue)
                .Sum(n => n.ValorNota);
        }

        public decimal ObterTotalNotasPagas(int mes, int ano)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.DataPagamento.HasValue && n.DataPagamento.Value.Month == mes && n.DataPagamento.Value.Year == ano)
                .Sum(n => n.ValorNota);
        }

        public decimal ObterTotalNotasVencidas(int mes, int ano)
        {
            return _addDbContext.NotaEmitida
                .Where(n => n.DataEmissao.Month == mes && n.DataEmissao.Year == ano && n.Status == StatusNota.PagamentoEmAtraso)
                .Sum(n => n.ValorNota);
        }


    }
}
