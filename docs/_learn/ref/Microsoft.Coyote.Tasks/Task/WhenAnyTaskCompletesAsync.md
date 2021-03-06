---
layout: reference
section: learn
title: WhenAnyTaskCompletesAsync
permalink: /learn/ref/Microsoft.Coyote.Tasks/Task/WhenAnyTaskCompletesAsync
---
# Task.WhenAnyTaskCompletesAsync&lt;TResult&gt; method

Creates a [`Task`](../TaskType) that will complete when any task in the specified enumerable collection have completed.

```csharp
public static Task<Task<TResult>> WhenAnyTaskCompletesAsync<TResult>(
    IEnumerable<Task<TResult>> tasks)
```

| parameter | description |
| --- | --- |
| tasks | The tasks to wait for completion. |

## See Also

* class [Task&lt;TResult&gt;](../Task-1Type)
* class [Task](../TaskType)
* namespace [Microsoft.Coyote.Tasks](../TaskType)
* assembly [Microsoft.Coyote](../../MicrosoftCoyoteAssembly)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.dll -->
