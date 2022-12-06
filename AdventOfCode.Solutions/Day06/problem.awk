#Example: awk -f problem.awk -v MATCH_LEN=4 input.txt
BEGIN { FS = "" }
{
    unique = "";
    for(i=1; i<NF; i++)
    {
        char = $i;
        idx = index(unique, char);

        if(idx > 0) unique = sprintf("%s%s",substr(unique,idx+1), char)
        else unique = sprintf("%s%s",unique,char)

        if(length(unique) >= MATCH_LEN) break;
    }
    
    print i;
}