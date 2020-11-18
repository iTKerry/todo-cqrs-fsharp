module TodoApp.ConsoleApp.Domain.State

open System
open TodoApp.ConsoleApp.Domain.Errors
open TodoApp.ConsoleApp.Domain.Events
open TodoApp.ConsoleApp.Domain.Commands

type Task = {
    Id         : int
    Name       : string
    DueDate    : DateTime option
    IsComplete : bool
}

type TasksBucket = { Tasks : Task list }
    with
    static member zeroState = { Tasks = [] }
    static member ApplyEvent state = function
        | TaskAdded args ->         
            let newTask =
                { Id = args.Id 
                  Name = args.Name 
                  DueDate = args.DueDate 
                  IsComplete = false }        
            { state with Tasks = newTask :: state.Tasks}    
        | TaskRemoved id -> 
            { state with Tasks = state.Tasks |> List.filter (fun x -> x.Id <> id) }    
        | TaskCompleted id ->         
            let task = 
                state.Tasks 
                |> List.find (fun x -> x.Id = id) 
                |> (fun t -> { t with IsComplete = true })        
            let otherTasks = state.Tasks |> List.filter (fun x -> x.Id <> id)        
            { state with Tasks = task :: otherTasks }    
        | TaskDueDateChanged args ->         
            let task = 
                state.Tasks 
                |> List.find (fun x -> x.Id = args.Id) 
                |> (fun t -> { t with DueDate = args.DueDate })
            let otherTasks = 
                state.Tasks 
                |> List.filter (fun x -> x.Id <> args.Id)        
            { state with Tasks = task :: otherTasks }
            