# SeleniumTestAspCore
Test ASP.Net MVC API using Selenium, and running in a Linux container on Azure Web Apps

# Introduction
The purpose behind this exercise was to understand how the [Selenium WebDriver](https://www.seleniumhq.org/projects/webdriver/) could be used in Azure Web Apps.  As per the docs on the [Unsupported Frameworks in Azure Web App Sandbox](https://github.com/projectkudu/kudu/wiki/Azure-Web-App-sandbox#unsupported-frameworks), PhantomJS/Selenium is unsupported as it tries to connect to a local address, and also uses GDI+.  This ruled out the use of Functions, so the next step was to see if we could it to work within a Linux Container on Azure Web Apps.

This project is a simple example, starting with the boilerplate ASP.Net MVC API project, and changing the Values method to call Google via the Selenium Web Driver.

# Getting Started
To get this project running, as-is, you will need to:
1. Create an Azure Container Registry, or use DockerHub
2. Build the Docker image defined in the included Dockerfile
3. Publish the Docker image to your container registry 
4. Create a Linux Container-based Web App and Plan
5. Point your Web App to the published container
6. Ensure that Always On is turned on in your Web App, so that it will check every 5 minutes for a newer version of  your container in the registry

# Contribute
Keen to hear from you on how this can be improved futher :-)
