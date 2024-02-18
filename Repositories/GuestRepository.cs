using Microsoft.EntityFrameworkCore;
using TimeZone.Entities;

namespace TimeZone.Repositories
{
    public class GuestRepository
    {
        private readonly TimeZoneContext _context;

        public GuestRepository(TimeZoneContext context)
        {
            _context = context;
        }

        public async Task<List<Guest>> GetAll()
        {
            return await _context.Guests.ToListAsync();
        }

        public async Task<Guest> Get(Guid id)
        {
            return await _context.Guests.FindAsync(id);
        }

        public async Task<Guest> Create(Guest guest)
        {
            guest.Id = Guid.NewGuid();
            _context.Add(guest);
            await _context.SaveChangesAsync();
            return guest;
        }

        public async Task<Guest> Update(Guest guest)
        {
            _context.Update(guest);
            await _context.SaveChangesAsync();
            return guest;
        }

        public async Task Delete(Guid id)
        {
            var guest = await _context.Guests.FindAsync(id);
            _context.Remove(guest);
            await _context.SaveChangesAsync();
        }
    }
}
