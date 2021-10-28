using ProductsCore.Models;
using System.Threading.Tasks;

namespace ProductsDataLayer.Repositories.EmailRepository
{
    public interface IEmailRepository
    {
        Task<int> RegisterEmailAsync(Email email);
        Task<string> GetConfirmMessageAsync(string email);
        Task ConfirmEmailAsync(string email);
    }
}