TestLibrary.so: StatusInterface.o Status.o PumpInterface.o Pump.o
	g++ -fPIC -shared StatusInterface.o Status.o PumpInterface.o Pump.o -o TestLibrary.so

StatusInterface.o: StatusInterface.cpp Status.h
	g++ -c StatusInterface.cpp

Status.o: Status.cpp Status.h
	g++ -c Status.cpp

Pump.o: Pump.cpp Pump.h Status.h
	g++ -c Pump.cpp

PumpInterface.o: PumpInterface.cpp Pump.h Status.h
	g++ -c PumpInterface.cpp

clean:
	rm *.o TestLibrary.so