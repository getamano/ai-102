

using Microsoft.Extensions.Configuration;

using Azure;
using Azure.AI.TextAnalytics;

namespace DetectedLanguage_App {

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
        
        // Create client using endpoint and key
        TextAnalyticsClient aiClient = new TextAnalyticsClient(endpoint, credentials);

        string text = "ሰላም, ይህ አማን ነው";

        // Get language
        DetectedLanguage detectedLanguage = aiClient.DetectLanguage(text);
        Console.WriteLine($"\nLanguage: {detectedLanguage.Name}");

      } catch(Exception e) {
          Console.WriteLine(e.ToString());
      }

    }
  }

}