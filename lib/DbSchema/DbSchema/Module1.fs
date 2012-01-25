namespace Db

open Microsoft.FSharp.Data.TypeProviders

[<Generate>]
type Schema = SqlDataConnection<"Data Source=localhost\SQLEXPRESS;Initial Catalog=FiveThreeOne;Integrated Security=SSPI;">

