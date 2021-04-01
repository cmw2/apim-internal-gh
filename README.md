# Introduction 
This stores the ARM templates for a demo API Management resource with dev, test, and prod instances.

# Docs
See https://docs.microsoft.com/en-us/azure/api-management/devops-api-development-templates and https://github.com/Azure/azure-api-management-devops-resource-kit for more information on the tools used.

# Commands
Here are the extract and create commands I used.  You'll need to modify them to match the details of your apim instances.

Note that I used a slightly modified version of the extract and create commands that generated relativePath urls.  Since you'll be using the standard one you'll want to also look at setting the linkedTemplatesSasToken and policyXMLSasToken config properties.

Extractor:

    apim-templates extract --sourceApimName apim-internal-dev-eastus --destinationApimName apim-internal-test-eastus --resourceGroup rg-apiint-dev-eastus --fileFolder "C:\repos\CASantaClara\apim-internal\src\arm\demo-conference-api-v1" --apiName demo-conference-api-v1;rev=3 --policyXMLBaseUrl https://stgcmwapiinttesteastus.blob.core.windows.net/apimarmtemplates/arm/ --linkedTemplatesBaseUrl https://stgcmwapiinttesteastus.blob.core.windows.net/apimarmtemplates/arm/

Creator:

Run this in the src/petStore/arm-creator-config directory

    apim-templates create --configFile valid.yml