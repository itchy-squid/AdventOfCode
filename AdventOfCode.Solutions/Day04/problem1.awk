BEGIN { FS = "[-,]" }
{ if(($1 <= $3 && $2 >=$4) || ($3 <= $1 && $4 >= $2)) ct++; }
END { print ct }
