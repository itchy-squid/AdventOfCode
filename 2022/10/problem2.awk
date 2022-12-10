function cycle() { 
    cycles++;

    sprite = sprintf("%s %s %s", (x - 1) % 40, x % 40, (x + 1) % 40);
    output = sprintf("%s%s%s", output, crt == 0 ? "\n" : "", index(sprite, crt) > 0 ? "#" : ".");
    crt = (crt + 1) % 40;

    if(n < length(coi) && coi[n+1] <= cycles) {
        ss += coi[n + 1] * x;
        n = n + 1;
    }
}

BEGIN { x = 1 }

/noop/ { 
    cycle();
}

/addx/ {
    cycle();
    cycle()
    x += $2;
}

END { print output }