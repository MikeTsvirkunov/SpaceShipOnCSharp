using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
using Hwdtech;
namespace XUnit.Coverlet.Collector;
using System.Linq;

public class TreeTest
{
    [Fact]
    public void Init_Score_Env()
    {
        // Create Scope
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Node", (object[] args) => new TreeNode((Func<object, object>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.NodeWithNexts", (object[] args) => new TreeNode((Func<object, object>)args[0], (Dictionary<object, object>)args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Tree", (object[] args) => new Tree((Func<object, object>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.CSVReader", (object[] args) => new CSVReader((string)args[0], (string)args[1])).Execute();
    }

    [Fact]
    public void check_teaching_of_tree(){
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Init_Score_Env();
        CSVReader testingCSVReader = (CSVReader)Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.CSVReader", @"./../../../Laba3_Tests/colision_vectors.csv", "; ");
        testingCSVReader.action();
        var table_for_teach = testingCSVReader.get_table();
        Func<object, object> func_for_test = (object z) => z;
        var tree_testing = Hwdtech.IoC.Resolve<Tree>("SpaceShip.Lib.Get.Tree", func_for_test);
        var list_of_features = new List<List<object>>();
        var results = new List<object>();
        foreach (var item in table_for_teach)
        {
            var feat = new List<object>(item.Values);
            feat.ForEach(c => c = Int32.Parse(c.ToString()));
            list_of_features.Add(feat.GetRange(0, feat.Count-2));
            results.Add((new List<object>(feat))[feat.Count - 1]);
        }
        tree_testing.teach(list_of_features, results);
        foreach (var item in list_of_features.Zip(results, (k, v) => new KeyValuePair<List<object>, object>(k, v)))
        {
            Assert.Equal(tree_testing.get_solution(item.Key), item.Value);
        }

        Assert.Equal(tree_testing.get_solution(new List<object>(){0, 0, 0, 0}), null);
    }
}