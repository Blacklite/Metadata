using Blacklite.Framework;
using Blacklite.Framework.Metadata.Modeling.Metadatums;
using Blacklite.Framework.Metadata.Mvc;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.TagHelpers;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.OptionsModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Blacklite.Framework.Metadata.TagHelpers
{
    public class ControlTagHelperOptionsCssClassContainer
    {
        private readonly IDictionary<ControlTagItem, ICollection<string>> _items = new Dictionary<ControlTagItem, ICollection<string>>();
        public ICollection<string> this[ControlTagItem item]
        {
            get
            {
                ICollection<string> value;
                if (!_items.TryGetValue(item, out value))
                {
                    value = new Collection<string>();
                    _items.Add(item, value);
                }

                return value;
            }
        }
    }
}