module TodoApp.ConsoleApp.CommandHandlers.TaskCommandHandler

open TodoApp.ConsoleApp.Domain.Errors
open TodoApp.ConsoleApp.Domain.State
open TodoApp.ConsoleApp.Domain.Events
open TodoApp.ConsoleApp.Domain.Commands
open TodoApp.ConsoleApp.Domain.Aggregate


let private ifTaskDoesNotAlreadyExist state id =    
    match state.Tasks |> List.tryFind (fun x -> x.Id = id) with    
    | Some _ -> TaskAlreadyExists id |> AppError.createResult   
    | None -> Result.Ok state
    
let private ifTaskExists state id =    
    match state.Tasks |> List.tryFind (fun x -> x.Id = id) with
    | Some task -> Result.Ok task    
    | None -> TaskDoesNotExist id |> AppError.createResult
    
let private ifNotAlreadyFinished task =    
    match task.IsComplete with    
    | true -> TaskAlreadyFinished "Task already finished" |> AppError.createResult     
    | false -> Result.Ok task
    
let executeCommand state = function     
    | AddTask args -> 
        args.Id 
        |> ifTaskDoesNotAlreadyExist state
        |> Result.map (fun _ -> TaskAdded args)
    | RemoveTask args -> 
        args 
        |> ifTaskExists state 
        |> Result.map (fun _ -> TaskRemoved args) 
    | CompleteTask args -> 
        args 
        |> ifTaskExists state 
        |> Result.map (fun _ -> TaskCompleted args)    
    | ChangeTaskDueDate args -> 
        args.Id 
        |> (ifTaskExists state >> Result.bind ifNotAlreadyFinished)
        |> Result.map (fun _ -> TaskDueDateChanged args)

type TaskCommandHandler() =
    let aggregate = { ZeroState = TaskState.zeroState
                      Apply = TaskState.ApplyEvent
                      Exec = executeCommand }
    member x.Handle =
        failwith "Not implemented exception!"