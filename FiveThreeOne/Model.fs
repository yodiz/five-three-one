[<AutoOpen>]
module FiveThreeOne.Service.Model

open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols


type Useridentification = {
  Id : System.Guid
}

type ExerciseType = Main|Suppliment

type Exercise = {
  WorkoutMax : float<kg>
  ExerciseType : ExerciseType
}

type NamedExercise = {
  Name : string
  Exercise : Exercise
}

type Week = Week1|Week2|Week3|Week4


type Repitition = 
  |Fixed of int
  |MaximumReps of int

type Set = {
  Repititions : Repitition
  Weight : float<kg>
}

type Sets = {
  Sets : Set array
}