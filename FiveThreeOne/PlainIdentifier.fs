namespace FiveThreeOne.Service.Impl

open FiveThreeOne.Service 
open FiveThreeOne.Service.Model
open Microsoft.FSharp.Data.TypeProviders

type PlainIdentifier(connectionString) =
  let getUserIdFor (db:Db.Schema.ServiceTypes.SimpleDataContextTypes.FiveThreeOne) name = 
    let users = query { for row in db.Useridentity do where (row.Name = name); select row.UserId }
    users |> Seq.tryPick Some

  let checkIfUserExists (db:Db.Schema.ServiceTypes.SimpleDataContextTypes.FiveThreeOne) name = getUserIdFor db name |> Option.isSome 

  let createUser (db:Db.Schema.ServiceTypes.SimpleDataContextTypes.FiveThreeOne) name = 
    let id = System.Guid.NewGuid()
    let user = new Db.Schema.ServiceTypes.User( UserId = id )
    let userIdent = new Db.Schema.ServiceTypes.Useridentity( UserId = id, Name=name)
    db.User.InsertOnSubmit(user)
    db.Useridentity.InsertOnSubmit(userIdent)
    db.DataContext.SubmitChanges()
    id

  interface IIdentification with
    member x.Authenticate name = 
      let db = Db.Schema.GetDataContext(connectionString)
      db.DataContext.Log <- System.Console.Out

      use tran = new System.Transactions.TransactionScope()

      let userId = getUserIdFor db name
      let userId =
        match userId with |Some id -> id |None -> createUser db name
    
      tran.Complete()

      { Useridentification.Id = userId }