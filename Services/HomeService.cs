using DondeComemos.Models;
using DondeComemos.Data;
using Microsoft.EntityFrameworkCore;

namespace DondeComemos.Services
{
    public interface IHomeService
    {
        HomeViewModel GetHomeData();
    }

    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _context;

        public HomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public HomeViewModel GetHomeData()
        {
            var restaurantesAleatorios = _context.Restaurantes
                .ToList()
                .OrderBy(r => Guid.NewGuid())
                .Take(3)
                .Select(r => new RestaurantCard
                {
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    ImagenUrl = r.ImagenUrl,
                    Rating = (int)r.Rating
                })
                .ToList();

            if (restaurantesAleatorios.Count == 0)
            {
                restaurantesAleatorios = new List<RestaurantCard>
                {
                    new RestaurantCard 
                    { 
                        Nombre = "Restaurante 1", 
                        Descripcion = "Breve descripción", 
                        ImagenUrl = "/img/rest1.jpg", 
                        Rating = 5 
                    },
                    new RestaurantCard 
                    { 
                        Nombre = "Restaurante 2", 
                        Descripcion = "Información", 
                        ImagenUrl = "/img/rest2.jpg", 
                        Rating = 5 
                    },
                    new RestaurantCard 
                    { 
                        Nombre = "Restaurante 3", 
                        Descripcion = "Breve descripción", 
                        ImagenUrl = "/img/rest3.jpg", 
                        Rating = 5 
                    },
                };
            }

            return new HomeViewModel
            {
                Favoritos = restaurantesAleatorios,
                Reseñas = new List<Testimonial>
                {
                    new Testimonial 
                    { 
                        Texto = "Excelente servicio y comida deliciosa", 
                        Usuario = "María García", 
                        Rol = "Cliente Frecuente", 
                        ImagenUrl = "/img/user1.jpg" 
                    },
                    new Testimonial 
                    { 
                        Texto = "Ambiente acogedor y precios justos", 
                        Usuario = "Carlos Rodríguez", 
                        Rol = "Food Blogger", 
                        ImagenUrl = "/img/user2.jpg" 
                    },
                    new Testimonial 
                    { 
                        Texto = "Gran variedad de opciones gastronómicas", 
                        Usuario = "Ana Martínez", 
                        Rol = "Crítica Gastronómica", 
                        ImagenUrl = "/img/user3.jpg" 
                    },
                }
            };
        }
    }
}