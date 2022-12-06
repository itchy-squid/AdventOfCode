BEGIN { convert="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" }

{
    if(NR % 3 == 1) pattern = $1;
    else {
        patsplit($1, matches, pattern);

        temp = $1;
        gsub(notpattern,"",temp);
        pattern = temp;
        }

    notpattern = sprintf("[^%s]",pattern);
    pattern = sprintf("[%s]",pattern);

    print "pattern: ", pattern;
    print "line:    ", $1;
    print "";

    if(NR % 3 == 0) Sum += index(convert, substr(notpattern,3,1));
}

END{print Sum}
