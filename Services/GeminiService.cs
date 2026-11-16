
using Google.GenAI;
using Microsoft.Extensions.Configuration;

namespace DevNotesAI.Services
{
    public class GeminiService
    {
        private readonly Client _client;

        public GeminiService(IConfiguration configuration)
        {
            string? apiKey = configuration["Gemini:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new InvalidOperationException(
                    "Gemini API key is missing from User Secrets.");
            }

            _client = new Client(apiKey: apiKey);
        }

        public async Task<string> ExplainCodeAsync(
            string code,
            string language)
        {
            string prompt = $"""
                You are a helpful programming tutor.
                Explain the following {language} code in simple language.

                Include:
                1. The purpose of the code.
                2. A step-by-step explanation.
                3. Important functions, methods, or classes.
                4. Any errors or improvements you notice.

                Code:
                {code}
                """;

            var response = await _client.Models.GenerateContentAsync(
                model: "gemini-3.5-flash-lite",
                contents: prompt);

            return response.Text ?? "Gemini returned an empty response.";

        }


        public async Task<string> AnalyzeFunctionsAsync(
            string code,
            string language)
        {
            string prompt = $"""
        You are an expert programming code analyzer.

        Analyze the following {language} code and list every
        function or method you find.

        For each function or method, explain:
        1. Its name.
        2. Its purpose.
        3. Its parameters.
        4. Its return type.
        5. How it works.

        If no functions or methods are found, clearly say so.

        Code:
        {code}
        """;

            var response = await _client.Models.GenerateContentAsync(
                model: "gemini-3.5-flash-lite",
                contents: prompt);

            return response.Text ?? "No function analysis was returned.";

        }


        public async Task<string> GenerateDocumentationAsync(
            string code,
            string language)
        {
            string prompt = $"""
        You are a professional software documentation writer.

        Generate clear, well-organized documentation for the
        following {language} source code.

        Include these sections:
        1. Project or Code Overview
        2. Purpose and Main Features
        3. Classes and Functions
        4. Parameters and Return Values
        5. How the Code Works
        6. Example Usage
        7. Important Notes or Limitations

        Use simple language and accurate technical details.
        Do not invent features that are not present in the code.
        If a section does not apply, state that briefly.

        Source Code:
        {code}
        """;

            var response = await _client.Models.GenerateContentAsync(
                model: "gemini-3.5-flash-lite",
                contents: prompt);

            return response.Text ??
                "Gemini did not return any documentation.";
        }
    }
}