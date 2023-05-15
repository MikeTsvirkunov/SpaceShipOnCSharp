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
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMassegeGetterExceptionHandler", (object[] args) => new DefaultExceptionHandler(new object())).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GameId.FromGameCommandMessage", (object[] args) =>  ((IGameIdContainer)args[0]).game_id).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Args.FromGameCommandMessage", (object[] args) => ((IArgContainer)args[0]).args).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.CommandName.FromGameCommandMessage", (object[] args) =>  ((ICommandNameContainer)args[0]).command_name).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Result.FromDefaultExceptionHandler", (object[] args) => "It\'s error").Execute();
        GameCommandMessage game_command_message_for_test = Hwdtech.IoC.Resolve<GameCommandMessage>("SpaceShip.Lib.Messages.GameCommandMessage");
        game_command_message_for_test.command_name = "cmd1";
        game_command_message_for_test.game_id = "game1";
        game_command_message_for_test.args = new List<string>(){ "game_object", "another_parametr" };
        GameCommandMessageGetter end_point_for_test = Hwdtech.IoC.Resolve<GameCommandMessageGetter>("SpaceShip.Lib.EndPoints.GameCommandMessageGetter");
        var results = (Dictionary<string, object>)end_point_for_test.get_message(game_command_message_for_test);
        Assert.Equal(game_command_message_for_test.command_name, results["command"]);
        Assert.Equal(game_command_message_for_test.game_id, results["game_id"]);
        Assert.Equal(game_command_message_for_test.args, results["args"]);
    }

    [Fact]
    public void EndPoint_test_error_on_game_id_check()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EndPoints.GameCommandMessageGetter", (object[] args) => new GameCommandMessageGetter()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Messages.GameCommandMessage", (object[] args) => new GameCommandMessage()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing", (object[] args) => new GameCommandMessagePreprocessingStrategy()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMassegeGetterExceptionHandler", (object[] args) => new DefaultExceptionHandler(new object())).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GameId.FromGameCommandMessage", (object[] args) => {throw new System.Exception("test exception thrown"); return new object();}).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Args.FromGameCommandMessage", (object[] args) => ((IArgContainer)args[0]).args).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.CommandName.FromGameCommandMessage", (object[] args) => ((ICommandNameContainer)args[0]).command_name).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Result.FromDefaultExceptionHandler", (object[] args) => "It\'s error").Execute();
        GameCommandMessage game_command_message_for_test = Hwdtech.IoC.Resolve<GameCommandMessage>("SpaceShip.Lib.Messages.GameCommandMessage");
        game_command_message_for_test.command_name = "cmd1";
        game_command_message_for_test.game_id = "game1";
        GameCommandMessageGetter end_point_for_test = Hwdtech.IoC.Resolve<GameCommandMessageGetter>("SpaceShip.Lib.EndPoints.GameCommandMessageGetter");
        var results = end_point_for_test.get_message(game_command_message_for_test);
        Assert.Equal(results, "It\'s error");
    }

    [Fact]
    public void EndPoint_test_error_on_command_name_check()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EndPoints.GameCommandMessageGetter", (object[] args) => new GameCommandMessageGetter()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Messages.GameCommandMessage", (object[] args) => new GameCommandMessage()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing", (object[] args) => new GameCommandMessagePreprocessingStrategy()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMassegeGetterExceptionHandler", (object[] args) => new DefaultExceptionHandler(new object())).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GameId.FromGameCommandMessage", (object[] args) => ((IGameIdContainer)args[0]).game_id).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.CommandName.FromGameCommandMessage", (object[] args) => { throw new System.Exception("test exception thrown"); return new object(); }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Args.FromGameCommandMessage", (object[] args) => ((IArgContainer)args[0]).args).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Result.FromDefaultExceptionHandler", (object[] args) => "It\'s error").Execute();
        GameCommandMessage game_command_message_for_test = Hwdtech.IoC.Resolve<GameCommandMessage>("SpaceShip.Lib.Messages.GameCommandMessage");
        game_command_message_for_test.command_name = "cmd1";
        game_command_message_for_test.game_id = "game1";
        GameCommandMessageGetter end_point_for_test = Hwdtech.IoC.Resolve<GameCommandMessageGetter>("SpaceShip.Lib.EndPoints.GameCommandMessageGetter");
        var results = end_point_for_test.get_message(game_command_message_for_test);
        Assert.Equal(results, "It\'s error");
    }

    [Fact]
    public void EndPoint_test_error_on_args_check()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EndPoints.GameCommandMessageGetter", (object[] args) => new GameCommandMessageGetter()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Messages.GameCommandMessage", (object[] args) => new GameCommandMessage()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing", (object[] args) => new GameCommandMessagePreprocessingStrategy()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMassegeGetterExceptionHandler", (object[] args) => new DefaultExceptionHandler(new object())).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GameId.FromGameCommandMessage", (object[] args) => ((IGameIdContainer)args[0]).game_id).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Args.FromGameCommandMessage", (object[] args) => { throw new System.Exception("test exception thrown"); return new object(); }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Result.FromDefaultExceptionHandler", (object[] args) => "It\'s error").Execute();
        GameCommandMessage game_command_message_for_test = Hwdtech.IoC.Resolve<GameCommandMessage>("SpaceShip.Lib.Messages.GameCommandMessage");
        game_command_message_for_test.command_name = "cmd1";
        game_command_message_for_test.game_id = "game1";
        GameCommandMessageGetter end_point_for_test = Hwdtech.IoC.Resolve<GameCommandMessageGetter>("SpaceShip.Lib.EndPoints.GameCommandMessageGetter");
        var results = end_point_for_test.get_message(game_command_message_for_test);
        Assert.Equal(results, "It\'s error");
    }

    [Fact]
    public void EndPoint_test_get_set_if_null()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EndPoints.GameCommandMessageGetter", (object[] args) => new GameCommandMessageGetter()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Messages.GameCommandMessage", (object[] args) => new GameCommandMessage()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing", (object[] args) => new GameCommandMessagePreprocessingStrategy()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMassegeGetterExceptionHandler", (object[] args) => new DefaultExceptionHandler(new object())).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GameId.FromGameCommandMessage", (object[] args) => ((IGameIdContainer)args[0]).game_id).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Args.FromGameCommandMessage", (object[] args) => ((IArgContainer)args[0]).args).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.CommandName.FromGameCommandMessage", (object[] args) => ((ICommandNameContainer)args[0]).command_name).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Result.FromDefaultExceptionHandler", (object[] args) => "It\'s error").Execute();
        GameCommandMessage game_command_message_for_test = Hwdtech.IoC.Resolve<GameCommandMessage>("SpaceShip.Lib.Messages.GameCommandMessage");
        GameCommandMessageGetter end_point_for_test = Hwdtech.IoC.Resolve<GameCommandMessageGetter>("SpaceShip.Lib.EndPoints.GameCommandMessageGetter");
        var results = (Dictionary<string, object>)end_point_for_test.get_message(game_command_message_for_test);
        Assert.Equal(game_command_message_for_test.command_name, results["command"]);
        Assert.Equal(game_command_message_for_test.game_id, results["game_id"]);
        Assert.Equal(game_command_message_for_test.args, results["args"]);
    }

    [Fact]
    public void EndPoint_test_set_to_queue_with_classes()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EndPoints.GameCommandMessageGetter", (object[] args) => new GameCommandMessageGetter()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Messages.GameCommandMessage", (object[] args) => new GameCommandMessage()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing", (object[] args) => new GameCommandMessagePreprocessingStrategy()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMassegeGetterExceptionHandler", (object[] args) => new DefaultExceptionHandler(new object())).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GameId.FromGameCommandMessage", (object[] args) => ((IGameIdContainer)args[0]).game_id).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Args.FromGameCommandMessage", (object[] args) => ((IArgContainer)args[0]).args).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.CommandName.FromGameCommandMessage", (object[] args) => ((ICommandNameContainer)args[0]).command_name).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Result.FromDefaultExceptionHandler", (object[] args) => "It\'s error").Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.ObjectIdContainer", (object[] args) => new ObjectIdContainer((Func<object, object, object>)args[0], (object)args[1])).Execute();
        var test_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command.Setup(p => p.action()).Verifiable();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "TestCMD", (object[] args) => test_command.Object).Execute();
        Func<object, object, object> func_gamequeue_search_in_idlist = (object id, object container) => ((Dictionary<string, BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>)container)[(string)id];
        var game1_queue = new BlockingCollection<SaceShips.Lib.Interfaces.ICommand>() { };
        var id_queuegame_dictionary = new Dictionary<string, BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>() { { "game1", game1_queue } };
        ObjectIdContainer id_game_container = Hwdtech.IoC.Resolve<ObjectIdContainer>("SpaceShip.Lib.Get.ObjectIdContainer", func_gamequeue_search_in_idlist, id_queuegame_dictionary);
        GameCommandMessage game_command_message_for_test = Hwdtech.IoC.Resolve<GameCommandMessage>("SpaceShip.Lib.Messages.GameCommandMessage");
        game_command_message_for_test.command_name = "TestCMD";
        game_command_message_for_test.game_id = "game1";
        game_command_message_for_test.args = new List<string>() { };
        GameCommandMessageGetter end_point_for_test = Hwdtech.IoC.Resolve<GameCommandMessageGetter>("SpaceShip.Lib.EndPoints.GameCommandMessageGetter");
        var results = (Dictionary<string, object>)end_point_for_test.get_message(game_command_message_for_test);
        var test_queue_get = (BlockingCollection<SaceShips.Lib.Interfaces.ICommand>)id_game_container.execute(results["game_id"]);
        var test_cmd_get = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>((string)results["command"]);
        Assert.Equal(test_cmd_get, test_command.Object);
        Assert.Equal(test_queue_get, game1_queue);
        test_queue_get.TryAdd(test_cmd_get);
        test_queue_get.Take().action();
        test_command.Verify(p => p.action(), Times.Once());
    }

    [Fact]
    public void EndPoint_test_set_to_queue()
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.EndPoints.GameCommandMessageGetter", (object[] args) => new GameCommandMessageGetter()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Messages.GameCommandMessage", (object[] args) => new GameCommandMessage()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMessagePreprocessing", (object[] args) => new GameCommandMessagePreprocessingStrategy()).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Strategies.GameCommandMassegeGetterExceptionHandler", (object[] args) => new DefaultExceptionHandler(new object())).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.GameId.FromGameCommandMessage", (object[] args) => ((IGameIdContainer)args[0]).game_id).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Args.FromGameCommandMessage", (object[] args) => ((IArgContainer)args[0]).args).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.CommandName.FromGameCommandMessage", (object[] args) => ((ICommandNameContainer)args[0]).command_name).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Result.FromDefaultExceptionHandler", (object[] args) => "It\'s error").Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Node", (object[] args) => new TreeNode((Func<object, object>)args[0])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Get.Tree", (object[] args) => new Tree((Func<object, object>)args[0])).Execute();
        var test_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        test_command.Setup(p => p.action()).Verifiable();
        Func<object, object> func_gamequeue_search = (object z) => z;
        var game1_queue = new BlockingCollection<SaceShips.Lib.Interfaces.ICommand>() { };
        var id_queuegame_container = new Dictionary<string, BlockingCollection<SaceShips.Lib.Interfaces.ICommand>>(){{"game1", game1_queue }};
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "TestCMD", (object[] args) => test_command.Object).Execute();
        GameCommandMessage game_command_message_for_test = Hwdtech.IoC.Resolve<GameCommandMessage>("SpaceShip.Lib.Messages.GameCommandMessage");
        game_command_message_for_test.command_name = "TestCMD";
        game_command_message_for_test.game_id = "game1";
        game_command_message_for_test.args = new List<string>() {};
        GameCommandMessageGetter end_point_for_test = Hwdtech.IoC.Resolve<GameCommandMessageGetter>("SpaceShip.Lib.EndPoints.GameCommandMessageGetter");
        var results = (Dictionary<string, object>)end_point_for_test.get_message(game_command_message_for_test);
        var test_queue_get = id_queuegame_container[(string)results["game_id"]];
        var test_cmd_get = Hwdtech.IoC.Resolve<SaceShips.Lib.Interfaces.ICommand>((string)results["command"]);
        Assert.Equal(test_cmd_get, test_command.Object);
        Assert.Equal(test_queue_get, game1_queue);
        test_queue_get.TryAdd(test_cmd_get);
        test_queue_get.Take().action();
        test_command.Verify(p => p.action(), Times.Once());
    }
}
