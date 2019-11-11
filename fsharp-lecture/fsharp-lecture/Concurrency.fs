module Concurrency
open System.Threading // only needed for cancellation tokens

let run() = 
    // ** Async workflows
    let asyncTimer x =
        let timer = new System.Timers.Timer(x)
        let timerEvent = Async.AwaitEvent (timer.Elapsed) |> Async.Ignore
        timer.Start()
        printfn "Waiting for timer to finish..."
        // Do other work
        Async.RunSynchronously timerEvent           // blocks
        printfn "Back in sync!"
//    asyncTimer 5000.0

    let manualAsync x = async {
        printfn "Started async workflow"
        do! Async.Sleep(x)                          // await
        printfn "Finished async work"
    }
//    Async.RunSynchronously (manualAsync 5000)

    let cancellationSource = new CancellationTokenSource()      // it's possible to cancel async tasks
    Async.Start(manualAsync 5000, cancellationSource.Token)
    Thread.Sleep(2500)
    cancellationSource.Cancel()
    printfn "Cancelled async task"

    // ** Actor model
    let printerAgent = MailboxProcessor.Start(fun inbox-> 
       let rec messageLoop() = async {
          let! msg = inbox.Receive()                        // Read a message
          printfn "Message is: %s" msg
          return! messageLoop()                             // Infinite loop
          }
       messageLoop() 
       )
//    printerAgent.Post "CPS"
//    printerAgent.Post "452"

    printfn "*** End of Concurrency demo ***"
