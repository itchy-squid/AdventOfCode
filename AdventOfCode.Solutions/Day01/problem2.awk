BEGIN{RS=""}

/[0-9]+/{
    sum[NR]=0;
    for (i=1;i<=NF;i++) sum[NR]+=$i; 
    }

END{
    n=asort(sum, sorted)
    print sorted[NR] + sorted[NR-1] + sorted[NR-2]
    }
