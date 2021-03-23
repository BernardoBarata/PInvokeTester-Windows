#include "Pump.h"

extern "C" __declspec(dllexport) PeristalticPump* CreateNewPump() {
	return new PeristalticPump();
}

extern "C" __declspec(dllexport) void PumpAdd(PeristalticPump * Pump, int a, int b) {
	Pump->add(a, b);
}

extern "C" __declspec(dllexport) int GetResult(PeristalticPump * Pump) {
	return Pump->getResult();
}

extern "C" __declspec(dllexport) Status* GenerateStatus(PeristalticPump * Pump) {
	Status statPtr = Pump->getStatus();
	return &statPtr;
}

extern "C" __declspec(dllexport) PeristalticPumpWrapper * CreateNewPumpWrapper() {
	return new PeristalticPumpWrapper();
}


extern "C" __declspec(dllexport) Status * GenerateStatusWrapper(PeristalticPumpWrapper * Pump) {
	Status* statPtr = Pump->GetStatus();
	return statPtr;
}