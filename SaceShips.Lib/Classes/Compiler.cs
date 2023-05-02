using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using SaceShips.Lib.Interfaces;
#nullable enable
namespace SaceShips.Lib.Classes;

public class Compiler: IStartegy
{
    List<Microsoft.CodeAnalysis.PortableExecutableReference> references;
    MemoryStream stream;
    CSharpParseOptions options;

    public Compiler(List<Microsoft.CodeAnalysis.PortableExecutableReference> references, MemoryStream stream, CSharpParseOptions options){
        this.references = references;
        this.stream = stream;
        this.options = options;
    }

    public object execute(params object[] args)
    {
            CSharpCompilation.Create("Hello.dll", new[] { SyntaxFactory.ParseSyntaxTree(SourceText.From((string)args[0]), options) }, references: references, options: new CSharpCompilationOptions(OutputKind.ConsoleApplication, optimizationLevel: OptimizationLevel.Release, assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default)).Emit(this.stream);
            this.stream.Seek(0, SeekOrigin.Begin);
            return this.stream.ToArray();
    }
}
