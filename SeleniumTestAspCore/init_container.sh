#!/usr/bin/env bash
# Reference: https://github.com/Azure-App-Service/dotnetcore/tree/master/2.1.1

sudo service ssh start

[ -z "$ASPNETCORE_URLS" ] && export ASPNETCORE_URLS=http://*:"$PORT"

# Start the Selenium server
echo Starting Selenium local server in the background.
exec /opt/bin/entry_point.sh &

# Wait for the server to be ready
#while [! curl -sSL "http://localhost:4444/wd/hub/status" 2>&1 | jq -r '.value.ready' 2>&1 | grep "true" >/dev/null]
#do
#    echo 'Waiting for the Grid'
#    sleep 1
#done

# Start the ASP.Net site
echo "Selenium Service is up. Starting ASP.Net site."
exec dotnet SeleniumTestAspCore.dll