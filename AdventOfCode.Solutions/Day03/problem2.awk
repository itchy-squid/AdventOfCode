BEGIN { convert = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" }

{
    if(NR % 3 != 1) gsub(notPattern,"",$1);
    notPattern = sprintf("[^%s]",$1);
    if(NR % 3 == 0) Sum += index(convert, substr(notPattern,3,1));
}

END { print Sum }
