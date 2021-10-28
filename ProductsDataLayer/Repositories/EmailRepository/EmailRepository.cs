using Microsoft.EntityFrameworkCore;
using ProductsCore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsDataLayer.Repositories.EmailRepository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly EFCoreContext _dbContext;

        public EmailRepository(EFCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ConfirmEmailAsync(string email)
        {
            var emailEntity = await GetEntityByEmail(email)
                .FirstOrDefaultAsync();

            emailEntity.IsConfirmed = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetConfirmMessageAsync(string email)
            => await GetEntityByEmail(email)
                .Select(x => x.ConfirmationString)
                .FirstOrDefaultAsync();

        public async Task<int> RegisterEmailAsync(Email email)
        {
            _dbContext.Emails.Add(email);
            await _dbContext.SaveChangesAsync();

            return email.Id;
        }

        private IQueryable<Email> GetEntityByEmail(string email)
            => _dbContext.Emails.Where(x => x.PostName == email);
    }
}
