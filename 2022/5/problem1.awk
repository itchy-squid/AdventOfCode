BEGIN { FS = "(   )? " }

/(\[[A-Z]\])/ {
    for(i=1;i<=NF;i++) boxes[i] = sprintf("%s%s", boxes[i], $i);
 }

/move [1-9].* from [1-9].* to [1-9].*/ {
    for(i=1; i<=$2; i++) {
        box = substr(boxes[$4],1,3);
        boxes[$4] = substr(boxes[$4],4);
        boxes[$6] = sprintf("%s%s",box,boxes[$6]);
    }
}

END { 
    for(i in boxes) result = sprintf("%s%s",result,substr(boxes[i], 2, 1))
    print result;
}