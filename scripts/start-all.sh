mkdir -p pid

function start_service () {
    echo "Starting $1..."
    (cd ~/$1 && exec dotnet $1.dll > /dev/null 2>&1 & echo $! > ./pid/$1.pid )
}

start_service Marketing.Api
start_service Sales.Api
start_service Shipping.Api
start_service Billing.Api
start_service EShop.UI