#@include "round.awk"

BEGIN {
    FS = "[:, ] *";
    ROUNDMODE = "Z"
}

function round() { 
    for(idx in monkeys) turn(monkeys[idx]);
}

function turn(monkey) {
    # Make worry an array so I can pass by reference.
    worry[1] = 0                

    while(take(monkey, worry)) {
        monkey["inspected"]++;
        
        # print "Item taken: " worry[1] "...";
        inspect(monkey, worry);
        throw(monkey, worry);

        # dump();
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

    if(op == "+") w[1] = p1 + p2;
    else if(op == "*") w[1] = p1 * p2;
    else print "UNKNOWN OPERATOR",m["op"][2];

    w[1] /= 3;
}

function evaluate(w, x) {
    return x == "old" ? w : x
}

function throw(m, w) {
    target = (w[1] % m["test"][1]) == 0 ? m["test"][2] : m["test"][3];
    monkeys[target]["items"] = monkeys[target]["items"] " " int(w[1]);
}

function dump () {
    for(i in monkeys){
        print "monkey",i;
        print "inpsected: ",monkeys[i]["inspected"];
        print "items:\t" monkeys[i]["items"]; 
        # dump_array(monkeys[i]["items"], "items");
        dump_array(monkeys[i]["op"], "op");
        dump_array(monkeys[i]["test"], "test");
        print "";
    }
}

function dump_array (arr, label) {
    output = ""
    for(j in arr){
        output = output " " arr[j];
    }
    print label ":\t" output;
}

/Monkey [0-9]+/ {
    curr = $2;
}

/Starting items/ {
    for(i=4; i<=NF; i++) monkeys[curr]["items"] = monkeys[curr]["items"] " " $i
}

/Operation/ {
    for(i=5; i<=NF; i++) monkeys[curr]["op"][i-4] = $i
}

/Test: divisible by/ {
    monkeys[curr]["test"][1] = $5
}

/If (true)|(false): throw to monkey/ {
    monkeys[curr]["test"][$3 == "true" ? 2 : 3] = $7
}

END {
    for(i=1; i<=20; i++) round();

    dump();
}