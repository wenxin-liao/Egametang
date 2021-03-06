﻿using System;
using System.Runtime.InteropServices;
using EasyHook;
using Log;

namespace Hooks
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
	public delegate int DSend(IntPtr handle, IntPtr buf, int count, int flag);

	public class SendHook: IDisposable
	{
		[DllImport("Ws2_32.dll", EntryPoint = "send")]
		public static extern int Send(IntPtr handle, IntPtr buf, int count, int flag);

		private readonly LocalHook localHook;

		public SendHook(DSend dSend)
		{
			try
			{
				this.localHook = LocalHook.Create(LocalHook.GetProcAddress("Ws2_32.dll", "send"), new DSend(dSend), this);
				this.localHook.ThreadACL.SetInclusiveACL(new[] { 0 });
			}
			catch (Exception)
			{
				Logger.Debug("Error creating send Hook");
				throw;
			}
		}

		public void Dispose()
		{
			this.localHook.Dispose();
		}
	}
}