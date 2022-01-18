# Score Profiles

Score Profiles is a feature that facilitates returning results with multiple scores.

## Flow

### 1. ATS requests available evaluation forms for tenant

The ATS requests all available evaluation forms for the tenant. The context must be looked up by the partner by looking at the auth info and the triggeredBy info. NB: The context is not project specific atm. Note the addition of the `ScoreProfiles` list to the schema:

#### EvaluationForms ScoreProfiles schema

| Field       | Description                                     |
| ----------- | ----------------------------------------------- |
| Id          | Unique ID used for lookup                       |
| Name        | Human readable name used in e.g. column headers |
| Description | A longer description to explain the score       |

#### Example EvaluationForms JSON structure

`POST /v1/EvaluationForms`

Request body:

_No project context._

```json
{
  "triggeredBy": {
    "email": "string",
    "firstName": "string",
    "lastName": "string"
  },
  "auth": {}
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
        "type": "string",
        "disabled": true
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

`POST /v1/Invitations`

```json
{
  "version": 0,
  "triggeredBy": {
    "email": "string",
    "firstName": "string",
    "lastName": "string"
  },
  "evaluationDetails": {
    "invitationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "evaluationFormId": "string",
    "sourceSystem": "string",
    "projectId": "string",
    "projectName": "string",
    "firstName": "string",
    "lastName": "string",
    "email": "string",
    "phoneNumber": "string",
    "preferredLanguage": "string",
    "noteToCandidate": "string",
    "customFieldValues": [
      {
        "id": "string",
        "value": "string"
      }
    ]
  },
  "auth": {}
}
```

### 3. Results posted back by the partner

The partner posts results back to the EvaluationAPI whenever there are updates. Results can be postedback 0 or more times per invitation.

The partner can post multiple scores back by using the `scoreProfiles`. Each score must map to a scoreProfile ID matching the available scoreProfiles indicated in the EvaluationForms returned in step 1. The actual score should be set in the `score` property and should be a numeric value.

The root `score` property will be deprecated, and is kept for backwards compatibility.

#### Results ScoreProfiles schema

| Field | Description               |
| ----- | ------------------------- |
| Id    | Unique ID used for lookup |
| Score | The actual score (float)  |

#### Example Results JSON structure

`POST /v1/Results`

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
      "score": "float"
    }
  ]
}
```
