      PROGRAM PRNTBDY

! print boundary file for diagnostics (05 Dec 2001)

      CHARACTER*11 LABEL(3)
      CHARACTER*1  CVAL(80)
      INTEGER DVAL(360), RVAL(360)
      REAL LO(3,3),HI(3,3)

      DATA LABEL/'LANDUSE.ASC','ROUGLEN.ASC','TERRAIN.ASC'/
      DATA LO/  1,  5, 10,  20, 100,  500,     0, 1000, 2000/
      DATA HI/  5, 10, 99, 100, 500, 9999,  1000, 2000, 9999/

      WRITE(*,*)'1 - landuse.asc'
      WRITE(*,*)'2 - rouglen.asc'
      WRITE(*,*)'3 - terrain.asc'
      WRITE(*,*)'Select Input File Number: '      
      READ(*,*)NUMB 

      OPEN(10,FILE=LABEL(NUMB))
!     open(20,file='terrain.new')

  100 READ(10,'(360I4))',END=900)DVAL

!     RVAL(1:180)=DVAL(181:360)
!     RVAL(181:360)=DVAL(1:180)
!     DVAL=RVAL 
!     write(20,'(360I4)')dval

      DO I=1,360
         CVAL(I)=' '
         IF    (DVAL(I).GT.LO(1,NUMB).AND.DVAL(I).LT.HI(1,NUMB))THEN
            CVAL(I)='.'
         ELSEIF(DVAL(I).GE.LO(2,NUMB).AND.DVAL(I).LT.HI(2,NUMB))THEN
            CVAL(I)='+'
         ELSEIF(DVAL(I).GE.LO(3,NUMB))THEN
            CVAL(I)='#'
         END IF
      END DO
      WRITE(*,'(120A1)')(CVAL(I),I=1,360,3)

!     skip records  
      READ(10,'(360I4))',END=900)DVAL
      READ(10,'(360I4))',END=900)DVAL
      READ(10,'(360I4))',END=900)DVAL
      GOTO 100

  900 STOP
      END
