using TH01.Data;
using TH01.Models.Interface;
namespace TH01.Models.Services
{
    public class ContactRepository:IContactRepository
    {
        private readonly CoffeeshopDbContext _context;

        public ContactRepository(CoffeeshopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SendContactMessageAsync(Contact model)
        {
            try
            {
                var contactMessage = new Contact
                {
                    Name = model.Name,
                    Email = model.Email,
                    Message = model.Message,
                    SentDate = DateTime.Now
                };

                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
