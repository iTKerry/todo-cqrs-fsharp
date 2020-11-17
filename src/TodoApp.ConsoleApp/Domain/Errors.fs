module TodoApp.ConsoleApp.Domain.Errors

type DomainError =
    | TaskAlreadyExists   of int
    | TaskDoesNotExist    of int
    | TaskAlreadyFinished of string

type AppError =
    | Domain of DomainError
    static member create(e:DomainError) = e |> Domain
    static member createResult(e:DomainError) = e |> Domain |> Error