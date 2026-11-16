using System.Collections.Generic;
using ProductsValidation.Models;

namespace ProductsValidation.Services
{
    public class Data
    {
        public List<Product> Products { get; } = new List<Product>
        {
            new Product() {Id = 1, Type = ProductCategory.Toy, Name = "ToyTrain", Description = "ToyTrain classic set", Price = 25},
            new Product() {Id = 2, Type = ProductCategory.Toy, Name = "ToyDrone", Description = "ToyDrone starter kit", Price = 65},
            new Product() {Id = 3, Type = ProductCategory.Clothes, Name = "Shirt", Description = "Shirt casual cotton", Price = 35},
            new Product() {Id = 4, Type = ProductCategory.Clothes, Name = "Sneakers", Description = "Sneakers street style", Price = 95},
            new Product() {Id = 5, Type = ProductCategory.Technique, Name = "Toaster", Description = "Toaster compact model", Price = 55},
            new Product() {Id = 6, Type = ProductCategory.Technique, Name = "Blender", Description = "Blender power mix", Price = 140},
            new Product() {Id = 7, Type = ProductCategory.Toy, Name = "ToyCar", Description = "ToyCar basic model", Price = 15},
            new Product() {Id = 8, Type = ProductCategory.Technique, Name = "Mixer", Description = "Mixer home set", Price = 120},
            new Product() {Id = 9, Type = ProductCategory.Clothes, Name = "Jacket", Description = "Jacket winter line", Price = 89},
            new Product() {Id = 10, Type = ProductCategory.Transport, Name = "Bike", Description = "Bike city edition", Price = 350},
        };

        public List<User> Users { get; } = new List<User>
        {
            new User() {Id = 0, Name = "UserAdmin", Email = "admin@gmail.com", Role = "admin"},
            new User() {Id = 0, Name = "Guest", Email = "guest@gmail.com", Role = "guest"},
            new User() {Id = 0, Name = "User", Email = "user1@gmail.com", Role = "user"},
            new User() {Id = 0, Name = "User2", Email = "user2@gmail.com", Role = "user"},

        };
    }
}
