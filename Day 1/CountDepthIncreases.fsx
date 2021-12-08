open System.IO

let inputFile = File.ReadAllLines $"{__SOURCE_DIRECTORY__}/input.txt"
printfn "First number: %A" inputFile.[0]

let inputArray = Array.map int inputFile

// Part 1
inputArray
|> Array.pairwise
|> Array.sumBy (fun (x, y) -> if y > x then 1 else 0)
|> printfn "Number of increments: %A"

// Part 2
inputArray
|> Array.windowed 3
|> Array.map Array.sum
|> Array.pairwise
|> Array.sumBy (fun (x, y) -> if y > x then 1 else 0)
|> printfn "Number of windowed-sum increments: %A" 