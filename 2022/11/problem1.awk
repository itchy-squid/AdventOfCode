# Ex: awk -f problem1.awk test.txt
@include "solve.awk"

BEGIN { N_ROUNDS = 20 }

function apply_relief(w){ w[1] = int(w[1] / 3); }
function presolve() { }