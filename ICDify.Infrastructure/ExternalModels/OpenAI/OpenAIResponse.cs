using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDify.Infrastructure.ExternalModels.OpenAI;

public class OpenAIResponse
{
    public List<Choice> Choices { get; set; } = new();
    public Usage Usage { get; set; }
    public string Model { get; set; }
}
