---
name: Engin3D Repository
description: Instructions for GitHub Issues, branches, commits, pull requests, milestones, tags, and releases in Engin3D.
applyTo: "./**/*"
---

# Engin3D GitHub Workflow

Engin3D uses GitHub Issues, Milestones, Pull Requests, tags, and Releases as the implementation history.

## Work hierarchy

```text
Milestone
└── Feature
    └── Task
```

Every implementation branch must correspond to a Task and reference its issue in the branch name. Tasks belong to exactly one Feature. Features belong to exactly one Milestone.

Use the repository issue templates exactly. Do not create ad-hoc tracking issues. Feature and Milestone descriptions summarize their child Features; implementation detail belongs in Tasks.

## Labels

Use only labels defined by the issue templates. Milestone issues use `enhancement` and `milestone`. Feature issues use `enhancement` and `feature`. Task issues use `task`. Do not add `feature` to Tasks or Milestones.

## Branches

Create implementation branches from the current target branch and use a Task-oriented name, for example `task/123-description`. Never rewrite or force-push history unless explicitly requested.

## Commits

Commit messages use one of the repository prefixes: `feat:`, `fix:`, `docs:`, or `tests:`. Keep descriptions concise. Use `;` only when one intentional commit contains multiple related change entries. Prefer one reviewable commit for a prompt implementation unless the issue explicitly requires separate commits.

## Pull Requests

Use the repository PR template exactly. Implementation PRs start as draft unless explicitly requested otherwise and must not be merged without explicit approval.

Draft titles use the repository convention:

```text
[DRAFT] [TASK|BUG|DOCS|SYNC] Title
```

When ready for review, remove `[DRAFT]` while preserving the change-type prefix. Assign implementation PRs to the repository owner when required by the repository workflow.

## Milestones, tags, releases

A milestone represents one releasable functional group. It must have no finish date and must contain the Features that comprise its scope. When complete, release exactly one immutable version tag and one GitHub Release for that milestone. Releases are created manually.

Tags are created and pushed remotely by the repository owner according to repository policy.

## Documentation

Developer-facing source comments, issue text, PR text, logs, and technical documentation use en-US. Keep README and project documentation aligned with implemented behavior and do not describe planned behavior as already implemented.
