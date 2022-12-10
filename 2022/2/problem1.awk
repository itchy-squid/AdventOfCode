/X/{S+=1}
/Y/{S+=2}
/Z/{S+=3}
/(A Y)|(B Z)|(C X)/{S+=6}
/(A X)|(B Y)|(C Z)/{S+=3}
END{print S}
