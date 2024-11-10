using System;
using FurnitureApp.Enums;
using FurnitureApp.Interfaces;
using FurnitureApp.Repositories;
using FurnitureApp.Services;

namespace FurnitureApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the repository and service
            string filePath = "furnitureDatabase.json";
            IFurnitureRepository furnitureRepository = new FurnitureRepository(filePath);
            IFurnitureService furnitureService = new FurnitureService(furnitureRepository);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nFurniture Management System");
                Console.WriteLine("1. Add Furniture");
                Console.WriteLine("2. Edit Furniture");
                Console.WriteLine("3. Delete Furniture");
                Console.WriteLine("4. List All Furniture");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddFurniture(furnitureService);
                        break;
                    case "2":
                        EditFurniture(furnitureService);
                        break;
                    case "3":
                        DeleteFurniture(furnitureService);
                        break;
                    case "4":
                        furnitureService.ListFurnitures();
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Exiting program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        static void AddFurniture(IFurnitureService furnitureService)
        {
            Console.Write("Enter furniture name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Select furniture type:");
            foreach (var type in Enum.GetValues(typeof(FurnitureType)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }
            FurnitureType furnitureType = (FurnitureType)int.Parse(Console.ReadLine());

            Console.Write("Enter price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Select furniture material:");
            foreach (var material in Enum.GetValues(typeof(FurnitureMaterial)))
            {
                Console.WriteLine($"{(int)material}. {material}");
            }
            FurnitureMaterial furnitureMaterial = (FurnitureMaterial)int.Parse(Console.ReadLine());

            furnitureService.AddFurniture(name, furnitureType, price, furnitureMaterial);
        }

        static void EditFurniture(IFurnitureService furnitureService)
        {
            Console.Write("Enter furniture ID to edit: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter new furniture name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Select new furniture type:");
            foreach (var type in Enum.GetValues(typeof(FurnitureType)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }
            FurnitureType furnitureType = (FurnitureType)int.Parse(Console.ReadLine());

            Console.Write("Enter new price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Select new furniture material:");
            foreach (var material in Enum.GetValues(typeof(FurnitureMaterial)))
            {
                Console.WriteLine($"{(int)material}. {material}");
            }
            FurnitureMaterial furnitureMaterial = (FurnitureMaterial)int.Parse(Console.ReadLine());

            furnitureService.UpdateFurniture(id, name, furnitureType, price, furnitureMaterial);
        }

        static void DeleteFurniture(IFurnitureService furnitureService)
        {
            Console.Write("Enter furniture ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            furnitureService.DeleteFurniture(id);
        }
    }
}
