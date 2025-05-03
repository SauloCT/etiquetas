


using System;
using System.Collections.Generic;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoBanco : EntityLastUpdate
    {
        public DtoBanco()
        {
            this.NumeroBanco = "0";
        }

        [KeyAttribute("Banco")]

        public string Nome { get; set; }

        public string EmpresaID { get; set; }


        public string Empresa { get; set; }


        public string Agencia { get; set; }

        public string Numero { get; set; }

        public string DigitoConta { get; set; }

        public string DigitoAgencia { get; set; }


        public string NumeroBanco { get; set; }

        public string NumeroConvenio { get; set; }


        public bool EmiteBoleto { get; set; }

        public bool EmiteBoletoRecebimento { get; set; }

        public bool CodigoBeneficiarioEhAgenciaConta { get; set; }

        public double Saldo { get; set; }

        public string Posto_CodigoOperacao { get; set; }

        public string CodigoTransmissao { get; set; }

        public string Inscricao { get; set; } //Criado especialmente para uniprime

        public string NumeroCliente { get; set; }

        public int ByteNossoNumero { get; set; }

        public List<CarteiraConta> Carteiras { get; set; }

        public long NumeroBoletoInicial { get; set; } // Prestar atenção no maxlength de um Int64

        public bool ProtestaBoleto { get; set; }

        public int ProtestoDiasPadrao { get; set; }

        public ModoContagem ModoContagemProtestoPadrao { get; set; }

        public bool DevolveBoleto { get; set; }

        public int DevolucaoDiasPadrao { get; set; }

        public ModoContagem ModoContagemDevolucaoPadrao { get; set; }

        public string InstrucaoPagamentoAposVencimento { get; set; }

        public bool DifereNumeroContaDeCedente { get; set; }

        public int NumeroCedente { get; set; }

        public int DigitoCedente { get; set; }

        public string InstrucaoAdicional { get; set; }

        public double? Juros { get; set; }

        public double? Multa { get; set; }

        public double? DescontoAntecipacao { get; set; }

        public bool IncluirContaFIDCnaRemessa { get; set; }
    }

    public enum ModalidadeEmissao
    {
        ClienteNumeraEmite = 0,
        BancoNumeraEmite = 1,
        BancoNumeraClienteEmite = 2
    }

    [Serializable]
    public class Entidade
    {
        public string Nome { get; set; }
        public string Numero { get; set; }
        public bool PodeEmitirBoleto { get; set; }
        public bool PodeEnviarBoleto { get; set; }
        public bool PodeProtestar { get; set; }
        public int ModoContagemProtestoPadrao { get; set; }
        public bool PodeMudarModoContagemProtesto { get; set; }
        public bool PodeDevolver { get; set; }
        public int ModoContagemDevolucaoPadrao { get; set; }
        public bool PodeMudarModoContagemDevolucao { get; set; }
        public string InstrucaoPagamentoAposVencimento { get; set; }
        public string InstrucaoAdicional { get; set; }
    }

    public class Carteira
    {
        public string NumeroBanco { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public bool ComRegistro { get; set; }
        public bool PodeProtestar { get; set; }
        public bool PodeDevolver { get; set; }
        public ModalidadeEmissao Modalidade { get; set; }
    }

    [Serializable]
    public class CarteiraConta
    {
        public string CodigoCarteira { get; set; }
        public string NomeCarteira { get; set; }
        public string NumeroConvenio { get; set; }
        public double ValorTaxa { get; set; }
        public bool SubtrairTaxa { get; set; }
        public bool ComRegistro { get; set; }
        public bool Padrao { get; set; }
        public bool SomarTaxa { get; set; }
    }
}