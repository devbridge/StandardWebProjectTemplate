using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace DevBridge.Kern.Tools
{
	// neveikia ant Win2000
	[SuppressUnmanagedCodeSecurity]
	internal static class SafeNativeMethods
	{
		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
		public static extern int StrCmpLogicalW(string psz1, string psz2);
	}

	public sealed class NaturalComparer : IComparer<string>
	{
		public int Compare(string a, string b)
		{
			return SafeNativeMethods.StrCmpLogicalW(a, b);
		}
	} 
}