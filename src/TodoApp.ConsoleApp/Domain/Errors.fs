module TodoApp.ConsoleApp.Domain.Errors

open System

type DomainError =
    | TaskAlreadyExists   of int
    | TaskDoesNotExist    of int
    | TaskAlreadyFinished of string

type AppError =
    | Domain of DomainError
    | Bug of exn
    static member create(e:DomainError) = e |> Domain
    static member createResult(e:DomainError) = e |> Domain |> Error
    static member createResult(e:exn) = e |> Bug |> Error 