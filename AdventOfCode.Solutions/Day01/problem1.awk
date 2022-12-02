BEGIN{
    RS="";
    max=0;
    }

/[0-9]+/{
    sum=0;
    for (i=1;i<=NF;i++) sum+=$i; 
    max = max < sum ? sum : max
    }

END{print max}
