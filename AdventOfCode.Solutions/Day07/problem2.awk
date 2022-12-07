/cd \// { curr = "" }
/cd [^\/\.]/{ curr = sprintf("%s/%s",$3,curr) }
/cd \.\./{ curr = substr(curr, index(curr, "/") + 1) }
/dir / { dirs[sprintf("%s/%s",$2,curr)] = 0 }

/[0-9].* /{
    itr = curr;
    while(itr != ""){
        dirs[itr] += $1;
        itr = substr(itr,index(itr,"/")+1);
    }

    dirs[""] += $1;
}

END {
    min = "";
    remaining = dirs[""] - 40000000;
    for(i in dirs) if(dirs[min] > dirs[i] && dirs[i] > remaining) min = i;
    print min,":",dirs[min];
}