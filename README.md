# PInvokeTester-Windows

This solution tests P/Invoke from C++ to C#, it uses a test .dll called TestLibrary.dll which includes a few different classes.

Don't be fooled by the name, there is a Linux branch which has a .so called TestLibrary.so. You can use that one to run under mono.

-------------------

To kind of mimic what we have in the mira2lib.so, we needed to have a class that has a function that returns a Status class. Just as in the PeristalticPump class that is in the mira2lib.so, for example.

The classes Pump and Status are defined in PeristalticPump.cpp/h and Status.cpp/h and their interfaces (for exporting to c#) are defined in PeristalticPumpInterface.cpp and StatusInterface.cpp - these functions in the interface are the ones that will be used in C#. This is done because C# cannot import directly classes from c++, it can only import IntPtr to those classes. That is why we have a "Creater" function for the classes which provides the C# side with a pointer to the instantiated class on the c++ side, and all the functions in the interface require a pointer to the class so that the C# side can use them.

-------------------

On the C# side, if you check the main() in Program.cs you have a switch case that enables you to choose between 3 tests. The first is testing a wrapper, which I'll explain in a bit. The second is to test what happens if we get the Status directly from the PeristalticPump. The third is to test a simple Add() function in the PeristalticPump to see if basic function importing works.

The 3rd test (TestPump()) calls Add(4,5) which adds these two numbers on the c++ side and stores them in a private variable ('result'). It then calls GetAddResult() which extracts that value from the c++ side back to the c# side.

The 2nd test (TestStatus()) attempts to collect the Status class directly from the Pump. This will highlight a problem that we need to deal with. On the c++ side, the Pump class's constructor creates a Status object and assigns it to a private variable, this is not how it is done in libMira2.so but it is just so we have an example to work with. Then it has a getter called getStatus() which gives this Status object away by value (and not reference - this is how it is done in libMira2.so functions). So then in the PumpInterface we need to send a pointer to this object to the C# side as C# cannot import pointers directly. This means that the pointer that is given is pointing to a local "copy" of the original Status and so its contents are lost in memory after this function is called. Meanwhile on the C# side as soon as we call the Pump constructor from the C# side it gets this pointer to the Status object "copy" and so if we use its functions we will not get the expected result. This is seen at the end of the test, we are supposed to set a speed of 4 and then get the speed and temperature and output them, the result should be 4 and 0 since the default values are 0 but the values are completely different, as if we were accessing the wrong addresses.

To fix this problem there are 2 solutions that we came up with. Either we change all the functions that return Status in your device classes to return Status* (which makes our lives a lot easier) or we define a wrapper class for each device that stores this status and gives us the pointer to it. 

That is where the 1st test and the PeristalticPumpWrapper comes into play. The Wrapper creates a Pump in its constructor (c++ side) and sets it to a private variable. The class has a Stop() function (naming was a random example of a function) - this sets its Status variable to receive the Status object copy from the Pump. The wrapperInterface function GenerateStatusWrapper calls this Stop() function and then collects the pointer to that Status variable in the Wrapper and feeds it to the C# side. With this approach the C# side can properly manipulate its variables without accessing wrong memory parts


---------------

This might seem confusing at first glance but the main parts are that C# cannot import classes directly from c++, it needs pointers. Also there is an issue in passing Status by value in the functions, either we change the functions to return Status* or we make wrapper classes whenever it is necessary (the 1st option is preferred).
