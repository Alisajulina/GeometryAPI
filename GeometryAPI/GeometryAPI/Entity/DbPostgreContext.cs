using GeometryAPI.Entity.Models;
using GeometryAPI.Entity.Models.Many;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryAPI.Entity
{
    public class DbPostgreContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<GenderEntity> Genders { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<FileContentEntity> FileContents { get; set; }
        public DbSet<FileStorageEntity> FileStorages { get; set; }
        public DbSet<ProductFileEntity> ProductFiles { get; set; }

        public DbSet<BrandEntity> Brands { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<SubCategoryEntity> SubCategories { get; set; }
        public DbSet<ClothTypeEntity> ClothTypes { get; set; }

        public DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderStatusEntity> OrderStatuses { get; set; }


        public DbPostgreContext(DbContextOptions<DbPostgreContext> options) : base(options)
        {
            if (Database.EnsureCreated())
                InitialInitialization();

        }

        private void InitialInitialization()
        {
            List<RoleEntity> roles = new List<RoleEntity>()
            {
                new RoleEntity() { Name = "Operator" },
                new RoleEntity() { Name = "Customer" }
            };

            List<GenderEntity> genders = new List<GenderEntity>()
            {
                new GenderEntity() { Name = "Male" },
                new GenderEntity() { Name = "Female" }
            };

            UserEntity user = new UserEntity()
            {
                Email = "test@test.ru",
                Password = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08", //TODO hash этого пароля test
                Role = roles[1],
                Name = "Test",
                LastName = "Test",
                Gender = genders[0]
            };

            Roles.AddRange(roles);
            Genders.AddRange(genders);
            Users.Add(user);
            SaveChanges();
        }

    }
}
