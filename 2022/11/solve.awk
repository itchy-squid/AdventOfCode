@include "library.awk"

BEGIN { FS = "[:, ] *" }
/Monkey [0-9]+/ { curr = $2 }
/Starting items/ { for(i=4; i<=NF; i++) monkeys[curr]["items"] = monkeys[curr]["items"] " " $i }
/Operation/ { for(i=5; i<=NF; i++) monkeys[curr]["op"][i-4] = $i }
/Test: divisible by/ { monkeys[curr]["test"][1] = $5 }
/If (true)|(false): throw to monkey/ { monkeys[curr]["test"][$3 == "true" ? 2 : 3] = $7 }

END { 
    presolve();
    
    for(r=1; r<=N_ROUNDS; r++) execute_round();
    for(i in monkeys) counts[i + 1] = monkeys[i]["inspected"];
    asort(counts);

    print counts[length(counts)] * counts[length(counts) - 1];
}