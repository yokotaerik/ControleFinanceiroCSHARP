using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ControleFinanceiro.Domain.Enums.User;
using Microsoft.AspNetCore.Identity;

namespace ControleFinanceiro.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public User() : base()
        {
        }

        public User(string userName, string email) : base()
        {
            UserName = userName;
            Email = email;
        }

        // Relacionamentos
        public virtual Collection<Account> Accounts { get; set; } = new Collection<Account>();
        public virtual Collection<Goal> Goals { get; set; } = new Collection<Goal>();
    }
}
