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
using System.Diagnostics;
using System.Threading;
namespace XUnit.Coverlet.Collector;
public class IterGenTest
{
    [Fact]
    public void get_id_IUObject_fueld_pairs_test()
    {
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        var empty_command = new Mock<SaceShips.Lib.Interfaces.ICommand>();
        empty_command.Setup(p => p.action());
        var test_list = new List<Mock<IUObject>>() { };
        var generate_id_iuobject_fueled_paires = (object x) =>
        {
            var test_iuobject = new Mock<IUObject>();
            test_iuobject.Setup(p => p.set_property("fuel", It.IsAny<Object>())).Verifiable();
            test_iuobject.Setup(p => p.set_property("position", It.IsAny<Object>())).Verifiable();
            test_list.Add(test_iuobject);
            test_iuobject.Object.set_property("fuel", It.IsAny<Object>());
            test_iuobject.Object.set_property("position", It.IsAny<Object>());
            ((Dictionary<Object, IUObject>)x).Add(test_iuobject.Object.GetHashCode(), test_iuobject.Object);
            return x;
        };
        var stop_criterion = (object iter, object gen) =>
        {
            if (((Dictionary<Object, IUObject>)(((Iterator)iter).position)).Count() < 3)
            {
                ((Iterator)iter).next();
                return gen;
            }
            else return empty_command.Object;
        };
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Iterator.Step", (object[] args) => ((Func<System.Object, System.Object>)args[0])(args[1])).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.IterGeneratorCommand.Run", (object[] args) => ((Func<System.Object, System.Object, System.Object>)args[1])(args[0], args[2])).Execute();
        var test_iterator = new Iterator(new Dictionary<Object, IUObject>() { }, generate_id_iuobject_fueled_paires);
        var test_iter_generator_command = new IterGeneratorCommand(test_iterator, stop_criterion);
        test_iter_generator_command.action();
        Assert.Equal(3, ((Dictionary<Object, IUObject>)test_iterator.position).Count());
        test_list.ForEach(i => i.Verify(p => p.set_property("fuel", It.IsAny<Object>())));
        test_list.ForEach(i => i.Verify(p => p.set_property("position", It.IsAny<Object>())));
    }
}
