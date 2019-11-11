module Metaprogramming
open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns

let run() =
    // ** Quotations
    let quote: Expr<'T> = <@ "CPS " + "452" @>   // typed
    let quote2: Expr = <@@ 1 + 1 @@>             // untyped
    printfn "%A" quote
    printfn "%A" quote2

    let complete = <@ let f x = x + 10 in f 20 @>
    printfn "%A" complete
    (* equivalent to
    let complete = <@ 
        let f x = x + 10
        f 20 @>
    *)

    //let notComplete = <@ let f x = x + 10 @> // error

    // ** Splicing
    let spliced = <@ "I'm in " + %quote @>
    let spliced2 = <@@ 1 + %%quote2 @@> // error
    //let spliced2 = <@@ "I'm in " + %%quote2 @@> // error, doesn't catch invalid type operation
    //let spliced2 = <@@ 1 + %%quote @@> // error, can't cast typed quotation as untyped
    printfn "%A" spliced
    printfn "%A" spliced2

    // ** F# code translation example from Microsoft docs
    
    let printQ expr =
        let rec print expr =
            match expr with
            | Application(expr1, expr2) ->
                // Function application.
                print expr1
                printf " "
                print expr2
            | SpecificCall <@@ (+) @@> (_, _, exprList) ->
                // Matches a call to (+). Must appear before Call pattern.
                print exprList.Head
                printf " + "
                print exprList.Tail.Head
            | Call(_, methodInfo, exprList) ->
                printf "%s.%s(" methodInfo.DeclaringType.Name methodInfo.Name
                if (exprList.IsEmpty) then printf ")" else
                print exprList.Head
                for expr in exprList.Tail do
                    printf ","
                    print expr
                printf ")"
            | Int32(n) ->
                printf "%d" n
            | Lambda(param, body) ->
                // Lambda expression.
                printf "fun (%s:%s) -> " param.Name (param.Type.ToString())
                print body
            | Let(var, expr1, expr2) ->
                // Let binding.
                if (var.IsMutable) then
                    printf "let mutable %s = " var.Name
                else
                    printf "let %s = " var.Name
                print expr1
                printf " in "
                print expr2
            | PropertyGet(_, propOrValInfo, _) ->
                printf "%s" propOrValInfo.Name
            | String(str) ->
                printf "%s" str
            | Value(value, typ) ->
                printf "%s" (value.ToString())
            | Var(var) ->
                printf "%s" var.Name
            | _ -> printf "%s" (expr.ToString())
        print expr
        printfn ""

    let a = 2

    // exprLambda has type "(int -> int)".
    let exprLambda = <@ fun x -> x + 1 @>
    // exprCall has type unit.
    let exprCall = <@ a + 1 @>
    let exprUnimplemented = <@ a - 1 @>

    printQ exprLambda
    printQ exprCall
    printQ exprUnimplemented
    printQ <@@ let f x = x + 10 in f 10 @@>

    printfn "*** End of Metaprogramming demo ***"
