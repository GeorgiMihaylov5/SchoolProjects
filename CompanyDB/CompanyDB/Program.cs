using CompanyDB.Data;
using CompanyDB.Presentation;
using System;

namespace CompanyDB
{
    class Program
    {
        static void Main(string[] args)
        {
            CompanyContext cm = new CompanyContext();
            Display d = new Display();
        }
    }
}
