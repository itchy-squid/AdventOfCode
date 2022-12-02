/X/{S+=0}
/Y/{S+=3}
/Z/{S+=6}
/(A Y)|(B X)|(C Z)/{S+=1}
/(A Z)|(B Y)|(C X)/{S+=2}
/(A X)|(B Z)|(C Y)/{S+=3}
END{print S}
