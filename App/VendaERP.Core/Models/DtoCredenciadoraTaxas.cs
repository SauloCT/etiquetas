using System;
using BorlanV2.DAO.DTO.SIGE_Entidades;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoCredenciadoraTaxas
    {
        public string FormaDePagamentoID { get; set; }
        public string FormaDePagamento { get; set; }

        public string PlanoDeContaID { get; set; }
        public string PlanoDeConta { get; set; }

        public string BandeiraCartao { get; set; }
        public string BandeiraCartaoTextString
        {
            get
            {
                switch (this.BandeiraCartao)
                {
                    case "01":
                        {
                            return "Visa";
                        }
                    case "02":
                        {
                            return "Mastercard";
                        }
                    case "03":
                        {
                            return "American Express";
                        }
                    case "04":
                        {
                            return "Sorocred";
                        }
                    case "05":
                        {
                            return "Diners Club";
                        }
                    case "06":
                        {
                            return "Elo";
                        }
                    case "07":
                        {
                            return "Hipercard";
                        }
                    case "08":
                        {
                            return "Aura";
                        }
                    case "09":
                        {
                            return "Cabal";
                        }
                    default:
                        {
                            return "Outros";
                        }
                }
            }
        }

        public double Taxa { get; set; }
        public OpcaoDesconto OpcaoDesconto { get; set; }

        public IntervaloParcelamento Intervalo { get; set; }
        public int DiasIntervalo { get; set; }

        public int DiasQuitacao { get; set; }
        public bool QuitarAutomatico { get; set; }

        public DtoCredenciadoraTaxasParcelas[] Parcelas { get; set; }

    }

    public enum OpcaoDesconto
    {
        GerarDespesaVinculada = 1,
        DescontarVenda = 2,
    }
}
