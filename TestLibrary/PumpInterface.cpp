#include "Pump.h"

extern "C" PeristalticPump* CreateNewPump() {
	return new PeristalticPump();
}

extern "C"  void PumpAdd(PeristalticPump * Pump, int a, int b) {
	Pump->add(a, b);
}

extern "C"  int GetResult(PeristalticPump * Pump) {
	return Pump->getResult();
}

extern "C"  Status* GenerateStatus(PeristalticPump * Pump) {
	Status statPtr = Pump->getStatus();
	return &statPtr;
}

extern "C"  PeristalticPumpWrapper * CreateNewPumpWrapper() {
	return new PeristalticPumpWrapper();
}


extern "C"  Status * GenerateStatusWrapper(PeristalticPumpWrapper * Pump) {
	Status* statPtr = Pump->GetStatus();
	return statPtr;
}