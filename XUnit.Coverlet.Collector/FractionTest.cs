using Xunit;
// using Moq;
using Xunit;
using SaceShips.Lib.Classes;
using SaceShips.Lib.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;
using System.Diagnostics;

namespace XUnit.Coverlet.Collector;

public class FractTest
{
    [Fact]
    public void FractCreate1Test()
    {
        var f = new Fraction(It.IsAny<int>(), It.IsAny<int>());
    }

    [Fact]
    public void FractCreat21Test()
    {
        double a = 0.1415926535897931;
        double delta = 0.000000001;
        var f = new Fraction(a, delta);
        Assert.True(a - (double)f.GetUp() / (double)f.GetDown() < delta);
    }

    [Fact]
    public void FractCreat22Test()
    {
        double a = 0.01;
        double delta = 0.01;
        var f = new Fraction(a, delta);
        Assert.Equal((double)f.GetUp() / (double)f.GetDown(), a);
        Assert.True(a - (double)f.GetUp() / (double)f.GetDown() < delta);
    }

    [Fact]
    public void FractCreat23Test()
    {
        double a = 0.999;
        double delta = 0.01;
        var f = new Fraction(a, delta);
        // Assert.Equal((double)f.GetUp() / (double)f.GetDown(), a);
        Assert.True(a - (double)f.GetUp() / (double)f.GetDown() < delta);
    }

    [Fact]
    public void FractCreat24Test()
    {
        double a = 0.999;
        double delta = 0.01;
        var f = new Fraction(a, delta);
        Assert.True(a - (double)f.GetUp() / (double)f.GetDown() < delta);
    }
    [Fact]
    public void FractCreat25Test()
    {
        double a = 3.04;
        double delta = 0.01;
        var f = new Fraction(a, delta);
        Assert.True(a - (double)f.GetUp() / (double)f.GetDown() < delta);
    }

    [Fact]
    public void FractSumfTest()
    {
        int a1 = 2;
        int b1 = 5;
        int a2 = 3;
        int b2 = 4;
        var f1 = new Fraction(a1, b1);
        var f2 = new Fraction(a2, b2);
        var f_real = f1 + f2;
        var expected = new Fraction(23, 20);
        Assert.Equal(f_real, expected);
        Assert.Equal(f_real.GetUp(), expected.GetUp());
        Assert.Equal(f_real.GetDown(), expected.GetDown());
    }

    [Fact]
    public void FractMulTest()
    {
        int a1 = 2;
        int b1 = 5;
        int a2 = 3;
        int b2 = 4;
        var f1 = new Fraction(a1, b1);
        var f2 = new Fraction(a2, b2);
        var f_real = f1 * f2;
        var expected = new Fraction(6, 20);
        // Assert.Equal(f_real, expected);
        Assert.Equal(f_real.GetUp(), expected.GetUp());
        Assert.Equal(f_real.GetDown(), expected.GetDown());
    }

    [Fact]
    public void FractDifTest()
    {
        int a1 = 2;
        int b1 = 5;
        int a2 = 3;
        int b2 = 4;
        var f1 = new Fraction(a1, b1);
        var f2 = new Fraction(a2, b2);
        var f_real = f1 - f2;
        var expected = new Fraction(-7, 20);
        Assert.Equal(f_real.GetUp(), expected.GetUp());
        Assert.Equal(f_real.GetDown(), expected.GetDown());
    }

    [Fact]
    public void FracUnequalTest()
    {
        int a1 = 2;
        int b1 = 5;
        int a2 = 3;
        int b2 = 4;
        var f1 = new Fraction(a1, b1);
        var f2 = new Fraction(a2, b2);
        Assert.True(f1 != f2);
    }

    [Fact]
    public void FracEqualTest()
    {
        int a1 = 2;
        int b1 = 5;
        int a2 = 2;
        int b2 = 5;
        var f1 = new Fraction(a1, b1);
        var f2 = new Fraction(a2, b2);
        Assert.True(f1 == f2);
    }

    [Fact]
    public void FracUnequalTest2()
    {
        int a1 = 2;
        int b1 = 5;
        var f1 = new Fraction(a1, b1);
        Assert.False(f1.Equals(a1));
    }

    [Fact]
    public void FractHashTest()
    {
        int a1 = 4;
        int b1 = 10;
        var f1 = new Fraction(a1, b1);
        int a2 = 4;
        int b2 = 10;
        var f2 = new Fraction(a2, b2);
        Assert.Equal(f1.GetHashCode(), f2.GetHashCode());
    }
}

// dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/*/coverage.cobertura.xml
