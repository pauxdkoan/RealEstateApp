


using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;


namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultImprovements
    {
        public static async Task SeedAsync(IImprovementRepository _improvementRepository)
        {
            List<Improvement> improvements = new List<Improvement>
            {
                new Improvement
                {
                    Name = "Piscina",
                    Description = "Piscina de 8x4",
                },
                new Improvement
                {
                    Name = "Ascensor",
                    Description = "Edificio con Ascensor",
                },
                new Improvement
                {
                    Name = "Seguridad",
                    Description = "Personal de Seguridad 24/7",
                },
                new Improvement
                {
                    Name = "Area de Juegos",
                    Description = "Area Comun",
                },
            };

            List<Improvement> allImprovements = await _improvementRepository.GetAllListAsync();

            foreach (var improvement in improvements)
            {
                if (!allImprovements.Any(i => i.Name == improvement.Name))
                {
                    await _improvementRepository.AddAsync(improvement);
                }
            }
        }
    }
}
