#!/bin/bash
set -e

echo "Waiting for container to start..."
sleep 10

# Checking container status
COMMAND_ID=$(aws ssm send-command \
  --instance-ids "${INSTANCE_ID}" \
  --document-name "AWS-RunShellScript" \
  --parameters "commands=['docker ps | grep ${CONTAINER_NAME}']" \
  --query "Command.CommandId" \
  --output text)

sleep 5

STATUS=$(aws ssm get-command-invocation \
  --command-id "$COMMAND_ID" \
  --instance-id "${INSTANCE_ID}" \
  --query "Status" \
  --output text)

if [ "$STATUS" != "Success" ]; then
  echo "::error::Container verification failed with status: $STATUS"
  exit 1
fi

CONTAINER_STATUS=$(aws ssm get-command-invocation \
  --command-id "$COMMAND_ID" \
  --instance-id "${INSTANCE_ID}" \
  --query "StandardOutputContent" \
  --output text)

echo "Container status:"
echo "$CONTAINER_STATUS"

if [[ "$CONTAINER_STATUS" != *"Up"* ]]; then
  echo "::error::Container is not running properly"
  exit 1
fi
