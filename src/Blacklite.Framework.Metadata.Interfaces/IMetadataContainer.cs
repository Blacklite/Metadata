#if ASPNET50 || ASPNETCORE50
using Microsoft.Framework.Runtime;
#endif
using System;
using System.Reflection;
using Blacklite.Framework.Metadata.Metadatums;

namespace Blacklite.Framework.Metadata
{
#if ASPNET50 || ASPNETCORE50
    [AssemblyNeutral]
#endif
    public interface IMetadataContainer
    {
        bool Has(IMetadata parentMetadata, Type metadatum);

        IMetadatum Get(IMetadata parentMetadata, Type metadatum);

        void Save(IMetadata parentMetadata, Type metadatum, IMetadatum value);

        bool Has<T>(IMetadata parentMetadata) where T : IMetadatum;

        T Get<T>(IMetadata parentMetadata) where T : IMetadatum;

        void Save<T>(IMetadata parentMetadata, T metadatum) where T : IMetadatum;
    }
}
