using socketChat.Hubs;

namespace socketChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
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

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("PolicyCors");
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapHub<ChatHub>("/chatHub");


            app.Run();
        }
    }
}
