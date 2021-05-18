#!/bin/bash
docker build -f ./Source/Dockerfile -t raalabs/timeseries-terasaki . --build-arg CONFIGURATION="Debug"
iotedgehubdev start -d deployment.json -v