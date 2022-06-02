using BooksApi.DTO;
using BooksApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BooksApi.Repos
{
    public interface IAccountRepo
    {
        Task<IdentityResult> SignupAsync(SignupModel signupModel);
    }

    //=======================================
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepo(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> SignupAsync(SignupModel signupModel)
        {
            var user = new ApplicationUser {
                FirstName = signupModel.FirstName,
                LastName = signupModel.LastName,
                Email = signupModel.Email,
                UserName = signupModel.Email
            };

            return await _userManager.CreateAsync(user, signupModel.Password);

        }
    }

    
}
