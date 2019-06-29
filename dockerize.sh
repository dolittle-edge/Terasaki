#!/bin/bash
export VERSION=$(git tag --sort=-version:refname | head -1)
docker build --no-cache -f ./Source/Dockerfile -t shipos/timeseries-terasaki:$VERSION . --build-arg CONFIGURATION="Release"
docker push shipos/timeseries-terasaki:$VERSION