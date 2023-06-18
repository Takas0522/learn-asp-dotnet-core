API ManagementとGraphQLを試して見るやつ。

``` mermaid
flowchart LR

swa[Static Web Apps]
apim[API Management]
oapi[Original REST Web API]
db[(Database)]
gr[Microsoft Graph]
swa --> apim
apim --Synthetic GraphQL--> oapi
apim --Data API Builder--> db
apim --Synthetic GraphQL--> gr
```

[Get Authorization Context Policy](https://learn.microsoft.com/ja-jp/azure/api-management/get-authorization-context-policy)