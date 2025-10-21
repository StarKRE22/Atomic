# 🧩 Entity Debug

Represents debug properties are available only in <b>Unity Editor</b> when using <b>Odin Inspector</b>

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Core](#ex1)
  - [Tags](#ex2)
  - [Values](#ex3)
  - [Behaviours](#ex4)
- [Debug Parameters](#-debug-parameters)

---

## 🗂 Examples of Usage

Below are debug inspector examples for the [Entity](Entity.md):

<div id="ex1"></div>

### 1️⃣ Core Debug

<img width="450" height="" alt="Entity component" src="../../Images/UnitEntityDebug.png" />

- **Options:**
    - Change name of an entity

---

<div id="ex2"></div>

### 2️⃣ Tag Debug

<img width="450" height="" alt="Entity component" src="../../Images/TagsDebug.png" />

- **Options:**
    - Search Tag by name
    - Delete Tag by the cross button

---

<div id="ex3"></div>

### 3️⃣ Value Debug

<img width="450" height="" alt="Entity component" src="../../Images/ValuesDebug.png" />

- **Options:**
    - Search Value by name
    - Change Value by reference
    - Delete Value by the cross button

---

<div id="ex4"></div>

### 4️⃣ Behaviour Debug

<img width="450" height="" alt="Entity component" src="../../Images/BehaviousDebug.png" />

- **Options:**
    - Search Behaviour by name
    - Delete Behaviour

---

## 🛠 Debug Parameters

| Parameter     | Description                                           |
|---------------|-------------------------------------------------------|
| `Name`        | Displays entity name in the Unity Editor.             |
| `Initialized` | Displays if the entity is initialized.                |
| `Enabled`     | Displays if the entity is enabled.                    |
| `Tags`        | Sorted list of tags for debug display.                |
| `Values`      | Sorted list of values for debug display.              |
| `Behaviours`  | Sorted list of attached behaviours for debug display. |