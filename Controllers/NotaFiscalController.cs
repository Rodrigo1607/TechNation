using Microsoft.AspNetCore.Mvc;
using TechNation.Enum;
using TechNation.Models;
using TechNation.Repository.Interface;
using TechNation.ViewModel;

namespace TechNation.Controllers
{
    public class NotaFiscalController : Controller
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;

        public NotaFiscalController(INotaFiscalRepository notaFiscalRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<NotaFiscal> notas = _notaFiscalRepository.ObterTodos();
            return View(notas);
        }



        public IActionResult Details(int id)
        {
            var nota = _notaFiscalRepository.ObterPorId(id);
            if (nota == null)
            {
                return StatusCode(404);
            }
            return View(nota);
        }
        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(NotaFiscal notafiscal)
        {
            if (ModelState.IsValid)
            {
                _notaFiscalRepository.Adicionar(notafiscal);
                return RedirectToAction("Index");
            }

            return View(notafiscal);
        }
        public IActionResult Atualizar(int id)
        {
            var nota = _notaFiscalRepository.ObterPorId(id);
            if (nota == null)
            {
                return StatusCode(404);
            }
            return View(nota);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Atualizar(NotaFiscal nota)
        {
            if (ModelState.IsValid)
            {
                _notaFiscalRepository.Atualizar(nota);

                return RedirectToAction("Index");
            }
            return View(nota);
        }

        public IActionResult ExcluirConfirmacao(int id)
        {
            var nota = _notaFiscalRepository.ObterPorId(id);
            if (nota == null)
            {
                return StatusCode(404);
            }
            return View(nota);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirmado(int id)
        {
            try
            {
                var notaFiscal = _notaFiscalRepository.Excluir(id);
                if (notaFiscal == null)
                {
                    // Exibir mensagem de erro se a nota fiscal não for encontrada
                    TempData["ErrorMessage"] = $"Nota fiscal com ID {id} não encontrada.";
                    return RedirectToAction("Index");
                }

                // Exibir mensagem de sucesso
                TempData["SuccessMessage"] = "Nota fiscal excluída com sucesso.";
            }
            catch (Exception ex)
            {
                // Capturar e exibir qualquer erro ocorrido durante a exclusão
                TempData["ErrorMessage"] = $"Erro ao excluir a nota fiscal com ID {id}: {ex.Message}";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Dashboard()
        {
            var notas = _notaFiscalRepository.ObterTodos();

            var totalNotasEmitidas = notas.Sum(n => n.ValorNota);
            var totalSemCobranca = notas
                .Where(n => n.Status == StatusNota.Emitida)
                .Sum(n => n.ValorNota);
            var totalVencidas = notas
                .Where(n => n.Status == StatusNota.PagamentoEmAtraso)
                .Sum(n => n.ValorNota);
            var totalAReceber = notas
                .Where(n => n.Status == StatusNota.CobrancaRealizada)
                .Sum(n => n.ValorNota);
            var totalPagas = notas
                .Where(n => n.Status == StatusNota.PagamentoRealizado)
                .Sum(n => n.ValorNota);

            // Gráficos de evolução
            var inadimplenciaMesAMes = notas
                 .Where(n => n.Status == StatusNota.PagamentoEmAtraso)
                 .GroupBy(n => n.DataEmissao.Month)
                 .Select(g => new DashboardChartData { Mes = g.Key, Total = g.Sum(n => n.ValorNota) })
                 .ToList();

            var receitaMesAMes = notas
                 .Where(n => n.Status == StatusNota.PagamentoRealizado)
                 .GroupBy(n => n.DataPagamento.Value.Month)
                 .Select(g => new DashboardChartData { Mes = g.Key, Total = g.Sum(n => n.ValorNota) })
                 .ToList();

            var viewModel = new DashboardViewModel
            {
                TotalNotasEmitidas = totalNotasEmitidas,
                TotalSemCobranca = totalSemCobranca,
                TotalVencidas = totalVencidas,
                TotalAReceber = totalAReceber,
                TotalPagas = totalPagas,
                Notas = notas,
                InadimplenciaMesAMes = inadimplenciaMesAMes,
                ReceitaMesAMes = receitaMesAMes
            };
            return View(viewModel);
        }
    }

}




