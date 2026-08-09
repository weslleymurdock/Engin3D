---
name: Engin3D Repository
description: Instructions for git/github usage at Engin3D repository.
applyTo: "./**/*"
---

# Engin3D

This git repository is a kind of .NET monorepo based organization for the client and server .net projects.

## Responsibilities

Branches in this repo must:

- belong to a [TASK](./../ISSUE_TEMPLATE/task.yml) issue and reference that issue in the branch name.

Issues in this repo must:

- have [TASK](./../ISSUE_TEMPLATE/task.yml) issues that belong to one [FEATURE](./../ISSUE_TEMPLATE/feature_request.yml) issue.
- have each [FEATURE](./../ISSUE_TEMPLATE/feature_request.yml) issue linked to exactly one [MILESTONE](./../ISSUE_TEMPLATE/new_milestone.yml) issue.
- have [MILESTONE](./../ISSUE_TEMPLATE/new_milestone.yml) issues that have no parents and belong to an active milestone approved for use in the repository project.

Milestones in this repo must:

- have only one related [MILESTONE](./../ISSUE_TEMPLATE/new_milestone.yml) issue / milestone.
- must not have a finish date.
- must release exactly one tag and exactly one release per milestone.

Tags in this repo must:

- have only tagged locally and push remotely by repository owner.
- belongs to one milestone.

Releases in this repo must:

- be done manually.

Commits in this repo must:

1. start with one of these prefixes: `feat:`, `fix:`, `docs:`, or `tests:`.
2. include a concise change description after the prefix, for example `docs: update readme` or `tests: implement mock`.
3. use `;` to separate multiple change entries when one commit covers more than one change, for example `fix: removed mismatching property; docs: update readme`.
4. use a single commit for prompt implementations so the diff is easier to review in the current open branch.

PR's in this repo must:

- be assigned to repository owner;
- open as draft, with the first prefix being `[DRAFT]` .
- the title must have the prefix according to the change of the issue (after draft, to be the first when exits the draft state)
