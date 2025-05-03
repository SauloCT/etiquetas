using System;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoIntegracoesTempData : Entity
    {
        public string IntegracaoId { get; set; }
        public string AuthenticationId { get; set; }
        public DateTime DataProcessamento { get; set; }
        public string Url { get; set; }
        public int StatusCode { get; set; }
        public string Descricao { get; set; }
        public string Request { get; set; }
        public string Retorno { get; set; }
    }
}