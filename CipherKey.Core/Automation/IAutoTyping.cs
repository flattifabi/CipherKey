namespace CipherKey.Core.Automation;

public interface IAutoTyping
{
    void AutoType(string username, string password);
    void CallWebsite(string websitePath);
}