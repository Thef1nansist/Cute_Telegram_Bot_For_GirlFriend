using BotTelega.Models;
using Microsoft.EntityFrameworkCore;


namespace BotTelega
{
    class Context :DbContext
    {
        public DbSet<Model> Cute { get; set; }

        public DbSet<ImageModel> imageModels { get; set; }

        public DbSet<UsersImageModel> userPicture { get; set; }
    
        public DbSet<AudioModel> audioModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Cute.db");
        }
    }
}
