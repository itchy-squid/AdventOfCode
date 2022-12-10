BEGIN { FS = "[-,]" }
{ if($1 <= $4 && $3 <= $2) ct++; }
END { print ct }
