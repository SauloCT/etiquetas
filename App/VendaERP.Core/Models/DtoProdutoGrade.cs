using System;
using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;


namespace VendaERP.Core.Models
{
    public class DtoProdutoGrade : Entity
    {

        public string Nome { get; set; }

        public List<AtributoGrade> Atributos { get; set; }

        public int MagentoID { get; set; }
    }

    public class AtributoGrade
    {
        public string AtributoId { get; set; }

        public string Descricao { get; set; }

        [BsonIgnore]
        public string[] ValoresPossiveis { get; set; }
    }

    [Serializable]
    public sealed class AtributoMovimentacao : ICloneable
    {
        public string AtributoId { get; set; }

        public string Descricao { get; set; }

        public string Abreviacao { get; set; }

        public string Valor { get; set; }

        //Foi comentado pois foi feito uma melhoria para poder editar as composições nas OPs, sem o valores não era possível puxar as informações ao adicionar um produto pai q tenha uma composição com grade
        //[BsonIgnore]
        public string[] ValoresPossiveis { get; set; }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public AtributoMovimentacao Clone()
        {
            var clone = new AtributoMovimentacao
            {
                AtributoId = AtributoId,
                Descricao = Descricao,
                Abreviacao = Abreviacao,
                Valor = Valor
            };

            if (ValoresPossiveis != null)
            {
                clone.ValoresPossiveis = new string[ValoresPossiveis.Length];
                for (var i = 0; i < ValoresPossiveis.Length; i++)
                {
                    clone.ValoresPossiveis[i] = ValoresPossiveis[i];
                }
            }

            return clone;
        }
    }
}