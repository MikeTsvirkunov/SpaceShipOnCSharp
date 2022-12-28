using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
using Hwdtech;
// using Hwdtech.IoC;
// using Hwdtech.ScopeBasedIoCImplementation;
namespace XUnit.Coverlet.Collector;
public class CSVReaderTest
{
    [Fact]
    public void Init_Score_Env()
    {
        // Create Scope
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        // Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Object.DictStringObject", (object[] args) => new List<Dictionary<string, object>>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.CSVReader", (object[] args) => new CSVReader((string)args[0], (string)args[1])).Execute();
    }

    [Fact]
    public void CSVReaderTestFull()
    {
        Init_Score_Env();
        CSVReader testingCSVReader = (CSVReader)Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.CSVReader", @"./../../../Laba3_Tests/colision_vectors.csv", "; ");
        testingCSVReader.action();
        var testing_table = testingCSVReader.get_table();
        var expected_table = new List<Dictionary<string, object>>(){
            new Dictionary<string, object>(){ {"x1", "4"}, {"y1", "7"}, {"vx1", "7"}, {"vy1", "2"}, {"x2", "2"}, {"y2", "9"}, {"vx2", "9"}, {"vy2", "2"}, {"c", "0"}, },
            new Dictionary<string, object>(){ {"x1", "1"}, {"y1", "4"}, {"vx1", "8"}, {"vy1", "9"}, {"x2", "10"}, {"y2", "1"}, {"vx2", "3"}, {"vy2", "9"}, {"c", "1"}, },
            new Dictionary<string, object>(){ {"x1", "1"}, {"y1", "2"}, {"vx1", "4"}, {"vy1", "5"}, {"x2", "7"}, {"y2", "10"}, {"vx2", "5"}, {"vy2", "6"}, {"c", "0"}, }
        };

        Assert.Equal(expected_table, testing_table);
    }

    [Fact]
    public void CSVReaderTestWIncorrectWay()
    {
        Init_Score_Env();
        CSVReader testingCSVReader = (CSVReader)Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.CSVReader", @"rubish", "; ");
        Assert.Throws<System.IO.FileNotFoundException>(() => testingCSVReader.action());
    }
}