using FurnitureApp.Data;
using FurnitureApp.Enums;

namespace FurnitureApp.Interfaces;

public interface IFurnitureService
{
    void AddFurniture(string name, FurnitureType furnitureType, decimal price, FurnitureMaterial furnitureMaterial);
    void UpdateFurniture(int id, string name, FurnitureType furnitureType, decimal price, FurnitureMaterial furnitureMaterial);
    void DeleteFurniture(int id);
    void ListFurnitures();
}