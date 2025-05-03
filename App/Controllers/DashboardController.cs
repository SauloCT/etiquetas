using App.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Diagnostics;
using System.Linq;
using VendaERP.Core;
using VendaERP.Core.Models;
using static MongoDB.Driver.WriteConcern;

namespace App.Controllers
{
    public class DashboardController : Controller
    {
        ///<summary>
        ///Variavel padrão de acesso ao Banco de Dados
        ///</summary>
        private readonly DBAccess _db;
        private DashboardPadrao _model;
        public DashboardController(DBAccess db)
        {
            this._db = db;
            this._model = new DashboardPadrao();
        }
        

        public IActionResult Index()
        {
            this._model.dashboard_vendas.TotalLancamentos = _db._repositoryLancamento.Count();
            var start = Convert.ToDateTime(DateTime.Now.Year + "-01-01");
            var end = Convert.ToDateTime(DateTime.Now.Year + "-12-31");
            var resumo = _db._repositoryVenda.Collection.AsQueryable<DtoVenda>().Where(x => x.Data >= start && x.Data <= end);
            _model.dashboard_vendas.TotalVendas = new double[12] {0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };
            _model.dashboard_vendas.TotalCustos = new double[12] {0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };
            _model.dashboard_vendas.Categorias = new List<string>();
            _model.dashboard_vendas.CategoriasPrice = new List<string>();
            _model.dashboard_vendas.CategoriasPercent = new List<string>();
            var categoriasPrice = new Dictionary<string, double>();
            var categoriasPercent = new Dictionary<string, double>();
            double somaVenda = 0.00;
            _model.dashboard_vendas.valor1 = new double();
            _model.dashboard_vendas.valor2 = new double();
            _model.dashboard_vendas.valor3 = new double();
            _model.dashboard_vendas.valor4 = new double();
            _model.dashboard_vendas.valor5 = new double();



            //_model.dashboard_vendas.TotalVendas[0] = _db._repositoryVenda.Collection.Aggregate<DtoVenda>().Match(janeiro).Group();
            for (int i = 0; i < 12; i++)
            {
                if (i == 11)
                {
                    _model.dashboard_vendas.TotalVendas[i] = resumo.AsQueryable<DtoVenda>().Where(x => x.Data >= Convert.ToDateTime(DateTime.Now.Year + $"-{(i + 1).ToString("00")}-01") && x.Data < Convert.ToDateTime((DateTime.Now.Year + 1) + "-01-01")).Sum(x => x.ValorFinal);

                    _model.dashboard_vendas.TotalCustos[i] = resumo.AsQueryable<DtoVenda>().Where(x => x.Data >= Convert.ToDateTime(DateTime.Now.Year + $"-{(i + 1).ToString("00")}-01") && x.Data < Convert.ToDateTime((DateTime.Now.Year + 1) + "-01-01")).Sum(x => x.CustoTotalDaVenda);
                }
                else
                {
                    _model.dashboard_vendas.TotalVendas[i] = resumo.AsQueryable<DtoVenda>().Where(x => x.Data >= Convert.ToDateTime(DateTime.Now.Year + $"-{(i + 1).ToString("00")}-01") && x.Data < Convert.ToDateTime(DateTime.Now.Year + $"-{(i + 2).ToString("00")}-01")).Sum(x => x.ValorFinal);
                    _model.dashboard_vendas.TotalCustos[i] = resumo.AsQueryable<DtoVenda>().Where(x => x.Data >= Convert.ToDateTime(DateTime.Now.Year + $"-{(i + 1).ToString("00")}-01") && x.Data < Convert.ToDateTime(DateTime.Now.Year + $"-{(i + 2).ToString("00")}-01")).Sum(x => x.CustoTotalDaVenda);
                }                    
                _model.dashboard_vendas.valor1 += _model.dashboard_vendas.TotalVendas[i];
                _model.dashboard_vendas.valor2 += _model.dashboard_vendas.TotalCustos[i];
            }





            foreach (var item in resumo)
            {



                if (!string.IsNullOrEmpty(item.VendaCategoria))
                {
                    if (categoriasPrice.ContainsKey(item.VendaCategoria))
                    {
                        categoriasPrice[item.VendaCategoria] += item.ValorFinal;
                    }
                    else
                    {
                        categoriasPrice.Add(item.VendaCategoria, item.ValorFinal);
                    }


                }

                //quantidade total dos itens está levando em consideração apenas o valor armazenado na DtoVenda em QuantidadeTotalItensVendidos
                _model.dashboard_vendas.valor4 += item.PedidoRelacionado;
                _model.dashboard_vendas.valor5 += item.QuantidadeTotalItensVendidos;

            }
            _model.dashboard_vendas.valor3 = _model.dashboard_vendas.valor1 - _model.dashboard_vendas.valor2;
            
            foreach (var item in categoriasPrice)
            {

                _model.dashboard_vendas.Categorias.Add(item.Key);
                _model.dashboard_vendas.CategoriasPrice.Add(item.Value.ToString("0.00").Replace(",", "."));
                somaVenda += item.Value;

            }
            somaVenda = Math.Round(somaVenda, 2);

            foreach(var item in resumo)
            {
                if (!string.IsNullOrEmpty(item.VendaCategoria))
                {
                    if (categoriasPercent.ContainsKey(item.VendaCategoria))
                    {
                        categoriasPercent[item.VendaCategoria] += (item.ValorFinal * 100.00) / somaVenda;
                    }
                    else
                    {
                        categoriasPercent.Add(item.VendaCategoria, (item.ValorFinal * 100.00) / somaVenda);
                    }


                }

            }
            foreach(var item in categoriasPercent)
            {
                _model.dashboard_vendas.CategoriasPercent.Add(item.Value.ToString("0.00").Replace(",", "."));
            }


            return View(this._model);
        }
        public IActionResult Financeiro()
        {
            var start = Convert.ToDateTime(DateTime.Now.Year+"-01-01");
            var end = Convert.ToDateTime(DateTime.Now.Year+"-12-31");
            var resumo = _db._repositoryLancamento.Collection.AsQueryable<DtoLancamento>().Where(x =>true && (x.DataFluxo >= start && x.DataFluxo <= end)).ToList();

            var resumo_Receita = resumo.Select(x => x.Despesa == false).ToList();
            var resumo_Despesa = resumo.Select(x => x.Despesa == true).ToList();
            _model.dashboard_financeiro.TotalDespesa = new double[12] { 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };
            _model.dashboard_financeiro.TotalReceita = new double[12] { 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00 };
            foreach (var item in resumo)
            {
                

                switch (item.DataFluxo.Month)
                {
                    case 1:
                        _model.dashboard_financeiro.TotalDespesa[0] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[0] += item.Despesa == false ? item.Entrada : 0;
                        

                        break;
                    case 2:
                        _model.dashboard_financeiro.TotalDespesa[1] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[1] += item.Despesa == false ? item.Entrada : 0;
                        break;
                    case 3:
                        _model.dashboard_financeiro.TotalDespesa[2] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[2] += item.Despesa == false ? item.Entrada : 0;
                        break;
                    case 4:
                        _model.dashboard_financeiro.TotalDespesa[3] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[3] += item.Despesa == false ? item.Entrada : 0;
                        break;
                    case 5:
                        _model.dashboard_financeiro.TotalDespesa[4] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[4] += item.Despesa == false ? item.Entrada : 0;
                        break;
                    case 6:
                        _model.dashboard_financeiro.TotalDespesa[5] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[5] += item.Despesa == false ? item.Entrada : 0;
                        break;
                    case 7:
                        _model.dashboard_financeiro.TotalDespesa[6] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[6] += item.Despesa == false ? item.Entrada : 0;
                        break;
                    case 8:
                        _model.dashboard_financeiro.TotalDespesa[7] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[7] += item.Despesa == false ? item.Entrada : 0;
                        break;
                    case 9:
                        _model.dashboard_financeiro.TotalDespesa[8] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[8] += item.Despesa == false ? item.Entrada : 0;
                        break;
                    case 10:
                        _model.dashboard_financeiro.TotalDespesa[9] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[9] += item.Despesa == false ? item.Entrada : 0;
                        break;
                    case 11:
                        _model.dashboard_financeiro.TotalDespesa[10] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[10] += item.Despesa == false ? item.Entrada : 0;
                        break;
                    case 12:
                        _model.dashboard_financeiro.TotalDespesa[11] += item.Despesa == true ? item.Saida : 0;
                        _model.dashboard_financeiro.TotalReceita[11] += item.Despesa == false ? item.Entrada : 0;
                        break;

                }
            }




                return View(this._model);
        }


            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}