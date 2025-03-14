namespace SmartFinanceAI.Domain;

using System;
using System.IO;
using System.Text.Json;

public class TranslationService
{
    private readonly JsonDocument _translations;

    public TranslationService()
    {
        var currentDirectory = AppContext.BaseDirectory;
        var jsonFilePath = Path.Combine(currentDirectory, "translations.json");

        if (!File.Exists(jsonFilePath))
        {
            throw new FileNotFoundException($"Translation file not found: {jsonFilePath}");
        }

        var json = File.ReadAllText(jsonFilePath);

        try
        {
            _translations = JsonDocument.Parse(json);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Invalid JSON format in translation file.", ex);
        }
    }

    public string Translate(string key, string language, params object[] args)
    {
        if (_translations.RootElement.TryGetProperty(language, out JsonElement languageElement))
        {
            string[] keyParts = key.Split('.');
            JsonElement currentElement = languageElement;

            foreach (string part in keyParts)
            {
                if (currentElement.TryGetProperty(part, out JsonElement nextElement))
                {
                    currentElement = nextElement;
                }
                else
                {
                    return key;
                }
            }

            string translation = currentElement.GetString()!;
            if (string.IsNullOrWhiteSpace(translation))
            {
                return key;
            }

            return args.Length > 0 ? string.Format(translation, args) : translation;
        }

        return key;
    }
}
