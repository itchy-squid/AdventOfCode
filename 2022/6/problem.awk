#Example: awk -f problem.awk -v MATCH_LEN=4 input.txt
BEGIN { FS = "" }
{
    unique = "";
    for(i=1; i<NF; i++)
    {
        idx = index(unique, $i);
        unique = sprintf("%s%s",idx > 0 ? substr(unique,idx+1) : unique, $i);
        if(length(unique) >= MATCH_LEN) break;
    }
    
    print i;
}