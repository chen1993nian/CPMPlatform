using EIS.AppBase;
using System;
using System.Runtime.CompilerServices;

namespace EIS.DataModel.Model
{
	[Serializable]
	public class AppFile : AppModelBase
	{
		public string AppId
		{
			get;
			set;
		}

		public string AppName
		{
			get;
			set;
		}

		public string BasePath
		{
			get;
			set;
		}

		public int DownCount
		{
			get;
			set;
		}

		public string FactFileName
		{
			get;
			set;
		}

		public string FileCode
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}

		public string FilePath
		{
			get;
			set;
		}

		public int FileSize
		{
			get;
			set;
		}

		public string FileType
		{
			get;
			set;
		}

		public decimal FileVer
		{
			get;
			set;
		}

		public string FolderID
		{
			get;
			set;
		}

		public string Inherit
		{
			get;
			set;
		}

		public int OrderId
		{
			get;
			set;
		}

		public int ValidVer
		{
			get;
			set;
		}

		public AppFile()
		{
		}
	}
}