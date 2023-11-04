
# Contributing to SustainACity
This is how you Contribute to the project.
Our workflow is based on the feature branch model. Here's how it works:

<details>
<summary>🌱 Create New Feature Branch</summary>

**Example (e.g., onboarding-screen):**
```bash
git branch onboarding-screen
git checkout onboarding-screen
```
This creates and checks out a new branch based on your current branch.

</details>

<details>
<summary>💾 Commit into Feature Branch</summary>

Remember to regularly commit your changes to prevent losing any work.
```bash
git commit -m "<your commit message>"
```

</details>

<details>
<summary>🔄 Merge the Feature Branch into Dev</summary>

Once your feature is ready, merge your changes into the `dev` branch:
```bash
git checkout dev
git pull
git merge onboarding-screen
git push
```

</details>

<details>
<summary>🗑️ Delete Your Old Feature Branch</summary>

After merging, you can delete your feature branch:
```bash
git branch -d onboarding-screen
git push origin --delete onboarding-screen
```

</details>
