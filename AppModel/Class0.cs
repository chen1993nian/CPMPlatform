using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIS.AppModel
{
    public class Class0
    {
        private static string[] string_0;

	static Class0()
	{
		string[] strArrays = new string[] { "SchemaType", "ArrayType", "ListType", "ExternalReference", "DeclaredType", "RecordType", "EnumerationType", "BasicType", "UnionType" };
		Class0.string_0 = strArrays;
	}

	public static string[] smethod_0()
	{
		return Class0.string_0;
	}
    }
}
