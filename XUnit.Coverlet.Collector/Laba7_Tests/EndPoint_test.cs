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
    public void EndPoint_test()
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
    public void EndPoint_test_if_null()
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
}
