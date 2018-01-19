using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
     
            // ����
            //string ipfilePath = @"C:\Documents and Settings\Daode\����\qqwry\QQWry.dat";
            //IPSearch ipSearch = new IPSearch(ipfilePath);
            //string ip = "72.51.27.51";
            //IPSearch.IPLocation loc = ipSearch.GetIPLocation(ip);
            //Console.WriteLine("����ip�ǣ�{0} ����λ�ã�{1} {2}", ip, loc.country, loc.area);
            //Console.ReadKey();
      


namespace EIS.AppCommon
{

    ///<summary>
    /// �ṩ�Ӵ���IP���ݿ�����IP��Ϣ�ķ�����
    /// ��лLumaQQ�ṩ����IP���ݿ��ʽ�ĵ���
    /// ----HeDaode 2007-12-28 �Ĵ�����ѧԺ
    ///</summary>
	public class IPSearch
	{
        FileStream ipFile;
        long ip;
        string ipfilePath;


        ///<summary>
        /// ���캯��
        ///</summary>
        ///<param name="ipfilePath">����IP���ݿ�·��</param>
        public IPSearch(string ipfilePath)
        {
            this.ipfilePath = ipfilePath;
        }


        ///<summary>
        /// ���������ֽڿ��е���ʼIPת����Long����
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
        /// ��ȡָ��IP���ڵ���λ��
        ///</summary>
        ///<param name="strIP">Ҫ��ѯ��IP��ַ</param>
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
                ipFile.Position += offset;//������ʼIP
                ipFile.Position = ReadLongX(3) + 4;//��������IP

              
                int flag = ipFile.ReadByte();//��ȡ��־
                if (flag == 1)//��ʾ���Һ͵�����ת��
                {
                    ipFile.Position = ReadLongX(3);
                    flag = ipFile.ReadByte();//�ٶ���־
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
        /// ���ַ�����ʽ��IPת��λlong
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
        /// ��ȡIP�ļ�����������
        ///</summary>
        ///<returns></returns>
		private byte[] ReadIPBlock()
		{
			byte[] numArray;
			try
			{
                long startPosition = ReadLongX(4);
                long endPosition = ReadLongX(4);
                long count = (endPosition - startPosition) / 7 + 1;//�ܼ�¼��
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
        /// ��IP�ļ��ж�ȡָ���ֽڲ�ת��λlong
        ///</summary>
        ///<param name="bytesCount">��Ҫת�����ֽ��������ⲻҪ����8�ֽ�</param>
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
        /// ��IP�ļ��ж�ȡ�ַ���
        ///</summary>
        ///<param name="flag">ת���־</param>
        ///<returns></returns>
        string ReadString(int flag)
        {
            if (flag == 1 || flag == 2)//ת���־
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
        /// ��IP����������ָ��IP������������
        ///</summary>
        ///<param name="ipArray">IP����</param>
        ///<param name="start">ָ����������ʼλ��</param>
        ///<param name="end">ָ�������Ľ���λ��</param>
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