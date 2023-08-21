using System.Linq;
using System.Threading.Tasks;
using Standard.AI.OpenAI.Clients.OpenAIs;
using Standard.AI.OpenAI.Models.Configurations;
using Standard.AI.OpenAI.Models.Services.Foundations.Completions;

namespace POC.NuVerify.Brokers.OpenAIs
{
    internal class OpenAIBroker
    {
        public async ValueTask<string> ScanFilesAsync(string codeBase1, string codeBase2)
        {
            var openAiConfigurations =
                new OpenAIConfigurations
                {
                    ApiKey = "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"
                };

            var openAIClient = new OpenAIClient(openAiConfigurations);

            var inputCompletion = new Completion
            {
                Request = new CompletionRequest
                {
                    Prompts = new string[] {
                        "Respond ONLY with true or false." +
                        "Do you see any significant core functionality changes between these two code basis:" +
                        $"CODE BASE #1: {codeBase1}" +
                        $"CODE BASE #2: {codeBase2}" },

                    Model = "text-davinci-003"
                }
            };

            Completion resultCompletion =
                await openAIClient.Completions.PromptCompletionAsync(
                    inputCompletion);

            return resultCompletion.Response.Choices.FirstOrDefault()?.Text;
        }
    }
}
