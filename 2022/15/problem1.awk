# Ex: awk -f problem1.awk -v LINE=10 test.txt

function abs(a) { return a > 0 ? a : -1 * a }
function min(a, b) { return a == "" ? b : a < b ? a : b }
function max(a, b) { return a == "" ? b : a > b ? a : b }
function dist(x1, y1, x2, y2){ return abs(x1 - x2) + abs(y1 - y2); }

function noBeacon(x, y) { 
    for(i = 1; i < NR; i++){
        split(signals[i], arr, " ");
        if (x == arr[4] && y == arr[5]) return 0;
    }

    for(i = 1; i < NR; i++){
        split(signals[i], arr, " ");
        # print "Comparing (" x "," y ") to (" arr[1] "," arr[2] ") with distance " dist(x,y,arr[1],arr[2]) " to " arr[3]
        if (dist(x, y, arr[1], arr[2]) <= arr[3]) return 1;
    }
    # print "";
    return 0;
}

BEGIN { 
    FS = "[,=:]"; 
    minX = "";
    maxX = "";
}

/Sensor at x=-?[0-9]+, y=-?[0-9]+: closest beacon is at x=-?[0-9]+, y=-?[0-9]+/ {
    d = dist($2, $4, $6, $8);

    signals[NR] = $2 " " $4 " " d " " $6 " " $8;
    print "(" $2 "," $4 ")\tb:(" $6 "," $8 ")\td:" d;

    minX = min(minX, $2 - d - 1);
    maxX = max(maxX, $2 + d + 1);
}

END {
    # output = "";
    for(x = minX; x <= maxX; x++){
        if(1 == noBeacon(x, LINE)) {
            ct++;
            # output = output "#";
         }
         # else{
        #    output = output ".";
        
        #}
    }

    print output
    print ct;
}