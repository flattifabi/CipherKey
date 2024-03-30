namespace CipherKey.Core.Automation;

public interface IAutoTypeConfiguration
{
    string WebPath { get; set; }
    bool OpenWebPath { get; set; }
}

public class AutoTypeConfiguration : IAutoTypeConfiguration
{
    public string WebPath { get; set; } = string.Empty;
    public bool OpenWebPath { get; set; } = true;
}