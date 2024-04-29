
using socketAPI.Hubs;

namespace socketAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSignalR();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PolicyCors",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("PolicyCors");
            app.UseAuthorization();

            app.MapHub<ChatHub>("/chatHub");
            app.MapControllers();

            app.Run();
        }
    }
}
