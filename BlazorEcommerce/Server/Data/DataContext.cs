using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Title = "The Hitchhiker's Guide to the Galaxy",
                Description = "The broad narrative of Hitchhiker follows the misadventures of the last surviving man, Arthur Dent, following the demolition of the Earth by a Vogon constructor fleet to make way for a hyperspace bypass. Dent is rescued from Earth's destruction by Ford Prefect—a human-like alien writer for the eccentric, electronic travel guide The Hitchhiker's Guide to the Galaxy—by hitchhiking onto a passing Vogon spacecraft. ",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg",
                Price = 9.99m
            },
            new Product
            {
                Id = 2,
                Title = "Dune",
                Description = "Dune is a 1965 epic science fiction novel by American author Frank Herbert, originally published as two separate serials in Analog magazine. It tied with Roger Zelazny's This Immortal for the Hugo Award in 1966 and it won the inaugural Nebula Award for Best Novel. It is the first installment of the Dune saga. In 2003, it was described as the world's best-selling science fiction novel.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/d/de/Dune-Frank_Herbert_%281965%29_First_edition.jpg/220px-Dune-Frank_Herbert_%281965%29_First_edition.jpg",
                Price = 12.99m
            },
            new Product
            {
                Id = 3,
                Title = "Ready Player One",
                Description = "Ready Player One is a 2011 science fiction novel, and the debut novel of American author Ernest Cline. The story, set in a dystopia in 2045, follows protagonist Wade Watts on his search for an Easter egg in a worldwide virtual reality game, the discovery of which would lead him to inherit the game creator's fortune.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/a/a4/Ready_Player_One_cover.jpg/220px-Ready_Player_One_cover.jpg",
                Price = 14.99m
            },
            new Product
            {
                Id = 4,
                Title = "The Call of Cthulhu",
                Description = "The story's narrator, Francis Wayland Thurston, recounts his discovery of various notes left behind by his great uncle, George Gammell Angell, a prominent professor of Semitic languages at Brown University in Providence, Rhode Island, who died during the winter of 1926, suspecting eldritch goings-on after being bumped into by a nautical-looking negro.",
                ImageUrl = "https://s2.adlibris.com/images/6601164/the-call-of-cthulhu.jpg",
                Price = 6.99m
            });
        }
        public DbSet<Product> Products { get; set; }
    }
}
