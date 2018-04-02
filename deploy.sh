#!/usr/bin/env bash

AWS_SERVER=$1
PUBLISH_PATH=bin/Debug/netcoreapp2.0/publish

function publish_app () {
    echo "-- Publishing app '$1' -----"
    scp -r src/$1/$PUBLISH_PATH ubuntu@$AWS_SERVER.compute-1.amazonaws.com:~/$1
}

dotnet publish src

publish_app Marketing.Api
publish_app Sales.Api
publish_app Shipping.Api
publish_app Billing.Api
publish_app EShop.UI
