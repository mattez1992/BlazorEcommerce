using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Server.DataConfig
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Books",
                    Url = "books"
                }
                , new Category
                {
                    Id = 2,
                    Name = "Movies",
                    Url = "movies"
                }, new Category
                {
                    Id = 3,
                    Name = "Video Games",
                    Url = "video-games"
                });
        }
    }
}
