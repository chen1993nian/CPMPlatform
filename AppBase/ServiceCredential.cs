using System;
using System.Runtime.CompilerServices;
using System.Web.Services.Protocols;

namespace EIS.AppBase
{
	public class ServiceCredential : SoapHeader
	{
        private string _UserName;
        private string _Password;
        private string _Token;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string PassWord
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

		public ServiceCredential()
		{
		}

        public ServiceCredential(string username, string password, string token)
        {
            UserName = username;
            PassWord = password;
            Token = token;
        }

	}
}