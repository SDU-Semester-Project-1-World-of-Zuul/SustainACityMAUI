# World of Zuul for .NET MAUI
## Introduction
World of Zuul is a classic text-based adventure game. This repository contains a port of the game to .NET MAUI, allowing it to run natively on various platforms including iOS, Android, macOS, and Windows. And i can't be bothered to write anymore, it's 23.25 o'clock.

# Workflow

1. create new feature branch (ex. onboarding-screen):

git branch onboarding-screen 
This will create a new branch based on the branch you are currently inside (meaning it will be a copy of the current branch).

2. checking out into feature branch

git checkout onboarding-screen
Now you are inside your feature branch and start implementing your changes.

3. commit into feature branch

Keep in mind to regularly commit your changes into your feature branches to prevent mistake to mess up you progress. You can commit your changes by either the VS Code integrated Git environment or by using the following command:

git commit -m "<your commit message>"
4. merge the feature branch into develop

When you are done with working on your feature you want to merge your changes to the developer branch. First make sure that you committed your latest changes to your feature branch! (step 3)

You can then checkout into the develop branch with:

git checkout develop
Git might inform you, that your are behind some commits (ex. @mzyeager pushed some changes before). If thats the case you need to pull the changes first:

git pull
then you can merge your feature branch:

git merge onboarding-screen
This will apply all changes and commits from the feature branch into the develop branch.

After reviewing your changes (on your own) you can push and update the develop branch:

git push
5. delete your old feature branch

After successfully implementing your feature branch you can delete your old branch:

git branch -d onboarding-screen
In case you pushed your local branch to the remote server, you need to adjust the delete command:

git push origin --delete onboarding-screen
