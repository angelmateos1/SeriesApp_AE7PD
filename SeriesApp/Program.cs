using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SeriesApp.Helpers;
using SeriesApp.Models;
using SeriesApp.Services;

namespace SeriesApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {

            var seriesService = new SeriesServices();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SeriesApp ===");
                Console.WriteLine("1. See all series");
                Console.WriteLine("2. See details of one serie");
                Console.WriteLine("3. Create one serie");
                Console.WriteLine("4. Edit one serie");
                Console.WriteLine("5. Delete one serie");
                Console.WriteLine("6. Exit");
                Console.Write("Select one option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await ShowAllSeries(seriesService);
                        break;
                    case "2":
                        await ShowSeriesDetails(seriesService);
                        break;
                    case "3":
                        await CreateNewSeries(seriesService);
                        break;
                    case "4":
                        await EditSeries(seriesService);
                        break;
                    case "5":
                        await DeleteSeries(seriesService);
                        break;
                    case "6":
                        Console.WriteLine("Thank you!");
                        return;
                    default:
                        Console.WriteLine("Type another option, please");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static async Task ShowAllSeries(SeriesServices seriesService)
        {
            Console.Clear();
            Console.WriteLine("=== All series ===");

            var seriesList = await seriesService.GetAll<Serie>("/api/Serie");

            if (seriesList == null || seriesList.Count == 0)
            {
                Console.WriteLine("No series available.");
            }
            else
            {
                foreach (var serie in seriesList)
                {
                    Console.WriteLine($"ID: {serie.ObjectId} | Name: {serie.Name} | Plataform: {serie.Platform}");
                }
            }

            Console.WriteLine("\nType anything to continue...");
            Console.ReadKey();
        }

        private static async Task ShowSeriesDetails(SeriesServices seriesService)
        {
            Console.Clear();
            Console.Write("Type the ID of the serie: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var serie = await seriesService.Get<Serie>("/api/Serie", id);

                if (serie != null)
                {
                    Console.WriteLine($"ID: {serie.ObjectId}");
                    Console.WriteLine($"name: {serie.Name}");
                    Console.WriteLine($"rate: {serie.Rate}");
                    Console.WriteLine($"language: {serie.Language}");
                    Console.WriteLine($"Platform: {serie.Platform}");
                }
                else
                {
                    Console.WriteLine("Serie not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Id.");
            }

            Console.WriteLine("\nType anything to continue...");
            Console.ReadKey();
        }

        private static async Task CreateNewSeries(SeriesServices seriesService)
        {
            Console.Clear();
            Console.WriteLine("Create new serie:");

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Plataform (1=Netflix, 2=Disney, 3=Hbo): ");
            if (int.TryParse(Console.ReadLine(), out int platform))
            {
                var newSeries = new Serie { Name = name, Platform = platform };
                var result = await seriesService.Add("/api/Serie", newSeries);

                if (result)
                {
                    Console.WriteLine("Serie created sucessfully.");
                }
                else
                {
                    Console.WriteLine("Error while creating new serie.");
                }
            }
            else
            {
                Console.WriteLine("Invalid plataform.");
            }

            Console.WriteLine("\nType anything to continue...");
            Console.ReadKey();
        }

        private static async Task EditSeries(SeriesServices seriesService)
        {
            Console.Clear();
            Console.Write("Id of serie to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var serie = await seriesService.Get<Serie>("/api/Serie", id);
                if (serie != null)
                {
                    Console.WriteLine($"Edit serie {serie.Name}:");

                    Console.Write("New name: ");
                    var newName = Console.ReadLine();
                    Console.Write("New plataform (1=Netflix, 2=Disney, 3=HBO): ");
                    if (int.TryParse(Console.ReadLine(), out int newPlatform))
                    {
                        serie.Name = newName;
                        serie.Platform = newPlatform;
                        var result = await seriesService.Update("/api/Serie", int.Parse(serie.ObjectId), serie);
                        if (result)
                        {
                            Console.WriteLine("Serie updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Error while updating the serie.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Platform.");
                    }
                }
                else
                {
                    Console.WriteLine("Serie not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Id.");
            }

            Console.WriteLine("\nPress any key to go back to the Home screen...");
            Console.ReadKey();
            await ShowAllSeries(seriesService);
        }

        private static async Task DeleteSeries(SeriesServices seriesService)
        {
            Console.Clear();
            Console.Write("Id of serie to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var serie = await seriesService.Get<Serie>("/api/Serie", id);
                if (serie != null)
                {
                    var result = await seriesService.Delete<Serie>("/api/Serie", id); // Use Delete method
                    if (result)
                    {
                        Console.WriteLine("Serie deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Error while deleting the serie.");
                    }
                }
                else
                {
                    Console.WriteLine("Serie not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Id.");
            }

            Console.WriteLine("\nPress any key to go back to the Home screen...");
            Console.ReadKey();
            await ShowAllSeries(seriesService);
        }
    }
}
