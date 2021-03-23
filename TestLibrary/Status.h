#pragma once
class Status
{
public:
	Status() = default;

	void setSpeed(int s);
	int getSpeed();

	void setTemp(int t);
	int getTemp();

private:

	int _speed=0;
	int _temperature = 0;
};
