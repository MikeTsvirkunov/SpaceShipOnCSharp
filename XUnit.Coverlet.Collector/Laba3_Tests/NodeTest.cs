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
public class TreeNodeTest
{
    [Fact]
    public void Init_Score_Env()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Node", (object[] args) => new TreeNode((Func<object, object>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.NodeWithNexts", (object[] args) => new TreeNode((Func<object, object>)args[0], (Dictionary<object, object?>)args[1])).Execute();
    }

    [Fact]
    public void check_node()
    {
        Init_Score_Env();
        Func<object, object> func_for_test = (object z) => z;
        var next_testing_node = Hwdtech.IoC.Resolve<TreeNode>("SpaceShip.Lib.Get.Node", func_for_test);
        Dictionary<object, object?> next_nodes = new Dictionary<object, object?>() {{"a", next_testing_node }, {"b", "a" }};
        var testing_node = Hwdtech.IoC.Resolve<TreeNode>("SpaceShip.Lib.Get.NodeWithNexts", func_for_test, next_nodes);
        Assert.Equal(next_testing_node, testing_node.step_forward("a"));
        Assert.Equal("a", testing_node.step_forward("b"));
        Assert.True(null == testing_node.step_forward("c"));
    }
}
