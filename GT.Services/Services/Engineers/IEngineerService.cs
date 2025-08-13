using GT.Application.Services.Engineers.Dtos;

namespace GT.Application.Services.Engineers;

public interface IEngineerService
{
    Task<IEnumerable<EngineerDto>> GetAllEngineersAsync();
    Task<Guid> InsertEngineerAsync(CreateEngineerDto engineerDto);
}
