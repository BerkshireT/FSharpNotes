module main
open System
(* types
open Microsoft.FSharp.Quotations

type Person = { FirstName: string; LastName: string }

let main() =
   let Tyler = { FirstName = "Tyler"; LastName = "Berkshire" } // construct
   printfn "%s" Tyler.FirstName

main()
*)

(* Metaprogramming
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns

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

// Output
// fun (x:System.Int32) -> x + 1
// a + 1
// Operators.op_Subtraction(a,1)
// let f = fun (x:System.Int32) -> x + 10 in f 10
*)

(* Piping etc
let main() =
    let list = [1 .. 10 ] |> Seq.filter (fun x -> x % 2 <> 0)
                          |> Seq.map (fun x -> x * x)
                          |> Seq.sum
    printfn "%A" list

    let composed = Seq.filter (fun x -> x % 2 <> 0)
                   >> Seq.map (fun x -> x * x)
                   >> Seq.sum
    printfn "%A" (composed [1 .. 10])
    //printfn "%A" <| composed [1 .. 10]

    let printTwo a b = printfn "%A and %A" a b
    printTwo "CPS" 452

    let printTwoCurried a =
        let printTwoSecond b =
            printfn "%A and %A" a b
        printTwoSecond
    printTwoCurried "CPS" 452

    let filterEvens = List.filter (fun x -> x % 2 = 0)
    printfn "%A" <| filterEvens [ 1 .. 5 ] 

    let replace oldStr newStr (s:String) = s.Replace(oldValue = oldStr, newValue = newStr)   // Input string is now the last parameter
    let result =
        "CPS"
        |> replace "CPS" "452"
    printfn "%A" result
    
main()
*)
(* agents
let printerAgent = MailboxProcessor.Start(fun inbox-> 
   let rec messageLoop() = async {
      let! msg = inbox.Receive()                        // Read a message
      printfn "Message is: %s" msg
      return! messageLoop()                             // Infinite loop
      }
   messageLoop() 
   )
printerAgent.Post "CPS"
printerAgent.Post "452"


*)

(* misc
let x = 100
let result = lazy (x + 10)
printfn "%i" (result.Force())      // 110

let seq1 = seq { for x in 1 .. 10 do yield x * x }   // 
let seq2 = seq { for x in 1 .. 10 -> x * x }         // equivalent
printfn "%A" seq1
printfn "%A" seq2

let divideWithContinuation breakCase successCase x y =
      if (y = 0)
      then breakCase()
      else successCase (x / y)

let zeroCase () = printfn "broke out"
let regularCase x = printfn "%A" x
let good = divideWithContinuation zeroCase regularCase 6 3   // 2
let bad = divideWithContinuation zeroCase regularCase 6 0    // "broke out"

let x = 1 in
   let y = 2 in
      let z = x + y in
         z |> ignore
1 |> (fun x ->              // This is the equivalent CPS transformation
   2 |> (fun y ->
      x + y |> (fun z ->
         z))) |> ignore

let simpleClosure = 
  let scope = "old scope" 
  let enclose() = 
    sprintf "%s" scope 
  let scope = "new scope" 
  sprintf "%s ---  %s" scope (enclose())

printfn "%A" simpleClosure                  // "new scope ---  old scope"
*)