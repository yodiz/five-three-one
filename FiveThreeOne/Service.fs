namespace FiveThreeOne.Service

open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols


type IIdentification = 
  abstract member Authenticate : string -> Useridentification

type IExercise = 
  abstract member CreateExercise : Useridentification -> NamedExercise -> unit
  abstract member UpdateExercise : Useridentification -> exerciseName:string -> Exercise -> unit
  abstract member GetExercise : Useridentification -> exerciseName:string -> NamedExercise option
  abstract member GetExercises : Useridentification -> NamedExercise array
  abstract member CalculateExerciseSets : Exercise -> Week -> Sets
  abstract member ReportSet : Useridentification -> exerciseName:string -> Week -> actualWeight:float<kg> -> actualRepititations:int -> comment:string -> unit

