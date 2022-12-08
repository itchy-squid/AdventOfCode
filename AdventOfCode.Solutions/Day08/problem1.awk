function max(a, b) { return a > b ? a : b }

BEGIN { FS = "" }

{
    left = -1;
    output = "";

    for(x=1;x<=NF;x++) {
        top = NR == 1 ? -1 : trees[x][NR]["top"];

        trees[x][NR]["height"] = $x;
        trees[x][NR]["top"] = $x > top;
        trees[x][NR]["left"] = $x > left;

        trees[x][NR+1]["top"] = max($x,top);
        left = max($x,left); 

        output = sprintf("%s%s",output,$x);
    }

    # sprint output;
}

END {
    
    output = "";
    for(y=NF; y>0; y--) {
        right = -1;

        for(x=NR; x>0; x--) {
            height = trees[x][y]["height"]; 
            bottom = y == NF ? -1 : trees[x][y]["bottom"]; 

            trees[x][y]["right"] = height > right;
            trees[x][y]["bottom"] = height > bottom;

            trees[x][y-1]["bottom"] = max(height, bottom);
            right = max(height, right);

            isVisible = trees[x][y]["left"] || trees[x][y]["top"] || trees[x][y]["right"] || trees[x][y]["bottom"];
            count += isVisible? 1 : 0;
            output = sprintf("%s%s\t%s",height,isVisible ? "v" : "", output);
        }

        output = sprintf("\r\n%s",output);
    }

    # print output;
    print count;
}