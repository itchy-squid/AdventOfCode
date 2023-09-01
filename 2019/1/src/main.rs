use std::fs::File;
use std::io::{BufRead, BufReader, Error};
use std::path::Path;

// Day 1 : The Tyranny of the Rocket Equation 
// The world's most painful Day 1
fn main() -> Result<(), Error> {
    let path_to_read= Path::new("../inputs/1-1.txt");
    let file = File::open(&path_to_read)?;
    let file = BufReader::new(file);
    
    let ns :Vec<_> = file
        .lines()
        .map(|l| l.expect("invalid input"))
        .map(|l| l.parse::<i64>().expect("expected a number"))
        .collect();

    let problem1 = ns
        .clone()
        .into_iter()
        .map(|n| n / 3i64 - 2i64)
        .reduce(|a, b| a + b);

    let problem2 = ns
        .into_iter()
        .map(calculate_fuel)
        .reduce(|a, b| a + b);
    
    println!("{}",problem1.expect("result must be an integer"));
    println!("{}",problem2.expect("result must be an integer"));
    Ok(())
}

fn calculate_fuel(n : i64) -> i64 {
    let requires = n / 3i64 - 2i64;

    if requires <= 0 { 0 }
    else { requires + calculate_fuel(requires) }
}