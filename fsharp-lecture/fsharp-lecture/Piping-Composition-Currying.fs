module Piping_etc

let run() = 
    // ** Piping
    let squareOddsPipe = [1 .. 10] |> Seq.filter (fun x -> x % 2 <> 0)
                          |> Seq.map (fun x -> x * x)
                          |> Seq.sum
    printfn "%A" squareOddsPipe

    // ** Composition
    let squareOddsComp = Seq.filter (fun x -> x % 2 <> 0)
                        >> Seq.map (fun x -> x * x)
                        >> Seq.sum
    printfn "%A" (squareOddsComp [1 .. 10])
    //printfn "%A" <| squareOddsComp [1 .. 10]
    
    // foo (fun x -> x |> bar |> baz)
    // foo (bar >> baz)

    // ** Currying
    let printTwo (a,b) = printfn "%A and %A" a b
    printTwo("CPS", 452)

    let printTwoCurried a =
        let printTwoSecond b =
            printfn "%A and %A" a b
        printTwoSecond
    printTwoCurried "CPS" 452

    let filterEvens = List.filter (fun x -> x % 2 = 0)      // partial application
    printfn "%A" <| filterEvens [ 1 .. 10 ] 

    let replace oldStr newStr (s:string) = s.Replace(oldValue = oldStr, newValue = newStr)   // Rewrite .NET library func for partial app
    let result x =
        x
        |> replace "CPS" "452"                              // input string is now the last param
    printfn "%A" (result "CPS 452 is the best CPS class")
    
    printfn "*** End of Piping, Composition, Currying demo ***"
