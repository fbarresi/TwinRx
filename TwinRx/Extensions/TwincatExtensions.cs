using System;
using TwinCAT.Ads;

namespace TwinRx.Extensions
{
    public static class TwincatExtensions
    {
        public static byte[] ConvertDateTimeToBuffer(this DateTime time)
        {
            var adsStream = new AdsStream(4);
            var writer = new AdsBinaryWriter(adsStream);
            writer.WritePlcType(time);
            return adsStream.GetBuffer();
        }

        public static TimeSpan ConvertBufferToTime(this byte[] buffer)
        {
            if (buffer == null) return TimeSpan.Zero;
            AdsStream adsStream = new AdsStream(buffer);
            AdsBinaryReader reader = new AdsBinaryReader(adsStream);
            return reader.ReadPlcTIME();
        }

        public static DateTime ConvertBufferToDateTime(this byte[] buffer)
        {
            if (buffer == null) return default(DateTime);
            AdsStream adsStream = new AdsStream(buffer);
            AdsBinaryReader reader = new AdsBinaryReader(adsStream);
            return reader.ReadPlcDATE();
        }
    }
}