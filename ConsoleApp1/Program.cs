using System;
using System.Linq;
using PCManagement.Models;
using PCManagement.Interfaces;
using PCManagement.Repositories;
using PCManagementApp.Models;
using System.ComponentModel.Design;

namespace PCManagement
{
    class Program
    {
        private static IPCStorage storage = new PCStorage();
        static void Main()
        {
  
            while (true)
            {
                Menu();
                switch (Console.ReadLine())
                {
                    case "1":
                        InsertPC();
                        break;
                    case "2":
                        PrintAllPCs();
                        break;
                    case "3":
                        EditPC();
                        break;
                    case "4":
                        DeletePC();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;

                }

                
            }
        }
        static void Menu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1 - Insert PC");
            Console.WriteLine("2 - All PCs list");
            Console.WriteLine("3 - Edit PC");
            Console.WriteLine("4 - Delete PC");
            Console.WriteLine("5 - Exit app");
        }
        static void InsertPC()
        {
            Console.Clear();
            Console.WriteLine("Select PC Type:");
            Console.WriteLine("1 - desktop");
            Console.WriteLine("2 - laptop");
            var type = Console.ReadLine();

            PC? pc = type switch
            {
                "1" => new Desktop(),
                "2" => new Laptop(),
                _ => null
            };

            if (pc == null)
            {
                Console.WriteLine("Invalid type selected.");
                return;
            }

            Console.Write("Enter PC Name: ");
            pc.Name = Console.ReadLine();
            Console.Write("Enter CPU: ");
            pc.CPU = Console.ReadLine();
            Console.Write("Enter GPU: ");
            pc.GPU = Console.ReadLine();
            Console.Write("Enter RAM (GB): ");
            pc.RAMSize = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter storage in GB: ");
            pc.Storage = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter UseCase (Work, School, Gaming): ");
            pc.UseCase = Console.ReadLine();

            storage.AddPC(pc);
            Console.WriteLine("PC added successfully!");
        }

        static void PrintAllPCs()
        {
            Console.Clear();
            Console.WriteLine("Filter by type:");
            Console.WriteLine("1 - desktop");
            Console.WriteLine("2 - laptop");
            Console.WriteLine("3 - all");

            var filter = Console.ReadLine();
            var pcs = storage.GetAllPCs();

            if (filter == "1")
            {
                pcs = pcs.Where(pc => pc is Desktop);
            }else if (filter == "2")
            {
                pcs = pcs.Where(pc => pc is Laptop);
            }


            foreach(var pc in pcs)
            {
                Console.WriteLine(pc);
            }

            if (!pcs.Any())
            {
                Console.WriteLine("There is no PCs :(");
            }
        }
        static void EditPC()
        {
            Console.Write("Enter PC ID to Edit: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            var pc = storage.GetPCById(id);

            if (pc == null)
            {
                Console.WriteLine("PC not found.");
                return;
            }

            Console.WriteLine("Leave field blank to keep current value.");
            Console.Write("Enter new Name: ");
            var name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name)) pc.Name = name;

            Console.Write("Enter new CPU: ");
            var cpu = Console.ReadLine();
            if (!string.IsNullOrEmpty(cpu)) pc.CPU = cpu;

            Console.Write("Enter new GPU: ");
            var gpu = Console.ReadLine();
            if (!string.IsNullOrEmpty(gpu)) pc.GPU = gpu;

            Console.Write("Enter new RAM (GB): ");
            var ramInput = Console.ReadLine();
            if (int.TryParse(ramInput, out var ram)) pc.RAMSize = ram;

            Console.Write("Enter new Storage (GB): ");
            var storageInput = Console.ReadLine();
            if (int.TryParse(storageInput, out var newStorage)) pc.Storage = newStorage;

            Console.Write("Enter new UseCase: ");
            var useCase = Console.ReadLine();
            if (!string.IsNullOrEmpty(useCase)) pc.UseCase = useCase;

            Console.WriteLine("PC updated successfully!");
        }
        static void DeletePC()
        {
            Console.Clear();
            Console.Write("Enter PC ID to Delete: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            var pc = storage.GetPCById(id);

            if (pc == null)
            {
                Console.WriteLine("PC not found.");
                return;
            }

            Console.WriteLine($"Are you sure you want to delete PC: {pc.Name}? (y/n)");
            var confirm = Console.ReadLine()?.ToLower();

            if (confirm == "y")
            {
                storage.DeletePC(id);
                Console.WriteLine("PC deleted successfully!");
            }
            else
            {
                Console.WriteLine("Delete operation cancelled.");
            }
        }
    }
}
