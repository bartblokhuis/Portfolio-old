# Portfolio

## General info
A portfolio where you can easily change the content using an admin panel build using Angular and .NET Core.

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
	
## Technologies
Project is created with:
* .NET Core 6.0
* Angular 12
* AdminLTE 3
	
## Setup

### API
To run this project, update the API's connection string in the appsettings.json it is also highly recommended to change the JWT secret.

### Admin panel
After launching the API you will need to access the admin panel to add some content to the portfolio.
First, go to the src/environment/environment.ts file and ensure that the baseApiUrl is the same as your API URL.
Now open a new terminal in the admin folder and run the following commands:
```
$ npm i
$ ng serve --port 4201
```
You should now be able to access the admin panel with the url localhost:4200  

The login details are:  
Username: Admin  
Password: Admin

### Public website
To start the public website first check again if the baseApiUrl in the environment.ts is the same as your API URL.
Now open a new terminal in the public folder and run the following commands:
```
$ npm i
$ npm run dev:ssr
```
