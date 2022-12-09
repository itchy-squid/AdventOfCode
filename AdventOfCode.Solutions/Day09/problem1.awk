function min(a, b) { return a < b ? a : b; }
function max(a, b) { return a > b ? a : b; }
function abs(c) { return c < 0 ? -1 * c : c; }
function sign(c) { return c < 0 ? -1 : c == 0 ? 0 : 1; }

BEGIN { }

function update(nTimes, dX, dY){
    for(ct = 1; ct <= nTimes; ct++ ) {
        x += dX;
        y += dY;

        d = max(abs(x - tX),abs(y - tY));
        tX += d > 1 ? sign(x - tX) : 0;
        tY += d > 1 ? sign(y - tY) : 0;

        visited[sprintf("%s,%s", tX, tY)]++;

        minX = min(x, minX);
        minY = min(y, minY);
        maxX = max(x, maxX);
        maxY = max(y, maxY);

        # print "distance:",d;

        # for(j = maxY; j >= minY; j--) {
        #     output = "";
        #     for(i = minX; i <= maxX; i++) output = sprintf("%s%s", output, x == i && y == j ? "H" : i == tX && j == tY ? "T" : i == 0 && j == 0 ? "s" : ".");
        #     print output;
        # }

        # print "";
    }
}

/L / { update($2, -1,  0) }
/R / { update($2,  1,  0) }
/U / { update($2,  0,  1) }
/D / { update($2,  0, -1) }

END { print length(visited) }