#!/bin/bash
export VERSION=$(git tag --sort=-version:refname | head -1)
docker build -f ./Source/Dockerfile -t dolittle/timeseries-terasaki:$VERSION .
docker push dolittle/timeseries-terasaki:$VERSION