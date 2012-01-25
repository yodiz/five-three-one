namespace FiveThreeOne.Service.Impl

open FiveThreeOne.Service 
open FiveThreeOne.Service.Model
open FiveThreeOne.Service.Impl
open Microsoft.FSharp.Data.TypeProviders

type PlainIdentifier(connectionString) =
  let getUserIdFor (db:dbSchema.ServiceTypes.SimpleDataContextTypes.FiveThreeOne) name = 
    let users = query { for row in db.Useridentity do where (row.Name = name); select row.UserId }
    users |> Seq.tryPick Some

  let checkIfUserExists (db:dbSchema.ServiceTypes.SimpleDataContextTypes.FiveThreeOne) name = getUserIdFor db name |> Option.isSome 

  let createUser (db:dbSchema.ServiceTypes.SimpleDataContextTypes.FiveThreeOne) name = 
    let id = System.Guid.NewGuid()
    let user = new dbSchema.ServiceTypes.User( UserId = id )
    let userIdent = new dbSchema.ServiceTypes.Useridentity( UserId = id, Name=name)
    db.User.InsertOnSubmit(user)
    db.Useridentity.InsertOnSubmit(userIdent)
    db.DataContext.SubmitChanges()
    id

  interface IIdentification with
    member x.Authenticate name = 
      let db = dbSchema.GetDataContext(connectionString)
      db.DataContext.Log <- System.Console.Out

      use tran = new System.Transactions.TransactionScope()

      let userId = getUserIdFor db name
      let userId =
        match userId with |Some id -> id |None -> createUser db name
    
      tran.Complete()

      { Useridentification.Id = userId }