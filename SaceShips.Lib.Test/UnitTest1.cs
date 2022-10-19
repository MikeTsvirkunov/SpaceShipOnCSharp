using Xunit;
using SaceShips.Lib;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;
namespace SaceShips.Lib.Test;

public class UnitTest1
{
    [Fact]
    public void Test_InterfaceObject()
    {
        var spaceobj = new Mock<InterfaceObject>();
        spaceobj.Setup(p => p.GetAllParams()).Returns(It.IsAny<ConcurrentDictionary<string, dynamic>>());
        spaceobj.Setup(p => p.GetParam(It.IsAny<string>())).Returns(It.IsAny<dynamic>());
        spaceobj.Setup(p => p.ParamExist(It.IsAny<string>())).Returns(It.IsAny<bool>());
    }
}