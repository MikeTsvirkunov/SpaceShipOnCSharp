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

public class MoveAbleObjTest
{
    [Fact]
    public void Test_Create_MoveableObject()
    {
        MoveableObject s = new MoveableObject(new Vector(12, 5), new Vector(12, 5));
    }
}
// cd XUnit.Coverlet.Collector/ 
// dotnet test --collect:"XPlat Code Coverage" && reportgenerator -reports:"TestResults/*/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html &&  rm -r TestResults/