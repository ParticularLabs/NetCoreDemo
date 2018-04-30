#!/bin/bash

for file in ~/pid/*.pid; do
  [ -e "$file" ] || continue
  filename=$(basename $file)
  pid=$(cat $file)
  echo "Killing process $pid: dotnet $filename"
  kill -s SIGINT $pid
  rm $file
done
