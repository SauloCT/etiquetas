using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;




namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoLogsErro : Entity
    {
        public Guid LogID { get; set; }

        public DateTime Data { get; set; }

        public string Exception { get; set; }

        public string VeioDe { get; set; }

        public string DeOnde { get; set; }

        public string UserName { get; set; }

        public string Browser { get; set; }


    }
}