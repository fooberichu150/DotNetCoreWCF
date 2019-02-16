using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreWCF.Client.Logic
{
	public static class StringTool
	{
		public static string SelectStringValue(string value1, string value2, bool nullAsEmpty = true)
		{
			string retVal = !string.IsNullOrWhiteSpace(value1) ? value1 : value2;

			return retVal ?? (nullAsEmpty ? string.Empty : null);
		}
	}
}
