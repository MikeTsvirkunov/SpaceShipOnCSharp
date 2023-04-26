using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Text;
using System.Net;
using System.Diagnostics;
using Hwdtech;
using System.Linq;
namespace XUnit.Coverlet.Collector;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

public class EP
{
    [Fact]
    public void EndPoint_test_get_set()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EndPoints.GameCommandMessageGetter", (object[] args) => new GameCommandMessageGetter()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Messages.GameCommandMessage", (object[] args) => new GameCommandMessage()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing", (object[] args) => new GameCommandMessagePreprocessingStrategy()).Execute();

        GameCommandMessage game_command_message_for_test = Hwdtech.IoC.Resolve<GameCommandMessage>("SpaceShip.Lib.Messages.GameCommandMessage");
        game_command_message_for_test.command = "cmd1";
        game_command_message_for_test.game_id = "game1";
        game_command_message_for_test.args = new List<string>(){ "game_object", "another_parametr" };

        GameCommandMessageGetter end_point_for_test = Hwdtech.IoC.Resolve<GameCommandMessageGetter>("SpaceShip.Lib.EndPoints.GameCommandMessageGetter");
        var results = (Dictionary<string, object>)end_point_for_test.get_message(game_command_message_for_test);
        Assert.Equal(game_command_message_for_test.command, results["command"]);
        Assert.Equal(game_command_message_for_test.game_id, results["game_id"]);
        Assert.Equal(game_command_message_for_test.args, results["args"]);
    }

    [Fact]
    public void EndPoint_test_get_set_if_null()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EndPoints.GameCommandMessageGetter", (object[] args) => new GameCommandMessageGetter()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Messages.GameCommandMessage", (object[] args) => new GameCommandMessage()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing", (object[] args) => new GameCommandMessagePreprocessingStrategy()).Execute();

        GameCommandMessage game_command_message_for_test = Hwdtech.IoC.Resolve<GameCommandMessage>("SpaceShip.Lib.Messages.GameCommandMessage");

        GameCommandMessageGetter end_point_for_test = Hwdtech.IoC.Resolve<GameCommandMessageGetter>("SpaceShip.Lib.EndPoints.GameCommandMessageGetter");
        var results = (Dictionary<string, object>)end_point_for_test.get_message(game_command_message_for_test);
        Assert.Equal(game_command_message_for_test.command, results["command"]);
        Assert.Equal(game_command_message_for_test.game_id, results["game_id"]);
        Assert.Equal(game_command_message_for_test.args, results["args"]);
    }

    [Fact]
    public void EndPoint_test_set_to_queue()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EndPoints.GameCommandMessageGetter", (object[] args) => new GameCommandMessageGetter()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Messages.GameCommandMessage", (object[] args) => new GameCommandMessage()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Node", (object[] args) => new TreeNode((Func<object, object>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Tree", (object[] args) => new Tree((Func<object, object>)args[0])).Execute();


        var test_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        // var test_object = new object();
        Func<object, object> func_gamequeue_search = (object z) => z;
        var tree_queuegame_container = Hwdtech.IoC.Resolve<Tree>("SpaceShip.Lib.Get.Tree", func_gamequeue_search);
        var tree_object_container = Hwdtech.IoC.Resolve<Tree>("SpaceShip.Lib.Get.Tree", func_gamequeue_search);
        var queuegame_id_list = new List<object>(){ "game1" };
        // object_id_list = new List<string>() { "object1" };
        var queuegame_list = new List<object>() { new BlockingCollection<SaceShips.Lib.Interfaces.ICommand>(){} };
        // object_list = new List<object>(){ test_object };

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "TestCMD", (object[] args) => test_command.Object).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing", (object[] args) => new GameCommandMessagePreprocessingStrategy()).Execute();

        GameCommandMessage game_command_message_for_test = Hwdtech.IoC.Resolve<GameCommandMessage>("SpaceShip.Lib.Messages.GameCommandMessage");
        game_command_message_for_test.command = "TestCMD";
        game_command_message_for_test.game_id = "game1";
        game_command_message_for_test.args = new List<string>() {};

        GameCommandMessageGetter end_point_for_test = Hwdtech.IoC.Resolve<GameCommandMessageGetter>("SpaceShip.Lib.EndPoints.GameCommandMessageGetter");
        var results = (Dictionary<string, object>)end_point_for_test.get_message(game_command_message_for_test);
        var test_queue_get = (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)tree_queuegame_container.get_solution( new List<object>(){ (string)results["game_id"] } );
        var test_cmd_get = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>((string)results["command"]);
        test_queue_get.TryAdd(test_cmd_get);
        test_queue_get.Take().action();
    }
}
