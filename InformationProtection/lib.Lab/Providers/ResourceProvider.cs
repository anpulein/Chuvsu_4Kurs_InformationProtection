namespace lib.Lab.Providers;

public class ResourceProvider
{
    public static string GetJsonSchema(string nameLab, string nameFile = "schemaHtml.json")
    {
        string path = Data.CurrentDirectory + $"{nameLab}/";
        string schemaFilePath = Path.Combine(path, nameFile);
        
        if (Directory.Exists(path) && File.Exists(schemaFilePath))
        {
            string jsonSchema = File.ReadAllText(schemaFilePath);
            return jsonSchema;
        }
        else
        {
            return null;
        }
    }
}