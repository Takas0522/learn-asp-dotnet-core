API ManagementとGraphQLを試して見るやつ。

``` mermaid
flowchart LR

swa[Static Web Apps]
apim[API Management]
oapi["Original REST Web API(DevTunnels)"]
db[(Database)]
gr[Microsoft Graph]
swa --> apim
apim --Synthetic GraphQL--> oapi
apim --Data API Builder--> db
apim --Synthetic GraphQL--> gr
```