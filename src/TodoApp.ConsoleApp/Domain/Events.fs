module TodoApp.ConsoleApp.Domain.Events

open TodoApp.ConsoleApp.Domain.Commands

type Event =
    | TaskAdded          of AddTaskArgs
    | TaskRemoved        of RemoveTaskArgs
    | TaskCompleted      of CompleteTaskArgs
    | TaskDueDateChanged of ChangeTaskDueDateArgs