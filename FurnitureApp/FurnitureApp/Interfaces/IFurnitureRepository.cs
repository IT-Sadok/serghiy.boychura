using FurnitureApp.Data;

namespace FurnitureApp.Interfaces;

public interface IFurnitureRepository
{
    void AddFurniture(Furniture furniture);
    void UpdateFurniture(Furniture furniture);
    void DeleteFurniture(int furnitureId);
    List<Furniture> GetAll();
    Furniture GetById(int furnitureId);
}