# The Shop Project
At this project, I developed three services, where two of them are REST and one is gRPC.

The main service is a REST service called Homework.Enigmatry.Shop.API, which allows registered customers (with the role of customer) to get offers for articles with specified IDs and limits, and buy those articles. Admin users can view all articles, orders, delete orders, etc.

The vendor REST and gRPC services share the same logic, where they check their in-memory database and sales agents to determine if they have the articles needed by TheShop API.

![image](https://user-images.githubusercontent.com/36825550/231297406-a409e8a3-05a1-45f3-985c-22b62e910b0d.png)

As you can see, the solution has 16 projects and three folders. In the docs folder, there are specified scripts for migrating the database to your machine, instructions, detailed UML class diagrams, and some hybrid versions of activity diagrams.

![image](https://user-images.githubusercontent.com/36825550/231295781-9a2d6335-1b7a-4df6-9b88-8fb38a73fd7a.png)

Before you run the application, please take a look at the appsettings.json file. All the fine configurations are specified there. If you want to try the app with a persistent database (not an in-memory solution), set 'UseInMemory' to 'true' and check if the 'Data' property has the correct value. Other configurations are nice to have, but not too important to be changed.

Business logic notes:
- If you are using SQL persistence configuration section ('UseInMemory' is set to 'true'), orders will be created only for articles that are stored in TheShop inventory in the database.
- An article can be sold only once with the status 'IsDelete' set to 'false'. It can have just one order for an article with ID=1 and 'IsDelete' set to 'false' at any given time.
