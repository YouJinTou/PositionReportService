# Trade Report Service
The solution can either be run as a console app or Windows service. It includes the following projects:
- Configuration -- handles read operations from the config.json file;
- ExternalAssemblies -- a folder to hold external assemblies for easier referencing;
- ConsoleService -- the application as a console application;
- Logging -- a logger that employs a different strategy based on what client we use;
- Reporting -- responsible for calling the API, aggregating the data, and persisting it as a .csv;
- Testing -- a small suite to test the functionality of Configuration and Reporting;
- WindowsService -- the application is a Windows service. It needs to be installed. To debug, attach to the service from VS.

I understand what I've done is probably overkill, but I wanted to push myself here.
