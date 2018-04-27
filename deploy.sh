#!/usr/bin/env bash

DEPLOY_SERVER=netcoredemo.particular.net
PUBLISH_PATH=bin/Debug/netcoreapp2.0/publish/*

function publish_app () {
    echo "-- Publishing app '$1' -----"
    ssh ubuntu@$DEPLOY_SERVER "mkdir -p ~/$1"
    scp -r src/$1/$PUBLISH_PATH ubuntu@$DEPLOY_SERVER:~/$1
}

dotnet publish src/EShop.sln

if [ $? -ne 0 ]
then
    echo "dotnet publish failed, cannot deploy" >&2
    exit 1
fi

scp -r scripts/* ubuntu@$DEPLOY_SERVER:~

publish_app Marketing.Api
publish_app Sales.Api
publish_app Shipping.Api
publish_app Billing.Api
publish_app EShop.UI
publish_app LoadGenerator
publish_app ITOps.WarehouseBridge
