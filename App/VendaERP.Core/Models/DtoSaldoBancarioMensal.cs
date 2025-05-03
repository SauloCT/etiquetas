using System;


namespace VendaERP.Core.Models
{
    [Serializable]
    public sealed class DtoSaldoBancarioMensal : Entity
    {
        public double Saldo { get; set; }

        public string BancoID { get; set; }

        public int Mes { get; set; }

        public int Ano { get; set; }

        public bool OutOfDate { get; set; }

        public double Conciliado { get; set; }

        public int Versao { get; set; }
    }
}