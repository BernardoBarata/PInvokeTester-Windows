#include "Pump.h"

//PumpWrapper functions
void PeristalticPumpWrapper::Stop() {
	_status = _pump.getStatus();
}

Status* PeristalticPumpWrapper::GetStatus() {
	return &_status;
}



//Pump functions
void PeristalticPump::add(int a,int b) {
	_result = a + b;

}

int PeristalticPump::getResult() {
	return _result;
}

Status PeristalticPump::getStatus() {
	return _status;
}
