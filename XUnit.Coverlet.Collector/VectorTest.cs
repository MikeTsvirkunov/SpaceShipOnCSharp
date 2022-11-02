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

public class VectorTest
{
    [Fact]
    public void Create_empty_Vector_Test()
    {
        Vector v = new Vector(It.IsAny<int>());
    }

    [Fact]
    public void Create_fulled_Vector_Test()
    {
        Vector v = new Vector(new dynamic[] { It.IsAny<dynamic>(), It.IsAny<dynamic>(), It.IsAny<dynamic>() });
    }

    [Fact]
    public void Dif_Vector()
    {
        Vector v1 = new Vector(new dynamic[] { 1, 2, 1 });
        Vector v2 = new Vector(new dynamic[] { 3, -2, 1 });
        Vector expected = new Vector(new dynamic[] { -2, 4, 0 });
        Assert.True(v1 - v2 == expected);
    }

    [Fact]
    public void Del_Vector()
    {
        int n = 3;
        Vector v = new Vector(new dynamic[] { 3, -2, 1 });
        Vector expected = new Vector(new dynamic[] { 9, -6, 3 });
        Assert.True(n * v == expected);
    }

    [Fact]
    public void Eq_NotEq_VectorTest()
    {
        Vector v1 = new Vector(new dynamic[] { 3, -2, 1 });
        Vector v2 = new Vector(new dynamic[] { 9, -6, 3 });
        Vector v3 = new Vector(new dynamic[] { 3, -2, 1 });
        Assert.False(v1 == v2);
        Assert.True(v1 != v2);
        Assert.True(v1 == v3);
    }

    [Fact]
    public void MoreLessTest()
    {
        Vector v1 = new Vector(new dynamic[] { 3, -2, 1 });
        Vector v2 = new Vector(new dynamic[] { 9, -6, 3 });
        Vector v3 = new Vector(new dynamic[] { 3, -2, 1, 4 });
        Assert.False(v2 < v1);
        Assert.False(v3 < v1);
        Assert.True(v2 > v1);
        Assert.True(v1 < v3);
        Assert.True(v2 > v3);
    }

    [Fact]
    public void GetHashCodeTest()
    {
        Vector v = new Vector(It.IsAny<int>());
        v.GetHashCode();
    }

    [Fact]
    public void GetStringTest()
    {
        Vector v = new Vector(new dynamic[] { 3, -2, 1, 4 });
        string s = Convert.ToString(v);
        Assert.Equal(s, "Vector(3, -2, 1, 4)");
    }

    [Fact]
    public void SumUnequalTest()
    {
        Vector v2 = new Vector(new dynamic[] { 9, -6, 3 });
        Vector v3 = new Vector(new dynamic[] { 3, -2, 1, 4 });
        try
        {
            Vector x = v2 + v3;
            Debug.Fail("Unknown How");
        }
        catch (ArgumentException)
        {

        }
    }
}