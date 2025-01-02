# Infomaniak.DynamicDNS

Infomaniak.DynamicDNS is a console application in .NET that allows you to associate a dynamic IP to a domain name with the infomaniak service.
More info here: (https://www.infomaniak.com/fr/support/faq/2357/decouvrir-dyndns-avec-un-domaine-infomaniak)

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
