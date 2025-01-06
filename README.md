# .NET Repo Template

## Workflows

Be sure to check the workflows and remove or adjust as needed.

* `.github/workflows/libanvl-dotnet-ci.yml`
* `.github/workflows/libanvl-nuget-release.yml`
* `.github/workflows/libanvl-docfx.yml`

The default workflows use reusable workflows and composite actions from [libanvl/ci](https://github.com/libanvl/ci/)

## Required Repo or Org Secrets

* CODECOV_TOKEN for uploading coverage to [Codecov](https://about.codecov.io/)
* NUGET_PUSH_KEY for pushing to [NuGet Gallery](https://www.nuget.org/)

## Integrating With Existing Repository

1. Add a pull-only remote for the libanvl/repo-dotnet template
2. Fetch the latest changes from the template repository
3. Merge the main branch from the template, allowing unrelated histories and squashing commits
4. Commit the squashed changes

```sh
git remote add template https://github.com/libanvl/repo-dotnet.git
git fetch template
git merge template/main --allow-unrelated-histories --squash
git commit -m "Integrate with libanvl/repo-dotnet template"
```
