BEGIN { convert="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" }

1{
    len = length($1);
    left = substr($1, 1, len/2);
    right = sprintf("[%s]",substr($1, len/2 + 1, len/2));
    idx = match(left, right);
    overlappingChar = substr(left,idx,1);
    score = index(convert, overlappingChar);
    Sum += score;
    }

END{print Sum}
