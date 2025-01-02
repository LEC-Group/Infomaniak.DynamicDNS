using Microsoft.Extensions.Options;

namespace Infomaniak.DynamicDNS;

public class DynamicDnsClient
{
    #region Constructor

    public DynamicDnsClient(DynamicDnsSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        Settings = settings;
    }

    public DynamicDnsClient(IOptions<DynamicDnsSettings> settings)
        : this(settings.Value)
    {
    }

    #endregion
    
    #region Properties

    public DynamicDnsSettings Settings
    {
        get;
        private set;
    }

    #endregion
    
    #region Methods

    public async Task<string> UpdateAsync()
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"https://infomaniak.com/nic/update?hostname={Settings.Hostname}&username={Settings.Username}&password={Settings.Password}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return content;
    } 

    #endregion
}