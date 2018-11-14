The Application is built on .NET core and Mongo Db

>net core
Software : Microsoft Visual Studio 2017 Enterprise Edition, Postman and Robomongo

There are 5 methods as expected i.e CRUD and on additional method for returning only HTML. Read has an option get data
with or without ID. Repository pattern has been used for development.
In DbModels folder you can find settings  or information used for connection with MongoDb. Outputs were verified on 
Postman software

Validator
As it was suggested a valid html should only contain valid HTml(so that it could be loaded properly), a validator has
been introduced at insert and update. Only valid html can be inserted in Db. The html validations is done using 
HTML agility package and it is only done for Tags not formed properly. However, more validations could be done 
according to HTML 5 specifications. For that purpose I have tried a Html Validatior software (Installing it locally) 
and writing a wrapper class to communicate with it. Though it solved purpose but WEB Api had dependancy on local
running sofwtare.

Logger
For logging error Log4net package is used and basic implementation is done. For purpose of this project only Error file 
is created in project folder.If you want to change location , you can do it in log4net.config file and change 
"File Value" to your local folder.

Sample path to get data "https://localhost:44310/api/banner/getbanner/"

Package Used
MongoDb driver(for connecting Mongo DB )
Html Agility Pack(as a valiadtor)
log4net(logging)


Test case has been done written for some Status Code, dependency Injection and for the  HTml Validator.
Nuget Packages Used 
Fluent Assertions
MOQ
MS Test Framework

 

    

 