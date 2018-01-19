using System;
using System.IO.Pipes;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
namespace EIS.Common.Pipe
{
	public class NamedPipeClient : IDisposable
	{
		private string _serverName;

		private string _pipName;

		private NamedPipeClientStream _pipeClient;

		private bool _disposed;

		public NamedPipeClient(string serverName, string pipName)
		{
            this._serverName = serverName;
            this._pipName = pipName;
            this._pipeClient = new NamedPipeClientStream(serverName, pipName, PipeDirection.InOut);

		}

		public void Dispose()
		{
            if ((this._disposed ? false : this._pipeClient != null))
            {
                this._pipeClient.Dispose();
                this._disposed = true;
            }
		}

		public string Send(string msg)
		{
            if (!this._pipeClient.IsConnected)
            {
                this._pipeClient.Connect(10000);
            }
            (new StreamString(this._pipeClient)).WriteString(msg);
            StreamReader streamReader = new StreamReader(this._pipeClient);
            string str = "";
            while (true)
            {
                string str1 = streamReader.ReadLine();
                string str2 = str1;
                if (str1 == null)
                {
                    break;
                }
                str = str2;
            }
            return str;
		}
	}
}