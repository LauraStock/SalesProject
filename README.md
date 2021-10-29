# SalesProject

Console Sales app project using C# and mySQL for QA Academy. Please note the working application is contained on the devlopment branhc of this github repository.

## Getting Started
1. Clone the repository and open
2. Checkout onto development branch 
3. Open MySQL workbench
4. Check the details of the MySQL connection match the information in SalesProject > Data > MySqlUtils.cs. 
5. To check the database connection, uncomment the first section of code in Program.cs
6. Once this is working, run Program.cs

## Features
The user can select to Create a sale in the database or Read from the database. 

Read options are to see for a specified time period:
* All sales
* Total sales
* Minimum
* Maximum price
* Average price 
* The number of sales

The time options are
* A single year
* A single month in a year
* A single day
* Between two years (inclusive)
* Between two months (inclusive)
* Between two dates (inclusive)

## Acknowledgments
The code for connecting to the MySQL database through C# is from Morgan from QA.

## Evaluation
This project had to be re-evaluated several times due to the differences in data types between MySQL and C# which resulted in different SQL queries being needed for viewing sales records for a particular year/month/day versus between two dates. In the future the easiest way to go about this would be to plan the functionality needed and work from the MySQL queries and connection in C# to the service, and then controller as manipulating the C# code to gather the data in the correct format is the easier part to redo. Overall this project was fairly successful but could do with some more refactoring to reduce the amount of code.

## Planning
The Jira board for this project can be found at https://tdp-demo.atlassian.net/jira/software/projects/QCSP/boards/2/roadmap
