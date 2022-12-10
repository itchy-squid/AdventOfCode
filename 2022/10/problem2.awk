function wrap(n) { return n % 40 };
function over(x) { return crt == (x % 40) }

function cycle() { 
    output = output (crt == 0 ? "\n" : "") (over(x - 1) || over(x) || over(x + 1) ? "#" : ".");
    crt = wrap(crt + 1);
}

BEGIN { x = 1 }
/noop/ { cycle() }

/addx/ {
    for(i = 0; i < 2; i++) cycle();
    x += $2;
}

END { print output }