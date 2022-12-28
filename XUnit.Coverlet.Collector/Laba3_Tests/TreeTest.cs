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
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.CheckingDictionary", (object[] args) => new List<Dictionary<string, object>>(){
            new Dictionary<string, object>(){ {"x1", "4"}, {"y1", "7"}, {"vx1", "7"}, {"vy1", "2"}, {"x2", "2"}, {"y2", "9"}, {"vx2", "9"}, {"vy2", "2"}, {"c", "0"}, },
            new Dictionary<string, object>(){ {"x1", "1"}, {"y1", "4"}, {"vx1", "8"}, {"vy1", "9"}, {"x2", "10"}, {"y2", "1"}, {"vx2", "3"}, {"vy2", "9"}, {"c", "1"}, },
            new Dictionary<string, object>(){ {"x1", "1"}, {"y1", "2"}, {"vx1", "4"}, {"vy1", "5"}, {"x2", "7"}, {"y2", "10"}, {"vx2", "5"}, {"vy2", "6"}, {"c", "0"}, }
        }).Execute();
    }

    [Fact]
    public void check_teaching_of_tree(){
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Init_Score_Env();
        var table_for_teach = Hwdtech.IoC.Resolve<List<Dictionary<string, object>>>("SpaceShip.Lib.Get.CheckingDictionary");
        Func<object, object> func_for_test = (object z) => z;
        var tree_testing = Hwdtech.IoC.Resolve<Tree>("SpaceShip.Lib.Get.Tree", func_for_test);
        var list_of_features = new List<List<object>>();
        var results = new List<object>();
        foreach (var item in table_for_teach)
        {
            var feat = new List<object>(item.Values);
            feat.ForEach(c => c = Int32.Parse((string?)c));
            list_of_features.Add(feat.GetRange(0, feat.Count-2));
            results.Add((new List<object>(feat))[feat.Count - 1]);
        }
        tree_testing.teach(list_of_features, results);
        foreach (var item in list_of_features.Zip(results, (k, v) => new KeyValuePair<List<object>, object>(k, v)))
        {
            Assert.Equal(tree_testing.get_solution(item.Key), item.Value);
        }

        Assert.True(tree_testing.get_solution(new List<object>(){0, 0, 0, 0}) == null);
    }
}
