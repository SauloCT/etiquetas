using App.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MongoDB.Driver;
using VendaERP.Core;
using VendaERP.Core.Models;
using RestSharp;
using System.Net;
using System.Web.WebPages.Html;
using static System.Formats.Asn1.AsnWriter;
using System.Runtime;
using System;

namespace App.Controllers
{
    public class IntegracoesController : Controller
    {
       
        
        private readonly DBAccess _db;
        private IntegracoesModel _model;
        public IntegracoesController(DBAccess db)
        {
            this._db = db;
            this._model = new IntegracoesModel();
        }
        public IActionResult Index()
        {
            return View(_model);
        }
        public IActionResult IFoodAuth(string? clientId, string? clientSecret)
        {
            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
            {
                GetNewTokenIFood(clientId, clientSecret);
                return Redirect("/Integracoes/IFoodList");
            }
            return View(_model);
        }

        private DtoIFoodAuthentication GetNewTokenIFood(string clientId, string clientSecret)
        {
            DtoIntegracoes integracao = _db._repositoryIntegracoes.Collection.Find<DtoIntegracoes>(x => x.NomeIntegracao == "iFood").Limit(1).FirstOrDefault();
            if (integracao == null)
            {
                _db._repositoryIntegracoes.Collection.InsertOne(new DtoIntegracoes { NomeIntegracao = "iFood" });
            }
            RestClient client = new RestClient("https://merchant-api.ifood.com.br/authentication/v1.0/");
            RestRequest request = new RestRequest("oauth/token", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grantType", "client_credentials");
            request.AddParameter("clientId", clientId);
            request.AddParameter("clientSecret", clientSecret);
            RestResponse response = client.Execute(request);
            DtoIntegracoesTempData integracaoTempData = new DtoIntegracoesTempData()
            {
                IntegracaoId = integracao != null ? integracao.Id : _db._repositoryIntegracoes.Collection.Find<DtoIntegracoes>(x => x.NomeIntegracao == "iFood").Limit(1).FirstOrDefault().Id,
                DataProcessamento = DateTime.Now,
                StatusCode = (int)response.StatusCode,
                Descricao = response.StatusDescription,
                Url = response.ResponseUri.ToString(),
                Request = JsonConvert.SerializeObject(request.Parameters),
                Retorno = string.IsNullOrEmpty(response.Content) ? null : response.Content
            };
            if (response == null)
            {
                throw new NotImplementedException();
            }
            DtoIFoodAuthentication authentication = null;
            if (response.IsSuccessStatusCode)
            {
                authentication = JsonConvert.DeserializeObject<DtoIFoodAuthentication>(response.Content);
                authentication.DataRequest = integracaoTempData.DataProcessamento;
                authentication.DataExpires = integracaoTempData.DataProcessamento.AddSeconds(authentication.ExpiresIn);
                authentication.ClientId = clientId;
                authentication.ClientSecret = clientSecret;
                _db._repositoryIFoodAuthentication.Collection.InsertOne(authentication);
                authentication = _db._repositoryIFoodAuthentication.Collection.Aggregate().Sort("{_id:-1}").Limit(1).FirstOrDefault();
                integracaoTempData.AuthenticationId = authentication.Id;
            }
            _db._repositoryIntegracoesTempData.Collection.InsertOne(integracaoTempData);
            return authentication;
        }

        public IActionResult IFoodList(int currentPage = 1, int pageSize = 10)
        {
            DtoIFoodAuthentication authentication = _db._repositoryIFoodAuthentication.Collection.Aggregate().Sort("{_id:-1}").Limit(1).FirstOrDefault();
            if (authentication == null)
            {
                return Redirect("/Integracoes/IFoodAuth");
            }
            if (DateTime.Now >= authentication.DataExpires)
            {
                authentication = GetNewTokenIFood(authentication.ClientId, authentication.ClientSecret);
            }
            RestClient client = new RestClient("https://merchant-api.ifood.com.br/");
            RestRequest request = new RestRequest("order/v1.0/events:polling", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", authentication.Type + " " + authentication.AccessToken);
            RestResponse response = client.Execute(request);
            string idIntegracao = _db._repositoryIntegracoes.Collection.Find(x => x.NomeIntegracao == "iFood").FirstOrDefault().Id;
            DtoIntegracoesTempData integracaoTempData = new DtoIntegracoesTempData()
            {
                AuthenticationId = authentication.Id,
                DataProcessamento = DateTime.Now,
                StatusCode = (int)response.StatusCode,
                Descricao = response.StatusDescription,
                Url = response.ResponseUri.ToString(),
                Request = JsonConvert.SerializeObject(request.Parameters),
                Retorno = string.IsNullOrEmpty(response.Content) ? null : response.Content,
                IntegracaoId = idIntegracao
            };
            _db._repositoryIntegracoesTempData.Collection.InsertOne(integracaoTempData);
            // Remover depois caso o problema com o horario n se repita
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                authentication = GetNewTokenIFood(authentication.ClientId, authentication.ClientSecret);
                request.AddOrUpdateHeader("Authorization", authentication.Type + " " + authentication.AccessToken);
                response = client.Execute(request);
                integracaoTempData = new DtoIntegracoesTempData()
                {
                    AuthenticationId = authentication.Id,
                    DataProcessamento = DateTime.Now,
                    StatusCode = (int)response.StatusCode,
                    Descricao = response.StatusDescription,
                    Url = response.ResponseUri.ToString(),
                    Request = JsonConvert.SerializeObject(request.Parameters),
                    Retorno = string.IsNullOrEmpty(response.Content) ? null : response.Content,
                    IntegracaoId = idIntegracao
                };
                _db._repositoryIntegracoesTempData.Collection.InsertOne(integracaoTempData);
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<IFoodStatusVenda> novosEventos = JsonConvert.DeserializeObject<List<IFoodStatusVenda>>(response.Content);
                DtoIFoodVenda venda = null;
                string eventosRegistrados = "[";
                foreach (var evento in novosEventos)
                {
                    if (eventosRegistrados == "[")
                        eventosRegistrados += "{\"id\":\""+evento.Id+"\"}";
                    else
                        eventosRegistrados += ",{\"id\":\""+evento.Id+"\"}";
                    if (evento.Code == "PLC")
                    {
                        request = new RestRequest("order/v1.0/orders/" + evento.OrderId, Method.Get);
                        request.AddOrUpdateHeader("Authorization", authentication.Type + " " + authentication.AccessToken);
                        response = client.Execute(request);
                        integracaoTempData = new DtoIntegracoesTempData()
                        {
                            AuthenticationId = authentication.Id,
                            DataProcessamento = DateTime.Now,
                            StatusCode = (int)response.StatusCode,
                            Descricao = response.StatusDescription,
                            Url = response.ResponseUri.ToString(),
                            Request = JsonConvert.SerializeObject(request.Parameters),
                            Retorno = string.IsNullOrEmpty(response.Content) ? null : response.Content,
                            IntegracaoId = idIntegracao
                        };
                        _db._repositoryIntegracoesTempData.Collection.InsertOne(integracaoTempData);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            venda = JsonConvert.DeserializeObject<DtoIFoodVenda>(response.Content);
                            DtoPessoa cliente = _db._repositoryPessoa.Collection.Find(x => (x.RazaoSocial == venda.Customer.Name || x.NomeFantasia == venda.Customer.Name ) && x.Telefone == venda.Customer.Phone.Number).FirstOrDefault();
                            if(cliente == null)
                            {
                                var novoCliente = new DtoPessoa()
                                {
                                    LastUpdate = DateTime.Now,
                                    RazaoSocial = venda.Customer.Name,
                                    NomeFantasia = venda.Customer.Name,
                                    Telefone = venda.Customer.Phone.Number,
                                };
                                _db._repositoryPessoa.Collection.InsertOne(novoCliente);
                                cliente = _db._repositoryPessoa.Collection.Find(x => (x.RazaoSocial == venda.Customer.Name || x.NomeFantasia == venda.Customer.Name) && x.Telefone == venda.Customer.Phone.Number).FirstOrDefault();
                            }
                            DtoVendaCategoria categoria = _db._repositoryVendaCategoria.Collection.Find(x => x.Nome == "Consumidor Final").FirstOrDefault();
                            DtoFormaPagamento formaPagamento = _db._repositoryFormaPagamento.Collection.Find(x => x.Nome == "iFood - "+venda.Payments.Methods[0].Method+" - " + venda.Payments.Methods[0].Type).FirstOrDefault();
                            if(formaPagamento == null)
                            {
                                _db._repositoryFormaPagamento.Collection.InsertOne(new DtoFormaPagamento()
                                {
                                    Nome = "iFood - " + venda.Payments.Methods[0].Method + " - " + venda.Payments.Methods[0].Type,
                                });
                                formaPagamento = _db._repositoryFormaPagamento.Collection.Find(x => x.Nome == "iFood - "+venda.Payments.Methods[0].Method+" - " + venda.Payments.Methods[0].Type).FirstOrDefault();
                            }
                            DtoEmpresa empresa = _db._repositoryEmpresa.Collection.Find(x => x.RazaoSocial == venda.Merchant.Name || x.NomeFantasia == venda.Merchant.Name).FirstOrDefault();
                            if(empresa == null)
                            {
                                _db._repositoryEmpresa.Collection.InsertOne(new DtoEmpresa()
                                {
                                    NomeFantasia = venda.Merchant.Name,
                                    RazaoSocial = venda.Merchant.Name,
                                });
                                empresa = _db._repositoryEmpresa.Collection.Find(x => x.RazaoSocial == venda.Merchant.Name || x.NomeFantasia == venda.Merchant.Name).FirstOrDefault();
                            }
                            DtoVenda vendaERP = new DtoVenda()
                            {
                                LastUpdate = DateTime.Now,
                                Data = DateTime.Now,
                                Status = StatusVenda.Pedido,
                                ValorFinal = venda.Total.OrderAmount,
                                ValorTroco = 0,
                                Alteracao = DateTime.Now,
                                Descricao = "Venda gerada pelo iFood",
                                Empresa = empresa.NomeFantasia,
                                EmpresaId = empresa.Id,
                                VendaCategoria = categoria.Nome,
                                VendaCategoriaID = categoria.Id,
                                Cliente = cliente.NomeFantasia,
                                ClienteId = cliente.Id,
                                Validade = DateTime.Now,
                                AlteradoPor = OrigemVendaStr.iFood,
                                OrigemVenda = OrigemVendaStr.iFood,
                                ValorFrete = venda.Total.DeliveryFee,
                                FormadePagamento = formaPagamento.Nome,
                                FormadePagamentoID = formaPagamento.Id,
                                CEP = venda.Delivery.DeliveryAddress.PostalCode,
                                Pais = venda.Delivery.DeliveryAddress.Country,
                                UF = venda.Delivery.DeliveryAddress.State,
                                Municipio = venda.Delivery.DeliveryAddress.City,
                                Bairro = venda.Delivery.DeliveryAddress.StreetName,
                                Logradouro = venda.Delivery.DeliveryAddress.StreetName,
                                LogradouroNumero = venda.Delivery.DeliveryAddress.StreetNumber,
                                LogradouroComplemento = venda.Delivery.DeliveryAddress.Complement,
                                FreteMeioEnvio = MeioEnvio.Outros,
                                FreteFormaEnvio = venda.Delivery.Mode,

                            };
                            _db._repositoryVenda.Collection.InsertOne(vendaERP);
                            vendaERP = _db._repositoryVenda.Collection.Find(x => x.Data == vendaERP.Data && x.Cliente == vendaERP.Cliente && x.Empresa == vendaERP.Empresa).FirstOrDefault();
                            if(vendaERP != null)
                                venda.VendaId = vendaERP.Id;
                            DtoLancamento lancamento = new DtoLancamento()
                            {
                                LastUpdate = DateTime.Now,
                                VendaID = vendaERP.Id,
                                Entrada = venda.Total.OrderAmount,
                                Cliente = cliente.NomeFantasia,
                                ClienteID = cliente.Id,
                                DataFluxo = DateTime.Now,
                                Empresa = empresa.NomeFantasia,
                                EmpresaID = empresa.Id,
                                Pago = false,
                            };
                            _db._repositoryLancamento.Collection.InsertOne(lancamento);
                        }
                    }
                    else
                    {
                        venda = _db._repositoryIFoodVenda.Collection.Find(x => x.VendaIFoodId == evento.OrderId).FirstOrDefault();
                        if (evento.Code == "CON")
                        {
                            DtoVenda vendaERP = _db._repositoryVenda.Collection.Find(x => x.Id == venda.VendaId).FirstOrDefault();
                            if(vendaERP != null)
                            {
                                vendaERP.Status = StatusVenda.PedidoFaturado;
                                _db._repositoryVenda.Collection.ReplaceOne(x => x.Id == vendaERP.Id, vendaERP);
                            }
                            DtoLancamento lancamento = _db._repositoryLancamento.Collection.Find(x => x.VendaID == venda.VendaId).FirstOrDefault();
                            if(lancamento != null)
                            {
                                lancamento.ValorPago = lancamento.Entrada;
                                lancamento.Pago = true;
                                _db._repositoryLancamento.Collection.ReplaceOne(x => x.Id == lancamento.Id, lancamento);
                            }
                        }else if (evento.Code == "CAN")
                        {
                            DtoVenda vendaERP = _db._repositoryVenda.Collection.Find(x => x.Id == venda.VendaId).FirstOrDefault();
                            vendaERP.Status = StatusVenda.PedidoCancelado;
                            _db._repositoryVenda.Collection.ReplaceOne(x => x.Id == vendaERP.Id, vendaERP);
                        }
                    }

                    if(venda.Status == null)
                        venda.Status = new List<IFoodStatusVenda>();

                    venda.Status.Add(evento);

                    if(venda.Id == null)
                        _db._repositoryIFoodVenda.Collection.InsertOne(venda);
                    else
                        _db._repositoryIFoodVenda.Collection.ReplaceOne(x=>x.Id == venda.Id, venda);
                }
                eventosRegistrados += "]";
                //Confirmação de captura dos eventos do iFood
                request = new RestRequest("order/v1.0/events/acknowledgment", Method.Post);
                request.AddOrUpdateHeader("Authorization", authentication.Type + " " + authentication.AccessToken);
                request.AddJsonBody(eventosRegistrados);
                response = client.Execute(request);
                integracaoTempData = new DtoIntegracoesTempData()
                {
                    AuthenticationId = authentication.Id,
                    DataProcessamento = DateTime.Now,
                    StatusCode = (int)response.StatusCode,
                    Descricao = response.StatusDescription,
                    Url = response.ResponseUri.ToString(),
                    Request = JsonConvert.SerializeObject(request.Parameters),
                    Retorno = string.IsNullOrEmpty(response.Content) ? null : response.Content,
                    IntegracaoId = idIntegracao
                };
                _db._repositoryIntegracoesTempData.Collection.InsertOne(integracaoTempData);
            }
            _model.Lista = _db._repositoryIFoodVenda.Collection.Aggregate().Sort("{ _id : -1}").ToList();
            if(currentPage < 1)
                currentPage = 1;
            var pager = new Pager(_model.Lista.Count(), currentPage, pageSize);
            this.ViewBag.pager = pager;
            _model.Lista = _model.Lista.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            return View(_model);
        }
    }
}
