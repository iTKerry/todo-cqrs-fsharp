module TodoApp.ConsoleApp.Domain.Commands

open System
open TodoApp.ConsoleApp.Domain.Errors

type TaskId = int

type RemoveTaskArgs = TaskId
type CompleteTaskArgs = TaskId

type AddTaskArgs =
    { Id      : TaskId
      Name    : string
      DueDate : DateTime option }

type ChangeTaskDueDateArgs =
    { Id      : TaskId
      DueDate : DateTime option }

type Command =
    | AddTask           of AddTaskArgs
    | RemoveTask        of RemoveTaskArgs
    | CompleteTask      of CompleteTaskArgs
    | ChangeTaskDueDate of ChangeTaskDueDateArgs

