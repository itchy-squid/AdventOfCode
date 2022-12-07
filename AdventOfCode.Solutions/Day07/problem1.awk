function dump(){
    # print;
    # print "curr:",curr;
    # for(i in files) print i,":",files[i];
    # for(i in directories) print i,":",directories[i];
    # print "";
}

/cd \//{
    curr = "";
    dump();
}
/cd [^\/\.]/{
    curr = sprintf("%s/%s",$3,curr);
    dump();
}
/cd \.\./{
    curr = substr(curr, index(curr, "/") + 1);
    dump();
}

/dir /{
    directories[sprintf("%s/%s",$2,curr)] = 0;
}

/[0-9].* /{
    files[sprintf("%s/%s",$2,curr)] = $1;
    
    itr = curr;
    while(itr != ""){
        directories[itr] += $1;
        itr = substr(itr,index(itr,"/")+1);
    }

    directories[""] += $1;    
    dump();
}

END {
    
    for(i in directories) if(directories[i] < 100000) sum += directories[i];
    print sum;
}