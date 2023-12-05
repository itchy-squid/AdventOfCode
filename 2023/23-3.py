#@title Day 3-1: Gear Ratios
import re, math, itertools

class bcolors:
    HEADER = '\033[95m'
    OKBLUE = '\033[94m'
    OKCYAN = '\033[96m'
    OKGREEN = '\033[92m'
    WARNING = '\033[93m'
    FAIL = '\033[91m'
    ENDC = '\033[0m'
    BOLD = '\033[1m'

fread = lambda path: open('inputs/'+path, 'r').read()
f = fread('23-3-1.txt').splitlines()

special = lambda x,y,n: [re.search('[^\\d.]',f[min(max(yi,0),len(f)-1)][max(x-1,0):min(x+n+1,len(f[0])-1)]) != None for yi in range(y-1,y+2)]
ps = {(m.start(),y): m.group() for (y,l) in enumerate(f) for m in re.finditer('\\d+',l)}
ns = [int(ps[(x,y)]) for (x,y) in ps if any(special(x,y,len(ps[(x,y)])))]
print(sum(ns))

stars = lambda x,y,n: [
  (m.start(),yi)
  for yi in range(max(0,y-1),min(y+2,len(f)))
  for m in re.finditer('\\*', f[yi]) if (x-2)<m.start()<(x+n+1)
]

possible_gears = {k: [v for ki, v in list(vs)]
  for k, vs in itertools.groupby(
    [(s, int(ps[(x,y)])) for (x,y) in ps for s in stars(x,y,len(ps[(x,y)]))],
    lambda t: t[0])}
gears = {k: possible_gears[k]#math.prod(possible_gears[k]) 
  for k in possible_gears 
  if len(possible_gears[k]) == 2}

for y,l in enumerate(f):

  for x,c in enumerate(f[y]):
    format=bcolors.OKGREEN if (x,y) in gears \
      else bcolors.WARNING if (x,y) in possible_gears \
      else bcolors.FAIL if c == '*' \
      else bcolors.OKBLUE
    print(format+c,end='')
  print('\n')