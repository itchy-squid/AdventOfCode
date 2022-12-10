function wrap(n) { return n % 40 };

function cycle() { 
    sprite = wrap(x - 1) " " x " " wrap(x + 1);
    output = sprintf("%s%s%s", output, crt == 0 ? "\n" : "", index(sprite, crt) > 0 ? "#" : ".");

    crt = wrap(crt + 1);
}

BEGIN { x = 1 }
/noop/ { cycle() }

/addx/ {
    cycle();
    cycle();
    x = wrap(x + $2);
}

END { print output }