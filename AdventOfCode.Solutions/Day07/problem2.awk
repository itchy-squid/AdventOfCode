/cd \// { curr = "" }
/cd [^\/\.]/{ curr = sprintf("%s/%s",$3,curr) }
/cd \.\./{ curr = substr(curr, index(curr, "/") + 1) }
/dir / { dirs[sprintf("%s/%s",$2,curr)] = 0 }

/[0-9].* / {
    for(itr = curr; itr != ""; itr = substr(itr,index(itr,"/")+1)) dirs[itr] += $1;
    dirs[""] += $1;
}

END {
    diff = dirs[""] - 40000000;
    for(i in dirs) if(dirs[min] > dirs[i] && dirs[i] > diff) min = i;
    print min,":",dirs[min];
}