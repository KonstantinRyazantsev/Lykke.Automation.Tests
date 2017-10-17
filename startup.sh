#!/bin/bash
echo "{\"text\":\"" > tmp.txt
b=$(dotnet vstest AFT-Assets.dll)
b=$(echo $b | tr '\n' ' ')
b=$(echo $b | tr '\\' "/")
b=$(echo $b | tr "'" "“")
echo -e $b >> tmp.txt
echo "\"}" >> tmp.txt

url="https://hooks.slack.com/services/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"
H="'Content-type: application/json'"

curl -X POST -H $H --data-binary @tmp.txt $url

sleep infinity