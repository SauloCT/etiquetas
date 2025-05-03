using System;
using System.Collections.Generic;


using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoOportunidade : Entity
    {
        
        [AutoIncrement]

        public int Codigo { get; set; }

        
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime DataAbertura { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime DataPrevFechamento { get; set; }

        public string ResponsavelID { get; set; }

        

        public string Responsavel { get; set; }

        

        public string Origem { get; set; }

       

        public string Fase { get; set; }

        public string VendaCategoriaID { get; set; }

        

        public string VendaCategoria { get; set; }

        public string ClienteID { get; set; }

       

        public string Cliente { get; set; }

        public string Contato { get; set; }

        

        public string TelefoneContato { get; set; }

        

        public string EmailContato { get; set; }


        public DateTime DataUltimoEvento { get; set; }

        

        public string EventosPendentes { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DataFechamento { get; set; } // Data de finalização

        public double Probabilidade { get; set; }

        public double ValorNegocio { get; set; }

        public string Descricao { get; set; }

        public string CampanhaID { get; set; }

        public string Campanha { get; set; }

        public string EmpresaID { get; set; }

        public string Empresa { get; set; }

        public bool NegociacaoConcluida { get; set; }

        public bool NegociacaoCancelada { get; set; }

        public string MotivosCancelamento { get; set; }

        public string IndicacaoResponsavelID { get; set; }

        public string IndicacaoResponsavel { get; set; }

        public string ParceiroID { get; set; }

        public string Parceiro { get; set; }

        public string Site { get; set; }

        public string Loja { get; set; }

        public string PontoVenda { get; set; }

        public string Flayer { get; set; }

        public string Radio { get; set; }

        public string Jornal { get; set; }

        public string Outros { get; set; }

        [BsonIgnore]
        public string LinkVenda { get; set; }

        public string StatusID { get; set; }

        public int StatusOrdem { get; set; }

        public List<DtoOportunidadeObservacoes> Observacoes { get; set; }
    }

    [Serializable]
    public class DtoOportunidadeObservacoes
    {
        public int Indice { get; set; }
        public string Descricao { get; set; }
        public bool Encerrado { get; set; }

        //deixei esses caras aqui por que senão da erro nos relatórios de oportunidades no teste@teste.com
        public string TipoObservacaoID { get; set; }
        public string Tipo { get; set; }
        /////////////////////////////////////////////

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Lembrete { get; set; }
    }
}


