using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp1.Data.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
    }
}
