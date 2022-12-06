BEGIN { convert="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" }

{
    if(NR % 3 == 1) pattern = $1;
    else {
        patsplit($1, matches, pattern);
        pattern = "";
        for (i in matches) pattern = sprintf("%s%s", pattern, matches[i]);
        }

    pattern = sprintf("[%s]",pattern);
    if(NR % 3 == 0) Sum += index(convert, substr(pattern,2,1));
}

END{print Sum}
