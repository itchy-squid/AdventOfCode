#Example: awk -f problem2.awk -v LENGTH=10 test.txt

function max(a, b) { return a > b ? a : b; }
function abs(c) { return c < 0 ? -1 * c : c; }
function sign(c) { return c < 0 ? -1 : c == 0 ? 0 : 1; }

function update(dX, dY, magnitude){
    for(ct = 1; ct <= magnitude; ct++ ) {
        step(dX, dY);
        visited[sprintf("%s,%s", s[LENGTH][0], s[LENGTH][1])]++;
    }
}

function step(dX, dY){
    s[1][abs(dY)] += dX + dY;
    propagate(2);
}

function propagate(i) {
    if(i > LENGTH) return;

    dX = s[i-1][0] - s[i][0];
    dY = s[i-1][1] - s[i][1]; 
    d = max(abs(dX), abs(dY));
    s[i][0] += d > 1 ? sign(dX) : 0;
    s[i][1] += d > 1 ? sign(dY) : 0;

    propagate(i+1);
}

/L / { update(-1,  0, $2) }
/R / { update( 1,  0, $2) }
/U / { update( 0,  1, $2) }
/D / { update( 0, -1, $2) }

END { print length(visited) }