/cd \// { curr = "" }
/cd [^\/\.]/{ curr = sprintf("%s/%s",$3,curr) }
/cd \.\./{ curr = substr(curr, index(curr, "/") + 1) }
/dir /{ directories[sprintf("%s/%s",$2,curr)] = 0 }
/[0-9].* / { for(itr = curr; itr != ""; itr = substr(itr,index(itr,"/")+1)) directories[itr] += $1 }

END {    
    for(i in directories) if(directories[i] < 100000) sum += directories[i];
    print sum;
}