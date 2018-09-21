# Hair Salon - Skye Nguyen

##### Epicodus Independent Project C# Week 3 - 09/21/2018.

## Description

This is an app for a hair salon. The owner should be able to add a list of the stylists, and for each stylist, add clients who see that stylist. The stylists work independently, so each client only belongs to a single stylist.

## Behavior-driven Development

* A salon employee needs to be able to see a list of all our stylists.
* A salon employee needs to be able to select a stylist, see their details, and see a list of all clients that belong to that stylist.
* A salon employee needs to add new stylists to our system when they are hired.
* A salon employee needs to be able to add new clients to a specific stylist. I should not be able to add a client if no stylists have been added.


## Setup/Installation Requirements

1. Clone this repository:
```
    $ git clone https://github.com/sn31/HairSalon.Solution
```
2. Change into the work directory::
```
    $ cd HairSalon.Solution
```
3. To edit the project, open the project in your preferred text editor.

4. To run the tests, move into the Test directory and run this command:
```
    $ dotnet test
```

## Re-creating MySql Database on Mac

1. Open MAMP and hit Start Servers.

2. In the terminal, connect to MySql using your username and password.

```
    $ /Applications/MAMP/Library/bin/mysql --host=localhost -uroot -proot
```
3. In MySql, run these commands to create the database and add details:
```
    > CREATE DATABASE hair_salon;
    > USE hair_salon;
    > CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
    > CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id Int32);
```

## Known Bugs

None known in this version.

## Support and contact details

Please contact us at skye@dames.es for more information and/or feedback.

## Technologies Used

* C# FixFormat 0.0.71
* C#/.Net Core 1.1
* MySql
* MAMP
* Git
* GitHub

### License: MIT.

#### Copyright (c) 2018 Skye Nguyen
