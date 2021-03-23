#include "Status.h"


void Status::setSpeed(int s) {
	_speed = s;

}

int Status::getSpeed() {
	return _speed;
}

void Status::setTemp(int t) {
	_temperature = t;
}

int Status::getTemp() {
	return _temperature;
}