open System
open System.IO
open System.Text

let inputFile = File.ReadAllLines $"{__SOURCE_DIRECTORY__}/input.txt"

type BitCount = {
    Zeroes : int
    Ones : int 
}

type PowerConsumption = {
    Gamma : int
    Epsilon : int
}

let stringToBitCountFolder (acc : BitCount) (x : char) : BitCount =
    match x with 
    | '0' -> { acc with Zeroes = acc.Zeroes + 1 }
    | '1' -> { acc with Ones = acc.Ones + 1 }
    | _ -> failwith "incorrect bit"

let processBitCount (acc : string * string) (x : BitCount) : (string * string) =
    let g, e = acc
    if x.Zeroes > x.Ones then
        (g + "0", e + "1")
    else
        (g + "1", e + "0")

let binarize ((a, b) : string * string) : (int * int) =
    (Convert.ToInt32(a, 2), Convert.ToInt32(b, 2))

inputFile
|> Seq.map (fun x -> x.ToCharArray())
|> Array.transpose
|> Seq.map (System.String >> (Seq.fold stringToBitCountFolder {Zeroes = 0; Ones = 0}))
|> Seq.fold processBitCount ("", "")
|> binarize
|> fun (a, b) -> a * b
|> printfn "%A"