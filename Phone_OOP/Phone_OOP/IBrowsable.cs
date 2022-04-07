using System;
using System.Collections.Generic;
using System.Text;

namespace Phone_OOP
{
    public interface IBrowsable: ICalling
    {
        public string BrowsingSite(string site);
    }
}
