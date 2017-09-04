using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace TwinRx.Interfaces.Structs
{
    public struct TwincatTimeBuffer
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Value;
    }
}