# Introduction
This repository contains a sample implementation for an API Connector for the Talentech Evaluation API using ASP.NET Core. Run the project locally in order to view OpenAPI docs, which are served at the default route. 

Key terms
----------
- Evaluation API - The Talentech app partners will integrate with
- Partner App - The existing app managed by the partner.
- API Connector - A facade API controlled by the Partner. This is what will connect the Evaluation API and the Partner App's APIs. 
- Invitations - An invitation for a candidate. This will be sent from the Evaluation API to the API connector
- Results - Results after a candidate has completed an assessment test or reference check. This will be posted from the API connector and back to the Evaluation API
- ApiBaseUrl - The base url of the Api Connector. This is used to resolve the endpoints the Evaluation API expects the API connector to make available.

# What is needed to integrate
The endpoints below must be implemented by a partner in order to integrate with the Talentech Evaluation API. 
- Invitations endpoint
- Health check endpoint
- Evaluations endpoint
- Authorization endpoints (See the OAuth section below)

The relative URLs implemented by the partners can be found here:
https://github.com/Talentech/EvaluationApiSampleIntegration/blob/master/src/Config/Constants.cs

The partner should post results back to the EvaluationApi. In order to do this, an access token must be retrieved from a token server managed by Talentech. This token should be included in the authorization header with the api call to the EvaluationApi. A sample implementation of how this can be done in dotnet core can be found here:
https://github.com/Talentech/EvaluationApiSampleIntegration/blob/master/src/Services/Clients/EvaluationApi/EvaluationApiClient.cs

# OAuth
The OAuth2.0 authorization_code grant is supported by the Evaluation API and is a mandatory requirement for partners who wish to integrate. 

The Api connector should implement the following endpoints in the API connector:
- AuthorizationEndpoint 
- TokenEndpoint
- DeauthorizationEndpoint

These will typically be simple proxy endpoints that route the request on to a backend OAuth service managed by the partner. 

The workflow for granting the Evaluation API access to the PartnerApp's resources works as follows:

- When an ATS customer subscribes to a partner integration, we will need to get their authorization details in order to access their data in the Partner's API on their behalf. 
- This is done by redirecting an admin user from the customer to the Authorization endpoint (which is managed by the Partner).
- At the authorization endpoint, the customer's user will authenticate themselves, as well as grant the Talentech app the required permissions.
- Once done, the user must be redirected back to the EvaluationApi's redirect_uri. An authorization code should be included as a query string parameter.
- When the user hits the Evaluation API's redirect_uri endpoint, an API call will be made to exchange the authorization code for a token.
- Important: The token returned will be stored as-is by us, and will be used in all subsequent API calls. Therefore, the schema is controlled by the partner, and not by Talentech.

Below is an example authorization request. **Please note that the redirect_uri parameter will be url-encoded in the request**:

GET {AuthorizationEndpoint}   
?response_type=code   
&client_id={client-id}   
&redirect_uri={evaluation_api_redirect_uri}   
&state={random-state-parameter}   

Once authenticated, the user should be redirected using a request like this:

GET {evaluation_api_redirect_uri}   
?state={unchanged-state-parameter}   
&code={unique_authorization_code}   

A token request is then sent back to the Token endpoint by the EvaluationApi. The data posted in the call below should be in "application/x-www-form-urlencoded" format:

POST {TokenEndpoint}   
client_id={EvaluationApiClientId}&   
client_secret={EvaluationApiClientSecret}&   
grant_type=authorization_code&   
code={unique_authorization_code}&   
redirect_uri={evaluation_api_redirect_uri}

The {evaluation_api_redirect_uri} is a parameter that should be validated by the partner api. The URL is as follows:
https://evaluation.talentech.io/Customers/OAuth/Callback

# Error handling
Whenever an API call to the Api connector returns an HTTP error code, the EvaluationApi will try to deserialize the HTTP content to a given format. The error objects have a type parameter. 
- SystemErrors are meant for the Evaluation API to use internally.
- UserErrors may be shown to end users.

