using System.Text.Json;

namespace Rosered11.Common.Application
{
    public record ErrorDTO(string code, string? message)
    {
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}