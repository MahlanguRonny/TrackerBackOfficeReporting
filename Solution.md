# Below describe tech stack and designs used for the trader reporting services

1. .Net 8 with Minimal Api - minimal api was picked as this is not a big project, just 2 endpoints (with optional batch endpoint) and .NET picked for long term support from microsoft
2. Repository Pattern - this is to help with seperation of concerns(avoid clutter in the minimal APIs) and to assist with the dependency injection
