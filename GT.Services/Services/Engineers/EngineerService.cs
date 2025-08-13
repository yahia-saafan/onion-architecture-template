using GT.Application.Services.Engineers.Dtos;
using GT.Core.Entities;
using GT.Core.Interfaces;

namespace GT.Application.Services.Engineers;

public class EngineerService(IRepository<Engineer> engineerRepository) : IEngineerService
{
    public async Task<IEnumerable<EngineerDto>> GetAllEngineersAsync()
    {
        var engineers = await engineerRepository.GetAllAsync();

        return engineers.Select(e => new EngineerDto(e.Id, e.Name, e.NameAR));
    }
    public async Task<Guid> InsertEngineerAsync(CreateEngineerDto engineerDto)
    {
        var engineer = new Engineer(Guid.NewGuid())
        {
            Name = engineerDto.Name, 
            NameAR = engineerDto.NameAR
        };

        await engineerRepository.InsertAsync(engineer);

        return engineer.Id;
    }
}
