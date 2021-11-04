using CrudApp1.Data.Models;
using CrudApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp1.Presentation
{
    public class Display
    {
        private int closeOperationId = 9;
        private AppServices services = new AppServices();

        private void ShowMenu()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 18) + "MENU" + new string(' ', 18));
            Console.WriteLine(new string('-', 40));
            Console.WriteLine("1. Add new manufacturer");
            Console.WriteLine("2. Update manufactorer");
            Console.WriteLine("3. Add new model");
            Console.WriteLine("4. Get all models");
            Console.WriteLine("5. Get model by Manufactorer ID");
            Console.WriteLine("6. Get model by ID");
            Console.WriteLine("7. Update model");
            Console.WriteLine("8. Remove model by ID");
            Console.WriteLine("9. Exit");
        }
        private void Input()
        {
            var operation = -1;
            do
            {
                ShowMenu();
                operation = int.Parse(Console.ReadLine());
                switch (operation)
                {
                    case 1:
                        AddManufactorer();
                        break;
                    case 2:
                        UpdateManufactor();
                        break;
                    case 3:
                        AddModel();
                        break;
                    case 4:
                        ListAll();
                        break;
                    case 5:
                        FetchByManufacturerId();
                        break;
                    case 6:
                        FetchByModelId();
                        break;
                    case 7:
                        UpdateModel();
                        break;
                    case 8:
                        DeleteModel();
                        break;
                    default:
                        break;
                }
            } while (operation != closeOperationId);
        }
        public Display()
        {
            Input();
        }
        private void AddManufactorer()
        {
            Manufacturer manufactorer = new Manufacturer();

            Console.WriteLine("Enter name: ");
            manufactorer.Name = Console.ReadLine();

            Console.WriteLine("Enter country: ");
            manufactorer.Country = Console.ReadLine();
            services.AddManufacturer(manufactorer);
        }
        private void UpdateManufactor()
        {
            Manufacturer manufactorer = new Manufacturer();

            Console.WriteLine("Enter ID to update: ");
            manufactorer.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter name: ");
            manufactorer.Name = Console.ReadLine();

            Console.WriteLine("Enter country: ");
            manufactorer.Country = Console.ReadLine();

            services.UpdateManufacturer(manufactorer);
        }

        private void AddModel()
        {
            Model model = new Model();

            Console.WriteLine("Enter name: ");
            model.Name = Console.ReadLine();

            Console.WriteLine("Enter year: ");
            model.Year = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter manufacturer ID: ");
            model.ManufacturerId = int.Parse(Console.ReadLine());

            services.AddModel(model);
        }


        private void ListAll()
        {
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 16) + "MODELS" + new string(' ', 16));
            Console.WriteLine(new string('-', 40));
            var models = services.GetModels();

            Console.WriteLine(models);
        }
        private void FetchByManufacturerId()
        {
            Console.WriteLine("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            var models = services.GetModelByManufacturerId(id);
            if (models != null)
            {
                foreach (var model in models)
                {
                    Console.WriteLine(new string('-', 40));
                    //Console.WriteLine("ID: " + model.Id);
                    //Console.WriteLine("Name: " + model.Name);
                    //Console.WriteLine("Year: " + model.Year);
                    Console.WriteLine($"{services.GetManufacturer(model.ManufacturerId).Name} {model.Name} {model.Year}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
        private void FetchByModelId()
        {
            Console.WriteLine("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            var model = services.GetModelById(id);

            if (model != null)
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("ID: " + model.Id);
                Console.WriteLine("Manufacturer: " + services.GetManufacturer(model.ManufacturerId).Name);
                Console.WriteLine("Name: " + model.Name);
                Console.WriteLine("Year: " + model.Year);
                Console.WriteLine(new string('-', 40));
            }

        }
        private void UpdateModel()
        {
            Model model = new Model();

            Console.WriteLine("Enter ID to update: ");
            model.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter name: ");
            model.Name = Console.ReadLine();

            Console.WriteLine("Enter year: ");
            model.Year = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter manufacturer ID: ");
            model.ManufacturerId= int.Parse(Console.ReadLine());


            services.UpdateModel(model);
        }
        private void DeleteModel()
        {
            Console.WriteLine("Enter ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            services.RemoveModel(id);
            Console.WriteLine("Done.");
        }
    }
}
