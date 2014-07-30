Travelling agency
=================

The Travelling Agency demo project utilizes the multiple child types feature in Sitefinity. Sitefinity 7.1 enables you to create complex hierarchies data structures using the multiple dynamic module types. 

The demo site contains a list of countries, cities and specific hotels and restaurants in each city. The sample site demonstrates how you can take advantage of the new way hierarchical dynamic types are displayed in the front end.

### Requirements

* Sitefinity license

* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2012 or later versions

* Windows Identity Foundation

   NOTE: Depending on the Microsoft OS version you are using, the method for downloading and installing or enabling the identity framework differs:

  * Windows 7 - download from [Windows Identity Foundation](http://www.microsoft.com/en-us/download/details.aspx?id=17331)

  * Windows 8 - in the Control Panel, turn on the relevant Windows feature Windows Identity Foundation 3.5* Windows Identity Foundation

### Prerequisites

Clear the NuGet cache files. To do this:

1. In Windows Explorer, open the **%localappdata%\NuGet\Cache** folder.
2. Select all files and delete them.


### Installation instructions: SDK Samples from GitHub

1. Restore the data base of the project ('TravellingAgency.bak') located in root folder of the project.
2. In Solution Explorer, navigate to _SitefinityWebApp_ -> *App_Data* -> _Sitefinity_ -> _Configuration_ and select the **DataConfig.config** file. 
2. Modify the **connectionString** value to match your server address.
3. Build the solution.


### Login

To login to Sitefinity backend, use the following credentials: 

**Username:** admin

**Password:** password

### Additional resources

[Tutorial: Create Travelling Agency site](http://www.sitefinity.com/documentation/documentationarticles/tutorial-create-travelling-agency-site)
