namespace CipherKey.Core.Models;

public class PublicApplicationConfiguration
{
    public int ID { get; set; } = 1;
    public string LastConnectedFilePath { get; set; } = string.Empty;
    public string LanguagePack { get; set; } = "en-EN";
    public string ThemeName { get; set; } = "Dark";
}