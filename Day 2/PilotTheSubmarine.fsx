open System
open System.IO
open System.Text

let inputFile = File.ReadAllLines $"{__SOURCE_DIRECTORY__}/input.txt"

type Direction =
    | Forward
    | Upwards
    | Downwards

type Instruction = {
    Direction : Direction
    Amount : int
}

type Position = {
    Depth : int
    Horizontal : int
    Aim : int
}

let submarinePosition = {Depth = 0; Horizontal = 0; Aim = 0}
let processRow (xs : string array) : Instruction =
    if xs.Length = 2 then
        match xs.[0] with
        | "forward" -> {Direction = Forward; Amount = int xs.[1]}
        | "down" -> {Direction = Downwards; Amount = int xs.[1]}
        | "up" -> {Direction = Upwards; Amount = int xs.[1]}
        | _ -> failwith "unknown instruction"
    else
        failwith "incorrectly formatted row"

let instructionArray = 
    inputFile
    |> Array.map ((fun x -> x.Split " ") >> processRow)

// Part 1
let accumulateInstructions (acc : Position) (x : Instruction) : Position =
    match x.Direction with
    | Forward -> { acc with Horizontal = acc.Horizontal + x.Amount }
    | Downwards -> { acc with Depth = acc.Depth + x.Amount }
    | Upwards -> { acc with Depth = acc.Depth - x.Amount }

instructionArray
|> Array.fold accumulateInstructions submarinePosition
|> (fun x -> x.Depth * x.Horizontal)
|> printfn "Final position: %A"

// Part 2
let accumulateInstructions2 (acc : Position) (x : Instruction) : Position =
    match x.Direction with
    | Forward -> 
        { acc with 
            Horizontal = acc.Horizontal + x.Amount
            Depth = acc.Depth + (acc.Aim * x.Amount)}

    | Downwards -> { acc with Aim = acc.Aim + x.Amount }
    | Upwards -> { acc with Aim = acc.Aim - x.Amount }

instructionArray
|> Array.fold accumulateInstructions2 submarinePosition
|> (fun x -> x.Depth * x.Horizontal)
|> printfn "Final Position: %A"