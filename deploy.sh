#!/usr/bin/env bash

function run_cmd() {
    ssh -o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null deploy@$SERVER_IP "$*"
}

if [[ -z "$SSH_KEY" ]] || [[ -z "$SERVER_IP" ]]; then
    echo "Fix environment variables please :)"
    exit 1
fi

eval $(ssh-agent)
ssh-add - <<< $SSH_KEY
run_cmd "cd Hades-frontend; git clean -fdx; git fetch origin master; git reset --hard origin/master; dotnet publish --configuration release"
eval $(ssh-agent -k)
