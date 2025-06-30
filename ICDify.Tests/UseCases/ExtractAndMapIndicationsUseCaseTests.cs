using ICDify.Application.DTOs;
using ICDify.Application.Interfaces;
using ICDify.Application.UseCases;
using Moq;

namespace ICDify.Tests.UseCases;

public class ExtractAndMapIndicationsUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsMappedIndications_AndPersistsThem()
    {
        // Arrange
        var mockMapper = new Mock<IIndicationMapper>();
        var mockRepo = new Mock<IDrugRepository>();

        var expectedIndications = new List<IndicationDto>
    {
        new("Asthma", "J45.9", "Asthma, unspecified"),
        new("Eczema", "L20.9", "Atopic dermatitis, unspecified")
    };

        mockMapper
            .Setup(m => m.ExtractAndMapAsync("Dupixent", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedIndications);

        var useCase = new ExtractAndMapIndicationsUseCase(mockMapper.Object, mockRepo.Object);

        // Act
        var result = await useCase.ExecuteAsync("Dupixent");

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("J45.9", result[0].ICD10Code);

        mockRepo.Verify(r =>
            r.SaveIndicationsAsync("Dupixent", expectedIndications, It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
