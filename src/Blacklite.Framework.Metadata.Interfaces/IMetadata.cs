#if ASPNET50 || ASPNETCORE50
using Microsoft.Framework.Runtime;
#endif
using System;
using Blacklite.Framework.Metadata.Metadatums;

namespace Blacklite.Framework.Metadata
{
#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IMetadata
    {
        string Key { get; }

        T Get<T>() where T : class, IMetadatum;
    }
}
