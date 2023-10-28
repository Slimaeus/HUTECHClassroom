using System.Reflection;

namespace HUTECHClassroom.Persistence;

public static class AssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
