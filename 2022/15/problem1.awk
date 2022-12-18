# Ex: awk -f problem1.awk -v LINE=10 test.txt

function abs(a) { return a > 0 ? a : -1 * a }
function min(a, b) { return a == "" ? b : a < b ? a : b }
function max(a, b) { return a == "" ? b : a > b ? a : b }
function dist(x1, y1, x2, y2){ return abs(x1 - x2) + abs(y1 - y2); }

function canHaveBeacon(x){
    if(beacons[x] == 1) return 1;
    
    for(i = 1; i < NR; i++){
        split(signals[i], arr, " ");
        if(arr[1] <= x && arr[2] >= x) return 0;
    }

    return 1;
}

BEGIN { 
    FS = "[,=:]"; 
    minX = "";
    maxX = "";
}

/Sensor at x=-?[0-9]+, y=-?[0-9]+: closest beacon is at x=-?[0-9]+, y=-?[0-9]+/ {
    d = dist($2, $4, $6, $8);

    print "s:(" $2 "," $4 ")\tb:(" $6 "," $8 ")\td:" d;

    delta = d - abs($4 - LINE);
    if(delta >= 0) {
        print "...blocking (" ($2 - delta) ","  ($2 + delta) ")";

        signals[NR] = ($2 - delta) " " ($2 + delta);

        minX = min(minX, $2 - d);
        maxX = max(maxX, $2 + d);
    }

    if($8 == LINE){
        beacons[$6] = 1;
    }
}

END {
    output = "";
    for(x = minX; x <= maxX; x++){
        if(0 == canHaveBeacon(x)) {
            ct++;
            output = output "#";
         }
         else{
           output = output ".";        
        }
    }

    print output
    print ct;
}