      'Comment line GOTO 123
  100 GOTO 200
  200 GOTO A$
  300 PRINT "GOTO 200": IF A>3 THEN 800
  400 GOTO A*A
  500 GOTO 2*B
  600 GOSUB 200: PRINT "": GOSUB 300: GOTO 500
  700 PRINT "GOSUB 200": GOTO 300
  800 GOTO "SAV"
  900 PRINT "GOTO 100": GOTO A$
  910 GOSUB V
  920 GOSUB 2*W
  930 GOSUB "WVR": GOSUB 100
  940 GOSUB B$
  950 GOTO 123
