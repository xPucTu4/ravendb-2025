# Codex Instructions

This repository contains the full RavenDB solution. For tasks within this environment, focus on the `Generator` project.

- Build only the `src/Generator` project and its tests.
- Do **not** build or test the rest of the solution.
- The other projects are provided for reference only.

## Build & Test

```sh
dotnet build src/Generator/Generator.csproj

dotnet test test/Generator.Tests/Generator.Tests.csproj
```

Only run the tests found under `Generator.Tests`.
