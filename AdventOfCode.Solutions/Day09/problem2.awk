#Example: awk -f problem2.awk -v LENGTH=9 test.txt

function max(a, b) { return a > b ? a : b; }
function abs(c) { return c < 0 ? -1 * c : c; }
function sign(c) { return c < 0 ? -1 : c == 0 ? 0 : 1; }

function update(nTimes, dX, dY){
    for(ct = 1; ct <= nTimes; ct++ ) {
        step(dX, dY);
        visited[sprintf("%s,%s", s[LENGTH][0], s[LENGTH][1])]++;
    }
}

function step(dX, dY){
    s[1][0] += dX;
    s[1][1] += dY;

    propagate(2, dX, dY);
}

function propagate(i, dX, dY) {
    if(i > LENGTH) return;

    dX = s[i-1][0] - s[i][0];
    dY = s[i-1][1] - s[i][1]; 
    d = max(abs(dX), abs(dY));
    s[i][0] += d > 1 ? sign(dX) : 0;
    s[i][1] += d > 1 ? sign(dY) : 0;

    propagate(i+1);
}

/L / { update($2, -1,  0) }
/R / { update($2,  1,  0) }
/U / { update($2,  0,  1) }
/D / { update($2,  0, -1) }

END { print length(visited) }