BEGIN { }
/(\[[A-Z]\])/{
    print $1, $2, $3
 }
END { }