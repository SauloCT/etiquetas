using System;

namespace BorlanV2.DAO.DTO.SIGE_Entidades
{
    [Serializable]
    public class DtoCredenciadoraTaxasParcelas
    {
        public double Valor { get; set; }
        public int Parcela { get; set; }
        public string PeriodoRecebimento { get; set; }
    }
}
