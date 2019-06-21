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

        public DbSet<WishListEntity> WishLists { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<SubCategoryEntity> SubCategories { get; set; }
        public DbSet<ClothTypeEntity> ClothTypes { get; set; }

        public DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderStatusEntity> OrderStatuses { get; set; }

        public DbSet<SizeDictionary> SizeDictionary { get; set; }


        public DbPostgreContext(DbContextOptions<DbPostgreContext> options) : base(options)
        {
            if (Database.EnsureCreated())
                InitialInitialization();

        }

        private void InitialInitialization()
        {

            List<CategoryEntity> categories = new List<CategoryEntity>()
            {
                new CategoryEntity() {Name = "Категория-1"},
                new CategoryEntity() {Name = "Категория-2"},
                new CategoryEntity() {Name = "Категория-3"}
            };

           

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

            Categories.AddRange(categories);
            

            Roles.AddRange(roles);
            Genders.AddRange(genders);
            Users.Add(user);
            SaveChanges();

            List<SubCategoryEntity> subCategories = new List<SubCategoryEntity>()
            {
                new SubCategoryEntity() {Name = "Подкатегория-1-1", CategoryId= categories[0].Id},
                new SubCategoryEntity() {Name = "Подкатегория-1-2", CategoryId= categories[0].Id},
                new SubCategoryEntity() {Name = "Подкатегория-1-3", CategoryId= categories[0].Id},
                new SubCategoryEntity() {Name = "Подкатегория-2-1", CategoryId= categories[1].Id},
                new SubCategoryEntity() {Name = "Подкатегория-2-2", CategoryId= categories[1].Id},
                new SubCategoryEntity() {Name = "Подкатегория-2-3", CategoryId= categories[1].Id},
                new SubCategoryEntity() {Name = "Подкатегория-3-1", CategoryId= categories[2].Id},
                new SubCategoryEntity() {Name = "Подкатегория-3-2", CategoryId= categories[2].Id},
                new SubCategoryEntity() {Name = "Подкатегория-3-3", CategoryId= categories[2].Id}
            };

            SubCategories.AddRange(subCategories);
            SaveChanges();
        }

    }
}
