mkdir -p pid

if [ ! -f $1/$1.dll ]; then
    echo "Can't start service, $1/$1.dll not found"
    exit 1
fi

echo "Starting $1..."
(cd ~/$1 && exec dotnet $1.dll > /dev/null 2>&1 & echo $! > ./pid/$1 )