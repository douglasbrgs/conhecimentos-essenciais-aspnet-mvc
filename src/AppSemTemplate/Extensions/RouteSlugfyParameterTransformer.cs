using System.Text.RegularExpressions;

namespace AppSemTemplate.Extensions
{
    public class RouteSlugfyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value is null)
            {
                return null;
            }

            // "([a-z])([A-Z])" expressão usada para encontrar um caractere minusculo seguido de um maiusculo
            // "$1-$2" insere um hifen entre os dois grupos (minuscula e maiuscula)

            return Regex.Replace(
                value.ToString()!,
                "([a-z])([A-Z])",
                "$1-$2",
                RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(100)
                ).ToLowerInvariant();
        }
    }
}