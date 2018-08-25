using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace Ohana3DS
{
    public class Nako
	{
		public struct GARC_File
		{
			public int Bits;
			public int Start_Offset;
			public int End_Offset;
			public int Length;
			public int Uncompressed_Length;
			public bool Compressed;
			public string Name;
		}
		public GARC_File[] Files;
		private int Data_Offset;
		private int FATB_Offset;

		public string Current_File;
		public struct Inserted_File
		{
			public string File_Name;
			public int Index;
		}

		public List<Inserted_File> Inserted_Files;
		public float Compression_Percentage;
		public bool Fast_Compression = false;
		public bool Load(string File_Name)
		{
			Current_File = File_Name;
			FileStream InFile = new FileStream(File_Name, FileMode.Open);

			if (Strings.StrReverse(Common.ReadMagic(InFile, 0, 4)) == "GARC") {
				Data_Offset = Common.Read32(InFile, 0x10);
				int FATO_Length = Common.Read32(InFile, 0x20);

				//+======+
				//| FATB |
				//+======+
				FATB_Offset = 0x20 + FATO_Length;
				int Total_Files = Common.Read16(InFile, FATB_Offset + 4);
				Files = new GARC_File[Total_Files];
				int Offset = FATB_Offset + 8;
				for (int Current_File = 0; Current_File <= Total_Files - 1; Current_File++) {
					var _with1 = Files[Current_File];
					_with1.Bits = Common.Read32(InFile, Offset);
					_with1.Start_Offset = Common.Read32(InFile, Offset + 4);
					_with1.End_Offset = Common.Read32(InFile, Offset + 8);
					_with1.Length = Common.Read32(InFile, Offset + 12);
					InFile.Seek(Data_Offset + _with1.Start_Offset, SeekOrigin.Begin);
					if (InFile.ReadByte() == 0x11) {
						_with1.Compressed = true;
						_with1.Uncompressed_Length = Common.Read24(InFile, Data_Offset + _with1.Start_Offset + 1);
					} else {
						_with1.Uncompressed_Length = _with1.Length;
					}
					string File_Magic = Common.ReadMagic(InFile, Data_Offset + _with1.Start_Offset + (_with1.Compressed ? 5 : 0), 4);
					string Format = Guess_Format(File_Magic);
					_with1.Name = "file_" + Current_File + Format;
					Offset += 16;
				}
			} else if (Common.Read32(InFile, 4) == 0x80) {
				List<GARC_File> Temp = new List<GARC_File>();
				int Index = 0;
				for (int i = 4; i <= 0x80; i += 4) {
					GARC_File File = default(GARC_File);
					var _with2 = File;
					_with2.Start_Offset = Common.Read32(InFile, i);
					if (_with2.Start_Offset == InFile.Length)
						break; // TODO: might not be correct. Was : Exit For
					_with2.End_Offset = Common.Read32(InFile, i + 4);
					_with2.Length = _with2.End_Offset - _with2.Start_Offset;
					InFile.Seek(_with2.Start_Offset, SeekOrigin.Begin);
					if (InFile.ReadByte() == 0x11) {
						_with2.Compressed = true;
						_with2.Uncompressed_Length = Common.Read24(InFile, _with2.Start_Offset + 1);
					} else {
						_with2.Uncompressed_Length = _with2.Length;
					}

					string File_Magic = Common.ReadMagic(InFile, _with2.Start_Offset + (_with2.Compressed ? 5 : 0), 4);
					string Format = Guess_Format(File_Magic);
					_with2.Name = "file_" + Index + Format;
					Index += 1;
					Temp.Add(File);
				}
				Files = Temp.ToArray();
			} else {
				InFile.Close();
				return false;
			}

			InFile.Close();

			Inserted_Files = new List<Inserted_File>();
			return true;
		}
		public string Guess_Format(string File_Magic)
		{
			string Format = null;
			string Temp = File_Magic.Substring(0, 2);
			switch (Temp) {
				case "PC":
				case "PT":
				case "PB":
				case "PF":
				case "PK":
				case "PO":
				case "GR":
				case "MM":
				case "AD":
					Format = "." + Strings.LCase(Temp);
					break;
				default:
					if (File_Magic.Substring(0, 3) == "BCH") {
						Format = ".bch";
					} else if (File_Magic == "CGFX") {
						Format = ".cgfx";
					} else {
						Format = ".bin";
					}
					break;
			}
			return Format;
		}
		public void Extract(FileStream InFile, string Output_File, int File_Index)
		{
			bool GARC = Strings.StrReverse(Common.ReadMagic(InFile, 0, 4)) == "GARC";
			var _with3 = Files[File_Index];
			InFile.Seek(GARC ? Data_Offset : 0 + _with3.Start_Offset, SeekOrigin.Begin);
			byte[] Data = new byte[_with3.Length + 1];
			InFile.Read(Data, 0, _with3.Length);

			FileStream OutFile = new FileStream(Output_File, FileMode.Create);
			if (_with3.Compressed) {
				Data = LZSS_Decompress(Data);
				OutFile.Write(Data, 0, Data.Length);
			} else {
				OutFile.Write(Data, 0, _with3.Length);
			}

			OutFile.Close();
		}
		public void Insert()
		{
			FileStream Original_File = new FileStream(Current_File, FileMode.Open);
			if (Strings.StrReverse(Common.ReadMagic(Original_File, 0, 4)) == "GARC") {
				Original_File.Seek(0, SeekOrigin.Begin);
				string Temp_GARC_File = Path.GetTempFileName();
				BinaryWriter Output_File = new BinaryWriter(new FileStream(Temp_GARC_File, FileMode.Create));

				byte[] Header = new byte[(FATB_Offset + 8)];
				Original_File.Read(Header, 0, Header.Length);
				Output_File.Write(Header);

				Int32 Dummy_Place_Holder = 0;
				for (int i = 0; i <= Files.Count() - 1; i++) {
					Output_File.Write(Dummy_Place_Holder);
					//Bits
					Output_File.Write(Dummy_Place_Holder);
					//Offset inicial
					Output_File.Write(Dummy_Place_Holder);
					//Offset final
					Output_File.Write(Dummy_Place_Holder);
					//Tamanho
				}

				int File_Index = 0;
				int Offset = 0;
				foreach (GARC_File File in Files) {
					bool Copy_Original = true;
					for (int i = 0; i <= Inserted_Files.Count - 1; i++) {
						if (Inserted_Files[i].Index == File_Index) {
							byte[] Data = System.IO.File.ReadAllBytes(Inserted_Files[i].File_Name);
							if (File.Compressed & Data.Length < 0x1000000)
								Data = LZSS_Compress(Data);

							Output_File.Seek((FATB_Offset + 8) + (File_Index * 16), SeekOrigin.Begin);
							Output_File.Write(File.Bits);
							Output_File.Write(Convert.ToInt32(Offset));
							Output_File.Write(Convert.ToInt32(Offset + Data.Length));
							Output_File.Write(Convert.ToInt32(Data.Length));

							Output_File.Seek(Data_Offset + Offset, SeekOrigin.Begin);
							Output_File.Write(Data);

							Offset += Data.Length;

							Copy_Original = false;
							break; // TODO: might not be correct. Was : Exit For
						}
					}

					if (Copy_Original) {
						byte[] Data = new byte[File.Length];
						Original_File.Seek(Data_Offset + File.Start_Offset, SeekOrigin.Begin);
						Original_File.Read(Data, 0, Data.Length);

						Output_File.Seek((FATB_Offset + 8) + (File_Index * 16), SeekOrigin.Begin);
						Output_File.Write(File.Bits);
						Output_File.Write(Convert.ToInt32(Offset));
						Output_File.Write(Convert.ToInt32(Offset + Data.Length));
						Output_File.Write(Convert.ToInt32(Data.Length));

						Output_File.Seek(Data_Offset + Offset, SeekOrigin.Begin);
						Output_File.Write(Data);

						Offset += Data.Length;
					}

					File_Index += 1;
				}

				Original_File.Close();
				Output_File.Close();

				File.Delete(Current_File);
				File.Copy(Temp_GARC_File, Current_File);
			} else if (Common.Read32(Original_File, 4) == 0x80) {
				MemoryStream Stream = new MemoryStream();
				BinaryWriter New_Data = new BinaryWriter(Stream);
				New_Data.Write(Common.Read32(Original_File, 0));
				//Magic
				int Out_Offset = 0x80;
				int Index = 0;
				for (int i = 4; i <= 0x80; i += 4) {
					int In_Offset = Common.Read32(Original_File, i);
					if (In_Offset == Original_File.Length) {
						New_Data.Seek(i, SeekOrigin.Begin);
						long Len = Stream.Length;
						if ((Len & 0xff) > 0)
							Len = (Len & 0xffffff00) + 0x100;
						New_Data.Write(Convert.ToInt32(Len));
						if (Len != Stream.Length) {
							New_Data.Seek(Convert.ToInt32(Len - 1), SeekOrigin.Begin);
							New_Data.Write(Convert.ToByte(0));
						}
						break; // TODO: might not be correct. Was : Exit For
					}
					int Length = 0;
					bool Copy_Original = true;
					for (int j = 0; j <= Inserted_Files.Count - 1; j++) {
						if (Inserted_Files[j].Index == Index) {
							byte[] Data = File.ReadAllBytes(Inserted_Files[j].File_Name);
							if (Files[Index].Compressed)
								Data = LZSS_Compress(Data);
							Length = Data.Length;
							if ((Length & 0xff) > 0)
								Length = (int)((Length & 0xffffff00) + 0x100);
							New_Data.Seek(Out_Offset, SeekOrigin.Begin);
							New_Data.Write(Data, 0, Data.Length);

							Copy_Original = false;
						}
					}
					if (Copy_Original) {
						Length = Common.Read32(Original_File, i + 4) - In_Offset;
						byte[] Buff = new byte[Length];
						Original_File.Seek(In_Offset, SeekOrigin.Begin);
						Original_File.Read(Buff, 0, Buff.Length);
						New_Data.Seek(Out_Offset, SeekOrigin.Begin);
						New_Data.Write(Buff, 0, Buff.Length);
					}
					New_Data.Seek(i, SeekOrigin.Begin);
					New_Data.Write(Convert.ToInt32(Out_Offset));
					Out_Offset += Length;
					Index += 1;
				}
				Original_File.Close();
				File.WriteAllBytes(Current_File, Stream.ToArray());
			}
		}
		public byte[] LZSS_Decompress(byte[] InData)
		{
			int Decompressed_Size = (int)((Common.Read32(InData, 0) & 0xffffff00) >> 8);
			byte[] Data = new byte[Decompressed_Size];
			byte[] Dic = new byte[4096];

			int Source_Offset = 4;
			int Destination_Offset = 0;
			int BitCount = 8;
			int Dictionary_Offset = 0;
			int BitFlags = 0;
			while (Destination_Offset < Decompressed_Size) {
				if (BitCount == 8) {
					BitFlags = InData[Source_Offset];
					Source_Offset += 1;
					BitCount = 0;
				}

				if ((BitFlags & Common.Power_Of_Two[7 - BitCount]) == 0) {
					Dic[Dictionary_Offset] = InData[Source_Offset];
					Source_Offset += 1;
					Data[Destination_Offset] = Dic[Dictionary_Offset];
					Dictionary_Offset = (Dictionary_Offset + 1) & 0xfff;
					Destination_Offset += 1;
				} else {
					int Back = 0;
					int Length = 0;
					int Indicator = (InData[Source_Offset] & 0xf0) >> 4;
					switch (Indicator) {
						case 0:
                            {
                                int Byte_1 = InData[Source_Offset];
                                int Byte_2 = InData[Source_Offset + 1];
                                int Byte_3 = InData[Source_Offset + 2];

                                Back = ((Byte_2 & 0xf) << 8) | Byte_3;
                                Length = (((Byte_1 & 0xf) << 4) | (Byte_2 >> 4)) + 0x11;
                                Source_Offset += 3;
                                break;
                            }
						case 1:
                            {
                                int Byte_1 = InData[Source_Offset];
                                int Byte_2 = InData[Source_Offset + 1];
                                int Byte_3 = InData[Source_Offset + 2];
                                int Byte_4 = InData[Source_Offset + 3];

                                Back = ((Byte_3 & 0xf) << 8) | Byte_4;
                                Length = (((Byte_1 & 0xf) << 12) | (Byte_2 << 4) | (Byte_3 >> 4)) + 0x111;
                                Source_Offset += 4;
                                break;
                            }
						default:
                            {
                                int Byte_1 = InData[Source_Offset];
                                int Byte_2 = InData[Source_Offset + 1];

                                Back = ((Byte_1 & 0xf) << 8) | Byte_2;
                                Length = Indicator + 1;
                                Source_Offset += 2;
                                break;
                            }
					}
					Back += 1;

					if (Destination_Offset + Length > Decompressed_Size)
						Length = Decompressed_Size - Destination_Offset;
					int Old_Offset = Dictionary_Offset;
					for (int i = 0; i <= Length - 1; i++) {
						Dic[Dictionary_Offset] = Dic[(Old_Offset - Back + i) & 0xfff];
						Data[Destination_Offset] = Dic[Dictionary_Offset];
						Destination_Offset += 1;
						Dictionary_Offset = (Dictionary_Offset + 1) & 0xfff;
					}
				}
				BitCount += 1;
			}

			return Data;
		}
		public byte[] LZSS_Compress(byte[] InData)
		{
			byte[] Dic = new byte[4096];
			int Dictionary_Offset = 0;

			byte[] Data = new byte[Convert.ToInt32(InData.Length + ((InData.Length) / 8) + 3) + 1];
			int Source_Offset = 0;
			int Destination_Offset = 0;
			int BitCount = 0;
			Data[0] = 0x11;
			Data[1] = Convert.ToByte(InData.Length & 0xff);
			Data[2] = Convert.ToByte((InData.Length & 0xff00) >> 8);
			Data[3] = Convert.ToByte((InData.Length & 0xff0000) >> 16);
			Destination_Offset = 4;
			int BitsPtr = 0;

			while (Source_Offset < InData.Length) {
				if (BitCount == 0) {
					BitsPtr = Destination_Offset;
					Destination_Offset += 1;
					BitCount = 8;
				}

				int DicPos = 0;
				int Found_Data = 0;
				bool Compressed_Data = false;
				int Index = Array.IndexOf(Dic, InData[Source_Offset]);
				if (Index != -1) {
					do {
						int DataSize = 0;
						for (int j = 0; j <= 15; j++) {
							if (Source_Offset + j >= InData.Length)
								break; // TODO: might not be correct. Was : Exit For
							if (Dic[(Index + j) & 0xfff] == InData[Source_Offset + j]) {
								DataSize += 1;
							} else {
								break; // TODO: might not be correct. Was : Exit For
							}
						}
						if (DataSize >= 3) {
							if (Index + DataSize < Dictionary_Offset | Index > Dictionary_Offset + DataSize) {
								if (DataSize > Found_Data) {
									Compressed_Data = true;
									Found_Data = DataSize;
									DicPos = Index;
								}
							}
						}
						if (Fast_Compression)
							break; // TODO: might not be correct. Was : Exit Do
						Index = Array.IndexOf(Dic, InData[Source_Offset], Index + 1);
						if (Index == -1)
							break; // TODO: might not be correct. Was : Exit Do
					} while (true);
				}

				if (Compressed_Data & ((DicPos < Source_Offset) | (Source_Offset > 0xfff))) {
					int Back = Dictionary_Offset - DicPos - 1;
					Data[BitsPtr] = (byte)(Data[BitsPtr] | Convert.ToByte((Math.Pow(2, (BitCount - 1)))));
					//Comprimido, define bit
					Data[Destination_Offset] = Convert.ToByte((((Found_Data - 1) & 0xf) * 0x10) + ((Back & 0xf00) / 0x100));
					Data[Destination_Offset + 1] = Convert.ToByte(Back & 0xff);
					Destination_Offset += 2;
					for (int j = 0; j <= Found_Data - 1; j++) {
						Dic[Dictionary_Offset] = InData[Source_Offset];
						Dictionary_Offset = (Dictionary_Offset + 1) & 0xfff;
						Source_Offset += 1;
					}
				} else {
					Data[Destination_Offset] = InData[Source_Offset];
					Dic[Dictionary_Offset] = Data[Destination_Offset];
					Dictionary_Offset = (Dictionary_Offset + 1) & 0xfff;
					Destination_Offset += 1;
					Source_Offset += 1;
				}

				Compression_Percentage = Convert.ToSingle((Source_Offset / InData.Length) * 100);

				BitCount -= 1;
			}

			Array.Resize(ref Data, Destination_Offset);

			return Data;
		}
	}
}
