using ChatApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Repository
{
    public class ChatRepository
    {
        private readonly ChatDbContext _context;

        public ChatRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatMessage>> GetAllAsync()
        {
            try
            {
                return await _context.ChatMessages
                             .OrderBy(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}
