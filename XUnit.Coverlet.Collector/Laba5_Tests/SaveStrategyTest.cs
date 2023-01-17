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

public class SaveStrategy
{
    [Fact]
    public void Init_Score_Env_Laba5()
    {
        var TestStrategy = new Mock<IStartegy>();
        TestStrategy.Setup(p => p.execute(1, 0)).Throws(new Exception());
        TestStrategy.Setup(p => p.execute(2, 3)).Throws(new TypeLoadException());
        TestStrategy.Setup(p => p.execute(2, 0)).Throws(new RankException());
        TestStrategy.Setup(p => p.execute(1, 1)).Returns(1);

        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Node", (object[] args) => new TreeNode((Func<object, object>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.NodeWithNexts", (object[] args) => new TreeNode((Func<object, object>)args[0], (Dictionary<object, object?>)args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Tree", (object[] args) => new Tree((Func<object, object>)args[0])).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategy.SaveStrategy", (object[] args) => new SaceShips.Lib.Classes.SaveStrategy(args[0], args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategy.TestStrategy", (object[] args) => TestStrategy.Object).Execute();
    }

    [Fact]
    public void check_for_registred_errors()
    {
        Init_Score_Env_Laba5();
        Func<object, object> func_for_test = (object z) => z.GetType();
        var error_tester = Hwdtech.IoC.Resolve<Tree>("SpaceShip.Lib.Get.Tree", func_for_test);
        var list_of_data = new List<List<object>>(){ new List<object>(){(object)Hwdtech.IoC.Resolve<IStartegy>("SpaceShip.Lib.Strategy.TestStrategy", new object[]{}), (object)(new Exception()) },
                                                     new List<object>(){(object)Hwdtech.IoC.Resolve<IStartegy>("SpaceShip.Lib.Strategy.TestStrategy", new object[]{}), (object)(new TypeLoadException()) }};
        var error_types = new List<object>(){ "Err of test strategy", "Error for 2 throw" };
        error_tester.teach(list_of_data, error_types);

        var testing_strategy = Hwdtech.IoC.Resolve<IStartegy>("SpaceShip.Lib.Strategy.TestStrategy", new object[] { });
        var save_strategy = Hwdtech.IoC.Resolve<IStartegy>("SpaceShip.Lib.Strategy.SaveStrategy", (object)testing_strategy, (object)error_tester);
        Assert.Equal((object)error_types[0], save_strategy.execute(1, 0));
        Assert.Equal(1, save_strategy.execute(1, 1));
        Assert.Equal((object)error_types[1], save_strategy.execute(2, 3));
        Assert.NotEqual((object)error_types[1], save_strategy.execute(1, 0));
    }

    [Fact]
    public void check_for_unregistred_errors()
    {
        Init_Score_Env_Laba5();
        Func<object, object> func_for_test = (object z) => z.GetType();
        var error_tester = Hwdtech.IoC.Resolve<Tree>("SpaceShip.Lib.Get.Tree", func_for_test);
        var list_of_data = new List<List<object>>(){ new List<object>(){(object)Hwdtech.IoC.Resolve<IStartegy>("SpaceShip.Lib.Strategy.TestStrategy", new object[]{}), (object)(new Exception()) },
                                                     new List<object>(){(object)Hwdtech.IoC.Resolve<IStartegy>("SpaceShip.Lib.Strategy.TestStrategy", new object[]{}), (object)(new TypeLoadException()) }};
        var error_types = new List<object>() { "Err of test strategy", "Error for 2 throw" };
        error_tester.teach(list_of_data, error_types);

        var testing_strategy = Hwdtech.IoC.Resolve<IStartegy>("SpaceShip.Lib.Strategy.TestStrategy", new object[] { });
        var save_strategy = Hwdtech.IoC.Resolve<IStartegy>("SpaceShip.Lib.Strategy.SaveStrategy", (object)testing_strategy, (object)error_tester);
        Assert.Equal(null, save_strategy.execute(2, 0));
    }
}
