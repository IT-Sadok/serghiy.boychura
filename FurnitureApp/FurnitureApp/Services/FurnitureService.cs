using FurnitureApp.Data;
using FurnitureApp.Enums;
using FurnitureApp.Interfaces;

namespace FurnitureApp.Services;

public class FurnitureService : IFurnitureService
{
    private readonly IFurnitureRepository _furnitureRepository;
    
    public FurnitureService(IFurnitureRepository furnitureRepository)
    {
        _furnitureRepository = furnitureRepository;
    }
    
    public void AddFurniture(string name, FurnitureType furnitureType, decimal price, FurnitureMaterial furnitureMaterial)
    {
        if (string.IsNullOrWhiteSpace(name) || price <= 0)
        {
            throw new ArgumentException("Invalid furniture details provided.");
        }

        var furniture = new Furniture
        {
            Id = GenerateUniqueId(),
            Name = name,
            Type = furnitureType,
            Material = furnitureMaterial,
            Price = price
        };

        _furnitureRepository.AddFurniture(furniture);
        Console.WriteLine("Furniture added successfully.");
    }

    public void UpdateFurniture(int id, string name, FurnitureType furnitureType, decimal price,
        FurnitureMaterial furnitureMaterial)
    {
        if (string.IsNullOrWhiteSpace(name) || price <= 0)
        {
            throw new ArgumentException("Invalid furniture details provided.");
        }

        var existingFurniture = _furnitureRepository.GetById(id);
        if (existingFurniture == null)
        {
            Console.WriteLine("Furniture with specified ID not found.");
            return;
        }

        existingFurniture.Name = name;
        existingFurniture.Type = furnitureType;
        existingFurniture.Material = furnitureMaterial;
        existingFurniture.Price = price;

        _furnitureRepository.UpdateFurniture(existingFurniture);
        Console.WriteLine("Furniture updated successfully.");
    }

    public void DeleteFurniture(int id)
    {
        var existingFurniture = _furnitureRepository.GetById(id);
        if (existingFurniture == null)
        {
            Console.WriteLine("Furniture with specified ID not found.");
            return;
        }

        _furnitureRepository.DeleteFurniture(id);
        Console.WriteLine("Furniture deleted successfully.");
    }

    public void ListFurnitures()
    {
        List<Furniture> furnitureList = _furnitureRepository.GetAll();

        if (furnitureList.Count == 0)
        {
            Console.WriteLine("No furniture items available.");
            return;
        }

        foreach (var furniture in furnitureList)
        {
            Console.WriteLine($"ID: {furniture.Id}, Name: {furniture.Name}, Type: {furniture.Type}, " +
                              $"Material: {furniture.Material}, Price: {furniture.Price:C}");
        }
    }
    private int GenerateUniqueId()
    {
        List<Furniture> furnitureList = _furnitureRepository.GetAll();
        return furnitureList.Count > 0 ? furnitureList[^1].Id + 1 : 1;
    }
}