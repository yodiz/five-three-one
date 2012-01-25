namespace FiveThreeOne.Service.Impl

open Microsoft.FSharp.Data.TypeProviders

[<Generate>]
type internal dbSchema = SqlDataConnection<"Data Source=localhost\SQLEXPRESS;Initial Catalog=FiveThreeOne;Integrated Security=SSPI;">

