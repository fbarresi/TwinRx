using System;
using System.Runtime.InteropServices;

namespace TwinRx.Interfaces.Structs
{
    public struct TwincatString
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Value;
    }
}