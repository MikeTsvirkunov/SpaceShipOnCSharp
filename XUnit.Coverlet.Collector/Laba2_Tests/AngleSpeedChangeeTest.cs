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
public class AngleSpeedChangeTest
{
    [Fact]
    public void Execute_SpeedChange_With_Anothe_With_UIObject()
    {
        var dict = new Dictionary<string, object>();
        var UObject = new Mock<IUObject>();
        UObject.Setup(x => x.set_property("angleSpeed", It.IsAny<Fraction>())).Callback<string, object>((string a, object z) => dict["angleSpeed"] = z);
        new AngleSpeedChange(UObject.Object, It.IsAny<Fraction>()).action();
        UObject.Setup(dict => dict.get_property("angleSpeed")).Returns(dict["angleSpeed"]).Verifiable();
        Assert.Equal(UObject.Object.get_property("angleSpeed"), dict["angleSpeed"]);
    }

    [Fact]
    public void Execute_SpeedChange_With_Another()
    {
        var UObject = new Mock<IUObject>();
        Assert.Throws<System.InvalidCastException>(() => new AngleSpeedChange(new object(), new Fraction(90, 1)).action());
    }
}
