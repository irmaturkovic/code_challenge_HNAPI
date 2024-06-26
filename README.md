﻿# HackerNewsAPI
HackerNewsAPI is a web application built with .NET and Angular 16 with following requirements:

1. A list of the newest stories
2. Each list item should include the title and a link to the news article. 
3. A search feature that allows users to find stories
4. A paging mechanism (on frontend side, I have some commented code how it would be implement od backend side)
5. Dependency injection
6. Caching of newest stories
7. Unit tests (I have implemented only two unit tests due to time constraints. Additional tests should be added in future iterations to ensure comprehensive test coverage. )


## Prerequisites
Before you begin, ensure you have met the following requirements:

.NET SDK installed on your machine.
Node.js installed on your machine.
Angular CLI installed globally on your machine (npm install -g @angular/cli).

## Running the Application
To run the application locally, follow these steps:

1. Clone the repository to your local machine:
git clone https://github.com/irmaturkovic/code_challenge_HNAPI.git

2. Run the application
from Visual Studio or dotnet run

3. Install the Angular dependencies:
cd HackerNewsUI/src npm install

4. Start Angular application
npm start 

Application is going to be run on http://localhost:4200/ (give it few seconds to load all data -> only first time when it's running after that data is cached)
Backed part is on https://localhost:7270/swagger/index.html

for getting newest stories : https://localhost:7270/api/HackerNewsItems?type=newstories
(I put 'type' since for getting top/best/newest stories always will be same http get request, but in my case I hardoced 'newest' since only that one si required)

## Additional Information
For more information about Angular, visit angular.io.
For more information about .NET, visit .NET Documentation.
