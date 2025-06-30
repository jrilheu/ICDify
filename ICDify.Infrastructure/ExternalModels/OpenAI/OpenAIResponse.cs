using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDify.Infrastructure.ExternalModels.OpenAI;

public class OpenAIResponse
{
    public string Id { get; set; }
    public string Model { get; set; }
    public string Output { get; set; } 
}
