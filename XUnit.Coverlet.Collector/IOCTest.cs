using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
using IoC;

public class IOCTest
{
    [Fact]
    public void Test_IOC_create()
    {
        // var IIOC = new Mock<IIOC>();
        // IIOC.SetupGet(p => p.store).Returns(new Dictionary<string, Is>(45, 1)).Verifiable();
        // spaceship_w_speed_and_angle_parametrs.SetupGet(p => p.angleSpeed).Returns(new Fraction(90, 1)).Verifiable();
        // new RotateMove(spaceship_w_speed_and_angle_parametrs.Object).action();

        // spaceship_w_speed_and_angle_parametrs.VerifySet(m => m.angle = new Fraction(135, 1));
        // spaceship_w_speed_and_angle_parametrs.Verify();
        // Queue<ICommand> queue = new Queue<ICommand>();
        // var StartMoveCommand = new Mock<ICommand>();
        // var CommandThatNeedToBeEnded = new Mock<IMoveCommandEndable>();
        // StartMoveCommand.Setup(x => x.action()).Callback(() => {queue.Enqueue(queue.Dequeue());}).Verifiable();
        // queue.Enqueue(StartMoveCommand.Object);
        // queue.Peek().action();
        // Assert.True(queue.Contains(StartMoveCommand.Object));
        // queue.Enqueue(StartMoveCommand.Object);
        Queue<IMoveCommandEndable> queue = new Queue<IMoveCommandEndable>();

        var spaceship_w_speed_and_angle_parametrs = new Mock<IRotateable>();
        spaceship_w_speed_and_angle_parametrs.SetupGet(p => p.angle).Returns(new Fraction(45, 1)).Verifiable();
        spaceship_w_speed_and_angle_parametrs.SetupGet(p => p.angleSpeed).Returns(new Fraction(90, 1)).Verifiable();
        var move_command_endable = new Mock<IMoveCommandEndable>();
        move_command_endable.SetupGet(p => p.Obj).Returns(spaceship_w_speed_and_angle_parametrs.Object);
        move_command_endable.SetupGet(p => p.Cmd).Returns(new RotateMove(spaceship_w_speed_and_angle_parametrs.Object));
        move_command_endable.Setup(f => f.action()).Callback(() => {move_command_endable.Object.Cmd.action();});
        move_command_endable.Setup(f => f.inject(new NullCommand())).Callback(() => { move_command_endable.SetupSet(s => s.Cmd = new NullCommand());});
        // String name = ('IOC.SetupStrategy';
        IoC.IoC.Resolve<EndMoveCommand>("IoC.Default");
        // IoC.IoC.Resolve<ICommand>("IoC.SetupStrategy", "Spaceship.Lib.Commands.EndMoveCommand", new EndMoveCommand(queue, spaceship_w_speed_and_angle_parametrs.Object));
        ICommand x = new EndMoveCommand(queue, spaceship_w_speed_and_angle_parametrs.Object);
        queue.Enqueue(move_command_endable.Object);
        x.action();
    }
}