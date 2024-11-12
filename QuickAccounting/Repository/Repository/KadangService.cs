using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.Recording;
using QuickAccounting.Repository.Interface;

namespace QuickAccounting.Repository.Repository
{
    public class KandangService : IKandangService
    {
        private readonly ApplicationDbContext _context;

        public KandangService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Kandang>> GetAll()
        {
            return await _context.Kandang.ToListAsync();
        }
    }
}
