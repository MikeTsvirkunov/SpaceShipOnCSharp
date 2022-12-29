using SaceShips.Lib.Interfaces;
using System.IO;
using System.Collections.Generic;
namespace SaceShips.Lib.Classes;


public class CSVReader: ICommand
{
    private string spliter;
    private string fileway;
    private List<Dictionary<string, object>> table;
    public CSVReader(string fileway, string spliter="; ")
    {
        this.table = new List<Dictionary<string, object>>();
        this.fileway = fileway;
        this.spliter = spliter;
    }

    public void action()
    {
        using (var reader = new StreamReader(fileway))
        {
            IList<string> listHEADERS = reader.ReadLine().Split("; ");
            
            while (!reader.EndOfStream)
            {
                string[] values = reader.ReadLine().Split("; ");
                table.Add(new Dictionary<string, object?>(listHEADERS.Zip(values, (k, v) => new KeyValuePair<string, object>(k, (object?)v))));
            }
        }
    }

    public List<Dictionary<string, object>> get_table(){
        return this.table;
    }
}
