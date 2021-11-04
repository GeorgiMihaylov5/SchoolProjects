using CrudApp1.Data;
using CrudApp1.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp1.Services
{
    public class AppServices
    {
        private AutoContext context;

        public void AddManufacturer(Manufacturer manufacturer)
        {
            using(context = new AutoContext())
            {
                context.Manufacturers.Add(manufacturer);
                context.SaveChanges();
            }
        }
        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            using (context = new AutoContext())
            {
                var oldManufacturer = context.Manufacturers.Find(manufacturer.Id);

                if (oldManufacturer != null)
                {
                    context.Entry(oldManufacturer).CurrentValues.SetValues(manufacturer);
                    context.SaveChanges();
                }
            }
        }
        public void AddModel(Model model)
        {
            using (context = new AutoContext())
            {
                context.Models.Add(model);
                context.SaveChanges();
            }
        }
        public string GetModels()
        {
            using (context = new AutoContext())
            {
                var models = context.Models.Select(x => new { x.Id, x.Name, x.Year, manufacturer = x.Manufacturer.Name });

                var sb = new StringBuilder();

                foreach (var item in models)
                {
                    sb.AppendLine($"{item.Id} {item.Name} {item.Year} {item.manufacturer}");
                }
                return sb.ToString();
            }
        }
        public List<Model> GetModelByManufacturerId(int id)
        {
            using (context = new AutoContext())
            {
                var models = context.Models.Where(x => x.Manufacturer.Id == id).ToList();
                return models;
            }
        }
        public Model GetModelById(int id)
        {
            using (context = new AutoContext())
            {
                var model = context.Models.Find(id);
                return model;
            }
        }
        public void UpdateModel(Model model)
        {
            using (context = new AutoContext())
            {
                var oldModel = context.Models.Find(model.Id);

                if (oldModel != null)
                {
                    context.Entry(oldModel).CurrentValues.SetValues(model);
                    context.SaveChanges();
                }
            }
        }
        public void RemoveModel(int id)
        {
            using (context = new AutoContext())
            {
                var model = context.Models.Find(id);
                if (model != null)
                {
                    context.Models.Remove(model);
                    context.SaveChanges();
                }
            }
        }
        public Manufacturer GetManufacturer(int id)
        {
            using (context = new AutoContext())
            {
                var manufacturer = context.Manufacturers.Find(id);
                return manufacturer;
            }
        }
    }
}
