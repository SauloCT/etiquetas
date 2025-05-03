using Microsoft.AspNetCore.Identity;

namespace Template.VendaERP.Core.Security
{
    public class CurrentUser : IdentityUser
    {
        public string GerenId { get; private set; }
        public string UserId { get; private set; }
        public string Email { get; private set; }
        public string ServerRegion { get; private set; }
        public string Role { get; private set; }
        public List<UserEmpresa> EmpresasUser { get; private set; }
    }

    public class UserEmpresa
    {
        public bool Padrao { get; set; }
        public string Nome { get; set; }
        public string Id { get; set; }

        public UserEmpresa()
        {
        }
    }
}
