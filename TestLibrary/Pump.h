#include "Status.h"

#pragma once

// simple class for the Peristaltic Pump, has a basic adding function which stores the result in _result
// a getter for the result and finally a getter for the Status - this one is supposed to test the functions that we have
// in the real PeristalticPump which return a Status (and not Status Ptr!)
class PeristalticPump
{
	public:
		PeristalticPump() {
			_status = Status();
		}

		void add(int a, int b);
		int getResult();
		Status getStatus();

	private:
		Status _status;
		int _result;
};


// This is a wrapper for the pump which wraps around it in order to get a pointer to the status so that it is easier
// to send this information to the C# side
class PeristalticPumpWrapper
{
	public:
		 PeristalticPumpWrapper() {
			_pump = PeristalticPump();
		}

		void Stop();
		Status* GetStatus();


	private:
		PeristalticPump _pump;
		Status _status;
};