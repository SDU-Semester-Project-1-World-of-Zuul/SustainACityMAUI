
# 🌍 World of Zuul for .NET MAUI
## 📜 Introduction
World of Zuul is a classic text-based adventure game. This repository contains a port of the game to .NET MAUI, allowing it to run natively on various platforms including iOS, Android, macOS, and Windows. And I can't be bothered to write anymore, it's 23.25 o'clock. 😴

<details>
<summary>🚀 Workflow - Feature Branch</summary>

**1. 🌱 Create New Feature Branch (ex. onboarding-screen):**
```bash
git branch onboarding-screen 
```
This will create a new branch based on the branch you are currently inside (meaning it will be a copy of the current branch).

---

**2. 🔍 Checking Out into Feature Branch**
```bash
git checkout onboarding-screen
```
Now you are inside your feature branch and can start implementing your changes.

---

**3. 💾 Commit into Feature Branch**

Remember to regularly commit your changes into your feature branches to prevent mistakes that could mess up your progress.
You can commit your changes by either using the VS Code integrated Git environment or by using the following command:
```bash
git commit -m "<your commit message>"
```

---

**4. 🔄 Merge the Feature Branch into Develop**

When you're done working on your feature, you'll want to merge your changes into the developer branch.
First, make sure you've committed your latest changes to your feature branch! (See Step 3)

You can then checkout into the develop branch with:
```bash
git checkout develop
```

Git might inform you that you are behind some commits (e.g., @mzyeager pushed some changes before). If that's the case, you need to pull the changes first:
```bash
git pull
```

Then you can merge your feature branch:
```bash
git merge onboarding-screen
```

This will apply all changes and commits from the feature branch into the develop branch.

After reviewing your changes (on your own), you can push and update the develop branch:
```bash
git push
```

---

**5. 🗑️ Delete Your Old Feature Branch**

After successfully implementing your feature, you can delete your old branch:
```bash
git branch -d onboarding-screen
```

In case you pushed your local branch to the remote server, you'll need to adjust the delete command:
```bash
git push origin --delete onboarding-screen
```

</details>
