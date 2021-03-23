using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace PInvokeTester
{
    #region Main Program
    class Program
	{

		static void Main(string[] args)
		{
			// can switch between these different functions
			int num = 0;
			switch (num)
			{
				case 0:

					TestWrapper();
					break;
				case 1:

					TestStatus();
					break;
				case 2:

					TestPump();
					break;

			}
		}

		/// <summary>
		/// This tests the wrapper and if it acquired the Status properly
		/// Expected result - Speed is 4 and Temp is 0
		/// </summary>
		public static void TestWrapper()
		{
			var pumpWrapper = new PeristalticPumpWrapper();
			Status testStatus = pumpWrapper.GetCurrentStatus();
			testStatus.SetStatSpeed(4);
			Console.WriteLine("Speed is " + testStatus.GetStatSpeed() + " and Temp is " + testStatus.GetStatTemp());
			while (true)
			{

			}
		}

		/// <summary>
		/// This tests the pump without wrapper and if it acquired the Status properly
		/// Expected result - Speed is 4 and Temp is 0
		/// </summary>
		public static void TestStatus()
		{
			var pump = new PeristalticPump();
			Status testStatus = pump.GetCurrentStatus();
			//var testStatus = new Status();
			testStatus.SetStatSpeed(4);
			Console.WriteLine("Speed is " + testStatus.GetStatSpeed() + " and Temp is " + testStatus.GetStatTemp());
			while (true)
			{

			}
		}

		/// <summary>
		/// This tests the pump's basic add function
		/// Expected result - 9
		/// </summary>
		public static void TestPump()
		{
			var pump = new PeristalticPump();
			pump.Add(4, 5);
			Console.WriteLine(pump.GetAddResult());
			while (true)
			{

			}

		}


	}

    #endregion

    #region Status Class



    /// <summary>
    /// Status C# class - contains a getter and setter for speed/temperature. The empty argument constructor is for testing the class
    /// while the constructor with an IntPtr is the one that is used by the Pump
    /// </summary>
    public class Status
	{
		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CreateStatus();

		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetSpeed(IntPtr statPtr, int s);

		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern int GetSpeed(IntPtr statPtr);

		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SetTemp(IntPtr statPtr, int t);

		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern int GetTemp(IntPtr statPtr);


		private readonly IntPtr _statusPointer;

		public Status() //Debug purposes
		{
			_statusPointer = CreateStatus();
		}

		public Status(IntPtr statPtr)
		{
			_statusPointer = statPtr;
		}

		public void SetStatSpeed(int s)
		{
			SetSpeed(_statusPointer, s);
		}
		public int GetStatSpeed()
		{
			return GetSpeed(_statusPointer);
		}

		public void SetStatTemp(int t)
		{
			SetTemp(_statusPointer, t);
		}
		public int GetStatTemp()
		{
			return GetTemp(_statusPointer);
		}
	}

    #endregion

    #region PeristalticPump Class


    /// <summary>
    /// PeristalticPump C# class 
    /// </summary>
    public class PeristalticPump
	{

		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CreateNewPump();

		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern void PumpAdd(IntPtr pumpPtr, int a, int b);

		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern int GetResult(IntPtr pumpPtr);

		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr GenerateStatus(IntPtr pumpPtr);

		private readonly IntPtr _pumpPointer;
		private readonly IntPtr _statusPointer;
		private readonly Status _status;

		public PeristalticPump()
		{
			_pumpPointer = CreateNewPump();
			_statusPointer = GenerateStatus(_pumpPointer);
			_status = new Status(_statusPointer);
		}

		public void Add(int a, int b)
		{
			PumpAdd(_pumpPointer, a, b);
		}

		public int GetAddResult()
		{
			return GetResult(_pumpPointer);
		}

		public Status GetCurrentStatus()
		{
			return _status;
		}



	}

    #endregion


    #region PeristalticPumpWrapper Class
    /// <summary>
    /// PeristalticPumpWrapper C# class - for solving the status problem
    /// </summary>
    public class PeristalticPumpWrapper
	{
		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr CreateNewPumpWrapper();

		[DllImport("TestLibrary.so", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr GenerateStatusWrapper(IntPtr pumpPtr);

		private readonly IntPtr _pumpWrapperPointer;
		private readonly IntPtr _statusPointer;
		private readonly Status _status;
		public PeristalticPumpWrapper()
		{
			_pumpWrapperPointer = CreateNewPumpWrapper();
			_statusPointer = GenerateStatusWrapper(_pumpWrapperPointer);
			_status = new Status(_statusPointer);
		}
		public Status GetCurrentStatus()
		{
			return _status;
		}
	}

    #endregion
}
