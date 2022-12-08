function left(x, y) 
{ 
    if (x == 1) return 0;
    for(i = x - 1; i > 1 && trees[x][y] > trees[i][y]; i--);
    return x - i; 
}

function top(x,y)
{
    if (y == 1) return 0;
    for(j = y - 1; j > 1 && trees[x][y] > trees[x][j]; j--);
    return y - j;
}

function right(x, y)
{ 
    if(x == NF) return 0;
    for(i = x + 1; i < NF && trees[x][y] > trees[i][y]; i++);
    return i - x; 
}

function bottom(x,y)
{
    if(y == NR) return 0;
    for(j = y + 1; j < NR && trees[x][y] > trees[x][j]; j++);
    return j - y;
}

BEGIN { FS = "" }
{ for(x=1;x<=NF;x++) trees[x][NR] = $x }

END {
    output = "";

    for(y=NF; y>0; y--) {
        for(x=NR; x>0; x--) {
            height = trees[x][y];
            score = left(x,y) * top(x,y) * right(x,y) * bottom(x,y);
            best = best > score ? best : score;

            output = sprintf("%s(%s=%sx%sx%sx%s)\t%s",height,score, left(x,y), top(x,y),right(x,y),bottom(x,y),output);
        }

        output = sprintf("\r\n%s",output);
    }

    # print output;
    print best;
}