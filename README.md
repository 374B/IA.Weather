The structure of the application is roughly as follows:

	UI (.WEB project - MVC/JS)
	-> API
		-> Services
			?-> Infrastructure (providers etc)

There are two ways to run the API

- WebHost
- SelfHost

By default the Web application (front end) is configured to talk to the SelfHost API. The required projects for this should run automatically.

When the app starts a Swagger page will also be launched (if running in debug).

For the .API project the dependencies are set up in the .Bindings project
For the .WEB project the dependences are set up within Global.asax (as there are only a few trivial dependencies)

The .WEB project is fairly simple. All of the complexity is behind the .API.

Please note

- Some exception handling/logging is missing
- There are a few (minority) cases where models/DTOs should have been used but have not been
- All of the providers exist in one solution which would not be the normal case
