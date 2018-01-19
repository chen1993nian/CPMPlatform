using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace EIS.Common.Pipe
{
	public class StreamString
	{
		private Stream ioStream;

		private UTF8Encoding streamEncoding;

		public StreamString(Stream ioStream)
		{
			this.ioStream = ioStream;
			this.streamEncoding = new UTF8Encoding();
		}

		public string ReadString()
		{
            int num = 0;
            num = this.ioStream.ReadByte() * 256;
            num = num + this.ioStream.ReadByte();
            byte[] numArray = new byte[num];
            this.ioStream.Read(numArray, 0, num);
            return this.streamEncoding.GetString(numArray);
		}

		public int WriteString(string outString)
		{
			int num;
            byte[] bytes = this.streamEncoding.GetBytes(outString);
            int length = (int)bytes.Length;
            if (length > 65535)
            {
                length = 65535;
            }
            this.ioStream.WriteByte((byte)(length / 256));
            this.ioStream.WriteByte((byte)(length & 255));
            this.ioStream.Write(bytes, 0, length);
            this.ioStream.Flush();
            return (int)bytes.Length + 2;
			return num;
		}
	}
}