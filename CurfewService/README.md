## Curfew Service
This is yet another tool for enforcing curfew on a computer. Rather than being triggered by the task scheduler though, this is designed to be a Windows Service, which I've found to be more reliable.

Additionally, I've hard coded the curfew boundaries in the ShutdownService file to 930pm -> 5am. I didn't put the values in config or take into account weekends, because I just don't really need anything more complicated right now.

## Installation
To use the service, you need to have [dotnet 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed.

To install, first publish from the project's directory:
- ```dotnet publish --output "C:\custom\publish\directory"```  
  
Then run powershell as an admin and create the service
- ```sc.exe create "Whatever you want to call the service" binpath="C:\wherever\the\executable\is.exe"```

The service should be created. At this point all you should need to do is navigate to "Services" (just search for the app, win + "services"), find the service named [whatever you called it in the last step], adjust properties as seen fit, and start the service.

I would suggest setting the Startup Type to "automatic"
