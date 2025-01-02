# Infomaniak.DynamicDNS

Infomaniak.DynamicDNS allows you to associate a dynamic IP with a domain name with the infomaniak.ch service.

appsettings.json: 

    {
	  "DynamicDnsSettings": {
	    "Username": "myUsername",
	    "Password": "myPassword",
	    "Hostname": "dyndns.mydomain.ch",
	    "Interval": 15 
	  }
	}

*Interval in minutes.
