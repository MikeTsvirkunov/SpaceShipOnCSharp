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
public class UIObjectTest
{
    [Fact]
    public void Init_Score_Env()
    {
        // Create Scope
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.CSVReader", (object[] args) => new CSVReader((string)args[0], (string)args[1])).Execute();
    }

    [Fact]
    public void CSVReaderTestFull()
    {
        Init_Score_Env();
        var testingCSVReader = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.CSVReader", @"colision_vectors.csv", "; ");
    }


}