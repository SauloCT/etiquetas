using App.Controllers;
using App.VendaERP.Core.Models;
using System.Collections.Generic;

namespace App.Models
{
    public class EtiquetasContentViewModel
    {
        public string NomeEmpresa { get; set; } = string.Empty;
        public DtoEtiquetasPadroes ModelEtiqueta { get; set; } = new();
        public OpcoesSelecionadas OpcoesSelecionadas { get; set; } = new();
        public List<ProdutoEscolhido> ListaItens { get; set; } = new();
    }
} 