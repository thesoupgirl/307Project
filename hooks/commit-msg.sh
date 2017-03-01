#!/bin/sh

# regex to validate in commit msg
commit_regex='(stuff|things|shit|fuck|mayhaps|thing)'
error_msg="Aborting commit. Your commit message is either using banned words or you're not being specific enough."

if ! grep -iqE "$commit_regex" "$1"; then
    echo "$error_msg" >&2
    exit 1
fi
