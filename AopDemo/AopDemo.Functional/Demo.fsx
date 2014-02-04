open System.Collections.Generic

let memoize (f : 'a -> 'b) =
    let cache = new Dictionary<'a, 'b>()

    let memoizedFunc (input : 'a) =
        // check if there is a cached result for this input
        match cache.TryGetValue(input) with
        | true, x   -> x
        | false, _  ->
            // evaluate and add result to cache
            let result = f input
            cache.Add(input, result)
            result

    // return the memoized version of f
    memoizedFunc

// function that sleeps for the specified number of seconds
// before returning the input
let f x =
    // sleep for x number of seconds
    System.Threading.Thread.Sleep(x * 1000)

    // return
    x

// create a memoized version of f
let memoizedF = memoize f

// run the original function 10 times
printfn "Running original function f 10 times"

#time
[1..10] |> List.map (fun i -> f 1) |> ignore
#time

printfn "Running memoized function f 10 times"

#time
[1..10] |> List.map (fun i -> memoizedF 1) |> ignore
#time

// WRONG way to memoize the function - fib doesn’t call the
// memoized version recursively
let wrongMemFib =
    let rec fib x =
        match x with
        | 0 | 1 -> 1
        | 2 -> 2
        | n -> fib (n - 1) + fib (n - 2)

    memoize fib

// CORRECT way to memoize the function - fib does call the
// memoized version recursively
let rec rightMemFib =
    let fib x =
        match x with
        | 0 | 1 -> 1
        | 2 -> 2
        | n -> rightMemFib (n - 1) + rightMemFib (n - 2)

    memoize fib

#time

// only fib 40 is memoized
wrongMemFib 40

// every fib x up to fib 40 is memoized
rightMemFib 40