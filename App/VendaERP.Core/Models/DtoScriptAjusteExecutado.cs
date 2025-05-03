
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaERP.Core.Models
{
    [Serializable]
    public class DtoScriptAjusteExecutado : Entity
    {
        public DateTime Data { get; set; }

        public string DataBaseId { get; set; }

        public string ScriptId { get; set; }

    }
}
