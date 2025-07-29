# Mediaspot Backend Technical Test

Welcome!

This repository is part of the Mediaspot technical test process. The goal is to provide a supportive environment for you to demonstrate your skills, reasoning, and communication. This is not just about code it's about how you approach problems and explain your thinking.

## 💡 What to Expect

- **This is more a conversation starter than a final exam.**
- **You are not expected to finish everything.**
- **We value clarity, reasoning, and communication as much as technical correctness.**
- **Don't over-engineer.** Simple, clear solutions are great.
- **The test should take just a few hours.**
- **It's okay to ask questions or make assumptions.**

## 🗝️ Key Domain Terms

- **Asset**: A digital entity managed by the system. Think of it as a logical container for media content (e.g., a video, audio, or image project).
- **Media File**: A physical file (like an .mp4, .mov, .jpg, etc.) associated with an asset. An asset can have one or more media files.
- **Transcode**: The process of converting a media file from one format or quality to another (e.g., from 4K to HD, or from .mov to .mp4). This is often needed for compatibility or performance reasons.
- **Transcode Job**: A record of a transcode operation, tracking its status (pending, running, succeeded, failed) and related metadata.
- **Preset**: A named set of parameters for transcoding (e.g., 'HD_720p', 'Mobile_Low', etc.).

## 🏗️ Project Structure

- `src/Mediaspot.Domain`: Core domain models and logic.
- `src/Mediaspot.Application`: Application layer (use cases, commands, queries).
- `src/Mediaspot.Infrastructure`: Data access and external integrations.
- `src/Mediaspot.Api`: API entry point.
- `src/Mediaspot.Worker`: Worker application. (not implemented)
- `tests/Mediaspot.UnitTests`: Unit tests.

## 🔍 What We're Looking For

- Can you read, understand, and extend the codebase?
- How do you reason about requirements and trade-offs?
- Are you comfortable asking questions or clarifying assumptions?
- Can you write clear, maintainable code and tests?

## 🎯 Tips for Success

- **Relax!** This is meant to be a positive experience.
- **Document your thought process.** Comments and commit messages are welcome.
- **If you get stuck, explain your approach.**
- **You can leave TODOs or notes.**
- **Feel free to ask for clarification.**
- **Feel free to list any improvements you would suggest, even if you don't have time to implement them.**
- _Pssst: We've hidden a small "consistency" bug somewhere in the project. It's not mandatory, but if you spot it, let us know!_

## 📝 Tasks

- **Basic:**

  - Add a `Title` domain entity (e.g., representing a movie, show, or media project).
    - Example properties: `Id`, `Name`, `Description`, `ReleaseDate`, `Type` (movie, series, etc.).
  - Implement related business rules (e.g., a Title must have a unique name).
  - Expose API endpoints to create, update, retrieve, and list Titles.

- **Middle:**

  - Extend the `Transcode Job` domain model following DDD and event-driven recommendations.
    - Add events for job status changes (e.g., `TranscodeJobStarted`, `TranscodeJobCompleted`).
    - Example properties: `Id`, `AssetId`, `Preset`, `Status`, `CreatedAt`, `UpdatedAt`, `Events`.
  - Add use cases for job management (e.g., start, complete, fail jobs).
  - Implement a simple Worker application to process jobs (dummy processing logic, e.g., mark as completed after a delay).

- **Advanced:**

  - Refactor `Asset` to be abstract and implement `AudioAsset` and `VideoAsset`.
    - `AudioAsset` example properties: `Duration`, `Bitrate`, `SampleRate`, `Channels`.
    - `VideoAsset` example properties: `Duration`, `Resolution`, `FrameRate`, `Codec`.
  - Handle transcoding for both asset types, including type-specific logic.
  - Implement transcode management (e.g., restrict allowed status transitions, handle errors).

- **Bonus:**

  - Add examples of how to architect use cases for archiving and dearchiving files (e.g., triggering side effects, making API calls to 3rd parties, event publishing).

- **Bonus 2:**

  - If you implemented the Worker, make it use a (fake) queue system to fetch and process jobs.

- **For each task:**
  - Implement few relevant tests

> If you can't process a stage, at least write a summary of what you would research/do for that part.

We look forward to discussing your work and learning about your approach!

---

_The Mediaspot Team_
