using System;

using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoAgenda : Entity
    {

        public string Responsavel { get; set; }

        public string ResponsavelID { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]

        public DateTime Data { get; set; }

        [BsonIgnore]
        public string Horastr { get; set; }


        public TimeSpan Hora { get; set; }


        public string Titulo { get; set; }


        public string Descricao { get; set; }


        public bool Tarefa { get; set; }


        public bool Concluido { get; set; }


        public bool Lembrete { get; set; }


        public string LembreteTipo { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LembreteCondicao { get; set; }


        public int OportunidadeCodigo { get; set; }

        public string GoogleEventID { get; set; }

        public string GoogleEventoPaiID { get; set; }


        public string Cliente { get; set; }

        public string ClienteID { get; set; }

        public string OrdemdeServicoID { get; set; }

        public string OrdemdeServicoAvulsaID { get; set; }


        public string TipoCompromisso { get; set; }

        public string AgendamentoRecorrenciaID { get; set; }

        public string FrotaLembreteID { get; set; }

        public string CobrancaClienteID { get; set; }

        #region Auxiliares NÃO SALVOS EM BANCO referentes a necessidades com o angularJS

        [BsonIgnore]
        public DtoAgenda[] _ParcelasRecorrencias { get; set; }

        [BsonIgnore]
        public ModoRecorrencia ModoRecorrencia { get; set; }

        [BsonIgnore]
        public IntervaloRecorrencia Intervalo { get; set; }

        [BsonIgnore]
        public int DiasIntervalo { get; set; }

        [BsonIgnore]
        public int NumeroRecorrencias { get; set; }

        #endregion
    }
    public enum ModoRecorrencia
    {
        NaoGerar = 0,
        RecorrenciaAut = 1,
        RecorrenciaMan = 2,
    }

    public enum IntervaloRecorrencia
    {
        Meses = 1,
        Dias = 2,
    }
}