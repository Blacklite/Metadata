using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using Microsoft.Framework.OptionsModel;
using System.Linq;
using System.Threading.Tasks;

namespace Blacklite.Framework.Metadata.TagHelpers
{
    public class ControlTagHelper : TagHelper
    {
        private const string ForAttributeName = "for";
        private const string ClassAttributeName = "class";
        private const string LabelClassAttributeName = "label-class";
        private const string ControlClassAttributeName = "control-class";
        private const string DescriptionClassAttributeName = "description-class";
        private const string HelpClassAttributeName = "help-class";
        private const string HorizontalAttributeName = "horizontal";

        public ControlTagHelper(IOptions<ControlTagHelperOptions> options, IControlGenerator generator) {
            _options = options;
            Generator = generator;
        }

        private IOptions<ControlTagHelperOptions> _options { get; set; }

        protected internal ControlTagHelperOptions Options { get { return _options.Options; } }

        // Protected to ensure subclasses are correctly activated. Internal for ease of use when testing.
        [ViewContext]
        protected internal ViewContext ViewContext { get; set; }

        // Protected to ensure subclasses are correctly activated. Internal for ease of use when testing.
        protected internal IControlGenerator Generator { get; set; }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [HtmlAttributeName(ClassAttributeName)]
        public string Class { get; set; }

        [HtmlAttributeName(LabelClassAttributeName)]
        public string LabelClass { get; set; }

        [HtmlAttributeName(ControlClassAttributeName)]
        public string ControlClass { get; set; }

        [HtmlAttributeName(DescriptionClassAttributeName)]
        public string DescriptionClass { get; set; }

        [HtmlAttributeName(HelpClassAttributeName)]
        public string HelpClass { get; set; }

        [HtmlAttributeName(HorizontalAttributeName)]
        public bool Horizontal { get; set; }

        /// <inheritdoc />
        /// <remarks>Does nothing if <see cref="For"/> is <c>null</c>.</remarks>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (For != null)
            {
                var attributes = context.AllAttributes.Select(x => x.Key).Union(output.Attributes.Select(x => x.Key));
                if (output.ContentSet)
                {
                    var childContent = await context.GetChildContentAsync();

                    if (!string.IsNullOrWhiteSpace(childContent))
                    {
                        output.Content = childContent;
                    }
                    else
                    {
                        output.Content = GetContainer(context, output);
                    }
                }
                else
                {
                    output.Content = GetContainer(context, output);
                }

                output.TagName = "div";

                output.Content += context.UniqueId + "<br />";
                output.Content += string.Join("<br />", attributes);
            }
        }

        private string GetContainer(TagHelperContext context, TagHelperOutput output)
        {
            var tagBuilder = Generator.GenerateContainer(ViewContext, For.Metadata, Options, For.Name, Class, LabelClass, ControlClass, DescriptionClass, HelpClass);
            tagBuilder.MergeAttributes(output.Attributes);
            return tagBuilder.InnerHtml;
        }
    }
}
