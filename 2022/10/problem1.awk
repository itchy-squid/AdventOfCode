function check() { 
    if(n < length(coi) && coi[n+1] <= cycles) {
        ss += coi[n + 1] * x;
        n = n + 1;
        
        dump("udpate");
    }
}

function dump(header) { 
    # print header;
    # print "\tx:     ",x;
    # print "\tcycles:",cycles;
    # print "\tsignal:",ss;
    # print "" 
}

BEGIN { 
    x = 1;
    split("20 60 100 140 180 220",coi);
}

/noop/ { 
    cycles++;
    check();

    if(cycles > 180) dump("noop");
}

/addx/ {
    cycles+=2;
    check();
    x += $2;

    if(cycles > 180) dump(sprintf("addx %d",$2));
}

END { print ss }