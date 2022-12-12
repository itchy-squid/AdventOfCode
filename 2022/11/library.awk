function execute_round() { for(round_i in monkeys) execute_turn(monkeys[round_i]); }

function execute_turn(monkey) {
    # Make worry an array so I can pass by reference.
    worry[1] = 0                

    while(take(monkey, worry)) {
        monkey["inspected"]++;
        
        inspect(monkey, worry);
        throw(monkey, worry);
    }
}

function take(m, w){
    if(match(m["items"], "[0-9]+") == 0) return 0;
    w[1] = substr(m["items"], RSTART, RLENGTH);
    m["items"] = substr(m["items"], RSTART + RLENGTH + 1);
    return 1;
}

function inspect(m, w) {
    op = m["op"][2];
    p1 = evaluate(w[1], m["op"][1]);
    p2 = evaluate(w[1], m["op"][3]);
    t = m["test"][1];

    if(op == "+") w[1] = p1 + p2;
    else if(op == "*") w[1] = p1 * p2;
    else print "UNKNOWN OPERATOR",m["op"][2];

    apply_relief(w);
}

function evaluate(w, x) { return x == "old" ? w : x; }

function throw(m, w) {
    target = (w[1] % m["test"][1]) == 0 ? m["test"][2] : m["test"][3];
    monkeys[target]["items"] = monkeys[target]["items"] " " w[1];
}