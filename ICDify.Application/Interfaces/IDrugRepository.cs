using ICDify.Application.DTOs;

namespace ICDify.Application.Interfaces;

public interface IDrugRepository
{
    Task SaveIndicationsAsync(string drugName, List<IndicationDto> indications, CancellationToken cancellationToken = default);
}
