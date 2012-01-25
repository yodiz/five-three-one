namespace FiveThreeOne.Service.Impl

open FiveThreeOne.Service 
open FiveThreeOne.Service.Model
open FiveThreeOne.Service.Impl
open Microsoft.FSharp.Data.TypeProviders

open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols

module private Convert = 
  let dbExerciseType2ExerciseType (str:string) = 
    match (str.ToUpper()) with
    |"MAIN" -> ExerciseType.Main 
    |"SUPPLIMENT" -> ExerciseType.Suppliment 
    |a -> failwithf "not implemented %A" a

  let exerciseType2dbExerciseType = function |ExerciseType.Main -> "MAIN" |ExerciseType.Suppliment -> "SUPPLIMENT"

  let week2dbWeek = function |Week1 -> 1uy |Week2 -> 2uy |Week3 -> 3uy |Week4 -> 4uy

  

  let exceriseToNamedExcersice (e : Db.Schema.ServiceTypes.Exercise) : NamedExercise = 
    { NamedExercise.Name = e.Name; Exercise = { Exercise.WorkoutMax = e.WorkoutMaxInKg * 1.0<kg>; ExerciseType = e.Type |> dbExerciseType2ExerciseType }  }


open Convert



type Exercise(connectionString) = 
  interface IExercise with
    member x.CreateExercise user exercise = 
      let db = Db.Schema.GetDataContext(connectionString)
      db.DataContext.Log <- System.Console.Out

      let dbExc = 
        new Db.Schema.ServiceTypes.Exercise 
          ( UserId = user.Id, Name = exercise.Name, 
            WorkoutMaxInKg = (exercise.Exercise.WorkoutMax/1.0<kg>), 
            Type = (exercise.Exercise.ExerciseType |> exerciseType2dbExerciseType) )

      db.Exercise.InsertOnSubmit(dbExc)
      db.DataContext.SubmitChanges()

    member x.UpdateExercise user name exercise = 
      let db = Db.Schema.GetDataContext(connectionString)
      db.DataContext.Log <- System.Console.Out

      let excersie = 
        query { for row in db.Exercise do where (row.UserId = user.Id && row.Name = name); select row }
      excersie 
      |> Seq.iter 
        (fun e -> 
          e.Type <- (exercise.ExerciseType.ToString())
          e.WorkoutMaxInKg <- exercise.WorkoutMax/1.0<kg>
        )

      db.DataContext.SubmitChanges()      

    member x.GetExercises user = 
      let db = Db.Schema.GetDataContext(connectionString)
      db.DataContext.Log <- System.Console.Out
      db.DataContext.ObjectTrackingEnabled <- false


      let excersie = 
        query { for row in db.Exercise do where (row.UserId = user.Id); select (exceriseToNamedExcersice row) }
      excersie |> Seq.toArray 

    member x.GetExercise user name = 
      let db = Db.Schema.GetDataContext(connectionString)
      db.DataContext.Log <- System.Console.Out
      db.DataContext.ObjectTrackingEnabled <- false

      let excersie = 
        query { for row in db.Exercise do where (row.UserId = user.Id && row.Name = name); select (exceriseToNamedExcersice row) }
      excersie |> Seq.tryPick Some

    member x.CalculateExerciseSets exercise week = 
      let weightMax = exercise.WorkoutMax
      let sets = 
        match exercise.ExerciseType, week with
        |ExerciseType.Main,Week.Week1 -> 
          [| 
            { Set.Repititions = Repitition.Fixed 5; Weight = weightMax*0.65 }
            { Set.Repititions = Repitition.Fixed 5; Weight = weightMax*0.75 }
            { Set.Repititions = Repitition.MaximumReps 5; Weight = weightMax*0.85 }
          |]
        |ExerciseType.Main,Week.Week2 -> 
          [| 
            { Set.Repititions = Repitition.Fixed 3; Weight = weightMax*0.70 }
            { Set.Repititions = Repitition.Fixed 3; Weight = weightMax*0.80 }
            { Set.Repititions = Repitition.MaximumReps 3; Weight = weightMax*0.90 }
          |]
        |ExerciseType.Main,Week.Week3 -> 
          [| 
            { Set.Repititions = Repitition.Fixed 5; Weight = weightMax*0.75 }
            { Set.Repititions = Repitition.Fixed 3; Weight = weightMax*0.85 }
            { Set.Repititions = Repitition.MaximumReps 1; Weight = weightMax*0.95 }
          |]
        |ExerciseType.Main,Week.Week4 -> 
          [| 
            { Set.Repititions = Repitition.Fixed 5; Weight = weightMax*0.4 }
            { Set.Repititions = Repitition.Fixed 5; Weight = weightMax*0.5 }
            { Set.Repititions = Repitition.Fixed 5; Weight = weightMax*0.6 }
          |]
        |ExerciseType.Suppliment, Week.Week1 -> 
          [| 
            { Set.Repititions = Repitition.Fixed 12; Weight = 0.0<kg> }
            { Set.Repititions = Repitition.Fixed 12; Weight = 0.0<kg> }
            { Set.Repititions = Repitition.Fixed 12; Weight = 0.0<kg> }
          |]
        |ExerciseType.Suppliment, Week.Week2 -> 
          [| 
            { Set.Repititions = Repitition.Fixed 8; Weight = 0.0<kg> }
            { Set.Repititions = Repitition.Fixed 8; Weight = 0.0<kg> }
            { Set.Repititions = Repitition.Fixed 8; Weight = 0.0<kg> }
            { Set.Repititions = Repitition.Fixed 8; Weight = 0.0<kg> }
          |]
        |ExerciseType.Suppliment, Week.Week3 -> 
          [| 
            { Set.Repititions = Repitition.Fixed 5; Weight = 0.0<kg> }
            { Set.Repititions = Repitition.Fixed 5; Weight = 0.0<kg> }
            { Set.Repititions = Repitition.Fixed 5; Weight = 0.0<kg> }
            { Set.Repititions = Repitition.Fixed 5; Weight = 0.0<kg> }
          |]
        |ExerciseType.Suppliment, Week.Week4 -> 
          [| 
            { Set.Repititions = Repitition.Fixed 20; Weight = 0.0<kg> }
            { Set.Repititions = Repitition.Fixed 20; Weight = 0.0<kg> }
          |]
      
      { Sets.Sets=sets }


    member x.ReportSet user name week actualWeight actualRepititations comment = 
      let db = Db.Schema.GetDataContext(connectionString)
      db.DataContext.Log <- System.Console.Out

      let dbExc = new Db.Schema.ServiceTypes.Result ()
      dbExc.UserId <- user.Id 
      dbExc.ExcerciseName <- name 
      dbExc.ExcersiceDate <- System.DateTime.Today 
      dbExc.Repititations <- byte actualRepititations
      dbExc.Week <- week2dbWeek week
      dbExc.WeightInKg <- actualWeight/1.0<kg>

      db.Result.InsertOnSubmit(dbExc)
      db.DataContext.SubmitChanges()