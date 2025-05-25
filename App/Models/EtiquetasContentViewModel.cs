using App.Controllers;
using App.VendaERP.Core.Models;
using System.Collections.Generic;

namespace App.Models
{
    public class EtiquetasContentViewModel
    {
        public string NomeEmpresa { get; set; }
        public DtoEtiquetasPadroes ModelEtiqueta { get; set; }
        public OpcoesSelecionadas OpcoesSelecionadas { get; set; }
        public List<ProdutoEscolhido> ListaItens { get; set; }
    }
} 