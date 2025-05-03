using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Xml.Linq;
using VendaERP.Core;

namespace VendaERP.Core
{
    /*PREZADO PARCEIRO, NÃO ALTERE NADA NESTA CLASSE. ELA SERA SUBSTITUIDA AUTOMATICAMENTE EM NOSSOS SERVIDORES*/

    /* VOCE DEVE ALTERAR AS INFORMAÇÕES DE CONEXÃO COM O BANCO DE DADOS SOMENTE NO ARQUIVO appsettings.json
     
        OS DETALHES DA CONEXÃO VOCÊ IRÁ CAPTURAR DENTRO DO PAINEL DE PARCEIRO, UTILIZE UMA CONTA TESTE PARA PROGRAMAR

    "MongoConnection": {
    "DbHost": "ESSE E O HOST, ENDEREÇO DO BANCO DE DADOS",
    "DBUser": "NOME DE USUARIO QUE VOCE INFORMOU NO PAINEL",
    "DBPassword": "SENHA QUE VOCE INFORMOU NO PAINEL",
    "DBName": "NOME DO BANCO DE DADOS DE TESTE"
  },

        
    */

    public class DBSettings
    {   
        public string DbHost { get; set; } = null!;

        public string DBUser { get; set; } = null!;

        public string DBPassword { get; set; } = null!;

        public string DBName { get; set; } = null!;
    }
}