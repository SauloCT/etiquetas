using App.Models;
using App.VendaERP.Core.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;
using VendaERP.Core;

namespace App.Controllers
{
    public class EtiquetasController : Controller
    {
        private readonly DBAccess _db;
        private Autocompletar autocompletar;
        public EtiquetasController(DBAccess db)
        {
            _db = db;
            this.autocompletar = new Autocompletar(db);
        }
        public IActionResult Index()
        {
            if (TempData.ContainsKey("message"))
                ViewBag.message = TempData["message"];
            return View(this.autocompletar);
        }
        public IActionResult NewModel()
        {
            return View();
        }
        public IActionResult SaveNewModel(string nome, string papel, string larguraPapel, string alturaPapel, string? larguraEtiqueta, string? alturaEtiqueta, string? espacamentoHorizontal, string? espacamentoVertical, string? margemEsquerda, string? margemSuperior, string? zoomImpressao, int? tamanhoFonte, int? tamanhoPreco, string? alturaBarras)
        {
            DtoEtiquetasPadroes padrao = new DtoEtiquetasPadroes()
            {
                Nome = nome,
                Papel = (TipoPapel)Enum.Parse(typeof(TipoPapel), papel),
                LarguraPapel = Double.Parse(larguraPapel.Replace(".", ",")),
                AlturaPapel = Double.Parse(alturaPapel.Replace(".", ",")),
                Largura = larguraEtiqueta != null ? Double.Parse(larguraEtiqueta.Replace(".", ",")) : null,
                Altura = alturaEtiqueta != null ? Double.Parse(alturaEtiqueta.Replace(".", ",")) : null,
                EspacamentoHorizontal = espacamentoHorizontal != null ? Double.Parse(espacamentoHorizontal.Replace(".", ",")) : null,
                EspacamentoVertical = espacamentoVertical != null ? Double.Parse(espacamentoVertical.Replace(".", ",")) : null,
                MargemEsquerda = margemEsquerda != null ? Double.Parse(margemEsquerda.Replace(".", ",")) : null,
                MargemSuperior = margemSuperior != null ? Double.Parse(margemSuperior.Replace(".", ",")) : null,
                ZoomImpressao = zoomImpressao != null ? Double.Parse(zoomImpressao.Replace(".", ",")) : null,
                TamanhoFonte = tamanhoFonte,
                TamanhoPreco = tamanhoPreco,
                AlturaEAN = alturaBarras != null ? Double.Parse(alturaBarras.Replace(".", ",")) : null
            };
            _db._repositoryEtiquetasPadroes.Collection.InsertOne(padrao);
            return Redirect("/Etiquetas/ListModels");
        }
        public IActionResult ListModels(int pageNumber)
        {
            var listModels = _db._repositoryEtiquetasPadroes.Collection.Find(x => true).Sort("{_id: -1}").ToList();
            var pager = new Pager(listModels.Count(), pageNumber, 15);
            var model = listModels.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View(model);
        }
        public HttpResponseMessage RemoveModel(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _db._repositoryEtiquetasPadroes.Collection.DeleteOne(x => x.Id == id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
        [Route("Etiquetas/EditModel/{id}")]
        public IActionResult EditModel(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                this.ViewBag.modelo = _db._repositoryEtiquetasPadroes.Collection.Find(x => x.Id == id).FirstOrDefault();
            }
            return View();
        }
        public IActionResult UpdateModel(string id, string nome, string papel, string larguraPapel, string alturaPapel, string? larguraEtiqueta, string? alturaEtiqueta, string? espacamentoHorizontal, string? espacamentoVertical, string? margemEsquerda, string? margemSuperior, string? zoomImpressao, int? tamanhoFonte, int? tamanhoPreco, string? alturaBarras)
        {
            if (!string.IsNullOrEmpty(id))
            {
                DtoEtiquetasPadroes modelo = new DtoEtiquetasPadroes()
                {
                    Id = id,
                    Nome = nome,
                    Papel = (TipoPapel)Enum.Parse(typeof(TipoPapel), papel),
                    LarguraPapel = Double.Parse(larguraPapel.Replace(".", ",")),
                    AlturaPapel = Double.Parse(alturaPapel.Replace(".", ",")),
                    Largura = larguraEtiqueta != null ? Double.Parse(larguraEtiqueta.Replace(".", ",")) : null,
                    Altura = alturaEtiqueta != null ? Double.Parse(alturaEtiqueta.Replace(".", ",")) : null,
                    EspacamentoHorizontal = espacamentoHorizontal != null ? Double.Parse(espacamentoHorizontal.Replace(".", ",")) : null,
                    EspacamentoVertical = espacamentoVertical != null ? Double.Parse(espacamentoVertical.Replace(".", ",")) : null,
                    MargemEsquerda = margemEsquerda != null ? Double.Parse(margemEsquerda.Replace(".", ",")) : null,
                    MargemSuperior = margemSuperior != null ? Double.Parse(margemSuperior.Replace(".", ",")) : null,
                    ZoomImpressao = zoomImpressao != null ? Double.Parse(zoomImpressao.Replace(".", ",")) : null,
                    TamanhoFonte = tamanhoFonte,
                    TamanhoPreco = tamanhoPreco,
                    AlturaEAN = alturaBarras != null ? Double.Parse(alturaBarras.Replace(".", ",")) : null
                };
                _db._repositoryEtiquetasPadroes.Collection.ReplaceOne(x => x.Id == id, modelo);
            }
            return Redirect("/Etiquetas/EditModel/" + id);
        }
        [HttpPost]
        public IActionResult Baixar(string empresa, string etiqueta, string clienteFornecedor, string tabelaDePreco, string deposito, bool dadoLadoCodigoBarras, bool imprimirCodigoBarras, bool imprimirNumeroCodigoBarras, bool imprimirCodigo, bool imprimirNome, bool imprimirPreco, bool precoComoCodigo, bool imprimirMarca, bool imprimirBorda, bool imprimirLote, bool imprimirNumeroSerie, string[] itens)
        {
            try
            {
                if (itens == null || itens.Length == 0)
                {
                    TempData["message"] = "Ao menos um item deve ser inserido";
                    return Redirect("/Etiquetas/Index");
                }
                string nomeEmpresa = null;
                try
                {
                    if(string.IsNullOrEmpty(empresa)){
                        TempData["message"] = "Erro: ID da empresa vazia ou nula.";
                        return Redirect("/Etiquetas/Index");
                    }
                    nomeEmpresa = _db._repositoryEmpresa.Collection.Find(x => x.Id == empresa)?.FirstOrDefault()?.NomeFantasia;
                    if (string.IsNullOrEmpty(nomeEmpresa))
                    {
                        TempData["message"] = "Erro: Empresa não encontrada";
                        return Redirect("/Etiquetas/Index");
                    }
                }
                catch
                {
                    TempData["message"] = "Erro: Falha ao realizar a busca no banco de dados pelo nome da empresa";
                    return Redirect("/Etiquetas/Index");
                }

                DtoEtiquetasPadroes modelEtiqueta = null;
                try
                {
                    if (string.IsNullOrEmpty(etiqueta))
                    {
                        TempData["message"] = "Erro: ID do modelo de etiqueta vazia ou nula.";
                        return Redirect("/Etiquetas/Index");
                    }
                    modelEtiqueta = _db._repositoryEtiquetasPadroes.Collection.Find(x => x.Id == etiqueta).FirstOrDefault();
                    if (modelEtiqueta == null)
                    {
                        TempData["message"] = "Erro: Empresa não encontrada";
                        return Redirect("/Etiquetas/Index");
                    }
                }
                catch
                {
                    TempData["message"] = "Erro: Falha ao realizar a busca no banco de dados pelo modelo de etiqueta";
                    return Redirect("/Etiquetas/Index");
                }
                List<ProdutoEscolhido> listaItens = new List<ProdutoEscolhido>();
                foreach(var item in itens){
                    //prod[0] == id / prod[1] == quantidade / prod[2] == lote / prod[3] == numeroSerie
                    string[] prod = item.Split(',');
                    var produdo = BsonSerializer.Deserialize<ProdutoEscolhido>(_db._repositoryProduto.Collection.Find(x => x.Id == prod[0]).Project(new BsonDocument { { "_id", true }, { "CodigoNFe", true }, { "Nome", true }, { "PrecoVenda", true }, { "Marca", true }, { "EAN_NFe", true } }).FirstOrDefault().ToJson());
                    if (produdo != null)
                    {
                        produdo.Quantidade = int.Parse(prod[1]);
                        if (!string.IsNullOrEmpty(prod[2]))
                            produdo.Lote = prod[2];
                        if (!string.IsNullOrEmpty(prod[3]))
                            produdo.NumeroSerie = prod[3];

                        listaItens.Add(produdo);
                    }
                }

                OpcoesSelecionadas opcoesSelecionadas = new OpcoesSelecionadas()
                {
                    DadoLadoCodigoBarras = dadoLadoCodigoBarras,
                    ImprimirBorda = imprimirBorda,
                    ImprimirCodigo = imprimirCodigo,
                    ImprimirCodigoBarras = imprimirCodigoBarras,
                    ImprimirLote = imprimirLote,
                    ImprimirMarca = imprimirMarca,
                    ImprimirNome = imprimirNome,
                    ImprimirNumeroCodigoBarras = imprimirNumeroCodigoBarras,
                    ImprimirNumeroSerie = imprimirNumeroSerie,
                    ImprimirPreco = imprimirPreco,
                    PrecoComoCodigo = precoComoCodigo
                };

                // Se for uma requisição AJAX, retorna apenas a view parcial
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("_EtiquetasContent", new EtiquetasContentViewModel { 
                        NomeEmpresa = nomeEmpresa,
                        ModelEtiqueta = modelEtiqueta,
                        OpcoesSelecionadas = opcoesSelecionadas,
                        ListaItens = listaItens
                    });
                }

                // Se não for AJAX, retorna a view completa como antes
                ViewBag.nomeEmpresa = nomeEmpresa;
                ViewBag.modelEtiqueta = modelEtiqueta;
                ViewBag.opcoesSelecionadas = opcoesSelecionadas;
                ViewBag.listaItens = listaItens;
                return View();
            }
            catch (Exception ex)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return BadRequest(ex.Message);
                }
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public string GetProduto(string deposito, string produto)
        {
            var produtoEscolhido = BsonSerializer.Deserialize<ProdutoEscolhido>(_db._repositoryProduto.Collection.Find(x => x.Id == produto).Project(new BsonDocument { 
                { "_id", true }, 
                { "CodigoNFe", true }, 
                { "Nome", true }, 
                { "PrecoVenda", true }, 
                { "Marca", true }, 
                { "NumeroSerie", true },
                { "EAN_NFe", true } 
            }).FirstOrDefault().ToJson());

            if (produtoEscolhido != null)
            {
                return "{" +
                    "\"Id\":\"" + produtoEscolhido.Id + "\"," +
                    "\"Codigo\":\"" + (produtoEscolhido.Codigo ?? "") + "\"," +
                    "\"CodigoBarras\":\"" + (produtoEscolhido.CodigoBarras ?? "") + "\"," +
                    //replace utilizado para tratar inserções no banco de dados de "
                    "\"Nome\":\"" + produtoEscolhido.Nome.Replace("\"","\\\"") + "\"," +
                    "\"PrecoVenda\":\"" + produtoEscolhido.Preco + "\"," +
                    "\"Marca\":\"" + (produtoEscolhido.Marca ?? "") + "\"," +
                    "\"NumeroSerie\":\"" + (produtoEscolhido.NumeroSerie ?? "") + "\"}";
            }
            return "Erro";
        }
    }
}
