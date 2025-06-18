using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AppSemTemplate.Extensions
{
    [HtmlTargetElement("*", Attributes = "tipo-botao, route-id")]
    public class BotaoTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public BotaoTagHelper(IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator)
        {
            _contextAccessor = contextAccessor;
            _linkGenerator = linkGenerator;
        }

        [HtmlAttributeName("tipo-botao")]
        public TipoBotao TipoBotaoSelecao { get; set; }

        [HtmlAttributeName("route-id")]
        public int RouteId { get; set; }

        private string? actionName;
        private string? className;
        private string? iconClass;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            switch (TipoBotaoSelecao)
            {
                case TipoBotao.Detalhar:
                    actionName = "Details";
                    className = "btn btn-info";
                    iconClass = "fa fa-search";
                    break;
                case TipoBotao.Editar:
                    actionName = "Edit";
                    className = "btn btn-warning";
                    iconClass = "fa fa-pencil";
                    break;
                case TipoBotao.Excluir:
                    actionName = "Delete";
                    className = "btn btn-danger";
                    iconClass = "fa fa-trash";
                    break;
            }

            var controller = _contextAccessor.HttpContext?.GetRouteData().Values["controller"]?.ToString();

            // Protocolo + host
            var host = $"{_contextAccessor.HttpContext.Request.Scheme}://" +
                $"{_contextAccessor.HttpContext.Request.Host.Value}";

            var indexPath = _linkGenerator.GetPathByAction(
                    _contextAccessor.HttpContext,
                    actionName,
                    controller,
                    values: new { id = RouteId }
                )!;

            // Montando o link HTML
            output.TagName = "a";

            //output.Attributes.SetAttribute("href", $"{controller}/{actionName}/{RouteId}");
            output.Attributes.SetAttribute("href", $"{host}{indexPath}");
            output.Attributes.SetAttribute("class", className);

            // Adicionando o span do icone
            var iconSpan = new TagBuilder("span");
            iconSpan.AddCssClass(iconClass);

            output.Content.AppendHtml(iconSpan);
        }

        public enum TipoBotao
        {
            Detalhar = 1,
            Editar,
            Excluir
        }
    }
}
