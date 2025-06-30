using ICDify.Application.DTOs;
using ICDify.Application.Interfaces;

namespace ICDify.Infrastructure.Mappers;
public class MockIndicationMapper : IIndicationMapper
{
    public Task<List<IndicationDto>> ExtractAndMapAsync(string drugName, CancellationToken cancellationToken = default)
    {
        var result = new List<IndicationDto>
        {
            new("Atopic dermatitis", "L20.9", "Atopic dermatitis, unspecified"),
            new("Asthma (eosinophilic)", "J45.50", "Severe persistent asthma, unspecified")
        };
        return Task.FromResult(result);
    }
}
