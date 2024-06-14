using Azure;

using Microsoft.Extensions.Configuration;
using Azure.AI.TextAnalytics;

namespace Sentiment {
    class Program {
        private static string? aiEndpoint;
        private static string? aiKey;

        static void Main(string[] args) {

            
            try {
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
                 IConfigurationRoot configuration = builder.Build();

                aiEndpoint   = configuration["Endpoint"] ?? "";
                aiKey = configuration["Key"] ?? "";

                AzureKeyCredential credentials = new AzureKeyCredential(aiKey);
                Uri endpoint = new Uri(aiEndpoint);

                string document =
                    "I had the best day of my life. I decided to go sky-diving and it made me appreciate my whole life so"
                    + "much more. I developed a deep-connection with my instructor as well, and I feel as if I've made a"
                    + "life-long friend in her.";


                TextAnalyticsClient client = new TextAnalyticsClient(endpoint, credentials);
                Response<DocumentSentiment> response = client.AnalyzeSentiment(document);

                DocumentSentiment docSentiment = response.Value;

                Console.WriteLine($"Document sentiment is {docSentiment.Sentiment} with: ");
                Console.WriteLine($"  Positive confidence score: {docSentiment.ConfidenceScores.Positive}");
                Console.WriteLine($"  Neutral confidence score: {docSentiment.ConfidenceScores.Neutral}");
                Console.WriteLine($"  Negative confidence score: {docSentiment.ConfidenceScores.Negative}");

            }
            catch (Exception ex) {}
        }
    }    
}