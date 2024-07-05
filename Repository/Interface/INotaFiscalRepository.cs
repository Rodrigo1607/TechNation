using TechNation.Enum;
using TechNation.Models;

namespace TechNation.Repository.Interface
{
    public interface INotaFiscalRepository
    {
        //Task<IEnumerable<NotaFiscal>> ObterTodos();
        //Task<NotaFiscal> ObterPorId(int id);
        //Task<IEnumerable<NotaFiscal>> ObterPorStatus(StatusNota status);
        //Task<IEnumerable<NotaFiscal>> ObterPorMesEmissao(int mes, int ano);
        //Task<IEnumerable<NotaFiscal>> ObterPorMesCobranca(int mes, int ano);
        //Task<IEnumerable<NotaFiscal>> ObterPorMesPagamento(int mes, int ano);
        //NotaFiscal Adicionar(NotaFiscal notaFiscal);
        //NotaFiscal Atualizar(NotaFiscal notaFiscal);
        //NotaFiscal Excluir(int id);
        //Task<IEnumerable<NotaFiscal>> FiltroAsync(DateTime? mesEmissao, DateTime? mesCobranca, DateTime? mesPagamento, string statusNota);
        //Task<decimal> ObterTotalNotasEmitidasAsync(int mes, int ano);
        //Task<decimal> ObterTotalNotasNaoCobradasAsync(int mes, int ano);
        //Task<decimal> ObterTotalNotasVencidasAsync(int mes, int ano);
        //Task<decimal> ObterTotalNotasAVencerAsync(int mes, int ano);
        //Task<decimal> ObterTotalNotasPagasAsync(int mes, int ano);
        //Task<IEnumerable<decimal>> ObterEvolucaoInadimplenciaMensalAsync(int ano);
        //Task<IEnumerable<decimal>> ObterEvolucaoReceitaMensalAsync(int ano);
        IEnumerable<NotaFiscal> ObterTodos();
        NotaFiscal ObterPorId(int id);
        IEnumerable<NotaFiscal> ObterPorStatus(StatusNota status);
        IEnumerable<NotaFiscal> ObterPorMesEmissao(int mes, int ano);
        IEnumerable<NotaFiscal> ObterPorMesCobranca(int mes, int ano);
        IEnumerable<NotaFiscal> ObterPorMesPagamento(int mes, int ano);

        NotaFiscal Adicionar(NotaFiscal notaFiscal);
        NotaFiscal Atualizar(NotaFiscal notaFiscal);
        NotaFiscal Excluir(int id);
        IEnumerable<NotaFiscal> Filtro(DateTime? mesEmissao, DateTime? mesCobranca, DateTime? mesPagamento, string statusNota);
        decimal ObterTotalNotasEmitidas(int mes, int ano);
        decimal ObterTotalNotasNaoCobradas(int mes, int ano);
        decimal ObterTotalNotasVencidas(int mes, int ano);
        decimal ObterTotalNotasAVencer(int mes, int ano);
        decimal ObterTotalNotasPagas(int mes, int ano);
        IEnumerable<decimal> ObterEvolucaoInadimplenciaMensal(int ano);
        IEnumerable<decimal> ObterEvolucaoReceitaMensal(int ano);



    }
}