using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace SaceShips.Lib.Classes;

public class Runner
{
    byte[] compiled_assembly;
    public Runner(byte[] compiled_assembly){
        this.compiled_assembly = compiled_assembly;
    }

    public void Execute(byte[] compiledAssembly, string[] args)
    {
        var assemblyLoadContextWeakRef = LoadAndExecute(compiledAssembly, args);

        for (var i = 0; i < 8 && assemblyLoadContextWeakRef.IsAlive; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference LoadAndExecute(byte[] compiledAssembly, string[] args)
    {
        using (var asm = new MemoryStream(compiledAssembly))
        {
            var assemblyLoadContext = Assembly.Load(compiledAssembly);

            var entry = assembly.EntryPoint;

            _ = entry != null && entry.GetParameters().Length > 0
                ? entry.Invoke(null, new object[] { args })
                : entry.Invoke(null, null);

            assemblyLoadContext.Unload();

            return new WeakReference(assemblyLoadContext);
        }
    }
}
