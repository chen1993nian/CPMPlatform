using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
     
            // 测试
            //string ipfilePath = @"C:\Documents and Settings\Daode\桌面\qqwry\QQWry.dat";
            //IPSearch ipSearch = new IPSearch(ipfilePath);
            //string ip = "72.51.27.51";
            //IPSearch.IPLocation loc = ipSearch.GetIPLocation(ip);
            //Console.WriteLine("你查的ip是：{0} 地理位置：{1} {2}", ip, loc.country, loc.area);
            //Console.ReadKey();
      


namespace EIS.AppCommon
{

    ///<summary>
    /// 提供从纯真IP数据库搜索IP信息的方法；
    /// 感谢LumaQQ提供纯真IP数据库格式文档；
    /// ----HeDaode 2007-12-28 四川教育学院
    ///</summary>
	public class IPSearch
	{
        FileStream ipFile;
        long ip;
        string ipfilePath;


        ///<summary>
        /// 构造函数
        ///</summary>
        ///<param name="ipfilePath">纯真IP数据库路径</param>
        public IPSearch(string ipfilePath)
        {
            this.ipfilePath = ipfilePath;
        }


        ///<summary>
        /// 将索引区字节块中的起始IP转换成Long数组
        ///</summary>
        ///<param name="ipBlock"></param>
		private long[] BlockToArray(byte[] ipBlock)
		{
            long[] ipArray;
			try
			{
                 ipArray = new long[ipBlock.Length / 7];
                int ipIndex = 0;
                byte[] temp = new byte[8];
                for (int i = 0; i < ipBlock.Length; i += 7)
                {
                    Array.Copy(ipBlock, i, temp, 0, 4);
                    ipArray[ipIndex] = BitConverter.ToInt64(temp, 0);
                    ipIndex++;
                }
                return ipArray; 
			}
			catch (Exception exception)
			{
				Exception innerException = exception.InnerException;
				if (innerException != null)
				{
					throw innerException;
				}
				throw;
			}
            return ipArray;
		}

        ///<summary>
        /// 获取指定IP所在地理位置
        ///</summary>
        ///<param name="strIP">要查询的IP地址</param>
        ///<returns></returns>
		public IPSearch.IPLocation GetIPLocation(string strIP)
		{
            IPLocation loc = new IPLocation();
			try
			{
                ip = IPToLong(strIP);
                ipFile = new FileStream(ipfilePath, FileMode.Open, FileAccess.Read);
                long[] ipArray = BlockToArray(ReadIPBlock());
                long offset = SearchIP(ipArray, 0, ipArray.Length - 1) * 7 + 4;
                ipFile.Position += offset;//跳过起始IP
                ipFile.Position = ReadLongX(3) + 4;//跳过结束IP

              
                int flag = ipFile.ReadByte();//读取标志
                if (flag == 1)//表示国家和地区被转向
                {
                    ipFile.Position = ReadLongX(3);
                    flag = ipFile.ReadByte();//再读标志
                }
                long countryOffset = ipFile.Position;
                loc.country = ReadString(flag);

                if (flag == 2)
                {
                    ipFile.Position = countryOffset + 3;
                }
                flag = ipFile.ReadByte();
                loc.area = ReadString(flag);

                ipFile.Close();
                ipFile = null;
                return loc;
			}
			catch (Exception exception)
			{
				Exception innerException = exception.InnerException;
				if (innerException != null)
				{
					throw innerException;
				}
				throw;
			}
            return loc;
		}

		public static IPSearch.IPLocation IPQuery(string strIP)
		{
			string str = string.Concat(HttpRuntime.BinDirectory, "qqwry.dat");
			return (new IPSearch(str)).GetIPLocation(strIP);
		}



        ///<summary>
        /// 将字符串形式的IP转换位long
        ///</summary>
        ///<param name="strIP"></param>
        ///<returns></returns>
		public long IPToLong(string strIP)
		{
            byte[] ip_bytes = new byte[8];
            string[] strArr = strIP.Split(new char[] { '.' });
            for (int i = 0; i < 4; i++)
            {
                ip_bytes[i] = byte.Parse(strArr[3 - i]);
            }
            return BitConverter.ToInt64(ip_bytes, 0);
		}


        ///<summary>
        /// 读取IP文件中索引区块
        ///</summary>
        ///<returns></returns>
		private byte[] ReadIPBlock()
		{
			byte[] numArray;
			try
			{
                long startPosition = ReadLongX(4);
                long endPosition = ReadLongX(4);
                long count = (endPosition - startPosition) / 7 + 1;//总记录数
                ipFile.Position = startPosition;
                byte[] ipBlock = new byte[count * 7];
                ipFile.Read(ipBlock, 0, ipBlock.Length);
                ipFile.Position = startPosition;
                return ipBlock;
			}
			catch (Exception exception)
			{
				Exception innerException = exception.InnerException;
				if (innerException != null)
				{
					throw innerException;
				}
				throw;
			}
			return numArray;
		}

        ///<summary>
        /// 从IP文件中读取指定字节并转换位long
        ///</summary>
        ///<param name="bytesCount">需要转换的字节数，主意不要超过8字节</param>
        ///<returns></returns>
        private long ReadLongX(int bytesCount)
		{
			long num;
			try
			{
                byte[] _bytes = new byte[8];
                ipFile.Read(_bytes, 0, bytesCount);
                return BitConverter.ToInt64(_bytes, 0);

			}
			catch (Exception exception)
			{
				Exception innerException = exception.InnerException;
				if (innerException != null)
				{
					throw innerException;
				}
				throw;
			}
			return num;
		}


        ///<summary>
        /// 从IP文件中读取字符串
        ///</summary>
        ///<param name="flag">转向标志</param>
        ///<returns></returns>
        string ReadString(int flag)
        {
            if (flag == 1 || flag == 2)//转向标志
                ipFile.Position = ReadLongX(3);
            else
                ipFile.Position -= 1;

            List<byte> list = new List<byte>();
            byte b = (byte)ipFile.ReadByte();
            while (b > 0)
            {
                list.Add(b);
                b = (byte)ipFile.ReadByte();
            }
            return Encoding.Default.GetString(list.ToArray());
        }



        ///<summary>
        /// 从IP数组中搜索指定IP并返回其索引
        ///</summary>
        ///<param name="ipArray">IP数组</param>
        ///<param name="start">指定搜索的起始位置</param>
        ///<param name="end">指定搜索的结束位置</param>
        ///<returns></returns>
		private int SearchIP(long[] ipArray, int start, int end)
		{
			int num;
			try
			{
                int middle = (start + end) / 2;
                if (middle == start)
                    return middle;
                else if (ip < ipArray[middle])
                    return SearchIP(ipArray, start, middle);
                else
                    return SearchIP(ipArray, middle, end);
			}
			catch (Exception exception)
			{
				Exception innerException = exception.InnerException;
				if (innerException != null)
				{
					throw innerException;
				}
				throw;
			}
			return num;
		}

		public struct IPLocation
		{
			public string country;

			public string area;
		}
	}
}