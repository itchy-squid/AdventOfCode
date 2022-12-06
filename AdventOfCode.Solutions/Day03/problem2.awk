BEGIN { convert = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" }

{
    if(NR % 3 != 1) gsub(pattern,"",$1);
    pattern = sprintf("[^%s]",$1);
    if(NR % 3 == 0) Sum += index(convert, substr($1,1,1));
}

END { print Sum }