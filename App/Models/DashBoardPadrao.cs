namespace App.Models
{
    public class DashboardPadrao
    {
        public DashboardPadrao()
        {
            dashboard_vendas = new DashboardVendas();
            dashboard_financeiro = new DashboardFinanceiro();
        }
        public DashboardVendas dashboard_vendas { get; set; }
        public DashboardFinanceiro dashboard_financeiro { get; set; }

    }
    public class DashboardVendas

    {
        public long TotalLancamentos { get; set; }

        public double[] TotalVendas { get; set; }
        
        public double[] TotalCustos { get; set; }

        public List<string> Categorias { get; set; }
        public List<string> CategoriasPrice { get; set; }
        
        public List<string> CategoriasPercent { get; set; }
        public double valor1, valor2, valor3, valor4, valor5;
    }
    public class DashboardFinanceiro
    {
        public double[] TotalDespesa { get; set; }

        public double[] TotalReceita { get; set; }
    }
}
