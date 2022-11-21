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

public class RotateAbleObjTest
{
    [Fact]
    public void Test_Create_RotateableObject()
    {
       RotateableObject s = new RotateableObject(new Fraction(5, 2), new Fraction(5, 2));
    }
}