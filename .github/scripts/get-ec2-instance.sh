#!/bin/bash
set -e

echo "Looking for instance with tag Name=${INSTANCE_NAME}"

# Get instance ID by tag-name
INSTANCE_ID=$(aws ec2 describe-instances \
  --filters "Name=tag:Name,Values=${INSTANCE_NAME}" "Name=instance-state-name,Values=running" \
  --query "Reservations[0].Instances[0].InstanceId" \
  --output text)

if [ "$INSTANCE_ID" == "None" ] || [ -z "$INSTANCE_ID" ]; then
  echo "::error::No running instance found with name '${INSTANCE_NAME}'"
  exit 1
fi

echo "Found instance: $INSTANCE_ID"
echo "INSTANCE_ID=${INSTANCE_ID}" >> $GITHUB_ENV

# Check SSM status
echo "Checking SSM status for instance $INSTANCE_ID (${INSTANCE_NAME})..."
aws ssm describe-instance-information --filters "Key=InstanceIds,Values=$INSTANCE_ID"
