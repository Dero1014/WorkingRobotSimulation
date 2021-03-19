Console - MPn moves robot to a point P of number n
	- G closes(grabs) gripper
	- R opens(releses) gripper
	- WnS/MS wait time of number n in seconds(S) or miliseconds(MS)
	- Sn speed of a robot of number n
	- IF(CONDITION) normal if statemnt that closes IEND
	- REPEAT(n) repeats everything inside n number of times and closes with REND
	- LOOP infinite loop until BREAK and closes with LEND

Extras : using PROXY0 in IF to check its state and LASER0 to check distance of the laser sensor

Example :

IF(PROXY0)
MP0
IEND

IF(LASER0<3)
MP0
IEND
	


	
	 