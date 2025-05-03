namespace VendaERP.Core.Models
{
    public sealed class DtoSequencialBoleto : Entity
    {

        //Versao 1: Implantação do Sequenciais
        //Versao 2: Inclusao de IgnoreOnMaxNum

        [IndexedColumn()]
        public string BancoID { get; set; }

        public long BoletoCodigo { get; set; }

        public int Versao { get; set; }
    }
}