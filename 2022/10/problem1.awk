function check() { 
    if(n + 20 <= cycles) {
        ss += (n + 20) * x;
        n += 40;
    }
}

BEGIN { x = 1 }

/noop/ { 
    cycles++;
    check();
}

/addx/ {
    cycles+=2;
    check();
    x += $2;
}

END { print ss }