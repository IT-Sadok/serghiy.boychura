using FurnitureApp.Data;
using FurnitureApp.Interfaces;
using System.Text.Json;

namespace FurnitureApp.Repositories;

public class FurnitureRepository : IFurnitureRepository
{
    private string _filePath;

    public FurnitureRepository(string filePath)
    {
        _filePath = filePath;
        EnsureDatabaseFileExists();
    }

    private void EnsureDatabaseFileExists()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }
    
    public void AddFurniture(Furniture furniture)
    {
        List<Furniture> furnitureList = GetAll();
        furnitureList.Add(furniture);
        SaveToFile(furnitureList);
    }

    private void SaveToFile(List<Furniture> furnitureList)
    {
        string json = JsonSerializer.Serialize(furnitureList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public void UpdateFurniture(Furniture furniture)
    {
        List<Furniture> furnitureList = GetAll();
        int index = furnitureList.FindIndex(f=>f.Id == furniture.Id);
        if (index != -1)
        {
            furnitureList[index] = furniture;
            SaveToFile(furnitureList);
        }
        else
        {
            throw new ArgumentException("Furniture with the specified ID not found.");
        }
    }

    public void DeleteFurniture(int furnitureId)
    {
        List<Furniture> furnitureList = GetAll();
        Furniture furnitureToDelete = furnitureList.FirstOrDefault(f => f.Id == furnitureId);
        if (furnitureToDelete != null)
        {
            furnitureList.Remove(furnitureToDelete);
            SaveToFile(furnitureList);
        }
        else
        {
            throw new ArgumentException("Furniture with the specified ID not found.");
        }
    }

    public List<Furniture> GetAll()
    {
        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Furniture>>(json) ?? new List<Furniture>();
    }

    public Furniture GetById(int furnitureId)
    {
        List<Furniture> furnitureList = GetAll();
        return furnitureList.FirstOrDefault(f => f.Id == furnitureId);
    }
}