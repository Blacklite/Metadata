using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blacklite.Framework.Metadata.Mvc
{
    class BlackliteMvcModelMetadataProvider : IModelMetadataProvider
    {
        private readonly IMetadataProvider _metadataProvider;
        private readonly IScopedMetadataContainer _metadataContainer;
        private readonly DataAnnotationsModelMetadataProvider _parameterProvider = new DataAnnotationsModelMetadataProvider();

        public BlackliteMvcModelMetadataProvider(IMetadataProvider metadataProvider, IScopedMetadataContainer metadataContainer)
        {
            _metadataProvider = metadataProvider;
            _metadataContainer = metadataContainer;
        }
        
        public IEnumerable<ModelMetadata> GetMetadataForProperties(object container, [NotNull] Type containerType)
        {
            var containerMetadata = _metadataProvider.GetMetadata(containerType);
            return GetMetadataForPropertiesCore(container, containerMetadata);
        }

        public ModelMetadata GetMetadataForProperty(Func<object> modelAccessor,
                                                    [NotNull] Type containerType,
                                                    [NotNull] string propertyName)
        {
            var containerMetadata = _metadataProvider.GetMetadata(containerType);
            var propertyMetadata = containerMetadata.Properties.Single(x => x.Name == propertyName);

            return CreatePropertyMetadata(modelAccessor, propertyMetadata);
        }

        public ModelMetadata GetMetadataForType(Func<object> modelAccessor, [NotNull] Type modelType)
        {
            var metadata = _metadataProvider.GetMetadata(modelType);
            return CreateMetadata(modelAccessor, metadata);
        }

        public ModelMetadata GetMetadataForParameter(
            Func<object> modelAccessor,
            [NotNull] MethodInfo methodInfo,
            [NotNull] string parameterName)
        {
            return _parameterProvider.GetMetadataForParameter(modelAccessor, methodInfo, parameterName);
        }

        private IEnumerable<ModelMetadata> GetMetadataForPropertiesCore(object container, ITypeMetadata metadata)
        {
            foreach (var property in metadata.Properties)
            {
                Func<object> modelAccessor = null;
                if (container != null)
                {
                    modelAccessor = () => property.GetValue<object>(container);
                }
                var propertyMetadata = CreatePropertyMetadata(modelAccessor, property);
                if (propertyMetadata != null)
                {
                    propertyMetadata.Container = container;
                }

                yield return propertyMetadata;
            }
        }


        // Override for applying the prototype + modelAccess to yield the final metadata
        private BlackliteMvcModelMetadata CreateMetadata(Func<object> modelAccessor, ITypeMetadata metadata)
        {
            return new BlackliteMvcModelMetadata(metadata, _metadataContainer, this, modelAccessor);
        }

        private BlackliteMvcModelMetadata CreatePropertyMetadata(Func<object> modelAccessor, IPropertyMetadata propertyMetadata)
        {
            return new BlackliteMvcModelMetadata(propertyMetadata, _metadataContainer, this, modelAccessor);
        }
    }
}