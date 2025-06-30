using ICDify.Application.DTOs;
using ICDify.Application.Interfaces;
using ICDify.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace ICDify.Infrastructure.Persistence;

public class DrugRepository : IDrugRepository
{
    private readonly ApplicationDbContext _context;

    public DrugRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveIndicationsAsync(string drugName, List<IndicationDto> indications, CancellationToken cancellationToken = default)
    {
        var drug = await _context.Drugs
            .Include(d => d.Indications)
            .FirstOrDefaultAsync(d => d.Name == drugName, cancellationToken);

        if (drug == null)
        {
            drug = new DrugEntity
            {
                Name = drugName,
                Indications = new List<IndicationEntity>()
            };
            _context.Drugs.Add(drug);
        }

        foreach (var dto in indications)
        {
            var exists = drug.Indications.Any(i => i.ICD10Code == dto.ICD10Code);
            if (!exists)
            {
                drug.Indications.Add(new IndicationEntity
                {
                    Condition = dto.Condition,
                    ICD10Code = dto.ICD10Code,
                    Description = dto.Description
                });
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }


}
