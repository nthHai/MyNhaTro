using Microsoft.AspNetCore.Identity;
using MyNhaTro.Models;

namespace MyNhaTro.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
