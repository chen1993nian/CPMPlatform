using System;
using System.Collections;
using System.Reflection;

namespace EIS.AppBase
{
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
	public class EnumDescription : Attribute
	{
		private string enumDisplayText;

		private int enumRank;

		private FieldInfo fieldIno;

		private static Hashtable cachedEnum;

		public string EnumDisplayText
		{
			get
			{
				return this.enumDisplayText;
			}
		}

		public int EnumRank
		{
			get
			{
				return this.enumRank;
			}
		}

		public int EnumValue
		{
			get
			{
				return (int)this.fieldIno.GetValue(null);
			}
		}

		public string FieldName
		{
			get
			{
				return this.fieldIno.Name;
			}
		}

		static EnumDescription()
		{
			EnumDescription.cachedEnum = new Hashtable();
		}

		public EnumDescription(string enumDisplayText, int enumRank)
		{
			this.enumDisplayText = enumDisplayText;
			this.enumRank = enumRank;
		}

		public EnumDescription(string enumDisplayText) : this(enumDisplayText, 5)
		{
		}

		public static string GetEnumText(Type enumType)
		{
			string str;
			EnumDescription[] customAttributes = (EnumDescription[])enumType.GetCustomAttributes(typeof(EnumDescription), false);
			str = ((int)customAttributes.Length == 1 ? customAttributes[0].EnumDisplayText : string.Empty);
			return str;
		}

		public static string GetFieldText(object enumValue)
		{
			string enumDisplayText;
			EnumDescription[] fieldTexts = EnumDescription.GetFieldTexts(enumValue.GetType(), EnumDescription.SortType.Default);
			int num = 0;
			while (true)
			{
				if (num < (int)fieldTexts.Length)
				{
					EnumDescription enumDescription = fieldTexts[num];
					if (!(enumDescription.fieldIno.Name == enumValue.ToString()))
					{
						num++;
					}
					else
					{
						enumDisplayText = enumDescription.EnumDisplayText;
						break;
					}
				}
				else
				{
					enumDisplayText = string.Empty;
					break;
				}
			}
			return enumDisplayText;
		}

		public static EnumDescription[] GetFieldTexts(Type enumType)
		{
			return EnumDescription.GetFieldTexts(enumType, EnumDescription.SortType.Default);
		}

		public static EnumDescription[] GetFieldTexts(Type enumType, EnumDescription.SortType sortType)
		{
			EnumDescription[] item = null;
			if (!EnumDescription.cachedEnum.Contains(enumType.FullName))
			{
				FieldInfo[] fields = enumType.GetFields();
				ArrayList arrayLists = new ArrayList();
				FieldInfo[] fieldInfoArray = fields;
				for (int i = 0; i < (int)fieldInfoArray.Length; i++)
				{
					FieldInfo fieldInfo = fieldInfoArray[i];
					object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(EnumDescription), false);
					if ((int)customAttributes.Length == 1)
					{
						((EnumDescription)customAttributes[0]).fieldIno = fieldInfo;
						arrayLists.Add(customAttributes[0]);
					}
				}
				EnumDescription.cachedEnum.Add(enumType.FullName, (EnumDescription[])arrayLists.ToArray(typeof(EnumDescription)));
			}
			item = (EnumDescription[])EnumDescription.cachedEnum[enumType.FullName];
			if ((int)item.Length <= 0)
			{
				throw new NotSupportedException(string.Concat("枚举类型[", enumType.Name, "]未定义属性EnumValueDescription"));
			}
			int num = 0;
			while (num < (int)item.Length)
			{
				if (sortType != EnumDescription.SortType.Default)
				{
					for (int j = num; j < (int)item.Length; j++)
					{
						bool flag = false;
						switch (sortType)
						{
							case EnumDescription.SortType.DisplayText:
							{
								if (string.Compare(item[num].EnumDisplayText, item[j].EnumDisplayText) > 0)
								{
									flag = true;
								}
								break;
							}
							case EnumDescription.SortType.Rank:
							{
								if (item[num].EnumRank > item[j].EnumRank)
								{
									flag = true;
								}
								break;
							}
						}
						if (flag)
						{
							EnumDescription enumDescription = item[num];
							item[num] = item[j];
							item[j] = enumDescription;
						}
					}
					num++;
				}
				else
				{
					break;
				}
			}
			return item;
		}

		public enum SortType
		{
			Default,
			DisplayText,
			Rank
		}
	}
}