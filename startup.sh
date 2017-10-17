#!/bin/bash
searchString="Total tests:"
url="https://hooks.slack.com/services/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"
H="'Content-type: application/json'"

b=$(dotnet vstest AFT-Assets.dll)

echo "{\"attachments\": [{\"text\":\"" > tmp.txt
b=$(echo $b | tr '\n' ' ')
b=$(echo $b | tr '\\' "/")
b=$(echo $b | tr "'" "“")

c=$(echo $b | grep -o "$searchString.*")

echo $b >> tmp.txt
echo "\"}],\"text\":\"" >> tmp.txt
echo $c >> tmp.txt
echo "\"}" >> tmp.txt

curl -X POST -H $H --data-binary @tmp.txt $url

sleep infinity