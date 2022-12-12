@include "library2.awk"

# Ex: awk -f problem.awk -v N_ROUNDS=20 test.txt

BEGIN { FS = "[:, ] *" }
/Monkey [0-9]+/ { curr = $2 }
/Starting items/ { for(i=4; i<=NF; i++) monkeys[curr]["items"] = monkeys[curr]["items"] " " $i }
/Operation/ { for(i=5; i<=NF; i++) monkeys[curr]["op"][i-4] = $i }
/Test: divisible by/ { monkeys[curr]["test"][1] = $5 }
/If (true)|(false): throw to monkey/ { monkeys[curr]["test"][$3 == "true" ? 2 : 3] = $7 }

END {
    SIMP = 1;
    for(i in monkeys) SIMP *= monkeys[i]["test"][1]; 
    for(r=1; r<=N_ROUNDS; r++) 
    {
        execute_round();
        if(r % 1000 == 0 || r == 1 || r == 20) {
            print "== After round " r " ==";
            for(i in monkeys) print "monkey " i " inspected " monkeys[i]["inspected"] " times.";
            print "";
        }
    }
    for(i in monkeys) counts[i + 1] = monkeys[i]["inspected"];
    asort(counts);

    # dump();
    print counts[length(counts)] " x " counts[length(counts) - 1] " = " (counts[length(counts)] * counts[length(counts) - 1]);
}