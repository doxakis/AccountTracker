﻿using AccountTracker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			SelfInstaller.UninstallMe();
			SelfInstaller.InstallMe();
			StartService("ActivityTrackerService", 10000);
		}

		public static void StartService(string serviceName, int timeoutMilliseconds)
		{
			ServiceController service = new ServiceController(serviceName);
			try
			{
				int millisec1 = 0;
				TimeSpan timeout;

				// count the rest of the timeout
				int millisec2 = Environment.TickCount;
				timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec1));
				service.Start();
				service.WaitForStatus(ServiceControllerStatus.Running, timeout);
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
			}
		}
		public static void StopService(string serviceName, int timeoutMilliseconds)
		{
			ServiceController service = new ServiceController(serviceName);
			try
			{
				int millisec1 = 0;
				TimeSpan timeout;
				if (service.Status == ServiceControllerStatus.Running)
				{
					millisec1 = Environment.TickCount;
					timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
					service.Stop();
					service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
				}
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
			}
		}
		public static void RestartService(string serviceName, int timeoutMilliseconds)
		{
			ServiceController service = new ServiceController(serviceName);
			try
			{
				int millisec1 = 0;
				TimeSpan timeout;
				if (service.Status == ServiceControllerStatus.Running)
				{
					millisec1 = Environment.TickCount;
					timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
					service.Stop();
					service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
				}
				// count the rest of the timeout
				int millisec2 = Environment.TickCount;
				timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
				service.Start();
				service.WaitForStatus(ServiceControllerStatus.Running, timeout);

			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message);
			}
		}
	}
}
