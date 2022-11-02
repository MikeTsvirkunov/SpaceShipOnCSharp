using Xunit;
// using Moq;
using Xunit;
using SaceShips.Lib;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;

namespace XUnit.Coverlet.Collector;

public class FractTest
{
    [Fact]
    public void FractCreat1Test()
    {
        var f = new Fraction(It.IsAny<int>(), It.IsAny<int>());
    }

    [Fact]
    public void FractCreat2Test()
    {
        double a = 0.1415926535897931;
        var f = new Fraction(a);
        Assert.Equal((double)f.GetUp() / (double)f.GetDown(), 3.1415926535897931);
    }
}

// dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/*/coverage.cobertura.xml
