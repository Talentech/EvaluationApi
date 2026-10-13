# Encryption

The Evaluation API supports end to end encryption of data sent between the ATS and the partner using RSA asymmetric encryption (4096-bit keys, OAEP with SHA-256). In addition to the transport encryption provided by TLS, you as partner can choose to encrypt certain data fields sent in the payload to add additional security. If you want to enable encryption for your API connector, you need to support both encrypted invitations triggered by the ATS as well as encrypted results you send back to the ATS. If the invitation is triggered from an ATS that does not support encryption, you should not send encrypted payloads back. The ATS will send encrypted invitations if the partner has provided a public key, and the ATS is expected to support encrypted results if they provide public key in the invitation.

## Flow description

The Partner App decides if encryption should be used by adding a public key in the _Public Key_ field in the _Encryption_ section in App Configuration. The ATS will then encrypt fields in the invitation payload using the provided public key and list all the encrypted fields in a separate field called `EncryptedFields` for each relevant section in the JSON payload. The `EncryptedFields` field is an array of strings. The key identifiers are used to easily identify the key pair used when encrypting the payload and enables key pairs to be rotated. The ATS will provide it's own public key as in the field `SourceSystemPublicKey` when triggering an invitation. If this field is provided, it means the ATS supports encryption and expects the results payload to be encrypted if supported by the partner app. If you have provided a public key but the ATS does not support encryption, the ATS will just ignore it and send unencrypted invitation payloads. If the invitation does not contain a `SourceSystemPublicKey`, you can not send encrypted result payloads back.

## Encrypted payloads

Each encrypted field will contain a key identifier and a base64url encoded RSA payload on the form `keyId:<base64url encoded encrypted string>`.

### Encryption scheme

Payloads must be encrypted with RSA with a modulus length of 4096, with OAEP SHA-256, following the PKCS #1 standard. **This implies a maximum message length of 446 bytes.**. When sent in a payload, public keys are provided as a PKCS #1 `RSAPublicKey` in the DER format and Base64Url encoded along with the keyId in the format `<keyId>:<base64url encoded public key>`, similar to how encrypted payloads work. Note that when configuring your Partner App, you may also use the PEM format (`BEGIN PUBLIC KEY` or `BEGIN RSA PUBLIC KEY`). EvaluationAPI will validate that public keys are supplied in the correct format and encoding and reject requests if they are not.

Example:

```text
partner-samplepartner-2021-01-13:MIICCgKCAgEA0nd4FTqVm87WwQ25g5HjVSP_dM5qyo5BbJfRw870fhzyxh9gCgV0gpBxJWQZQheCXtyGwgfG8r0hjDrGipvvmLAWjC5sTJD2_7R1-tIjy_k9JIQn6Z1ZAZeIMtHVuAlnOmNuAmVp4DPQ0rQfcw9kYpK8hPF6PqYivBH2nWnvrJH3g4_2u-eIGPEmCmfOBvfY0B4pbRNfvoGwICjNy15asCmk1a61s_WrEvBQvsDpelyNEltXt_rDLmcox6BqotA9rE_2ZgRqpYlVoSsbzy8frdWeWpXdKi0Ml5DSK7zl839sDWCXSG0AKR0MW1FSwAxBe8ePrnlJYemIBXe8EB1vFzwm52cd8vg1Lk9YrPC81lzr0_FKiLqG-wQsyhnn0SV3vRntiRaYStToGTf9ZywcWAPEKW7zdBjPpn_qscRp4rVK0tqa7vu8UonS8JNsrV26RpZM4h7uswI9_4-9YNNO0t_Zau3neTxmhUn3lTLaOlQOEIQFOIIX_3zJ3XmjjBcx52GIHH6hpUgv8aRM97xhhGgeujSGltgU5RUsPDAt8IFXJuNdqpe6TdjEZT0IfbBs5n8Lj0Q7nU4BwTA-TZ4dYLSUQVJ3pOYlqhGNhXXS6Qrpa17NIylPal5iEKOpqeH7D15nDv4KNaqR-JN_bKhKldI0kNX-U8kyLjPJ852ru9ECAwEAAQ
```

### Key identifier

To allow key rotation, each key pair must be associated with a unique id, the key identifier, created by the key holder. Public keys are identified by an arbitrary string alias and provided in the _Public Key Identifier_ field in the _Encryption_ section in App Configuration. Beware that the key holder is responsible for avoiding emitting key id collisions upon rotation. We suggest adopting a naming convention. Example: `partner-<partner slug>-<date of key generation>`, e.g. `partner-samplepartner-2021-01-12`.

### Key rotation

To make sure key rotation does not affect ongoing processes, key pairs should be available for 30 days before they are taken out of rotation. If keys need to be removed sooner, manual coordination between ATS and partners must be done.

### Which fields can be encrypted?

Only fields containing personal identifiable information are supported for encrypted values. To indicate to the consumer that a field contains encrypted data, their names must be added to the `EncryptedFields` field. All values must be provided in PascalCase.

Example of valid values:

```json
{
  "EncryptedFields": ["FirstName","LastName","Email","PhoneNumber"]
}
```

Example of invalid values (camelCase):

```json
{
  "EncryptedFields": ["firstName","lastName","email","phoneNumber"]
}
```

For reference, the valid EncryptedFields values are listed below:

#### Invitations

| TriggeredBy            |
| ---------------------- |
| FirstName              |
| LastName               |
| Email                  |
| ExternalUserIdentifier |

| EvaluationDetails    |
| -------------------- |
| PersonalId           |
| FirstName            |
| LastName             |
| Email                |
| PhoneNumber          |
| Address.CareOf       |
| Address.Address      |
| Address.PostalCode   |
| Address.PostalCity   |
| Address.Municipality |
| Address.Country      |

The address values are listed in `EvaluationDetails.EncryptedFields`, with the `Address.` prefix.

#### Evaluation forms request

| TriggeredBy |
| ----------- |
| FirstName   |
| LastName    |
| Email       |

#### Results

| ReportUrls |
| ---------- |
| Uri        |

Example payloads are provided below.

_Form definitions, custom fields and custom field values are never encrypted. In the evaluation forms request, only the `TriggeredBy` block can be encrypted._

### Invitation payload sent by the EvaluationAPI to the partner app

This example is shortened. The full payload is in [payload-examples/invitation.json](payload-examples/invitation.json).

```json
{
    "Version": 1,
    "TriggeredBy": {
        "EncryptedFields": ["FirstName", "LastName", "Email"],
        "Email": "<encrypted data>",
        "FirstName": "<encrypted data>",
        "LastName": "<encrypted data>",
        "ExternalUserIdentifier": null
    },
    "EvaluationDetails": {
        "InvitationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "EvaluationFormId": "string",
        "SourceSystem": "string",
        "SourceSystemPublicKey": "keyId:<base64url encoded public key>",
        "ProjectId": "string",
        "ProjectName": "string",
        "EncryptedFields": ["FirstName", "LastName", "Email", "PhoneNumber"],
        "FirstName": "<encrypted data>",
        "LastName": "<encrypted data>",
        "Email": "<encrypted data>",
        "PhoneNumber": "<encrypted data>",
        "PreferredLanguage": "string",
        "NoteToCandidate": "Note to the candidate",
        "CustomFieldValues": []
    },
    "Auth": {
        "CustomerAccountId": "string",
        "PartnerApiRefreshToken": "string"
    }
}
```

### Example of a results payload sent by the partner app

```json
{
  "InvitationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
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

## Error handling

In the event that either party, ATS or Partner, is unable to process incoming encrypted payloads, an error using status code `422 Unprocessable Entity` should be returned to indicate a problem.

Example scenarios:

- Platform does not support encryption
- Unknown keyId (the receiver had no key stored that matched the keyId)
- Decryption failure (the receiver had a key, but decryption failed to yield valid results)

## Appendix: Example implementation Node.JS

Following implements an example encryptor and decryptor. The entire file can be executed without any dependencies and will write out a JavaScript object with encrypted values, and then the same with the values in plain text.

```js
#!/usr/bin/env node
const crypto = require("crypto");

// Generate the key and assign it an ID.
// This only needs to be done once but is included in the example to make it runnable.
const { publicKey, privateKey } = crypto.generateKeyPairSync("rsa", {
  modulusLength: 4096,
});
// Add these to a dummy key storage for later look-up:
const keyId = "partner-sample-2021-01-14";
const keys = [{ keyId, publicKey, privateKey }];
const encodedPublicKey = publicKey
  .export({ type: "pkcs1", format: "der" })
  .toString("base64url");
console.log(`Key format: ${keyId}:${encodedPublicKey}`);

// Example function to encrypt a value
const encryptor = (value) => {
  const encryptedValue = crypto.publicEncrypt(
    {
      key: publicKey,
      padding: crypto.constants.RSA_PKCS1_OAEP_PADDING,
      oaepHash: "sha256",
    },
    Buffer.from(value)
  );
  const base64UrlValue = encryptedValue.toString("base64url");
  return `${keyId}:${base64UrlValue}`;
};

// Example function to decrypt a value
const decryptor = (value) => {
  const [keyId, base64UrlValue] = value.split(":", 2);
  const encryptedValue = Buffer.from(base64UrlValue, "base64url");
  return crypto
    .privateDecrypt(
      {
        key: keys.find((key) => key.keyId === keyId).privateKey,
        padding: crypto.constants.RSA_PKCS1_OAEP_PADDING,
        oaepHash: "sha256",
      },
      encryptedValue
    )
    .toString();
};

// Example object:
const encryptedPayload = {
  Version: 1,
  Auth: {},
  TriggeredBy: {},
  EvaluationDetails: {
    InvitationId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    EvaluationFormId: "string",
    SourceSystem: "string",
    ProjectId: "string",
    ProjectName: "string",
    EncryptedFields: ["FirstName", "LastName", "Email", "PhoneNumber"],
    FirstName: encryptor("Jane"),
    LastName: encryptor("Doe"),
    Email: encryptor("jane.doe@example.com"),
    PhoneNumber: encryptor("+46701740605"),
    PreferredLanguage: "nl-NL",
    NoteToCandidate: "Note to the candidate",
    CustomFieldValues: [],
  },
};
console.log(encryptedPayload);
// Example output (illustrative, the encrypted values differ on every run):
// …
// EncryptedFields: [ 'FirstName', 'LastName', 'Email', 'PhoneNumber' ],
// FirstName: '5c0f11d9fa72e022032afe82e2eb234e:XWmIRGIrn0rbSkrWgeEN-7el7S32bdVah9hLlLGQC9NlbUCsByGFyQxYJwk7aA3vrAmfEhW3HDfzbSrogsFfNQ07nSNJy-JdGs06Pkq01CSjAsL33pRkZh5ygL6C8FW-5Oe1-qoV8-FWegxcC3GOLx7sqrbIirGmSqoOnbPX-a_77HBuT7pQ1TKyYr4K6vkHgA3gpsu1t-AET_04jrZbJ81hhHwdqnAoVdV3G23AvNRgfxwioJ6FafUlUGydHG5jDLycPj-jvBTeZjByd0_bGZ9hRi94zkXYcD6FzF6Ymb4saRVH9-Ys8Oxv25QWE_pIPrU3OsDm0U1IT1l_Rn93GA==',

// …
// On the receiving end the private key is used to decrypt the fields:
const decryptedPayload = { ...encryptedPayload };
for (const field of decryptedPayload.EvaluationDetails.EncryptedFields) {
  decryptedPayload.EvaluationDetails[field] = decryptor(
    decryptedPayload.EvaluationDetails[field]
  );
}
console.log(decryptedPayload);
// Example output:
// …
// EncryptedFields: [ 'FirstName', 'LastName', 'Email', 'PhoneNumber' ],
// FirstName: 'Jane',
// …
```
