module Module1

open FiveThreeOne.Service

open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols

let identifier : IIdentification = upcast Impl.PlainIdentifier("Data Source=localhost\SQLEXPRESS;Initial Catalog=FiveThreeOne;Integrated Security=SSPI;")

let exs : IExercise = upcast Impl.Exercise("Data Source=localhost\SQLEXPRESS;Initial Catalog=FiveThreeOne;Integrated Security=SSPI;")

printfn "%A" (identifier.Authenticate("Micke B"))


let exsercise = { Exercise.WorkoutMax = 100.0<kg>; ExerciseType=ExerciseType.Main }

let sets = exs.CalculateExerciseSets exsercise Week.Week1 

printf "%A" sets

System.Console.ReadLine() |> ignore