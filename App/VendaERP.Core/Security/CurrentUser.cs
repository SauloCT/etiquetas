using Microsoft.AspNetCore.Identity;

namespace Template.VendaERP.Core.Security
{
    public class CurrentUser : IdentityUser
    {
        public string GerenId { get; private set; } = string.Empty;
        public string UserId { get; private set; } = string.Empty;
        public string ServerRegion { get; private set; } = string.Empty;
        public string Role { get; private set; } = string.Empty;
        public List<UserEmpresa> EmpresasUser { get; private set; } = new();
    }

    public class UserEmpresa
    {
        public bool Padrao { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        public UserEmpresa()
        {
        }
    }
}
