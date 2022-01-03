This project contains a dotnet 6 based asp.net core web api project.

In order to run the project sucessfully you first need to install the databases.
These are SQL server databases. There is a "prod" database and a "test" database.
Run the scripts in sql server management studio in your local machine.
Once you have run them so that the connection string matches the one in appsettings.json you are good to go.

Now you can open the solution in Visual Studio. I used 2022.
Rebuild and run it.
It should take you to the open api site.

There is an api endpoint for creating a booking for a particular car. That is the one you should start with.
Example registration numbers are in the Vehicles table.
Use one of those or else you will get an error back.
Enter your person number in the format YYYYMMDD-NNNN and for startdate 
you can go back a couple of days in order to make the closing of the booking easier.

Then hit send and if all goes good you should receive a booking number.

The second endpoint for closing the booking and getting the price back.

Use the booking number from the first endpoint and enter that.
Enter values for the endMileage. You can leave the endDate as it is.
Hit send and you should receive the booking number and total price in the response.