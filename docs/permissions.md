# Checking what a user may see

Some partners show a result inside their own UI and need to know whether the person looking at it is
allowed to. The Evaluation API answers that per invitation, by asking the ATS the invitation came from.

This endpoint is optional. Nothing else in the integration depends on it.

## The call

```
GET {Evaluation API base URL}/v1/invitations/{invitationId}/permissions?userId={userId}
GET {Evaluation API base URL}/v1/invitations/{invitationId}/permissions?email={email}
```

The Evaluation API base URL is the one Talentech gives you, see the [README](../README.md#key-terms).

At least one of `userId` (the Talentech user id) and `email` must be given; both may be. The invitation
carries the recruiter's `Email` in `TriggeredBy`, but no Talentech user id. Authenticate the call with the
same bearer token you use to post results.

## The answer

```json
{
  "permissions": [
    "referencecheck-report:read",
    "assessmenttest-report:read"
  ]
}
```

A permission that is not in the list is not granted. Two things are worth knowing about how the list is
produced:

- **The ATS decides, not Talentech.** Each ATS evaluates the user against its own access rules, so the
  same person can hold a permission in one customer's ATS and not in another's.
- **Names the Evaluation API does not recognise are dropped** from the response rather than passed
  through, so you will only ever see permissions from the published set.

The published set:

| Permission | Allows |
|---|---|
| `referencecheck-report:read` | reading the reference check report |
| `backgroundcheck-report:read` | reading the background check report |
| `assessmenttest-report:read` | reading the assessment test results |
| `contract-report:read` | reading the contract data |
| `feedback-report:read` | reading the feedback report |
| `invitation:create` | creating invitations |

## Status codes

| Code | Meaning |
|---|---|
| 200 | the list of allowed permissions, possibly empty |
| 400 | neither `userId` nor `email` was given, or `invitationId` is not a GUID |
| 401 | the bearer token is missing or not valid |
| 404 | no invitation with that id |
| 410 | the invitation belongs to an integration that has been archived |
| 502 | the ATS could not answer |

A 502 usually means the ATS that sent the invitation has no permissions endpoint configured with us. That
is set per ATS, not per customer, so it applies to every invitation from that ATS. It is a configuration
matter on our side, not something wrong in your request. Ask us and we will check it rather than retrying.
