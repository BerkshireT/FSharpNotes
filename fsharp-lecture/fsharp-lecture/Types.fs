module Types

// must be declared in namespaces/modules
type Student = { First: string; Last: string; ID: int } // product
type Staff =                                            // sum/union
    | Professor of Student list * last: string          // product within union
    | TA of Student * Staff * course: string

type BinTree<'a> =                                      // Generic
    | Leaf
    | T of BinTree<'a> * 'a * BinTree<'a>

let run() =
   let s1 = { First = "Tyler"; Last = "Berkshire"; ID = 1 }
   let s2 = { First = "Tom"; Last = "McKernan"; ID = 2 }
   let s3 = { First = "Jacob"; Last = "Schwartz"; ID = 3 }
   let listOfStudents = [s1;s2;s3]
   let p1 = Professor(listOfStudents, "Perugini")
   let t1 = TA(s1, p1, "CPS 999")

   for x in listOfStudents do printfn "%d: %s, %s" x.ID x.Last x.First

//   printfn "*** Pretty Print: %A" p1
   let professorPrint x = 
       match x with
       | Professor(students, lastname) -> 
          printf "Professor: %s, Students: " lastname
          for student in students do printf "%s, " student.Last
          printfn ""
//   professorPrint p1 |> ignore

//   printfn "*** Pretty Print: %A" t1
   let taPrint x =
      match x with
      | TA(student, staff, course) ->
         let getLastName y = 
            match y with
            | Professor(_, lastname) -> lastname
         printf "TA: %s, Professor: %s, Course: %s" (student.First + " " + student.Last) (getLastName staff) course
//   taPrint t1 |> ignore

   // ** tuples
   let tup1 = ("CPS", 452)
   let tup2 = ("CPS", 452, [s1;s2;s3])
   printfn "First: %A, Second: %A " (fst tup1) (snd tup1)
   //printfn "%A %A %A" (fst tup2) (snd tup2) (thd tup2)      // error
   let fst1 (x, _, _) = x
   let snd1 (_, x, _) = x
   let thd (_, _, x) = x
   printfn "%A %A %A" (fst1 tup2) (snd1 tup2) (thd tup2)
   tup1.GetHashCode() |> ignore             // implicit hash values for dict use

   printfn "*** End of Types demo ***"
