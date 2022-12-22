using SaceShips.Lib.Interfaces;
using System.IO;
using System.Collections.Generic;
namespace SaceShips.Lib.Classes;


public class CSVReader: ICommand
{
    private string spliter;
    private string fileway;
    public CSVReader(string fileway, string spliter="; ")
    {
        this.fileway = fileway;
        this.spliter = spliter;
    }

    public void action()
    {
        using (var reader = new StreamReader(fileway))
        {
            IList<string> listHEADERS = reader.ReadLine().Split("; ");
            List<Dictionary<string, double>> table = new List<Dictionary<string, double>>();
            
            while (!reader.EndOfStream)
            {
                var values = reader.ReadLine().Split("; ");
                table.Add(new Dictionary<string, double>(listHEADERS.Zip(values, (k, v) => new KeyValuePair<string, double>(k, Convert.ToDouble(v)))));
            }
        }
    }
}
