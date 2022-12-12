# Ex: awk -f problem2.awk test.txt
@include "solve.awk"

BEGIN { 
    SIMP = 1;
    N_ROUNDS = 10000;
}

function apply_relief(w){ w[1] = w[1] % SIMP; }
function presolve() { for(i in monkeys) SIMP *= monkeys[i]["test"][1]; }