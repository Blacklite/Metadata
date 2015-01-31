using Blacklite.Framework.Metadata.Modeling.Metadatums;
using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Mvc;
using Blacklite.Framework.Metadata.Mvc.Metadatums;

namespace Blacklite.Framework.Metadata.Mvc
{
    class BlackliteMvcModelMetadata : ModelMetadata
    {
        private static readonly string HtmlName = DataType.Html.ToString();
        private IMetadata _metadata;
        private IMetadataContainer _metadataContainer;

        public BlackliteMvcModelMetadata(
            IPropertyMetadata metadata,
            IScopedMetadataContainer metadataContainer,
            IModelMetadataProvider provider,
            Func<object> modelAccessor)
            : this(metadata, metadataContainer, provider, metadata.ParentMetadata.Type, modelAccessor, metadata.PropertyType, metadata.Name)
        {
        }

        public BlackliteMvcModelMetadata(
            ITypeMetadata metadata,
            IScopedMetadataContainer metadataContainer,
            IModelMetadataProvider provider,
            Func<object> modelAccessor)
            : this(metadata, metadataContainer, provider, null, modelAccessor, metadata.Type, null)
        {
        }


        public BlackliteMvcModelMetadata(
            IMetadata metadata,
            IScopedMetadataContainer metadataContainer,
            IModelMetadataProvider provider,
            Type containerType,
            Func<object> modelAccessor,
            Type modelType,
            string propertyName)
            : base(provider, containerType, modelAccessor, modelType, propertyName)
        {
            _metadata = metadata;
            _metadataContainer = metadataContainer;
            ConvertEmptyStringToNull = true;
            NullDisplayText = "(empty)";
        }
        public IMetadata Metadata { get { return _metadata; } }

        public override string DataTypeName
        {
            get
            {
                return _metadata.Get<InfoType>()?.Name ?? string.Empty;
            }

            set
            {
                _metadataContainer.Save(_metadata, new InfoType(value));
            }
        }

        public override string Description
        {
            get
            {
                return _metadata.Get<Description>();
            }

            set
            {
                _metadataContainer.Save(_metadata, new Description(value));
            }
        }

        public override string DisplayFormatString
        {
            get
            {
                return _metadata.Get<DisplayFormat>()?.Format;
            }

            set
            {
                _metadataContainer.Save(_metadata, new DisplayFormat(value, _metadata.Get<DisplayFormat>().HtmlEncode));
            }
        }

        public override string DisplayName
        {
            get
            {
                return _metadata.Get<DisplayName>();
            }

            set
            {
                _metadataContainer.Save(_metadata, new DisplayName(value));
            }
        }

        public override string EditFormatString
        {
            get
            {
                return _metadata.Get<EditFormat>()?.Format;
            }

            set
            {
                _metadataContainer.Save(_metadata, new EditFormat(value, _metadata.Get<EditFormat>().HtmlEncode));
            }
        }

        public override bool HasNonDefaultEditFormat
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_metadata.Get<EditFormat>().Format);
            }

            set
            {
                base.HasNonDefaultEditFormat = value;
            }
        }

        public override bool HtmlEncode
        {
            get
            {
                return IsReadOnly ? _metadata.Get<DisplayFormat>().HtmlEncode : _metadata.Get<EditFormat>().HtmlEncode;
            }

            set
            {
                if (IsReadOnly)
                {
                    _metadataContainer.Save(_metadata, new DisplayFormat(_metadata.Get<DisplayFormat>().Format, value));
                }
                else
                {
                    _metadataContainer.Save(_metadata, new EditFormat(_metadata.Get<EditFormat>().Format, value));
                }
            }
        }

        public override bool HideSurroundingHtml
        {
            get
            {
                return _metadata.Get<HiddenInput>();
            }

            set
            {
                _metadataContainer.Save(_metadata, new HiddenInput(value));
            }
        }

        public override int Order
        {
            get
            {
                return base.Order;
            }

            set
            {
                _metadataContainer.Save(_metadata, new Order(value));
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return _metadata.Get<ReadOnly>();
            }

            set
            {
                _metadataContainer.Save(_metadata, new ReadOnly(value));
            }
        }

        public override bool IsRequired
        {
            get
            {
                return _metadata.Get<Required>();
            }

            set
            {
                _metadataContainer.Save(_metadata, new Required(value));
            }
        }

        public override bool ShowForDisplay
        {
            get
            {
                return _metadata.Get<ShowForDisplay>();
            }

            set
            {
                _metadataContainer.Save(_metadata, new ShowForDisplay(value));
            }
        }

        public override bool ShowForEdit
        {
            get
            {
                return _metadata.Get<ShowForEdit>();
            }

            set
            {
                _metadataContainer.Save(_metadata, new ShowForEdit(value));
            }
        }
    }
}