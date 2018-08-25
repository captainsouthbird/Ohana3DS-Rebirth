using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.IO;
namespace Ohana3DS
{
    public class Minko
	{
		public string[] Strings;
		public void Extract_Strings(string File_Name)
		{
			byte[] Data = File.ReadAllBytes(File_Name);
			int Text_Sections = Common.Read16(Data, 0);
			int Lines = Common.Read16(Data, 2);
			int Total_Size = Common.Read32(Data, 4);
			int Initial_Key = Common.Read32(Data, 8);
			int Section_Data = Common.Read32(Data, 12);

			int Key = 0x7c89;
			Strings = new string[Lines];

			for (int Line = 0; Line <= Lines - 1; Line++) {
				int CurrKey = Key;
				int Offset = Common.Read32(Data, Line * 8 + Section_Data + 4) + Section_Data;
				int Length = Common.Read16(Data, Line * 8 + Section_Data + 8);
				int Start_Offset = Offset;

				string CurrLine = null;

				while (Offset < Start_Offset + Length * 2) {
					int Current_Data = (Common.Read16(Data, Offset) ^ CurrKey) & 0xffff;
					CurrKey = (((CurrKey << 3) | (CurrKey >> 13)) & 0xffff);
					Offset += 2;
					if (Current_Data == 0)
						break; // TODO: might not be correct. Was : Exit While

					if (Current_Data == 0x10) {
						int VarLen = (Common.Read16(Data, Offset) ^ CurrKey) & 0xffff;
						CurrKey = (((CurrKey << 3) | (CurrKey >> 13)) & 0xffff);
						int VarType = (Common.Read16(Data, Offset + 2) ^ CurrKey) & 0xffff;
						CurrKey = (((CurrKey << 3) | (CurrKey >> 13)) & 0xffff);
						Offset += 4;

						CurrLine += "\\code:0x" + Conversion.Hex(VarLen).PadLeft(4, '0');
						CurrLine += "\\0x" + Conversion.Hex(VarType).PadLeft(4, '0');
						VarLen -= 1;
						while (VarLen > 0) {
							int VarData = (Common.Read16(Data, Offset) ^ CurrKey) & 0xffff;
							CurrKey = (((CurrKey << 3) | (CurrKey >> 13)) & 0xffff);
							Offset += 2;
							VarLen -= 1;

							CurrLine += "\\0x" + Conversion.Hex(VarData).PadLeft(4, '0');
						}
					} else {
						switch (Current_Data) {
							case 0xe07f:
								Current_Data = 0x202f;
								break;
							case 0xe08d:
								Current_Data = 0x2026;
								break;
							case 0xe08e:
								Current_Data = 0x2642;
								break;
							case 0xe08f:
								Current_Data = 0x2640;
								break;
						}
						CurrLine += (char)(Current_Data);
					}
				}

				Key = (Key + 0x2983) & 0xffff;
				Strings[Line] = CurrLine;
			}
		}
		public void Insert_Strings(string[] Texts, string Out_File)
		{
			BinaryWriter Data = new BinaryWriter(new FileStream(Out_File, FileMode.Create));
			Data.Write(Convert.ToUInt16(1));
			Data.Write(Convert.ToUInt16(Texts.Length));
			Data.Write(Convert.ToUInt32(0));
			//Tamanho total
			Data.Write(Convert.ToUInt32(0));
			Data.Write(Convert.ToUInt32(0x10));

			int Key = 0x7c89;

			int Header_Offset = 0x14;
			int Base = Header_Offset + Texts.Count() * 8;
			Data.Seek(Base, SeekOrigin.Begin);

			int Total_Bytes = 0;
			foreach (string Line in Texts) {
				int CurrKey = Key;

				int Byte_Count = 0;
				int Block_Length = 0;
				int CurrOffset = Total_Bytes;
				for (int i = 0; i <= Line.Length - 1; i++) {
					bool Code_Mode = false;

					if (i + 6 < Line.Length) {
						if (Line.Substring(i, 6) == "\\code:") {
							Code_Mode = true;

							int VarLen = Convert.ToInt32(Line.Substring(i + 8, 4), 16);
							int Value = (0x10 ^ CurrKey) & 0xffff;
							CurrKey = (((CurrKey << 3) | (CurrKey >> 13)) & 0xffff);
							Data.Write(Convert.ToUInt16(Value));
							Value = ((VarLen & 0xffff) ^ CurrKey) & 0xffff;
							CurrKey = (((CurrKey << 3) | (CurrKey >> 13)) & 0xffff);
							Data.Write(Convert.ToUInt16(Value));
							Byte_Count += 4;

							int StrPos = 12;
							for (int j = 0; j <= VarLen - 1; j++) {
								Value = Convert.ToInt32(Line.Substring(i + StrPos + 3, 4), 16);
								Value = ((Value & 0xffff) ^ CurrKey) & 0xffff;
								CurrKey = (((CurrKey << 3) | (CurrKey >> 13)) & 0xffff);
								Data.Write(Convert.ToUInt16(Value));
								StrPos += 7;
								Byte_Count += 2;
								Block_Length += 1;
							}

							i += StrPos - 1;
						}
					}

					if (!Code_Mode) {
						string Character = Line.Substring(i, 1);
						int CharCode = Character[0];
						switch (CharCode) {
							case 0x202f:
								CharCode = 0xe07f;
								break;
							case 0x2026:
								CharCode = 0xe08d;
								break;
							case 0x2642:
								CharCode = 0xe08e;
								break;
							case 0x2640:
								CharCode = 0xe08f;
								break;
						}
						CharCode = ((CharCode & 0xffff) ^ CurrKey) & 0xffff;
						CurrKey = (((CurrKey << 3) | (CurrKey >> 13)) & 0xffff);
						Data.Write(Convert.ToUInt16(CharCode));
						Byte_Count += 2;
					}
				}

				int Zero = (0 ^ CurrKey) & 0xffff;
				Data.Write(Convert.ToUInt16(Zero));
				Byte_Count += 2;
				Total_Bytes += Byte_Count;
				if (Total_Bytes % 4 > 0) {
					Data.Write(Convert.ToUInt16(0));
					Total_Bytes += 2;
				}

				Data.Seek(Header_Offset, SeekOrigin.Begin);
				Data.Write(Convert.ToUInt32(CurrOffset + 4 + Texts.Count() * 8));
				Data.Write(Convert.ToUInt32(Byte_Count / 2));
				Header_Offset += 8;

				Data.Seek(Base + Total_Bytes, SeekOrigin.Begin);

				Key = (Key + 0x2983) & 0xffff;
			}

			Data.Seek(4, SeekOrigin.Begin);
			Data.Write(Convert.ToUInt32(Total_Bytes + Base - 0x10));
			Data.Seek(0x10, SeekOrigin.Begin);
			Data.Write(Convert.ToUInt32(Total_Bytes + Base - 0x10));

			Data.Close();
		}
	}
}
