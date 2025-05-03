using System;


namespace VendaERP.Core.Models
{
    public sealed class DtoSequencialNota : Entity
    {
        /// <summary>
        /// Versão 1 - Incrementado para iniciar o controle do número do Lote NFe
        /// Versão 2 - Incrementado controle das numerações por número da série da Nota
        /// Versão 3 - Incrementado sequencial para RPS
        /// </summary>
        [NonSerialized]
        public const int VersaoAtual = 3; 

        [IndexedColumn("Cnpj", "Serie")]
        public string Cnpj { get; set; }

        public string Serie { get; set; }

        public int NumeroNfe { get; set; }
        public int NumeroNfce { get; set; }
        public int LoteNfce { get; set; }
        public int NumeroRPS { get; set; }
        //public int LoteNfe { get; set; }  Controle não será por empresa

        // Isso é pro caso de dar algum problema e eu quiser reiniciar o sequencial.
        public int Versao { get; set; }
    }
}