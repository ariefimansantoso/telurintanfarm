using QuickAccounting.Data.Recording;

namespace QuickAccounting.Repository.Interface
{
    public interface IKandangService
    {
        Task<List<Kandang>> GetAll();
    }
}