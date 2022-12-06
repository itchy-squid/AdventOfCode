BEGIN { convert="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" }

1{
    len = length($1);
    left = substr($1, 1, len/2);
    sum += index(convert, substr(left, match(left, sprintf("[%s]",substr($1, len/2 + 1))),1));
    }

END{print sum}
