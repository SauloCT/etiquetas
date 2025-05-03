
using System;

namespace VendaERP.Core.Models
{
	[Serializable]
	public class DtoTamanho : Entity
	{

        public string Nome { get; set; }


        public double Largura { get; set; }


        public double Altura { get; set; }


        public double Profundidade { get; set; }


        public double Diametro { get; set; }

		public TipoObjeto TipoObjeto { get; set; }


        public bool VisivelSigep { get; set; }

		public bool VeioSigep { get; set; }

		public bool TemMesmasDimensoes(double largura, double altura, double profundidade)
		{
			return Largura == largura && Profundidade == profundidade && Altura == altura;
		}
	}

    public enum TipoObjeto
    {
        ENVELOPE = 1,
        CAIXAPACOTE = 2,
        ROLOCILINDRO = 3,
    }
}