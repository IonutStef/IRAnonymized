# IRAnonymized-assignment

Demo API to import data from a CSV file.

## Getting Started

### Prerequisites

Docker (Optional) 

### Installing

* Start Docker
* Run Development file from PowerShell. 
It will start an instance of MongoDb and one of RabbitMQ.
Mogo will be fund at localhost:27017 (no authentication) 
RabbitMQ at localhost:15672 (guest - guest)

```
.\Development.ps1
```

* Configure *appsettings.json* from **IRAnonymized.Assignment.Consummer** and **IRAnonymized.Assignment.WebApi**
to match the needs. 
```
appOptions.LocalStorageSourceFolderPath - Local path to where the file will be saved at.

jsonSettings.DatabaseFilePath - Path to the file which will store the data.

mongoSettings.ConnectionString - Connection string for the MongoDb instance.
mongoSettings.DatabaseName - Name of the Database.
```

* Run **IRAnonymized.Assignment.Consummer** 
The console connects to RabbitMQ and consumes 

```
InportFileEvent
```

* Start IRAnonymized.Assignment.WebApi
It will open Swagger.

Post Requests to **api/Import** will upload a file to the configured **appOptions.LocalStorageSourceFolderPath** location and send an event to **IRAnonymized.Assignment.Consummer**, which will read the file, line by line, and insert the entries into database.

## Running the tests

Test were skipped. They can be found in IRAnonymized.Assignment.Services.UnitTests
