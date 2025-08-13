namespace GT.Core.Results;

public class LookupDto<T> : ILookupDto
{
    public T Id { get; set; }

    public string Name { get; set; }

    object ILookupDto.Id => Id!;
}