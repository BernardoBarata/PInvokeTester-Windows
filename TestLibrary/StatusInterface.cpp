#include "Status.h"

extern "C" __declspec(dllexport) Status * CreateStatus() {
	return new Status();
}

extern "C" __declspec(dllexport) void SetSpeed(Status * status, int s) {
	status->setSpeed(s);
}

extern "C" __declspec(dllexport) int GetSpeed(Status * status) {
	return status->getSpeed();
}

extern "C" __declspec(dllexport) void SetTemp(Status * status, int t) {
	status->setTemp(t);
}

extern "C" __declspec(dllexport) int GetTemp(Status * status) {
	return status->getTemp();
}