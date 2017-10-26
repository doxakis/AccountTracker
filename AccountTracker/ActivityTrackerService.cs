using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AccountTracker
{
	partial class ActivityTrackerService : ServiceBase
	{
		private System.Timers.Timer timer;

		public ActivityTrackerService()
		{
			InitializeComponent();
		}

		protected override void OnSessionChange(SessionChangeDescription changeDescription)
		{
			base.OnSessionChange(changeDescription);

			var contents = DateTime.Now.ToLongTimeString() + "\t" +
				changeDescription.Reason.ToString() + "\t" +
				changeDescription.SessionId +
				Environment.NewLine;

			var pathDb = (ConfigurationManager.AppSettings["pathDb"] ?? "").ToString();
			Directory.CreateDirectory(pathDb + @"\OnSessionChange\");
			File.AppendAllText(pathDb + @"\OnSessionChange\" + DateTime.Today.ToShortDateString().Replace("/", "-") + ".csv", contents);
		}

		protected override void OnStart(string[] args)
		{
			timer = new System.Timers.Timer();
			timer.Elapsed += new ElapsedEventHandler(timer_Tick);
			timer.Interval = 15000; // ms
			timer.Enabled = true;
			timer.Start();
		}

		protected override void OnStop()
		{
			timer.Stop();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			/*string contents = "";
			try
			{
				contents = DateTime.Now.ToLongTimeString() + "\t" +
					GetActiveWindowTitle() +
					Environment.NewLine;
			}
			catch (Exception ex)
			{
				contents = DateTime.Now.ToLongTimeString() + "\t" +
					"Unknown: " + ex.Message +
					Environment.NewLine;
			}
			
			var pathDb = (ConfigurationManager.AppSettings["pathDb"] ?? "").ToString();
			Directory.CreateDirectory(pathDb + @"\Activities\");
			File.AppendAllText(pathDb + @"\Activities\" + DateTime.Today.ToShortDateString().Replace("/", "-") + ".csv", contents);*/
		}
	}
}
