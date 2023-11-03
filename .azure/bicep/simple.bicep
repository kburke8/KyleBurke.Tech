// Parameters
@description('The location into which your Azure resources should be deployed.')
param location string = resourceGroup().location

@description('The name of the project.')
param projectName string

// Define the names for resources.
var appServiceAppName = 'as-${projectName}'
var appServicePlanName = 'plan-${projectName}'

// Resources
resource appServicePlan 'Microsoft.Web/serverfarms@2021-01-15' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'F1'
  }
}

resource appServiceApp 'Microsoft.Web/sites@2021-01-15' = {
  name: appServiceAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      healthCheckPath: '/health'
      netFrameworkVersion: 'v8.0'
    }
  }
}

// Output
output appServiceAppName string = appServiceApp.name
output appServiceAppHostName string = appServiceApp.properties.defaultHostName
