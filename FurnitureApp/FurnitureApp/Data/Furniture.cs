using FurnitureApp.Enums;

namespace FurnitureApp.Data;

public class Furniture
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public FurnitureType Type { get; set; }
    public decimal? Price { get; set; }
    public FurnitureMaterial Material { get; set; }
}