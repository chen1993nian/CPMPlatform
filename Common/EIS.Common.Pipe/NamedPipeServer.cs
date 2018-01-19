using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using System.Threading;
namespace EIS.Common.Pipe
{
	public class NamedPipeServer
	{
		private NamedPipeServer.DelegateProcessMessage msgHandler;

		private List<NamedPipeServerStream> _serverPool;

		private string _pipName;

		public NamedPipeServer(string pipName, NamedPipeServer.DelegateProcessMessage handler)
		{
            this._pipName = pipName;
            this.msgHandler = handler;
		}

		protected NamedPipeServerStream CreateNamedPipeServerStream()
		{
            NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream(this._pipName, PipeDirection.InOut, 10);
            this._serverPool.Add(namedPipeServerStream);
            int count = this._serverPool.Count;
            Console.WriteLine(string.Concat("Pool=", count.ToString()));
            return namedPipeServerStream;
		}

		protected void DistroyObject(NamedPipeServerStream npss)
		{
            npss.Close();
            if (this._serverPool.Contains(npss))
            {
                this._serverPool.Remove(npss);
            }
		}

		protected virtual void ProcessMessage(string str, NamedPipeServerStream pipeServer)
		{
            StreamWriter streamWriter = new StreamWriter(pipeServer);
            try
            {
                streamWriter.AutoFlush = true;
                streamWriter.WriteLine("1=");
            }
            finally
            {
                if (streamWriter != null)
                {
                    ((IDisposable)streamWriter).Dispose();
                }
            }
		}

		public void Run()
		{
            NamedPipeServerStream namedPipeServerStream = this.CreateNamedPipeServerStream();
            try
            {
                namedPipeServerStream.WaitForConnection();
                Action action = new Action(this.Run);
                action.BeginInvoke(null, null);
                try
                {
                    try
                    {
                        bool flag = true;
                        while (true)
                        {
                            if (flag)
                            {
                                string str = (new StreamString(namedPipeServerStream)).ReadString();
                                string delegateProcessMessage0 = this.msgHandler(str);
                                StreamWriter streamWriter = new StreamWriter(namedPipeServerStream);
                                try
                                {
                                    streamWriter.AutoFlush = true;
                                    streamWriter.WriteLine(delegateProcessMessage0);
                                }
                                finally
                                {
                                    if (streamWriter != null)
                                    {
                                        ((IDisposable)streamWriter).Dispose();
                                    }
                                }
                                if (!namedPipeServerStream.IsConnected)
                                {
                                    flag = false;
                                    break;
                                }
                                else
                                {
                                    Thread.Sleep(50);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    catch (IOException oException)
                    {
                        Console.WriteLine("ERROR: {0}", oException.Message);
                    }
                }
                finally
                {
                    this.DistroyObject(namedPipeServerStream);
                }
            }
            finally
            {
                if (namedPipeServerStream != null)
                {
                    ((IDisposable)namedPipeServerStream).Dispose();
                }
            }
		}

		public void Stop()
		{
            for (int i = 0; i < this._serverPool.Count; i++)
            {
                this.DistroyObject(this._serverPool[i]);
            }
		}

		public delegate string DelegateProcessMessage(string msg);
	}
}