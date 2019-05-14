# TopazDB Core
The combined TopazDB stack, comprised of the following docker containers:

* topazdb/core - gateway for accessing both the GUI and API
* [topazdb/gui](https://github.com/topazdb/gui) - TopazDB's graphical user interface and API consumer
* [topazdb/api](https://github.com/topazdb/api) - TopazDB's REST API
* [topazdb/server](https://github.com/topazdb/server) - TopazDB's mysql backend

## Configuration Variables

### Environment
These configuration variables must be present on your system as environment variables.  It is recommended that they are exported in your .xprofile

#### TOPAZ_ENV
**Possible Values:** "production" or "development"<br>
Setting this to development will build Topaz from the source files and turn on auto-rebuilds for the GUI and API.  If set to production, the official images will be pulled from Docker Hub and used in place of the source files. 

#### TOPAZ_NOMOUNT
*Development Mode Only*<br>
**Possible Values:** true or false<br>
**Default Value:** false<br>
If true, Sunny will not be automatically mounted to your system when Topaz starts.  In production, it is assumed that Sunny will already be mounted to the host.  **

<br>

### Database/MySQL
Variables for controlling MySQL setup and connectivity.  These can be present as environment variables or placed in a .env file within the same directory as docker-compose\[.production\].yml.

#### TOPAZ_USER
**Possible Values:** any string<br>
**Example:** johndoe<br>
Database/mysql username to use.

#### TOPAZ_PASSWORD
**Possible Values:** any string<br>
**Example:** (UZg2]XxYnV&-CQ<br>
Database/mysql password to use. 

#### TOPAZ_DATABASE
**Possible Values:** any string<br>
**Example:** topazdb<br>
Database name to use within mysql

<br>

### Okta
Variables for integrating with Okta. These can be present as environment variables or placed in a .env file. TopazDB implements the [OpenID connect authorization code flow.](https://developer.okta.com/authentication-guide/implementing-authentication/auth-code/)

#### TOPAZ_OKTA_DOMAIN
**Possible Values:** URL in the form of protocol://hostname/path<br>
**Example:** example.oktapreview.com<br>
The Okta domain to use for SSO/Identity management.

#### TOPAZ_OKTA_CLIENTID
**Possible Values:** OAuth 2.0 Token/Identifier<br>
**Example:** 0oabv6kx4qq6h1U5l0h7<br>
The Client ID from Okta.  See [this page](https://developer.okta.com/docs/api/getting_started/finding_your_app_credentials/) for locating your client id.

#### TOPAZ_OKTA_CLIENTSECRET
**Possible Values:** OAuth 2.0 Token<br>
The Client Secret from Okta.  See [this page](https://developer.okta.com/docs/api/getting_started/finding_your_app_credentials/) for locating your client secret.

#### TOPAZ_OKTA_REDIRECTURI
**Possible Values:** URI/URL in the form of protocol://hostname/path<br>
**Example:** http://localhost/<br>
The URI/URL to redirect to after a successful sign-on.

#### TOPAZ_OKTA_AUTHSERV
**Possible Values:** any string<br>
**Default Value:** default<br>
The [Okta Authorization server](https://developer.okta.com/docs/api/resources/authorization-servers/) to use.

<br>

### Miscellaneous
Miscellaneous variables.  These can be present as environment variables or placed in a .env file.

#### TOPAZ_BASEURL
**Possible Values:** a URL in the form of protocol://hostname/path<br>
**Example:** http://localhost/<br>
The URL where TopazDB should be accessible at.