using Blacklite.Framework.Metadata.Metadatums;
using System;

namespace Blacklite.Framework.Metadata.Storage
{
    public interface IMetadataStore
    {
        bool Has(IMetadata parentMetadata, Type metadatum);

        IMetadatum Get(IMetadata parentMetadata, Type metadatum);

        void Save(IMetadata parentMetadata, Type metadatum, IMetadatum value);

        bool Has<T>(IMetadata parentMetadata) where T : IMetadatum;

        T Get<T>(IMetadata parentMetadata) where T : IMetadatum;

        void Save<T>(IMetadata parentMetadata, T metadatum) where T : IMetadatum;
    }
}
