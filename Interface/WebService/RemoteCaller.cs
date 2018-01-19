using EIS.AppBase;
using System;
using System.Collections;
using System.Web.Services.Protocols;
using System.Xml;

namespace EIS.Interface.WebService
{
	public class RemoteCaller
	{
		private string string_0;

		private byte[] byte_0;

		private IRemoteCall iremoteCall_0;

		private ArrayList arrayList_0;

		public IRemoteCall Caller
		{
			get
			{
				return this.iremoteCall_0;
			}
			set
			{
				this.iremoteCall_0 = value;
			}
		}

		public string MethodName
		{
			get
			{
				return this.string_0;
			}
			set
			{
				this.string_0 = value;
			}
		}

		public byte[] ParamByte
		{
			get
			{
				return this.byte_0;
			}
			set
			{
				this.byte_0 = value;
			}
		}

		public ArrayList Params
		{
			get
			{
				return this.arrayList_0;
			}
			set
			{
				this.arrayList_0 = value;
			}
		}

		public RemoteCaller(IRemoteCall caller)
		{
			this.iremoteCall_0 = caller;
			this.arrayList_0 = new ArrayList();
		}

		public byte[] Call(string methodName, object param)
		{
			byte[] numArray;
			try
			{
				this.string_0 = methodName;
				this.byte_0 = Utility.SerializeToBinary(param);
				numArray = this.iremoteCall_0.GeneralCall(this.string_0, this.byte_0);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (!(exception is SoapException))
				{
					throw exception;
				}
				throw new Exception(((SoapException)exception).Detail["Message"].InnerText);
			}
			return numArray;
		}

		public byte[] Call(string methodName, ArrayList param)
		{
			byte[] numArray;
			try
			{
				this.string_0 = methodName;
				this.arrayList_0 = param;
				this.byte_0 = Utility.SerializeToBinary(this.arrayList_0);
				numArray = this.iremoteCall_0.GeneralCall(this.string_0, this.byte_0);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (!(exception is SoapException))
				{
					throw exception;
				}
				throw new Exception(((SoapException)exception).Detail["Message"].InnerText);
			}
			return numArray;
		}

		public byte[] Call(string methodName, params object[] param)
		{
			byte[] numArray;
			try
			{
				object[] objArray = param;
				for (int i = 0; i < (int)objArray.Length; i++)
				{
					object obj = objArray[i];
					this.arrayList_0.Add(obj);
				}
				this.string_0 = methodName;
				this.byte_0 = Utility.SerializeToBinary(this.arrayList_0);
				numArray = this.iremoteCall_0.GeneralCall(this.string_0, this.byte_0);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (!(exception is SoapException))
				{
					throw exception;
				}
				throw new Exception(((SoapException)exception).Detail["Message"].InnerText);
			}
			return numArray;
		}

		public byte[] Call()
		{
			byte[] numArray;
			try
			{
				if (string.IsNullOrEmpty(this.string_0))
				{
					throw new Exception("远程方法不能为空！");
				}
				numArray = this.iremoteCall_0.GeneralCall(this.string_0, this.byte_0);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (!(exception is SoapException))
				{
					throw exception;
				}
				throw new Exception(((SoapException)exception).Detail["Message"].InnerText);
			}
			return numArray;
		}

		public T Call<T>()
		{
			return Utility.DeserializeToObject<T>(this.Call());
		}

		public T Call<T>(string methodName, ArrayList param)
		{
			return Utility.DeserializeToObject<T>(this.Call(methodName, param));
		}

		public T Call<T>(string methodName, object param)
		{
			T obj;
			try
			{
				this.string_0 = methodName;
				this.byte_0 = Utility.SerializeToBinary(param);
				byte[] numArray = this.iremoteCall_0.GeneralCall(this.string_0, this.byte_0);
				obj = Utility.DeserializeToObject<T>(numArray);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (!(exception is SoapException))
				{
					throw exception;
				}
				throw new Exception(((SoapException)exception).Detail["Message"].InnerText);
			}
			return obj;
		}

		public T Call<T>(string methodName, params object[] param)
		{
			return Utility.DeserializeToObject<T>(this.Call(methodName, param));
		}
	}
}