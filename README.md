# Introduction 
This is skeleton code for a domain service.

# Contains
1.	Solution layout - Feature based
2.	Controller example
3.	Interface example
4.	Logging service
5.	Swagger configured

# Build and Test
This includes System Tests that use Test Server.

# Getting Started
1.	Clone this repository to your local machine
2.	Load and build sln
3.	Close sln (otherwise next steps will fail)
4.	Execute `dotnet new -i C:\<REPO>\CodeSkeleton-WebApi`
5.  Execute `dotnet new apiskeleton --name <ProjectName without API i.e. Composer>  --port <port number for http (length 4)> --output C:\<REPO>\<Cloned Repo Directory>`
	- NOTE you will get notified of unable to replace the .git folder.
6.	Load sln
7.	Rebuild
8.	Run Tests

# Additional
To setup Azure DevOps / pipeline etc, visit https://dev.azure.com/cndlfntdkngdm/DevOps/_wiki/wikis/DevOps.wiki/298/Create-New-Repository

To Uninstall a template execute `dotnet new -u C:\<REPO>\CodeSkeleton-WebApi`