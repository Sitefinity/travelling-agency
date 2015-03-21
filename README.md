Travelling agency
=================

[![Build Status](http://sdk-jenkins-ci.cloudapp.net/buildStatus/icon?job=Telerik.Sitefinity.Samples.TravellingAgency.CI)](http://sdk-jenkins-ci.cloudapp.net/job/Telerik.Sitefinity.Samples.TravellingAgency.CI/)

The Travelling Agency demo project utilizes the multiple child types feature in Sitefinity. Sitefinity 7.1 enables you to create complex hierarchies data structures using the multiple dynamic module types. 

The demo site contains a list of countries, cities and specific hotels and restaurants in each city. The sample site demonstrates how you can take advantage of the new way hierarchical dynamic types are displayed in the front end.

### Requirements

* Sitefinity license
* .NET Framework 4
* Visual Studio 2012
* Microsoft SQL Server 2008R2 or later versions
* Windows Identity Foundation
   NOTE: Depending on the Microsoft OS version you are using, the method for downloading and installing or enabling the identity framework differs:

  * **Windows 7** - download from [Windows Identity Foundation](http://www.microsoft.com/en-us/download/details.aspx?id=17331)
  * **Windows 8** - in the Control Panel, turn on the relevant Windows feature Windows Identity Foundation 3.5* Windows Identity Foundation

### Prerequisites

Clear the NuGet cache files. To do this:

1. In Windows Explorer, open the **%localappdata%\NuGet\Cache** folder.
2. Select all files and delete them.

### Nuget package restoration
The solution in this repository relies on NuGet packages with automatic package restore while the build procedure takes place.   
For a full list of the referenced packages and their versions see the [packages.config](https://github.com/Sitefinity-SDK/travelling-agency/blob/master/TravellingAgency/packages.config) file.    
For a history and additional information related to package versions on different releases of this repository, see the [Releases page](https://github.com/Sitefinity-SDK/travelling-agency/releases).    


### Installation instructions: SDK Samples from GitHub

1. Extract and restore the database of the project ('TravellingAgency.zip') located in the App_Data folder.
1. In Solution Explorer, navigate to _SitefinityWebApp_ » *App_Data* » _Sitefinity_ » _Configuration_ and select the **DataConfig.config** file.
2. Modify the **connectionString** value to match your server address.
3. Build the solution.


### Login

To login to Sitefinity backend, use the following credentials:  
**Username:** admin  
**Password:** password

### Additional resources

**Introduction video:**  
Sitefinity dynamic modules multiple child types
[![Tooltip](https://raw.githubusercontent.com/Sitefinity-SDK/travelling-agency/master/TravellingAgency.png)](http://youtu.be/WCfc9GQoGoQ)


**Advanced tutorial:**  
Multiple child types per level in Sitefinity's dynamic modules
[![Tooltip](https://raw.githubusercontent.com/Sitefinity-SDK/travelling-agency/master/TravellingAgencyDemo2.png)](http://youtu.be/C2t_iT8OGDg)

**Sitefinity documentation:**   
[Tutorial: Create Travelling agency site with hierarchical types](http://docs.sitefinity.com/tutorial-create-travelling-agency-site-with-hierarchical-types)
