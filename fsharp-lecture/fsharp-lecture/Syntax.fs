module Syntax

let run() =
    // single-line comment
    (* multi line
        comment *)

    // ** let defines immutable values
    let myName = "Tyler"

    // ** lists are homogeneous
    let myList1 = ["1";"2";"3";"4"]
    let myList2 = [1;2;3;4]
    let myList3 = [5;6;7;8;9]

    // ** concatenate two lists
    printfn "%A" (5 :: myList2) // pretty printing built in
    //printfn "%A" (myList2 :: myList3)     // error, can only add one element to front
    printfn "%A" (myList2 @ myList3)
    //printfn "%A" (myList1 @ myList3)      //error

    // ** functions
    let add x y = x + y
    let add2 (x,y) = x + y                  // not the same, not curried
    printfn "%A" (add 1 2)
    printfn "%A" (add2(1,2))
    let sumOfSquares = List.sum ( List.map (fun x -> x * x) [1..10] ) // parens also clarify precedence
    printfn "%A" sumOfSquares
    let multiLine = 
        printfn "This is a "
        printfn "multiline function"
    let multiTest = multiLine               // prints return null
    printfn "%A" multiTest

    // ** pattern matching
    let rec reverse lst =
        match lst with
        | [] -> []
        | [x] -> [x]
        | x::xs -> reverse xs @ [x]

    //printfn "%A" (reverse [1;2;3;4;5])

    // ** nullable wrappers
    let valid = Some(100)
    let invalid = None
    let printValid x =
        match x with
        | Some y -> printfn "Valid input - %A" y
        | None -> printfn "Invalid input - %A" x
    printValid valid
    printValid invalid
    
    printfn "*** End of syntax demo ***"
