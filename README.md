# Installation
Make sure you have the .NET 6 SDK installed and dotnet as a path variable in your system. Download it in the following link: [https://dotnet.microsoft.com/en-us/download/dotnet/6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

The first step is to install the package dependencies in both projects by opening the terminal in ./import_test and running: **dotnet restore**

# Running the code
## Import script
Before executing the script make sure the appsettings is set up correctly. Make sure the Capterra and SoftwareAdvice attributes target the desired file path. Both files are located in: ./feed-products

Once the appsettings file is set up, open the terminal in ./import_saas folder and execute **dotnet run**

## Unit tests
Open the terminal in ./import_test folder and execute **dotnet test**

# Summary
For the coding assesment I chose to work with tools I am already familiar with. The script is a .NET 6 console app and the unit testing framework used is XUnit.

The 3rd party libraries used are:
1. AutoMapper - To transform one class to another
2. Newtonsoft.Json - To parse the softwareadvice.json file
3. YamlDotNet - To parse de capterra.yaml file
4. Moq - To mock classes for unit tests

The main goal of the code base is to make it as flexible as possible in case a module changes or any new sources of data are added (like the one stated in the assignment). With this goal in mind I tried creating a code base as decoupled as possible. For example when the database changes we can focus on changing just the Data Access layer and as long as we implement it using the IDbService interface, the script should work correctly.

When the third provider is added we will need to create a new Importer class which implements the IImporter interface. We might need to create new models and mappings to the database model class *Product* depending on the data structure of the .csv file. 

If I had more time I would implement a better logging functionality. I would also implement the logic to download the files and keep track of which files have been downloaded to avoid downloading the same file again and again. 