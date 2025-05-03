using System;


namespace VendaERP.Core.Models
{
    public sealed class DtoSequencialMDFe : EntityLastUpdate
    {
        /// <summary>
        /// Versão 1 - Implementado para iniciar o controle do número do Lote MDFe e o Numero do MDFe
        /// </summary>
        [NonSerialized]
        public const int VersaoAtual = 1;

        [IndexedColumn("Cnpj", "Serie")]
        public string Cnpj { get; set; }
        public string Serie { get; set; }
        public int NumeroMDFe { get; set; }
        public int LoteMDFe { get; set; }
        public int Versao { get; set; }
    }
}
