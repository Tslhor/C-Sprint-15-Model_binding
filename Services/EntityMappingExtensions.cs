using ProductsValidation.Models;

namespace ProductsValidation.Services
{
    public static class EntityMappingExtensions
    {
        public static Product ToCopy(this Product source)
        {
            if (source == null)
            {
                return null;
            }

            return new Product
            {
                Id = source.Id,
                Type = source.Type,
                Name = source.Name,
                Description = source.Description,
                Price = source.Price
            };
        }

        public static User ToCopy(this User source)
        {
            if (source == null)
            {
                return null;
            }

            return new User
            {
                Id = source.Id,
                Name = source.Name,
                Email = source.Email,
                Role = source.Role
            };
        }
    }
}
