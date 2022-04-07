using System;
using System.Collections.Generic;
using System.Text;

namespace Phone_OOP
{
    public class StationaryPhone : ICalling
    {
        public string CallNumber(string number)
        {
            for (int i = 0; i < number.Length; i++)
            {
                if (!char.IsDigit(number[i]))
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
    }
}
