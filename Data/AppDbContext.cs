using Microsoft.EntityFrameworkCore; // DbContext
using api.Models; // DbContext

namespace api.Data
{
    // Contexto de dados da aplicação. Usamos para alinhar as classe com o banco de dados, EX: Classe X se refere a tabela Y.
    public class AppDbContext : DbContext
    {

        //Se nosso AppDbContext representa o DB em memória, o DbSet é a representação da tabela. 
        //Para o EF saber que de fato queremos mapear tal classe pro banco,devemos criar o DbSet.
        public DbSet<Todo> todos {get; set;}

        // Sobrescrevemos o método OnConfiguring, pois ele oferece um DbContextOptionsBuilder, onde podemos ultilzir ele para setar a connection string.
        // Vale ressaltar que a forma mais adequada de setar a connection string é usando appsettings.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(connectionString:"DataSource=app.db;Cache=Shared");
    }
}