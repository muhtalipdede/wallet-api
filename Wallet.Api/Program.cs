using Microsoft.EntityFrameworkCore;
using Wallet.Business;
using Wallet.Data;
using Wallet.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WalletDbContext>(options => options.UseInMemoryDatabase("WalletDb"));
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<IWalletTransactionRepository, WalletTransactionRepository>();
builder.Services.AddScoped<IWalletTransactionService, WalletTransactionService>();
builder.Services.AddScoped<IWalletService, WalletService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
