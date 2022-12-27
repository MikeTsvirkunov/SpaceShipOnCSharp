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
        testingCSVReader.get_table();
    }

    [Fact]
    public void CSVReaderTestWIncorrectWay()
    {
        Init_Score_Env();
        CSVReader testingCSVReader = (CSVReader)Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.CSVReader", @"rubish", "; ");
        Assert.Throws<System.IO.FileNotFoundException>(() => testingCSVReader.action());
    }
}