#Example: awk -f problem.awk -v MATCH_LEN=4 input.txt
{
    for(i=1; i<length($1); i++)
    {
        char = substr($1, i, 1);
        idx = index(unique, char);

        if(idx > 0) unique = sprintf("%s%s",substr(unique,idx+1), char)
        else unique = sprintf("%s%s",unique,char)

        if(length(unique) >= MATCH_LEN) exit 0;
    }
}

END { print i }