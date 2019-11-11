module Misc

let run() =
    // ** Lazy evaluation
    let x = 100
    let result = lazy (x + 10)
//    printfn "%i" (result.Force())

    // ** Sequences
    let seq1 = seq { for x in 1 .. 1000 do yield x * x }
    let seq2 = seq { for x in 1 .. 1000 -> x * x }              // equivalent
//    printfn "%A" seq1
//    printfn "%A" seq2

    // ** Continuations
    let divideWithContinuation breakCase successCase x y =
          if (y = 0)
          then breakCase()
          else successCase (x / y)

    let zeroCase () = printfn "broke out"
    let regularCase x = printfn "%A" x
//    let good = divideWithContinuation zeroCase regularCase 6 3
//    let bad = divideWithContinuation zeroCase regularCase 6 0

    let x = 1 in
       let y = 2 in
          let z = x + y in
             z |> ignore
    1 |> (fun x ->              // This is the equivalent CPS transformation
       2 |> (fun y ->
          x + y |> (fun z ->
             z))) |> ignore

    // ** Closures
    let simpleClosure = 
        let scope = "old scope" 
        let enclose() = 
            sprintf "%s" scope 
        let scope = "new scope" 
        sprintf "%s ---  %s" scope (enclose())
//    printfn "%A" simpleClosure

    // ** Tail Recursion
    let factTail x =
        let rec factTailHelp x acc =
            if x <= 1 then acc
            else factTailHelp (x - 1) (acc * x)
        factTailHelp x 1
//    printfn "%A" (factTail 5)

    printfn "*** End of Misc demo **"
