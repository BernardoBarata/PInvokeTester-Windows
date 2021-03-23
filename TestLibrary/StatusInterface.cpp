#include "Status.h"

extern "C"  Status * CreateStatus() {
	return new Status();
}

extern "C"  void SetSpeed(Status * status, int s) {
	status->setSpeed(s);
}

extern "C"  int GetSpeed(Status * status) {
	return status->getSpeed();
}

extern "C"  void SetTemp(Status * status, int t) {
	status->setTemp(t);
}

extern "C" int GetTemp(Status * status) {
	return status->getTemp();
}