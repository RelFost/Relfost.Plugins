
# Dependies Class Usage Guide

## Introduction

The `Dependies` class is designed to manage and verify the loading status of plugin dependencies in Oxide and Carbon environments. This class allows you to initialize a set of dependencies, periodically check their load status, and trigger a callback or hook once all dependencies are loaded.

## Features

- Initialize dependencies and check their loading status.
- Periodically check the loading status of each dependency.
- Trigger a callback or hook once all dependencies are loaded.

## Installation

1. Ensure you have the necessary environment set up with Oxide and Carbon.
2. Download the compiled `Relfost.Plugins.dll` file from the [Releases page](https://github.com/RelFost/Relfost.Plugins/releases).
3. Place the `Relfost.Plugins.dll` file into the `carbon/extensions` directory of your project.

## Usage

### Step 1: Create a List of Dependencies

Create a list of dependencies (plugin names) that you want to manage.

```csharp
List<string> dependencies = new List<string> { "PluginParent1", "PluginParent2" };
```

### Step 2: Instantiate the Dependies Class

Instantiate the `Dependies` class with the list of dependencies, an optional callback hook name, and an optional callback function.

```csharp
Dependies dependenciesManager = new Dependies(dependencies);
```

Or with a custom callback hook name:

```csharp
Dependies dependenciesManager = new Dependies(dependencies, "CustomDependiesLoaded");
```

Or with a callback function:

```csharp
Dependies dependenciesManager = new Dependies(dependencies, callback: MyCustomCallback);
```

### Step 3: Define the Callback Hook or Function

Define the callback hook or function that will be triggered once all dependencies are loaded. This should be a method in your plugin class.

#### Using a Hook:

```csharp
public void OnAllDependiesLoaded()
{
    Puts("All dependencies are loaded!");
}
```

#### Using a Callback Function:

```csharp
private void MyCustomCallback()
{
    Puts("Custom callback executed: All dependencies are loaded!");
}
```

### Step 4: Adjust the Check Interval (Optional)

You can adjust the interval at which the dependencies are checked for their loading status by setting the `CheckInterval` property (in milliseconds).

```csharp
dependenciesManager.CheckInterval = 1000; // Set interval to 1 second
```

### Example

Here's a full example demonstrating the usage of the `Dependies` class within an Oxide plugin:

```csharp
using Oxide.Plugins;
using Oxide.Core;
using Oxide.Core.Plugins;
using System.Collections.Generic;
using Relfost.Plugins;

namespace Carbon.Plugins
{
    [Info("PluginWithDependies", "<author>", "1.0.0")]
    [Description("<optional_description>")]
    public class PluginWithDependies : CarbonPlugin
    {
        private void OnServerInitialized()
        {
            List<string> dependencies = new List<string> { "PluginParent1", "PluginParent2" };
            Dependies dependenciesManager = new Dependies(dependencies, callback: MyCustomCallback);
            // Start checking dependencies

            // Adjusting the check interval
            dependenciesManager.CheckInterval = 1000; // Set interval to 1 second
        }

        private void MyCustomCallback()
        {
            Puts("Custom callback executed: All dependencies are loaded!");
        }
    }
}
```

## Notes

- The `Dependies` class uses asynchronous methods (`async/await`) to periodically check the loading status of each dependency without blocking the main thread.
- The callback hook or function is invoked once all dependencies are loaded, allowing you to execute any initialization logic that depends on these plugins.
- The second argument of the `Dependies` constructor is optional and defaults to "OnAllDependiesLoaded" if not provided.
