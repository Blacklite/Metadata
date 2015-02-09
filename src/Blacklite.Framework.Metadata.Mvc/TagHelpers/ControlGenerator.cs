using Blacklite.Framework;
using Blacklite.Framework.Metadata.Modeling.Metadatums;
using Blacklite.Framework.Metadata.Mvc;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.Rendering;
using System.Linq;

namespace Blacklite.Framework.Metadata.TagHelpers
{
    /// <summary>
    /// This interface is the interface that you can implement to customize how the controls will render.
    /// </summary>
    public interface IControlGenerator
    {
        TagBuilder GenerateContainer([NotNull] ViewContext viewContext, [NotNull] ModelMetadata modelMetadata, [NotNull] ControlTagHelperOptions options, string name, string classes, string labelClasses, string controlClasses, string descriptionClasses, string helpClasses, object htmlAttributes = null);

        TagBuilder GenerateLabel([NotNull] ViewContext viewContext, [NotNull] ModelMetadata modelMetadata, [NotNull] ControlTagHelperOptions options, string name, string classes = null, object htmlAttributes = null);

        TagBuilder GenerateControl([NotNull] ViewContext viewContext, [NotNull] ModelMetadata modelMetadata, [NotNull] ControlTagHelperOptions options, string name, string classes = null, object htmlAttributes = null);

        TagBuilder GenerateControlContainer([NotNull] ViewContext viewContext, [NotNull] ModelMetadata modelMetadata, [NotNull] ControlTagHelperOptions options, string name, string classes = null, object htmlAttributes = null);

        TagBuilder GenerateHelpText([NotNull] ViewContext viewContext, [NotNull] ModelMetadata modelMetadata, [NotNull] ControlTagHelperOptions options, string name, string classes = null, object htmlAttributes = null);

        TagBuilder GenerateDescription([NotNull] ViewContext viewContext, [NotNull] ModelMetadata modelMetadata, [NotNull] ControlTagHelperOptions options, string name, string classes = null, object htmlAttributes = null);
    }

    class DefaultControlGenerator : IControlGenerator
    {
        private readonly IHtmlGenerator _htmlGenerator;
        public DefaultControlGenerator(IHtmlGenerator htmlGenerator)
        {
            _htmlGenerator = htmlGenerator;
        }

        public static string GetClasses(ControlTagHelperOptions options, string classes, ControlTagItem item)
        {
            if (classes == null && !options.Classes[item].Any())
            {
                return string.Empty;
            }

            if (classes == null)
            {
                return string.Join(" ", options.Classes[item]);
            }

            if (!options.Classes[item].Any())
            {
                return classes;
            }

            return string.Join(" ", classes
                .Split(' ')
                .Union(options.Classes[item])
                .Distinct());
        }

        public TagBuilder GenerateContainer(ViewContext viewContext, ModelMetadata modelMetadata, ControlTagHelperOptions options, string name, string classes, string labelClasses, string controlClasses, string descriptionClasses, string helpClasses, object htmlAttributes = null)
        {
            var metadata = modelMetadata.AsMetadata();
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var builder = new TagBuilder("div");
            builder.AddCssClass(GetClasses(options, classes, ControlTagItem.Container));

            var label = GenerateLabel(viewContext, modelMetadata, options, name, labelClasses);
            var controlContainer = GenerateControlContainer(viewContext, modelMetadata, options, name, controlClasses);
            // bootstrap
            var controlCls = string.Join(" ", (controlClasses ?? string.Empty).Split(' ').Where(x => !x.StartsWith("col-")));
            // /bootstrap
            var control = GenerateControl(viewContext, modelMetadata, options, name, controlCls);
            var description = GenerateDescription(viewContext, modelMetadata, options, name, descriptionClasses);

            controlContainer.InnerHtml = control.ToString(TagRenderMode.SelfClosing);
            builder.InnerHtml = label.ToString();
            builder.InnerHtml += controlContainer.ToString();
            builder.InnerHtml += description.ToString();

            return builder;
        }

        public TagBuilder GenerateControlContainer(ViewContext viewContext, ModelMetadata modelMetadata, ControlTagHelperOptions options, string name, string classes = null, object htmlAttributes = null)
        {
            var metadata = modelMetadata.AsMetadata();
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var builder = new TagBuilder("div");

            classes = GetClasses(options, classes, ControlTagItem.ControlContainer);

            builder.AddCssClass(classes);
            return builder;
        }

        public TagBuilder GenerateControl(ViewContext viewContext, ModelMetadata modelMetadata, ControlTagHelperOptions options, string name, string classes = null, object htmlAttributes = null)
        {
            var metadata = modelMetadata.AsMetadata();
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var builder = _htmlGenerator.GenerateTextBox(viewContext, modelMetadata, name, modelMetadata.Model, modelMetadata.EditFormatString, htmlAttributes);

            builder.Attributes.Add("placeholder", metadata.Get<DisplayName>());
            classes = GetClasses(options, classes, ControlTagItem.Control);

            builder.AddCssClass(classes);
            return builder;
        }

        public TagBuilder GenerateDescription(ViewContext viewContext, ModelMetadata modelMetadata, ControlTagHelperOptions options, string name, string classes = null, object htmlAttributes = null)
        {
            var metadata = modelMetadata.AsMetadata();
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var builder = new TagBuilder("div");

            classes = GetClasses(options, classes, ControlTagItem.Description);

            builder.AddCssClass(classes);
            return builder;
        }

        public TagBuilder GenerateHelpText(ViewContext viewContext, ModelMetadata modelMetadata, ControlTagHelperOptions options, string name, string classes = null, object htmlAttributes = null)
        {
            var metadata = modelMetadata.AsMetadata();
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var builder = new TagBuilder("div");

            classes = GetClasses(options, classes, ControlTagItem.Help);

            builder.AddCssClass(classes);
            return builder;
        }

        public TagBuilder GenerateLabel(ViewContext viewContext, ModelMetadata modelMetadata, ControlTagHelperOptions options, string name, string classes = null, object htmlAttributes = null)
        {
            var metadata = modelMetadata.AsMetadata();
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var builder = _htmlGenerator.GenerateLabel(viewContext, modelMetadata, name, metadata.Get<DisplayName>(), htmlAttributes);

            if (metadata.Get<Required>())
                builder.InnerHtml += "*";

            classes = GetClasses(options, classes, ControlTagItem.Label);

            builder.AddCssClass(classes);
            return builder;
        }
    }
}