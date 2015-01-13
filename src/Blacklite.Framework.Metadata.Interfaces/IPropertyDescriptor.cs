#if ASPNET50 || ASPNETCORE50
using Microsoft.Framework.Runtime;
#endif
using System;
using System.Collections.Generic;

namespace Blacklite.Framework.Metadata
{
#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IPropertyDescriptor
    {
        IEnumerable<IPropertyDescriber> Describe(Type type);
    }
}
