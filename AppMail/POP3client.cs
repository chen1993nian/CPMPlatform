using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace EIS.AppMail
{
	public class POP3client
	{
		public string user;

		public string string_0;

		public string string_1;

		public int popPort = 110;

		public bool popSSL = false;

		public bool error;

		public POP3client.connect_state state = POP3client.connect_state.disc;

		public bool bReadInputStreamCharByChar = false;

		private TcpClient tcpClient_0;

		public Stream NetStrm;

		private StreamReader streamReader_0;

		private string string_2;

		private byte[] byte_0;

		private string string_3 = "\r\n";

		public POP3client()
		{
		}

		public POP3client(int ReadInputStreamCharByChar)
		{
			this.bReadInputStreamCharByChar = (ReadInputStreamCharByChar == 1 ? true : false);
		}

		public POP3client(string pop_server, int pop_port, bool pop_ssl, string user_name, string password)
		{
			this.string_1 = pop_server;
			this.popPort = pop_port;
			this.popSSL = pop_ssl;
			this.user = user_name;
			this.string_0 = password;
		}

		public string connect(string pop_server, int pop_port, bool pop_ssl)
		{
			this.string_1 = pop_server;
			this.popPort = pop_port;
			this.popSSL = pop_ssl;
			return this.connect();
		}

		public string connect()
		{
			string str;
			try
			{
				this.tcpClient_0 = new TcpClient(this.string_1, this.popPort);
				if (this.popSSL)
				{
					this.NetStrm = new SslStream(this.tcpClient_0.GetStream());
					((SslStream)this.NetStrm).AuthenticateAsClient(this.string_1);
				}
				else
				{
					this.NetStrm = this.tcpClient_0.GetStream();
				}
				this.streamReader_0 = new StreamReader(this.NetStrm, Encoding.Default);
				this.state = POP3client.connect_state.AUTHORIZATION;
				str = this.streamReader_0.ReadLine();
			}
			catch (Exception exception)
			{
				str = string.Concat("Error: ", exception.ToString());
			}
			return str;
		}

		public string DELE(int msg_number)
		{
			string str;
			if (this.state == POP3client.connect_state.TRANSACTION)
			{
				this.method_1(string.Concat("DELE ", msg_number.ToString()));
				str = this.method_2();
			}
			else
			{
				str = "Connection state not = TRANSACTION";
			}
			return str;
		}

		public string LIST()
		{
			string str = "";
			if (this.state == POP3client.connect_state.TRANSACTION)
			{
				this.method_1("LIST");
				str = this.method_3();
			}
			else
			{
				str = "Connection state not = TRANSACTION";
			}
			return str;
		}

		public string LIST(int msg_number)
		{
			string str = "";
			if (this.state == POP3client.connect_state.TRANSACTION)
			{
				this.method_1(string.Concat("LIST ", msg_number.ToString()));
				str = this.method_2();
			}
			else
			{
				str = "Connection state not = TRANSACTION";
			}
			return str;
		}

		private string method_0()
		{
			string str = "disconnected successfully.";
			if (this.state == POP3client.connect_state.disc)
			{
				str = "Not Connected.";
			}
			else
			{
				this.NetStrm.Close();
				this.streamReader_0.Close();
				this.state = POP3client.connect_state.disc;
			}
			return str;
		}

		private void method_1(string string_4)
		{
			this.string_2 = string.Concat(string_4, this.string_3);
			this.byte_0 = Encoding.ASCII.GetBytes(this.string_2.ToCharArray());
			this.NetStrm.Write(this.byte_0, 0, (int)this.byte_0.Length);
		}

		private string method_2()
		{
			string str;
			try
			{
				string str1 = this.streamReader_0.ReadLine();
				this.method_5(str1);
				str = str1;
			}
			catch (Exception exception)
			{
				str = string.Concat("Error in read_single_line_response(): ", exception.ToString());
			}
			return str;
		}

		private string method_3()
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder(0x1388);
			try
			{
				string str1 = this.streamReader_0.ReadLine();
				this.method_5(str1);
				if (this.error)
				{
					str = str1;
				}
				else
				{
					while (str1 != ".")
					{
						stringBuilder.Append(string.Concat(str1, this.string_3));
						str1 = this.streamReader_0.ReadLine();
					}
					str = stringBuilder.ToString();
				}
			}
			catch (Exception exception)
			{
				str = string.Concat("Error in read_multi_line_response(): ", exception.ToString());
			}
			return str;
		}

		private string method_4()
		{
			string str;
			StringBuilder stringBuilder = new StringBuilder(0x1000);
			try
			{
				byte[] numArray = new byte[0x1000];
				int num = 0;
				num = this.tcpClient_0.GetStream().Read(numArray, 0, (int)numArray.Length);
				while (num > 0)
				{
					for (int i = 0; i < num; i++)
					{
						stringBuilder.Append(Convert.ToChar(numArray[i]));
					}
					if ((stringBuilder.Length <= 4 || stringBuilder[stringBuilder.Length - 1] != '\n' || stringBuilder[stringBuilder.Length - 2] != '\r' || stringBuilder[stringBuilder.Length - 3] != '.' || stringBuilder[stringBuilder.Length - 4] != '\n' ? true : stringBuilder[stringBuilder.Length - 5] != '\r'))
					{
						num = this.tcpClient_0.GetStream().Read(numArray, 0, (int)numArray.Length);
					}
					else
					{
						stringBuilder[stringBuilder.Length - 3] = '\0';
						num = 0;
					}
				}
				str = stringBuilder.ToString();
			}
			catch (Exception exception)
			{
				str = string.Concat("Error in read_multi_line_response(): ", exception.ToString());
			}
			return str;
		}

		private void method_5(string string_4)
		{
			if (!string_4.StartsWith("-"))
			{
				this.error = false;
			}
			else
			{
				this.error = true;
			}
		}

		public string NOOP()
		{
			string str;
			if (this.state == POP3client.connect_state.TRANSACTION)
			{
				this.method_1("NOOP");
				str = this.method_2();
			}
			else
			{
				str = "Connection state not = TRANSACTION";
			}
			return str;
		}

		public string PASS()
		{
			string str;
			if (this.state != POP3client.connect_state.AUTHORIZATION)
			{
				str = "Connection state not = AUTHORIZATION";
			}
			else if (this.string_0 == null)
			{
				str = "No Password set.";
			}
			else
			{
				this.method_1(string.Concat("PASS ", this.string_0));
				str = this.method_2();
				if (!this.error)
				{
					this.state = POP3client.connect_state.TRANSACTION;
				}
			}
			return str;
		}

		public string PASS(string password)
		{
			this.string_0 = password;
			return this.PASS();
		}

		public string QUIT()
		{
			string str;
			if (this.state == POP3client.connect_state.disc)
			{
				str = "Not Connected.";
			}
			else
			{
				this.method_1("QUIT");
				str = this.method_2();
				str = string.Concat(str, this.string_3, this.method_0());
			}
			return str;
		}

		public string RETR(int int_0)
		{
			string str = "";
			if (this.state == POP3client.connect_state.TRANSACTION)
			{
				this.method_1(string.Concat("RETR ", int_0.ToString()));
				str = (!this.bReadInputStreamCharByChar ? this.method_3() : this.method_4());
			}
			else
			{
				str = "Connection state not = TRANSACTION";
			}
			return str;
		}

		public string RSET()
		{
			string str;
			if (this.state == POP3client.connect_state.TRANSACTION)
			{
				this.method_1("RSET");
				str = this.method_2();
			}
			else
			{
				str = "Connection state not = TRANSACTION";
			}
			return str;
		}

		public string STAT()
		{
			string str;
			if (this.state != POP3client.connect_state.TRANSACTION)
			{
				str = "Connection state not = TRANSACTION";
			}
			else
			{
				this.method_1("STAT");
				str = this.method_2();
			}
			return str;
		}

		public string UIDL(int int_0)
		{
			string str = "";
			if (this.state == POP3client.connect_state.TRANSACTION)
			{
				this.method_1(string.Concat("UIDL ", int_0.ToString()));
				str = this.method_2();
			}
			else
			{
				str = "Connection state not = TRANSACTION";
			}
			return str;
		}

		public string USER()
		{
			string str;
			if (this.state != POP3client.connect_state.AUTHORIZATION)
			{
				str = "Connection state not = AUTHORIZATION";
			}
			else if (this.user == null)
			{
				str = "No User specified.";
			}
			else
			{
				this.method_1(string.Concat("USER ", this.user));
				str = this.method_2();
			}
			return str;
		}

		public string USER(string user_name)
		{
			this.user = user_name;
			return this.USER();
		}

		public enum connect_state
		{
			disc,
			AUTHORIZATION,
			TRANSACTION,
			UPDATE
		}
	}
}