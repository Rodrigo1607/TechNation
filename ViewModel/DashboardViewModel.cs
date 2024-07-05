using System;
using System.Collections.Generic;
using TechNation.Enum;
using TechNation.Models;

namespace TechNation.ViewModel
{
    public class DashboardViewModel
    {
        public decimal TotalNotasEmitidas { get; set; }
        public decimal TotalSemCobranca { get; set; }
        public decimal TotalVencidas { get; set; }
        public decimal TotalAReceber { get; set; }
        public decimal TotalPagas { get; set; }
        public IEnumerable<NotaFiscal> Notas { get; set; }
        public IEnumerable<DashboardChartData> InadimplenciaMesAMes { get; set; }
        public IEnumerable<DashboardChartData> ReceitaMesAMes { get; set; }
    }

    public class DashboardChartData
    {
        public int Mes { get; set; }
        public decimal Total { get; set; }
    }
}
