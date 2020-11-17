module TodoApp.ConsoleApp.Domain.Aggregate

type Aggregate<'TState, 'TCommand, 'TEvent, 'TError> =
    { ZeroState: 'TState
      Apply:     'TState -> 'TEvent -> 'TState
      Exec:      'TState -> 'TCommand -> Result<'TEvent, 'TError> }
    with
    static member createHandler = failwith "Not Implemented exception!"