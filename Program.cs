using ChatApp.Repository;
using ChatApp.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =============================
// 🔹 1. CORS（フロント通信許可）
// =============================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// =============================
// 🔹 2. Controller を追加
// =============================
builder.Services.AddControllers();

// =============================
// 🔹 3. PostgreSQLへの接続設定
// =============================
var connectionString = builder.Configuration.GetConnectionString("ChatDb");
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseNpgsql(connectionString));

// =============================
// 🔹 4. Logic / Repository の登録
// =============================
builder.Services.AddScoped<ChatRepository>();
builder.Services.AddScoped<ChatLogic>();

// =============================
// 🔹 5. HTTPクライアント登録
// =============================
builder.Services.AddHttpClient();

// =============================
// 🔹 6. アプリ設定
// =============================
var app = builder.Build();

app.UseCors("AllowAll");

// Swagger関連は削除済み
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();