using Microsoft.VisualBasic;
using System;
using System.IO;
namespace Ohana3DS
{
    static class Common
	{
		public static Ohana MyOhana = new Ohana();

		public static Nako MyNako = new Nako();
		public static byte[] Power_Of_Two = new byte[8];
		public static UInt64 Read64(FileStream Data, int Address)
		{
			Data.Seek(Address, SeekOrigin.Begin);
			return Convert.ToUInt64((Data.ReadByte() & 0xff) + ((Data.ReadByte() & 0xff) << 8) + ((Data.ReadByte() & 0xff) << 16) + ((Data.ReadByte() & 0xff) << 24) + ((Data.ReadByte() & 0xff) << 32) + ((Data.ReadByte() & 0xff) << 40) + ((Data.ReadByte() & 0xff) << 48) + ((Data.ReadByte() & 0xff) << 56));
		}
		public static int Read32(FileStream Data, int Address)
		{
			Data.Seek(Address, SeekOrigin.Begin);
			return (Data.ReadByte() & 0xff) + ((Data.ReadByte() & 0xff) << 8) + ((Data.ReadByte() & 0xff) << 16) + ((Data.ReadByte() & 0xff) << 24);
		}
		public static int Read24(FileStream Data, int Address)
		{
			Data.Seek(Address, SeekOrigin.Begin);
			return (Data.ReadByte() & 0xff) + ((Data.ReadByte() & 0xff) << 8) + ((Data.ReadByte() & 0xff) << 16);
		}
		public static int Read16(FileStream Data, int Address)
		{
			Data.Seek(Address, SeekOrigin.Begin);
			return (Data.ReadByte() & 0xff) + ((Data.ReadByte() & 0xff) << 8);
		}

		public static int Read32(byte[] Data, int Address)
		{
			return (Data[Address] & 0xff) + ((Data[Address + 1] & 0xff) << 8) + ((Data[Address + 2] & 0xff) << 16) + ((Data[Address + 3] & 0xff) << 24);
		}
		public static int Read24(byte[] Data, int Address)
		{
			return (Data[Address] & 0xff) + ((Data[Address + 1] & 0xff) << 8) + ((Data[Address + 2] & 0xff) << 16);
		}
		public static int Read16(byte[] Data, int Address)
		{
			return (Data[Address] & 0xff) + ((Data[Address + 1] & 0xff) << 8);
		}

		public static string ReadMagic(FileStream Data, int Address, int Length)
		{
			Data.Seek(Address, SeekOrigin.Begin);
			string Magic = null;
			for (int i = 0; i <= Length - 1; i++) {
				Magic += Strings.Chr(Data.ReadByte());
			}
			return Magic;
		}
		public static string ReadMagic(byte[] Data, int Address, int Length)
		{
			string Magic = null;
			for (int Offset = Address; Offset <= Address + Length - 1; Offset++) {
				Magic += Strings.Chr(Data[Offset]);
			}
			return Magic;
		}
	}
}
