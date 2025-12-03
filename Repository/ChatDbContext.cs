using ChatApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Repository
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options) { }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ChatMessages テーブルのマッピング
            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.ToTable("chatmessages"); // PostgreSQL テーブル名は小文字

                entity.HasKey(e => e.Id); // 主キー
                entity.Property(e => e.Id).HasColumnName("id"); // 小文字カラム名にマッピング
                entity.Property(e => e.UserMessage).HasColumnName("usermessage");
                entity.Property(e => e.BotReply).HasColumnName("botreply");
                entity.Property(e => e.CreatedDate).HasColumnName("createdat");
            });
        }
    }
}
