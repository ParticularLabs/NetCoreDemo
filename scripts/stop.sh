#!/bin/bash

if [ ! -f pid/$1 ]; then
    echo "Could not find process id for $1"
    exit 1
fi

pid=$(cat pid/$1)
echo "Killing process $pid: dotnet $1"
kill -s SIGINT $pid
rm pid/$1