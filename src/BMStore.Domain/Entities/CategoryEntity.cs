namespace BMStore.Domain.Entities;

public record CategoryEntity
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Photo { get; init; } = string.Empty;
    public CategoryEntity? ParentCategory { get; init; }
   //კატეგორიებზე ველები რომელიც გამოიყენება პროდუქტის გასაფილტრად
}
