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
public class GenObjectTest
{
    [Fact]
    public void get_list_of_IUObject_test()
    {
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        var generate_empty_iuobject_object = (object x) => {
            var test_iuobject = new Mock<IUObject>();
            test_iuobject.Setup(p => p.set_property("fuel", It.IsAny<Object>())).Verifiable();
            ((List<IUObject>)x).Add(test_iuobject.Object);
            return x;
        };
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Iterator.Step", (object[] args) => ((Func<System.Object, System.Object>)args[0])(args[1])).Execute();
        var test_iterator = new Iterator(new List<IUObject>() { }, generate_empty_iuobject_object);
        test_iterator.next();
        test_iterator.next();
        test_iterator.next();
        Assert.Equal(((List<IUObject>)test_iterator.position).Count(), 3);
    }
    [Fact]
    public void set_fuel_list_of_IUObject_test()
    {
        var game_scope = Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"));
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", game_scope).Execute();
        var test_list = new List<Mock<IUObject>>(){};
        var generate_empty_iuobject_object = (object x) =>
        {
            var test_iuobject = new Mock<IUObject>();
            test_iuobject.Setup(p => p.set_property("fuel", It.IsAny<Object>())).Verifiable();
            test_list.Add(test_iuobject);
            ((List<IUObject>)x).Add(test_iuobject.Object);
            return x;
        };
        var set_fuel = (object x) =>
        {
            ((List<IUObject>)x)[0].set_property("fuel", It.IsAny<Object>());
            return ((List<IUObject>)x).Skip<IUObject>(1).ToList();
        };
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SpaceShip.Lib.Iterator.Step", (object[] args) => ((Func<System.Object, System.Object>)args[0])(args[1])).Execute();
        var test_iterator_create_iuobject_list = new Iterator(new List<IUObject>() { }, generate_empty_iuobject_object);
        test_iterator_create_iuobject_list.next();
        test_iterator_create_iuobject_list.next();
        test_iterator_create_iuobject_list.next();
        var test_iterator_set_fuel = new Iterator(test_iterator_create_iuobject_list.position, set_fuel);
        test_iterator_set_fuel.next();
        test_iterator_set_fuel.next();
        test_iterator_set_fuel.next();
        test_list.ForEach(i => i.Verify(p => p.set_property("fuel", It.IsAny<Object>())));
    }
}
