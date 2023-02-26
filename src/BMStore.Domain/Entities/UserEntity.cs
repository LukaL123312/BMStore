namespace BMStore.Domain.Entities;

public record UserEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string SubDomainName { get; set; }
    public IReadOnlyCollection<PackageEntity>? Packages { get; set; }

}
