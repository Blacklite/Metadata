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
    class ControlTagHelperOptionsSetup : ConfigureOptions<ControlTagHelperOptions>
    {
        public ControlTagHelperOptionsSetup() : base(Configure)
        {
        }

        public static void Configure(ControlTagHelperOptions options)
        {

        }
    }
}