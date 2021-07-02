# Evaluation API custom fields
The Evaluation API endpoint `/v1/EvaluationForms` allows partners to describe custom fields we can render in the ATS.

Currently we support two types of custom fields: Select lists and checkboxes.

Both types are added in the array named `CustomFields` on the `EvaluationForm` structure described in the OpenAPI-specification.

All custom fields share the following properties:
- `id`, unique identifier for the custom field.
- `disabled`, indicating whether to render the custom field or not.
- `label`, the display label rendered for the custom field.
- `type`, used to aid in serialization of the supported types of custom field.

In addition to these there are type-specific properties.

## Select list
The select list is well suited for showing single choices in the ATS.

It contains a single extra property, `Options`, an array consisting of `Option` types. An `Option` has an `Id` property and a `Label` property used for the same purposes as described above.

The type property should have the value "select".

```jsonc
{
    "id": "unique-string-id-for-custom-select-list",
    "disabled": false,
    "label": "Display label for select list",
    "type": "select",
    "options": [
        {
            "id": "unique-string-id-for-option",
            "label": "Display label for option"
        }
    ]
}
```

## Checkbox
Checkboxes are well suited for providing additional "multiple choice" inputs to the evaluation form.

A checkbox should contain a single extra property, `Value`, a boolean where `true` represents the checkbox being checked, and `false` represents it being unchecked.

The type property should have the value "checkbox".

```jsonc
{
    "id": "unique-string-id-for-custom-select-list",
    "disabled": false,
    "label": "Display label for select list",
    "type": "checkbox",
    "value": true
}
```

## Full example response
```jsonc
[
    {
        "id": "id-from-partner-1",
        "name": "Human friendly name for the form",
        "languages": [
            {
                "name": "English",
                "languageCode": "en"
            }
        ],
        "description": "string",
        "customFields": [
            {
                "id": "custom-select-list-id-from-partner-1",
                "label": "Human friendly label for the select list",
                "disabled": false,
                "type": "select",
                "options": [
                    {
                        "id": "select-option-id-from-partner-1",
                        "label": "Choice 1"
                    },
                    {
                        "id": "select-option-id-2-from-partner-1",
                        "label": "Choice 2"
                    }
                ]
            },
            {
                "id": "custom-checkbox-id-from-partner-1",
                "label": "Human friendly label for the checkbox",
                "disabled": false,
                "type": "checkbox",
                "value": true
            }
        ]
    }
]
```