#!/bin/bash

if [ -z "$1" ]
then
    url="http://localhost:5255"
else
    url=$1
fi

ng-openapi-gen -i "$url/swagger/v1/swagger.json" -o ./src/app/api/

