using System.Reflection;
using System.Runtime.Loader;



//Assembly assembly = Assembly.LoadFrom("SpAssamblyLibrary.dll");

//Console.WriteLine($"{assembly.FullName}");
//Type[] types = assembly.GetTypes();
//foreach (Type t in types)
//    Console.WriteLine($"{t.Name}");

LoadAndCalc(10);

GC.Collect();
GC.WaitForPendingFinalizers();

Console.WriteLine("\nCurrent Assemblies:");
foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
    Console.WriteLine($"\t{asm.GetName().Name}");


void LoadAndCalc(double number)
{
    AssemblyLoadContext context = new AssemblyLoadContext("LibAssambly", true);
    context.Unloading += Context_Unloading;

    var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "SpAssamblyLibrary.dll");
    var assembly = context.LoadFromAssemblyPath(assemblyPath);

    var mathLib = assembly.GetType("SpAssamblyLibrary.MathLib");

    if(mathLib is not null)
    {
        var squareMethod = mathLib.GetMethod("Square", BindingFlags.Public | BindingFlags.Static);
        var result = squareMethod?.Invoke(null, new object[] { number });

        if(result is double)
            Console.WriteLine($"Square {number} = {result}");
    }

    Console.WriteLine("\nCurrent Assemblies:");
    foreach(Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
        Console.WriteLine($"\t{asm.GetName().Name}");

    context.Unload();

}

void Context_Unloading(AssemblyLoadContext obj)
{
    Console.WriteLine($"\nAssembly context {obj.Name} unloading\n");
}