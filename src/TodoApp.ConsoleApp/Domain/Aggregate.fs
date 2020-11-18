module TodoApp.ConsoleApp.Domain.Aggregate

open TodoApp.ConsoleApp.Domain
open TodoApp.ConsoleApp.Domain.State
open TodoApp.ConsoleApp.Domain.Events

type TasksRepository = {
    Load : unit -> TasksBucket
    Save : Event -> unit
}

type Aggregate<'TState, 'TCommand, 'TEvent, 'TError> =
    { ZeroState: 'TState
      Apply:     'TState -> 'TEvent -> 'TState
      Exec:      'TState -> 'TCommand -> Result<'TEvent, 'TError> }
    with
    static member createHandler 
        ( aggregate : Aggregate<'TState, 'TCommand, 'TEvent, 'TError>,
          load : unit -> 'TState,
          save : 'TEvent -> unit) command =
        let state = load()
        let eventRes = command |> aggregate.Exec state
        match eventRes with
        | Ok value -> save value |> (fun _ -> Result.Ok value)
        | Error msg -> Result.Error msg