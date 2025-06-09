namespace TH01.Models.Interface
{
    public interface IContactRepository
    {
        Task<bool> SendContactMessageAsync(Contact model);
    }
}
