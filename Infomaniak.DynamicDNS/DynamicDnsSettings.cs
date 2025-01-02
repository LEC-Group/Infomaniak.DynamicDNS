namespace Infomaniak.DynamicDNS;

public class DynamicDnsSettings
{
    public string Username 
    { 
        get;
        set;
    } = string.Empty;

    public string Password 
    {
        get; 
        set; 
    } = string.Empty;

    public string Hostname
    { 
        get; 
        set; 
    } = string.Empty;

    public int Interval
    {
        get;
        set;
    }
}