using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
using Hwdtech;
using System.Linq;
namespace XUnit.Coverlet.Collector;

public class MacroCommandTest
{
    [Fact]
    public void Init_Score_Env_Laba_4_t2()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.EmptyICommandList", (object[] args) => new List<SaceShips.Lib.Interfaces.ICommand>()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.KeyPairStrategyParams", (object[] args) => (object)(new KeyValuePair<SaceShips.Lib.Interfaces.IStartegy, object[]>((SaceShips.Lib.Interfaces.IStartegy)args[0], (object[])args[1]))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.KeyPairStrategyParamsList", (object[] args) => new List<KeyValuePair<SaceShips.Lib.Interfaces.IStartegy, object[]>>((System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<SaceShips.Lib.Interfaces.IStartegy, object[]>>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.MacroStrategy", (object[] args) => new MacroStartegy((List<SaceShips.Lib.Interfaces.IStartegy>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Comands.MacroCommand", (object[] args) => new MacroCommand((List<SaceShips.Lib.Interfaces.ICommand>)args[0])).Execute();
    }

    [Fact]
    public void test_generate_strategy_list()
    {
        Init_Score_Env_Laba_4_t2();

        var cmd1 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd1.Setup(p => p.action()).Verifiable();
        var strategy1 = new Mock<IStartegy>();
        strategy1.Setup(p => p.execute(It.IsAny<IUObject>(), It.IsAny<object[]>())).Returns(cmd1.Object);

        var cmd2 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd2.Setup(p => p.action()).Verifiable();
        var strategy2 = new Mock<IStartegy>();
        strategy2.Setup(p => p.execute(It.IsAny<IUObject>(), It.IsAny<object[]>())).Returns(cmd2.Object);

        var cmd3 = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        cmd3.Setup(p => p.action()).Verifiable();
        var strategy3 = new Mock<IStartegy>();
        strategy3.Setup(p => p.execute(It.IsAny<IUObject>(), It.IsAny<object[]>())).Returns(cmd3.Object);


        var mass_of_checking_strategies = new List<SaceShips.Lib.Interfaces.IStartegy>() { strategy1.Object, strategy2.Object, strategy3.Object };
        var mass_of_args = new List<object[]>() { It.IsAny<object[]>(), It.IsAny<object[]>(), It.IsAny<object[]>() };
        var x = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.IStartegy>("SpaceShip.Lib.Comands.MacroStrategy", mass_of_checking_strategies).execute(It.IsAny<IUObject>(), mass_of_args);

        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.MacroCommand", x).action();
        cmd1.Verify();
        cmd2.Verify();
        cmd3.Verify();
    }

    [Fact]
    public void test_MacroCommand_with_empty_mass()
    {
        Init_Score_Env_Laba_4_t2();
        var mass_of_checking_cmds = new List<SaceShips.Lib.Interfaces.ICommand>();
        Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.MacroCommand", mass_of_checking_cmds).action();
    }

    [Fact]
    public void test_MacroCommand_with_incorrect_list()
    {
        Init_Score_Env_Laba_4_t2();
        Assert.Throws<System.InvalidCastException>(() => Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>("SpaceShip.Lib.Comands.MacroCommand", 1).action());
    }
}
