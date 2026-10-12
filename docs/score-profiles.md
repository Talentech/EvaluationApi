# Score Profiles

Score Profiles is a feature that facilitates returning results with multiple scores.

## Flow

### 1. ATS requests available evaluation forms for tenant

The ATS requests all available evaluation forms for the tenant. The context must be looked up by the partner by looking at the auth info and the triggeredBy info. The request carries `ProjectId` when the ATS sends one, otherwise it is `null`. Note the addition of the `ScoreProfiles` list to the schema:

#### EvaluationForms ScoreProfiles schema

| Field       | Description                                                                                      |
| ----------- | ------------------------------------------------------------------------------------------------ |
| Id          | Unique ID used for lookup. The ID should be one word without spaces.                             |
| Name        | Human readable name used in e.g. column headers.                                                 |
| Description | A longer description to explain the score                                                        |

#### Example EvaluationForms JSON structure

`POST {ApiBaseUrl}/evaluationforms`, a call to your API connector.

Request body:

```json
{
  "TriggeredBy": {
    "EncryptedFields": [],
    "Email": "recruiter@example.com",
    "FirstName": "string",
    "LastName": "string",
    "ExternalUserIdentifier": null
  },
  "Auth": {
    "CustomerAccountId": "string",
    "PartnerApiRefreshToken": "string"
  },
  "ProjectId": "string"
}
```

Response:

```json
[
  {
    "id": "string",
    "name": "string",
    "languages": [
      {
        "name": "string",
        "languageCode": "string"
      }
    ],
    "description": "string",
    "customFields": [
      {
        "id": "string",
        "label": "string",
        "type": "checkbox",
        "disabled": true,
        "value": false
      }
    ],
    "scoreProfiles": [
      {
        "id": "string",
        "name": "string",
        "description": "string"
      }
    ]
  }
]
```

### 2. ATS triggers invitation to a specific evaluationForm

An invitation is sent. This is always done in context of a projectId and for a specific evaluationForm. In terms of scoreProfiles, this request is unaltered.

#### Example Invitations JSON structure

`POST {ApiBaseUrl}/invitations`, a call to your API connector.

The payload is in [payload-examples/invitation.json](payload-examples/invitation.json).

### 3. Results posted back by the partner

The partner posts results back to the EvaluationAPI whenever there are updates. Results can be posted back 0 or more times per invitation.

The partner can post multiple scores back by using the `scoreProfiles`. Each score should map to a scoreProfile ID matching the available scoreProfiles indicated in the EvaluationForms returned in step 1. The Evaluation API does not check this; the ATS uses the ID to look up the name. The actual score is set in the `score` property of each score profile and must be a number. The root `score` is a string.

The root `score` property will be deprecated, and is kept for backwards compatibility.

#### Results ScoreProfiles schema

| Field | Description               |
| ----- | ------------------------- |
| Id    | Unique ID used for lookup |
| Score | The actual score (float)  |

#### Example Results JSON structure

`POST {Evaluation API base URL}/v1/Results`, see the [README](../README.md#posting-results).

```json
{
  "invitationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "status": "Created",
  "message": "string",
  "description": "string",
  "score": "string",
  "reportUrls": [
    {
      "name": "string",
      "type": "WebPage",
      "uri": "string"
    }
  ],
  "scoreProfiles": [
    {
      "id": "string",
      "score": 6.5
    }
  ]
}
```
