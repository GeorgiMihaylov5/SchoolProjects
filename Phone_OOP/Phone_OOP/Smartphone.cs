using System;
using System.Collections.Generic;
using System.Linq;

namespace Phone_OOP
{
    public class Smartphone : IBrowsable
    {
        public string CallNumber(string number)
        {
            for (int i = 0; i < number.Length; i++)
            {
                if(!char.IsDigit(number[i]))
                {
                    throw new ArgumentException("Invalid number!");
                }
            }
            return number;
        }
        //public string CallNumber(string number)
        //{
        //    if(number.All(char.IsDigit))
        //    {
        //        return number;
        //    }
        //    throw new ArgumentException("Invalid number!");
        //}


        public string BrowsingSite(string site)
        {
            for (int i = 0; i < site.Length; i++)
            {
                if (char.IsDigit(site[i]))
                {
                    throw new ArgumentException("Invalid URL!");
                }
            }
            return site;
        }
        //public string BrowsingSite1(string site)
        //{
        //    if(site.Any(char.IsDigit))
        //    {
        //        return site;
        //    }
        //    throw new ArgumentException("Invalid URL!");
        //}
    }
}
