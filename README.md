There are two ways to run the API

- WebHost
- SelfHost

For the SelfHost you will be required to run VS as administrator or alternatively register the port (9000)

	CMD Prompt (Admin) >
		netsh http add urlacl url=http://+:9000/ user=DOMAIN\User listen=yes


To explain:

	Use of dynamics
	Exception handling