# Encryption

The Evaluation API supports end to end encryption of data sent between the ATS and the partner using RSA512 asymmetric encryption.

In addition to the transport encryption provided by TLS, you as partner can choose to encrypt certain data fields sent in the payload to add additional security.

If you want to enable encryption for your API connector, you need to support both encrypted invitations triggered by the ATS as well as encrypted results you send back to the ATS.

If the invitation is triggered from an ATS that does not support encryption, you should not send encrypted payloads back.

The ATS will send encrypted invitations if the partner has provided a public key, and the ATS is expected to support encrypted results if they provide public key in the invitation.



## Flow description
The Partner App decides if encryption should be used by adding a public key in the *Public Key* field in the *Encryption* section in App Configuration.

The ATS will then encrypt fields in the invitation payload using the provided public key and list all the encrypted fields in a separate field called `EncryptedFields` for each relevant section in the JSON payload. The `EncryptedFields` field is an array of strings.


The key identifiers are used to easily identify the key pair used when encrypting the payload and enables key pairs to be rotated.

The ATS will provide it's own public key as in the field `SourceSystemPublicKey` when triggering an invitation. If this field is provided, it means the ATS supports encryption and expects the results payload to be encrypted if supported by the partner app.

If you have provided a public key but the ATS does not support encryption, the ATS will just ignore it and send unencrypted invitation payloads. If the invitation does not contain a `SourceSystemPublicKey`, you can not send encrypted result payloads back.



## Encrypted payloads

Each encrypted field will contain a key identifier and a base64 encoded RSA payload on the form `keyId:<base64 encoded encrypted string>`.

### Which fields can be encrypted?

Only fields with personal identifiable information supports encrypted values. 

**Invitations**

TriggeredBy: FirstName, LastName, Email
EvaluationDetails: FirstName, LastName, Email, PhoneNumber


**Results**

ReportUrls: Uri

Encryption is not supported for EvaluationForms and Custom Fields.


### Invitation payload sent by the EvaluationAPI to the partner app

```
{
    Version: 1,
    Auth: { … },
    TriggeredBy: { … },
    EvaluationDetails: {
        "InvitationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "EvaluationFormId": "string",
        "SourceSystem": "string",
        "SourceSystemPublicKey": "string",
        "ProjectId": "string",
        "ProjectName": "string",
        "EncryptedFields": ["FirstName","LastName","Email","PhoneNumber"],
        "FirstName": "<encrypted data>",
        "LastName": "<encrypted data>",
        "Email": "<encrypted data>",
        "PhoneNumber": "<encrypted data>",
        "PreferredLanguage": "string",
        "NoteToCandidate": "Note to the candidate",
        "CustomFieldValues": []
    }
}
```


### Example of a results payload sent by the partner app

```
{
  "TenantId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "InvitationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "PartnerAppId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "PartnerName": "string",
  "CustomerIntegrationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "EvaluationFormId": "string",
  "CreatedBy": "string",
  "ProjectId": "string",
  "CandidateId": "string",
  "Status": "Created",
  "Message": "string",
  "Description": "string",
  "Score": "string",
  "ReportUrls": [
    {
      "EncryptedFields": ["Uri"],
      "Name": "string",
      "Type": "WebPage",
      "Uri": "<encrypted data>"
    }
  ]
}
```

## Key rotation
To allow key rotation, each key pair must be associated with a unique id. To make sure key rotation does not affect ongoing processes, key pairs should be available for 30 days before they are taken out of rotation. If keys need to be removed sooner, manual coordination between ATS and partners must be done.





# Appendix: Example implementation Node.JS
Following implements an example encryptor and decryptor. The entire file can be executed without any dependencies and will write out a JavaScript object with encrypted values, and then the same with the values in plain text.


**crypt.js**

```
#!/usr/bin/env node
const crypto = require("crypto");

// Generate the key and assign it an ID.
// This only needs to be done once but is included in the example to make it runnable.
const { publicKey, privateKey } = crypto.generateKeyPairSync("rsa", {
   modulusLength: 2048,
});
// For this example we hash the public key to get a unique key identifier.
// Any key identifier will work, it will need to be decided when keys are exchanged.
const hasher = crypto.createHash("md5");
hasher.update(publicKey.export({ type: "pkcs1", format: "pem" }));
const keyId = hasher.digest("hex");
// Add these to a dummy key storage for later look-up:
const keys = [{ keyId, publicKey, privateKey }];

// Example function to encrypt a value
const encryptor = (value) => {
   const encryptedValue = crypto.publicEncrypt(
       {
           key: publicKey,
           padding: crypto.constants.RSA_PKCS1_OAEP_PADDING,
           oaepHash: "sha512",
       },
       Buffer.from(value)
   );
   const base64UrlValue = encryptedValue.toString("base64")
       .replace(/\+/g, "-")
       .replace(/\//g, "_");
   return `${keyId}:${base64UrlValue}`
}

// Example function to decrypt a value
const decryptor = (value) => {
   const [keyId, base64UrlValue] = value.split(":", 2);
   const encryptedValue = Buffer.from(base64UrlValue.replace(/-/g, "+").replace(/_/g, "\/"), "base64");
   return crypto.privateDecrypt(
       {
           key: keys.find(key => key.keyId === keyId).privateKey,
           padding: crypto.constants.RSA_PKCS1_OAEP_PADDING,
           oaepHash: "sha512",
       },
       encryptedValue
   ).toString();
}

// Example object:
const encryptedPayload = {
   Version: 1,
   Auth: { },
   TriggeredBy: { },
   EvaluationDetails: {
       InvitationId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
       EvaluationFormId: "string",
       SourceSystem: "string",
       ProjectId: "string",
       ProjectName: "string",
       EncryptedFields: ["FirstName","LastName","Email","PhoneNumber"],
       FirstName: encryptor("Martijn"),
       LastName: encryptor("van der Ven"),
       Email: encryptor("martijn.vanderven@refapp.com"),
       PhoneNumber: encryptor("+46730000000"),
       PreferredLanguage: "nl-NL",
       NoteToCandidate: "Note to the candidate",
       CustomFieldValues: []
   }
};
console.log(encryptedPayload);
// Example output:
// …
// EncryptedFields: [ 'FirstName', 'LastName', 'Email', 'PhoneNumber' ],
// FirstName: '5c0f11d9fa72e022032afe82e2eb234e:XWmIRGIrn0rbSkrWgeEN-7el7S32bdVah9hLlLGQC9NlbUCsByGFyQxYJwk7aA3vrAmfEhW3HDfzbSrogsFfNQ07nSNJy-JdGs06Pkq01CSjAsL33pRkZh5ygL6C8FW-5Oe1-qoV8-FWegxcC3GOLx7sqrbIirGmSqoOnbPX-a_77HBuT7pQ1TKyYr4K6vkHgA3gpsu1t-AET_04jrZbJ81hhHwdqnAoVdV3G23AvNRgfxwioJ6FafUlUGydHG5jDLycPj-jvBTeZjByd0_bGZ9hRi94zkXYcD6FzF6Ymb4saRVH9-Ys8Oxv25QWE_pIPrU3OsDm0U1IT1l_Rn93GA==',

// …
// On the receiving end the private key is used to decrypt the fields:
const decryptedPayload = { ...encryptedPayload };
for (const field of decryptedPayload.EvaluationDetails.EncryptedFields) {
   decryptedPayload.EvaluationDetails[field] = decryptor(decryptedPayload.EvaluationDetails[field]);
}
console.log(decryptedPayload);
// Example output:
// …
// EncryptedFields: [ 'FirstName', 'LastName', 'Email', 'PhoneNumber' ],
// FirstName: 'Martijn',
// …
```