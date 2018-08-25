using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using static Microsoft.DirectX.Direct3D.CustomVertex;

namespace Ohana3DS
{
    public class Ohana
	{

		#region "Declares"

		private Device Device;

		const bool Coll_Debug = false;
		public struct Data_Entry
		{
			public int Offset;
			public int Length;
			public int Format;
		}
		public struct OhanaVertex
		{
			public float X;
			public float Y;
			public float Z;
			public float NX;
			public float NY;
			public float NZ;
			public int Color;
			public float U;

			public float V;
			//---

			public int Bone_1;
			public int Bone_2;
			public int Bone_3;
			public int Bone_4;
			public float Weight_1;
			public float Weight_2;
			public float Weight_3;
			public float Weight_4;
		}
		public struct VertexList
		{

			public int Texture_ID;
			public Data_Entry Vertex_Entry;

			public OhanaVertex[] Vertice;
			public int[] Index;
			public List<Data_Entry> Per_Face_Entry;
			public List<int[]> Per_Face_Index;
			public int Texture_ID_Offset;
		}
		public struct OhanaTexture
		{
			public string Name;
			public Bitmap Image;
			public Bitmap Image_Mirrored;
			public Texture Texture;

			public bool Has_Alpha;
			public int Offset;
			public int Format;
		}
		public struct OhanaBone
		{
			public string Name;
			public int Parent_ID;
			public Vector3 Translation;
			public Vector3 Rotation;
			public Vector3 Scale;
		}
		public enum ModelType
		{
			Character,
			Map
		}
		private enum BCH_Version
		{
			XY,
			ORAS
		}

		private struct Vertex_Face
		{
			public int Vtx_A_Coord_Index;
			public int Vtx_A_Normal_Index;

			public int Vtx_A_UV_Index;
			public int Vtx_B_Coord_Index;
			public int Vtx_B_Normal_Index;

			public int Vtx_B_UV_Index;
			public int Vtx_C_Coord_Index;
			public int Vtx_C_Normal_Index;
			public int Vtx_C_UV_Index;
		}

		public VertexList[] Model_Object;
		public List<OhanaTexture> Model_Texture;
		public OhanaBone[] Model_Bone;
		public string[] Model_Texture_Index;

		public string[] Model_Bump_Map_Index;

		public Microsoft.DirectX.Direct3D.CustomVertex.PositionOnly[] Collision;
		public string Magic;

		public ModelType Model_Type;
		public bool Lighting = true;
		public float Scale = 32f;

		public float Load_Scale;
		public struct OhanaInfo
		{
			public int Vertex_Count;
			public int Triangles_Count;
			public int Bones_Count;
			public int Textures_Count;
		}

		public OhanaInfo Info;

		private int Total_Vertex;
		private int SWidth;
		private int SHeight;
		public float Zoom = 1f;
		public Vector2 Rotation;

		public Vector2 Translation;
		private float Max_X_Neg;
		private float Max_X_Pos;
		public float Max_Y_Neg;

		public float Max_Y_Pos;
		public bool Rendering;
		public string Current_Model;
		public string Current_Texture;
		public string Temp_Model_File;
		public string Temp_Texture_File;
		public bool BCH_Have_Textures;

		public Color bgCol = Color.Black;
		public int Selected_Object;
		public int Selected_Face;
		public bool Edit_Mode;

		public bool Map_Properties_Mode;

		public float Texture_Insertion_Percentage;
		private int[] Tile_Order = {
			0,
			1,
			8,
			9,
			2,
			3,
			10,
			11,
			16,
			17,
			24,
			25,
			18,
			19,
			26,
			27,
			4,
			5,
			12,
			13,
			6,
			7,
			14,
			15,
			20,
			21,
			28,
			29,
			22,
			23,
			30,
			31,
			32,
			33,
			40,
			41,
			34,
			35,
			42,
			43,
			48,
			49,
			56,
			57,
			50,
			51,
			58,
			59,
			36,
			37,
			44,
			45,
			38,
			39,
			46,
			47,
			52,
			53,
			60,
			61,
			54,
			55,
			62,
			63
		};
		private int[,] Modulation_Table = {
			{
				2,
				8,
				-2,
				-8
			},
			{
				5,
				17,
				-5,
				-17
			},
			{
				9,
				29,
				-9,
				-29
			},
			{
				13,
				42,
				-13,
				-42
			},
			{
				18,
				60,
				-18,
				-60
			},
			{
				24,
				80,
				-24,
				-80
			},
			{
				33,
				106,
				-33,
				-106
			},
			{
				47,
				183,
				-47,
				-183
			}
			#endregion
		};

		#region "DirectX Initialize"
		public void Initialize(PictureBox Picture)
		{
			PresentParameters Present = new PresentParameters();
			var _with1 = Present;
			_with1.BackBufferCount = 1;
			_with1.BackBufferFormat = Manager.Adapters[0].CurrentDisplayMode.Format;
			_with1.BackBufferWidth = Picture.Width;
			_with1.BackBufferHeight = Picture.Height;
			SWidth = Picture.Width;
			SHeight = Picture.Height;
			_with1.Windowed = true;
			_with1.SwapEffect = SwapEffect.Discard;
			_with1.EnableAutoDepthStencil = true;
			_with1.AutoDepthStencilFormat = DepthFormat.D16;
			MultiSampleType Samples = default(MultiSampleType);
			for (Samples = MultiSampleType.SixteenSamples; Samples >= MultiSampleType.None; Samples += -1) {
				if (Manager.CheckDeviceMultiSampleType(0, DeviceType.Hardware, Format.D16, true, Samples))
					break; // TODO: might not be correct. Was : Exit For
			}
			_with1.MultiSample = Samples;

			Device = new Device(0, DeviceType.Hardware, Picture.Handle, CreateFlags.HardwareVertexProcessing, Present);
			var _with2 = Device;
			_with2.RenderState.CullMode = Cull.None;
			_with2.RenderState.ZBufferEnable = true;
			_with2.RenderState.AlphaBlendEnable = true;
			_with2.RenderState.SourceBlend = Blend.SourceAlpha;
			_with2.RenderState.DestinationBlend = Blend.InvSourceAlpha;
			_with2.RenderState.BlendOperation = BlendOperation.Add;
			_with2.RenderState.AlphaFunction = Compare.GreaterEqual;
			_with2.RenderState.ReferenceAlpha = 0x7f;
			_with2.RenderState.AlphaTestEnable = true;

			_with2.SamplerState[0].MaxMipLevel = 1;
			_with2.SamplerState[0].MipFilter = TextureFilter.Anisotropic;
			_with2.SamplerState[0].MinFilter = TextureFilter.Anisotropic;
			_with2.SamplerState[0].MagFilter = TextureFilter.Anisotropic;
			_with2.SamplerState[0].MaxAnisotropy = 16;
		}
		#endregion

		#region "Model"
		public bool Load_Model(string File_Name, bool DX = true)
		{
			byte[] Temp = File.ReadAllBytes(File_Name);
			Magic = Common.ReadMagic(Temp, 0, 3);
			int BCH_Offset = 0;
			BCH_Version Version = default(BCH_Version);

			//Reset
			Total_Vertex = 0;
			Max_X_Neg = 0;
			Max_X_Pos = 0;
			Max_Y_Neg = 0;
			Max_Y_Pos = 0;
			Model_Object = null;
			Current_Model = null;
			if (Temp_Model_File != null) {
				File.Delete(Temp_Model_File);
				Temp_Model_File = null;
			}
			BCH_Have_Textures = false;

			string Magic_2_Bytes = Magic.Substring(0, 2);
			//Verifica se o Magic é de um modelo
			if (Magic_2_Bytes != "MM" & Magic_2_Bytes != "TM" & Magic_2_Bytes != "PC" & Magic_2_Bytes != "GR" & Magic != "BCH") {
				return false;
			}
			if (Common.Read24(Temp, 0x80) == 0x484342) {
				BCH_Offset = 0x80;
				Model_Type = ModelType.Character;
			} else if (Magic == "BCH") {
				BCH_Offset = 0;
				Model_Type = ModelType.Character;
			} else if (Magic_2_Bytes == "GR") {
				BCH_Offset = Common.Read32(Temp, 8);
				Model_Type = ModelType.Map;
			} else {
				return false;
			}

			Load_Scale = Scale;
			Current_Model = File_Name;
			Temp_Model_File = Path.GetTempFileName();
			File.WriteAllBytes(Temp_Model_File, Temp);

			if (Model_Type == ModelType.Map) {
				int Coll_Offset = Common.Read32(Temp, 0xc) + 0x20;
				int Length = Common.Read32(Temp, 0x10) - Coll_Offset;
				Collision = new PositionOnly[Length / 16 + 1];
				int Index = 0;
				for (int Offset = Coll_Offset; Offset <= Coll_Offset + Length - 1; Offset += 16) {
					if (Common.Read32(Temp, Offset) == 0)
						break; // TODO: might not be correct. Was : Exit For
					var _with3 = Collision[Index];
					_with3.X = BitConverter.ToSingle(Temp, Offset) / Load_Scale;
					_with3.Y = BitConverter.ToSingle(Temp, Offset + 4) / Load_Scale;
					_with3.Z = BitConverter.ToSingle(Temp, Offset + 8) / Load_Scale;
					Index += 1;
				}
			}

			byte[] Data = new byte[Temp.Length - BCH_Offset + 1];
			Buffer.BlockCopy(Temp, BCH_Offset, Data, 0, Temp.Length - BCH_Offset);

			int Header_Offset = Common.Read32(Data, 8);
			if (Header_Offset == 0x44)
				Version = BCH_Version.ORAS;
			else
				Version = BCH_Version.XY;

			int Texture_Names_Offset = Common.Read32(Data, 0xc);
			int Description_Offset = Common.Read32(Data, 0x10);
			int Data_Offset = Common.Read32(Data, 0x14);
			int Texture_Names_Length = Common.Read32(Data, 0x20);
			int BCH_Texture_Table = Header_Offset + Common.Read32(Data, Header_Offset + 0x24);
			int BCH_Texture_Count = Common.Read32(Data, Header_Offset + 0x28);
			//O modelo tem texturas embutidas
			if (BCH_Texture_Count > 0) {
				BCH_Have_Textures = true;
				if (File.Exists(Temp_Texture_File))
					File.Delete(Temp_Texture_File);
				Current_Texture = null;
				Temp_Texture_File = null;

				Load_BCH_Textures(Data, BCH_Texture_Count, BCH_Offset, Header_Offset, Data_Offset, Description_Offset, Texture_Names_Offset, BCH_Texture_Table, Version);
			}
			int Table_Offset = Common.Read32(Data, Header_Offset + Common.Read32(Data, Header_Offset));
			if (Table_Offset == 0)
				return BCH_Texture_Count > 0;
			Table_Offset += Header_Offset + 0x34;

			int Texture_Entries = Common.Read32(Data, Table_Offset + 4);
			int Bone_Entries = Common.Read32(Data, Table_Offset + 0x40);
			int Bones_Offset = Header_Offset + Common.Read32(Data, Table_Offset + 0x44);
			int Entries = Common.Read32(Data, Table_Offset + 0x10);
			int Texture_Table_Offset = 0;
			if (Version == BCH_Version.XY) {
				Texture_Table_Offset = 0x78 + Common.Read32(Data, Table_Offset);
			} else if (Version == BCH_Version.ORAS) {
				Texture_Table_Offset = 0x48 + Common.Read32(Data, Table_Offset);
			}
			Table_Offset = Header_Offset + Common.Read32(Data, Table_Offset + 0x14);
			//+==========+
			//| Vertices |
			//+==========+
			List<int> Vertex_Offsets = new List<int>();
			List<int> Face_Offsets = new List<int>();
			for (int Entry = 0; Entry <= Entries - 1; Entry++) {
				int Base_Offset = Table_Offset + (Entry * 0x38);

				int Vertex_Offset = Description_Offset + Common.Read32(Data, Base_Offset + 8);
				Vertex_Offsets.Add(Data_Offset + Common.Read32(Data, Vertex_Offset + 0x30));

				int Face_Offset = Common.Read32(Data, Base_Offset + 0x10);
				Face_Offset = Description_Offset + Common.Read32(Data, Face_Offset + (Header_Offset + 0x2c));
				Face_Offsets.Add(Data_Offset + Common.Read32(Data, Face_Offset + 0x10));
			}
			Vertex_Offsets.Sort();
			Face_Offsets.Sort();
			Vertex_Offsets.Add(Face_Offsets[0]);

			List<int> Texture_ID_List = new List<int>();
			Model_Object = new VertexList[Entries];
			int Vertex_Count = 0;
			for (int Entry = 0; Entry <= Entries - 1; Entry++) {
				int Base_Offset = Table_Offset + (Entry * 0x38);
				int Texture_ID = Common.Read16(Data, Base_Offset);
				if (!Texture_ID_List.Contains(Texture_ID))
					Texture_ID_List.Add(Texture_ID);
				int Vertex_Offset = Description_Offset + Common.Read32(Data, Base_Offset + 8);
				int Face_Offset = Common.Read32(Data, Base_Offset + 0x10);
				int Face_Count = Common.Read32(Data, Base_Offset + 0x14);
				int[] Faces = new int[Face_Count];
				for (int Index = 0; Index <= Face_Count - 1; Index++) {
					Faces[Index] = Description_Offset + Common.Read32(Data, Face_Offset + (Header_Offset + 0x2c) + (Index * 0x34));
				}

				Face_Offset = Description_Offset + Common.Read32(Data, Face_Offset + (Header_Offset + 0x2c));
				int Vertex_Data_Offset = Data_Offset + Common.Read32(Data, Vertex_Offset + 0x30);

				int Vertex_Data_Format = Data[Vertex_Offset + 0x3a];
				int Vertex_Flags = Common.Read32(Data, Face_Offset);
				int Vertex_Data_Length = 0;
				if (Version == BCH_Version.XY) {
					int Face_Data_Offset = Data_Offset + Common.Read32(Data, Face_Offset + 0x10);
					Vertex_Data_Length = Face_Data_Offset - Vertex_Data_Offset;
				} else if (Version == BCH_Version.ORAS) {
					Vertex_Data_Length = Vertex_Offsets[Vertex_Offsets.IndexOf(Vertex_Data_Offset) + 1] - Vertex_Data_Offset;
				}

				List<int> Index_List = new List<int>();
				Model_Object[Entry].Per_Face_Entry = new List<Data_Entry>();
				Model_Object[Entry].Per_Face_Index = new List<int[]>();
				if (Entry == Entries - 1) {
					int Face_Total_Length = 0;
					foreach (int Face in Faces) {
						Face_Total_Length += Common.Read32(Data, Face + 0x18);
					}
					int Temp_Length = Face_Total_Length * Vertex_Data_Format;
					if (Temp_Length < Vertex_Data_Length)
						Vertex_Data_Length = Temp_Length;
				}
				int Count = Vertex_Data_Length / Vertex_Data_Format;
				foreach (int Face in Faces) {
					int Face_Data_Offset = Data_Offset + Common.Read32(Data, Face + 0x10);
					int Face_Data_Length = Common.Read32(Data, Face + 0x18);
					int Face_Data_Format = 2;

					int Temp_Offset = Face_Data_Offset;
					for (int Index = 0; Index <= Face_Data_Length - 1; Index += 3) {
						int Temp_1 = Convert.ToInt32(Common.Read16(Data, Temp_Offset));
						int Temp_2 = Convert.ToInt32(Common.Read16(Data, Temp_Offset + 2));
						int Temp_3 = Convert.ToInt32(Common.Read16(Data, Temp_Offset + 4));

						if (Temp_1 > Count | Temp_2 > Count | Temp_3 > Count) {
							Face_Data_Format = 1;
							break; // TODO: might not be correct. Was : Exit For
						}
						Temp_Offset += 6;
					}

					Data_Entry Face_Entry = default(Data_Entry);
					Face_Entry.Offset = Face_Data_Offset + BCH_Offset;
					Face_Entry.Length = Face_Data_Length * Face_Data_Format;
					Face_Entry.Format = Face_Data_Format;
					Model_Object[Entry].Per_Face_Entry.Add(Face_Entry);

					int CurrOffset = Face_Data_Offset;
					List<int> Per_Face_Index = new List<int>();
					for (int Index = 0; Index <= Face_Data_Length - 1; Index += 3) {
						if (Face_Data_Format == 2) {
							Per_Face_Index.Add(Convert.ToInt32(Common.Read16(Data, CurrOffset)));
							Per_Face_Index.Add(Convert.ToInt32(Common.Read16(Data, CurrOffset + 2)));
							Per_Face_Index.Add(Convert.ToInt32(Common.Read16(Data, CurrOffset + 4)));
						} else {
							Per_Face_Index.Add(Data[CurrOffset]);
							Per_Face_Index.Add(Data[CurrOffset + 1]);
							Per_Face_Index.Add(Data[CurrOffset + 2]);
						}
						CurrOffset += 3 * Face_Data_Format;
						Total_Vertex += 3;
					}
					Index_List.AddRange(Per_Face_Index);
					Model_Object[Entry].Per_Face_Index.Add(Per_Face_Index.ToArray());
				}

				Model_Object[Entry].Vertice = new OhanaVertex[Count];
				int Offset = Vertex_Data_Offset;
				for (int Index = 0; Index <= Count - 1; Index++) {
					var _with4 = Model_Object[Entry].Vertice[Index];
					_with4.X = BitConverter.ToSingle(Data, Offset) / Scale;
					_with4.Y = BitConverter.ToSingle(Data, Offset + 4) / Scale;
					_with4.Z = BitConverter.ToSingle(Data, Offset + 8) / Scale;

                    unchecked
                    {
                        _with4.Color = (int)0xffffffff;
                    }
					switch (Vertex_Data_Format) {
						case 0x10:
							_with4.Color = Common.Read32(Data, Offset + 12);
							break;
						case 0x14:
						case 0x18:
						case 0x1c:
							if ((Vertex_Flags & 0xffff) != 0x285) {
								_with4.U = BitConverter.ToSingle(Data, Offset + 12);
								_with4.V = BitConverter.ToSingle(Data, Offset + 16);
							}
							break;
						case 0x20:
						case 0x30:
						case 0x38:
							int Flags = Vertex_Flags & 0xffff;
							if (Flags != 0xa680 & Flags != 0xec81) {
								_with4.NX = BitConverter.ToSingle(Data, Offset + 12) / Scale;
								_with4.NY = BitConverter.ToSingle(Data, Offset + 16) / Scale;
								_with4.NZ = BitConverter.ToSingle(Data, Offset + 20) / Scale;
							}

							_with4.U = BitConverter.ToSingle(Data, Offset + 24);
							_with4.V = BitConverter.ToSingle(Data, Offset + 28);
							break;
						case 0x24:
						case 0x28:
						case 0x2c:
							_with4.NX = BitConverter.ToSingle(Data, Offset + 12) / Scale;
							_with4.NY = BitConverter.ToSingle(Data, Offset + 16) / Scale;
							_with4.NZ = BitConverter.ToSingle(Data, Offset + 20) / Scale;

							_with4.U = BitConverter.ToSingle(Data, Offset + 24);
							_with4.V = BitConverter.ToSingle(Data, Offset + 28);

							if (Vertex_Data_Format == 0x24) {
								switch (Vertex_Flags & 0xffff) {
									case 0x8e82:
									case 0xaa83:
									case 0xaadb:
									case 0xac81:
									case 0xae83:
										_with4.Color = Common.Read32(Data, Offset + 32);
										break;
								}
							} else {
								switch (Vertex_Flags & 0xffff) {
									case 0xaa83:
									case 0xab83:
									case 0xae83:
									case 0xaf83:
									case 0xef83:
									case 0xefd3:
										_with4.Color = Common.Read32(Data, Offset + 32);
										break;
								}
							}
							break;
						case 0x34:
							_with4.NX = BitConverter.ToSingle(Data, Offset + 12) / Scale;
							_with4.NY = BitConverter.ToSingle(Data, Offset + 16) / Scale;
							_with4.NZ = BitConverter.ToSingle(Data, Offset + 20) / Scale;

							_with4.U = BitConverter.ToSingle(Data, Offset + 24);
							_with4.V = BitConverter.ToSingle(Data, Offset + 28);
							break;
					}

					if (_with4.X > Max_X_Pos)
						Max_X_Pos = _with4.X;
					if (_with4.X < Max_X_Neg)
						Max_X_Neg = _with4.X;
					if (_with4.Y > Max_Y_Pos)
						Max_Y_Pos = _with4.Y;
					if (_with4.Y < Max_Y_Neg)
						Max_Y_Neg = _with4.Y;

					Vertex_Count += 1;
					Offset += Vertex_Data_Format;
				}

				var _with5 = Model_Object[Entry];
				_with5.Index = Index_List.ToArray();
				_with5.Texture_ID = Texture_ID;
				_with5.Texture_ID_Offset = Base_Offset + BCH_Offset;

				_with5.Vertex_Entry.Offset = Vertex_Data_Offset + BCH_Offset;
				_with5.Vertex_Entry.Length = Vertex_Data_Length;
				_with5.Vertex_Entry.Format = Vertex_Data_Format;
			}

			//+==========+
			//| Texturas |
			//+==========+
			int Name_Table_Base_Pointer = 0;
			int Name_Table_Length = 0;
			if (Version == BCH_Version.XY) {
				if (Model_Type == ModelType.Character) {
					if (Magic_2_Bytes == "MM") {
						Name_Table_Base_Pointer = 0xc;
					} else {
						Name_Table_Base_Pointer = 8;
					}
				} else if (Model_Type == ModelType.Map) {
					Name_Table_Base_Pointer = 0x14;
				}
				Name_Table_Length = 0x58;
			} else if (Version == BCH_Version.ORAS) {
				if (Magic_2_Bytes == "MM") {
					Name_Table_Base_Pointer = 0x1c;
				} else {
					Name_Table_Base_Pointer = 0x18;
				}
				Name_Table_Length = 0x2c;
			}

			if (BCH_Have_Textures) {
				Model_Texture_Index = new string[Texture_Entries];
				Model_Bump_Map_Index = new string[Texture_Entries];
				for (var Index = 0; Index <= BCH_Texture_Count - 1; Index++) {
					int Name_Offset = Texture_Names_Offset + Common.Read32(Data, (Header_Offset + Common.Read32(Data, BCH_Texture_Table + (Index * 4))) + 0x1c);
					string Texture_Name = null;
					do {
						int Value = Data[Name_Offset];
						Name_Offset += 1;
						if (Value != 0)
							Texture_Name += Strings.Chr(Value);
						else
							break; // TODO: might not be correct. Was : Exit Do
					} while (true);

					if (Index < Texture_Entries) {
						int Model_Texture_Name_Offset = Texture_Names_Offset + Common.Read32(Data, (Texture_Table_Offset + Index * Name_Table_Length) + Name_Table_Base_Pointer);
						string Model_Texture_Name = null;
						do {
							int Value = Data[Model_Texture_Name_Offset];
							Model_Texture_Name_Offset += 1;
							if (Value != 0)
								Model_Texture_Name += Strings.Chr(Value);
							else
								break; // TODO: might not be correct. Was : Exit Do
						} while (true);

						if (Model_Texture_Name != null & Model_Texture_Name != "projection_dummy") {
							Model_Texture_Index[Index] = Model_Texture_Name;
						//Workaround
						} else {
							Model_Texture_Index[Index] = Texture_Name;
						}
					}
				}
			} else {
				Model_Texture_Index = new string[Texture_Entries];
				Model_Bump_Map_Index = new string[Texture_Entries];
				for (int Index = 0; Index <= Texture_Entries - 1; Index++) {
					int Texture_Offset = Texture_Names_Offset + Common.Read32(Data, (Texture_Table_Offset + Index * Name_Table_Length) + Name_Table_Base_Pointer);
					int Normal_Offset = Texture_Names_Offset + Common.Read32(Data, (Texture_Table_Offset + Index * Name_Table_Length) + Name_Table_Base_Pointer + 8);
					//Textura
					for (int i = 0; i <= 1; i++) {
						string Texture_Name = null;
						do {
							int Value = Data[Texture_Offset];
							Texture_Offset += 1;
							if (Value != 0)
								Texture_Name += Strings.Chr(Value);
							else
								break; // TODO: might not be correct. Was : Exit Do
						} while (true);
						Model_Texture_Index[Index] = Texture_Name;
						if (Texture_Name != "projection_dummy" | Model_Type != ModelType.Map)
							break; // TODO: might not be correct. Was : Exit For
						//Workaround
						Texture_Offset = Texture_Names_Offset + Common.Read32(Data, (Texture_Table_Offset + Index * Name_Table_Length) + Name_Table_Base_Pointer + 4);
					}

					if (Model_Type == ModelType.Character) {
						//Mapa de Normals/Bump Map
						string Normal_Name = null;
						do {
							int Value = Data[Normal_Offset];
							Normal_Offset += 1;
							if (Value != 0)
								Normal_Name += Strings.Chr(Value);
							else
								break; // TODO: might not be correct. Was : Exit Do
						} while (true);
						Model_Bump_Map_Index[Index] = Normal_Name;
					}
				}
			}

			//+=======+
			//| Bones |
			//+=======+
			Model_Bone = new OhanaBone[Bone_Entries];
			Bones_Offset += (Bone_Entries * 0xc) + 0xc;
			for (int Bone = 0; Bone <= Bone_Entries - 1; Bone++) {
				int Bone_Parent = Signed_Short(Common.Read16(Data, Bones_Offset + 4));
				int Bone_Name_Offset = Common.Read32(Data, Bones_Offset + 92);

				OhanaBone MyBone = default(OhanaBone);
				var _with6 = MyBone;
				_with6.Name = null;
				do {
					int Value = Data[Texture_Names_Offset + Bone_Name_Offset];
					Bone_Name_Offset += 1;
					if (Value != 0)
						_with6.Name += Strings.Chr(Value);
					else
						break; // TODO: might not be correct. Was : Exit Do
				} while (true);
				_with6.Parent_ID = Bone_Parent;

				_with6.Translation.X = BitConverter.ToSingle(Data, Bones_Offset + 32) / Scale;
				_with6.Translation.Y = BitConverter.ToSingle(Data, Bones_Offset + 36) / Scale;
				_with6.Translation.Z = BitConverter.ToSingle(Data, Bones_Offset + 40) / Scale;

				_with6.Rotation.X = BitConverter.ToSingle(Data, Bones_Offset + 20);
				_with6.Rotation.Y = BitConverter.ToSingle(Data, Bones_Offset + 24);
				_with6.Rotation.Z = BitConverter.ToSingle(Data, Bones_Offset + 28);

				_with6.Scale.X = BitConverter.ToSingle(Data, Bones_Offset + 8);
				_with6.Scale.Y = BitConverter.ToSingle(Data, Bones_Offset + 12);
				_with6.Scale.Z = BitConverter.ToSingle(Data, Bones_Offset + 16);
				Model_Bone[Bone] = MyBone;

				Matrix Bone_Mtx = new Matrix();

				Bone_Mtx.M11 = BitConverter.ToSingle(Data, Bones_Offset + 44);
				Bone_Mtx.M12 = BitConverter.ToSingle(Data, Bones_Offset + 48);
				Bone_Mtx.M13 = BitConverter.ToSingle(Data, Bones_Offset + 52);
				Bone_Mtx.M14 = BitConverter.ToSingle(Data, Bones_Offset + 56);

				Bone_Mtx.M21 = BitConverter.ToSingle(Data, Bones_Offset + 60);
				Bone_Mtx.M22 = BitConverter.ToSingle(Data, Bones_Offset + 64);
				Bone_Mtx.M23 = BitConverter.ToSingle(Data, Bones_Offset + 68);
				Bone_Mtx.M24 = BitConverter.ToSingle(Data, Bones_Offset + 72);

				Bone_Mtx.M31 = BitConverter.ToSingle(Data, Bones_Offset + 76);
				Bone_Mtx.M32 = BitConverter.ToSingle(Data, Bones_Offset + 80);
				Bone_Mtx.M33 = BitConverter.ToSingle(Data, Bones_Offset + 84);
				Bone_Mtx.M34 = BitConverter.ToSingle(Data, Bones_Offset + 88);

				Bones_Offset += 100;
			}

			List<string> TempLst = new List<string>();
			for (int Index = 0; Index <= Model_Texture_Index.Length - 1; Index++) {
				if (!TempLst.Contains(Model_Texture_Index[Index]))
					TempLst.Add(Model_Texture_Index[Index]);
			}
			var _with7 = Info;
			_with7.Vertex_Count = Vertex_Count;
			_with7.Triangles_Count = Total_Vertex / 3;
			_with7.Bones_Count = Bone_Entries;
			_with7.Textures_Count = Texture_ID_List.Count - (Texture_Entries - TempLst.Count);

			if (DX)
				Switch_Lighting(Lighting);

			return true;
		}

		public void Export_SMD(string File_Name)
		{
			NumberFormatInfo Info = new NumberFormatInfo();
			Info.NumberDecimalSeparator = ".";
			Info.NumberDecimalDigits = 6;

			StringBuilder Out = new StringBuilder();
			Out.AppendLine("version 1");
			Out.AppendLine("nodes");
			int Node_Index = 0;
			foreach (OhanaBone Bone in Model_Bone) {
				var _with8 = Bone;
				Out.AppendLine(Node_Index + " \"" + Bone.Name + "\" " + Bone.Parent_ID);
				Node_Index += 1;
			}
			Out.AppendLine("end");
			Out.AppendLine("skeleton");
			Out.AppendLine("time 0");
			int Bone_Index = 0;
			foreach (OhanaBone Bone in Model_Bone) {
				var _with9 = Bone;
				Out.AppendLine(Bone_Index + " " + _with9.Translation.X.ToString("N", Info) + " " + _with9.Translation.Y.ToString("N", Info) + " " + _with9.Translation.Z.ToString("N", Info) + " " + _with9.Rotation.X.ToString("N", Info) + " " + _with9.Rotation.Y.ToString("N", Info) + " " + _with9.Rotation.Z.ToString("N", Info));
				Bone_Index += 1;
			}
			Out.AppendLine("end");
			Out.AppendLine("triangles");
			int Temp_Count = 0;
			foreach (VertexList Model in Model_Object) {
				var _with10 = Model;
				foreach (int Index in _with10.Index) {
					if (Temp_Count == 0)
						Out.AppendLine(Model_Texture_Index[_with10.Texture_ID] + ".png");
					OhanaVertex CurrVert = default(OhanaVertex);
					if (Index < _with10.Vertice.Length)
						CurrVert = _with10.Vertice[Index];

					string Bone_Info = null;
					int Links = 0;
					if (CurrVert.Weight_1 > 0) {
						Links = 1;
						Bone_Info = " " + CurrVert.Bone_1 + " " + CurrVert.Weight_1.ToString(Info);
					}
					if (CurrVert.Weight_2 > 0) {
						Links += 1;
						Bone_Info += " " + CurrVert.Bone_2 + " " + CurrVert.Weight_2.ToString(Info);
					}
					if (CurrVert.Weight_3 > 0) {
						Links += 1;
						Bone_Info += " " + CurrVert.Bone_3 + " " + CurrVert.Weight_3.ToString(Info);
					}
					if (CurrVert.Weight_4 > 0) {
						Links += 1;
						Bone_Info += " " + CurrVert.Bone_4 + " " + CurrVert.Weight_4.ToString(Info);
					}
					Bone_Info = Links + Bone_Info;

					Out.AppendLine(Model_Bone.Length + " " + CurrVert.X.ToString("N", Info) + " " + CurrVert.Y.ToString("N", Info) + " " + CurrVert.Z.ToString("N", Info) + " " + CurrVert.NX.ToString("N", Info) + " " + CurrVert.NY.ToString("N", Info) + " " + CurrVert.NZ.ToString("N", Info) + " " + CurrVert.U.ToString("N", Info) + " " + CurrVert.V.ToString("N", Info) + " " + Bone_Info);
					if (Temp_Count < 2)
						Temp_Count += 1;
					else
						Temp_Count = 0;
				}
			}
			Out.AppendLine("end");

			File.WriteAllText(File_Name, Out.ToString());
		}
		#endregion

		#region "Textures"

		#region "Load"
		public void Load_Textures(string File_Name, bool Create_DX_Texture = true)
		{
			BCH_Version Version = default(BCH_Version);

			byte[] Data = File.ReadAllBytes(File_Name);
			Current_Texture = File_Name;
			if (Temp_Texture_File != null)
				File.Delete(Temp_Texture_File);
			Temp_Texture_File = Path.GetTempFileName();
			File.WriteAllBytes(Temp_Texture_File, Data);

			string CLIM_Magic = Common.ReadMagic(Data, Data.Length - 0x28, 4);
			string CGFX_Magic = Common.ReadMagic(Data, 0, 4);
			if (CLIM_Magic == "CLIM") {
				Model_Texture = new List<OhanaTexture>();

				int Header_Offset = Data.Length - 0x14;
				int Index = 0;
				while (Header_Offset > 0) {
					if (Common.ReadMagic(Data, Header_Offset, 4) != "imag")
						break; // TODO: might not be correct. Was : Exit While

					int Width = Common.Read16(Data, Header_Offset + 8);
					Width = Convert.ToInt32(Math.Pow(2, Math.Ceiling(Math.Log(Width) / Math.Log(2))));
					//Arredonda para o mais próximo 2^n
					int Height = Common.Read16(Data, Header_Offset + 10);
					Height = Convert.ToInt32(Math.Pow(2, Math.Ceiling(Math.Log(Height) / Math.Log(2))));
					//Arredonda para o mais próximo 2^n
					int Format = Common.Read32(Data, Header_Offset + 12);
					int Length = Common.Read32(Data, Header_Offset + 16);
					int Offset = Header_Offset - 0x14 - Length;

					int Actual_Format = 0;
					switch (Format) {
						case 0:
							Actual_Format = 7;
							break;
						case 1:
							Actual_Format = 8;
							break;
						case 2:
							Actual_Format = 9;
							break;
						case 3:
							Actual_Format = 5;
							break;
						case 4:
							Actual_Format = 6;
							break;
						case 5:
							Actual_Format = 3;
							break;
						case 6:
							Actual_Format = 1;
							break;
						case 7:
							Actual_Format = 2;
							break;
						case 8:
							Actual_Format = 4;
							break;
						case 9:
							Actual_Format = 0;
							break;
						case 10:
							Actual_Format = 12;
							break;
						case 11:
							Actual_Format = 13;
							break;
						case 12:
							Actual_Format = 10;
							break;
					}

					byte[] Out = Convert_Texture(Data, Offset, Actual_Format, Width, Height);

					OhanaTexture MyTex = new OhanaTexture();
					Bitmap Img = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					BitmapData ImgData = Img.LockBits(new Rectangle(0, 0, Img.Width, Img.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
					Marshal.Copy(Out, 0, ImgData.Scan0, Out.Length);
					Img.UnlockBits(ImgData);
					MyTex.Image = Img;
					MyTex.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);

					Texture Texture = null;
					if (Create_DX_Texture) {
						Texture = new Texture(Device, Width, Height, 1, Usage.None, Microsoft.DirectX.Direct3D.Format.A8R8G8B8, Pool.Managed);
						GraphicsStream pData = Texture.LockRectangle(0, LockFlags.None);
						pData.Write(Out, 0, Out.Length);
						Texture.UnlockRectangle(0);
					}

					var _with11 = MyTex;
					if (Create_DX_Texture)
						_with11.Texture = Texture;

					_with11.Name = "bclim_" + Index;
					_with11.Has_Alpha = Check_Alpha(Out);
					_with11.Offset = Offset;
					_with11.Format = Actual_Format;
					Model_Texture.Add(MyTex);

					Header_Offset -= (Length + 0x80);
					Index += 1;
				}
			} else if (CGFX_Magic == "CGFX") {
				Model_Texture = new List<OhanaTexture>();

				int DICT_Texture_Block = 0x28 + Common.Read32(Data, 0x28);
				var Entries = Common.Read32(Data, DICT_Texture_Block + 8);
				int Base_Offset = DICT_Texture_Block + 0x1c;
				for (int Offset = Base_Offset; Offset <= Base_Offset + (Entries * 0x10) - 1; Offset += 0x10) {
					int Name_Offset = Offset + 8 + Common.Read32(Data, Offset + 8);
					int TXOB_Offset = Offset + 0xc + Common.Read32(Data, Offset + 0xc);

					string Texture_Name = null;
					do {
						int Value = Data[Name_Offset];
						Name_Offset += 1;
						if (Value != 0)
							Texture_Name += Strings.Chr(Value);
						else
							break; // TODO: might not be correct. Was : Exit Do
					} while (true);

					int Height = Common.Read32(Data, TXOB_Offset + 0x18);
					int Width = Common.Read32(Data, TXOB_Offset + 0x1c);
					int Format = Common.Read32(Data, TXOB_Offset + 0x34);
					int Length = Common.Read32(Data, TXOB_Offset + 0x44);
					int Data_Offset = TXOB_Offset + 0x48 + Common.Read32(Data, TXOB_Offset + 0x48);

					byte[] Out = Convert_Texture(Data, Data_Offset, Format, Width, Height);

					OhanaTexture MyTex = new OhanaTexture();
					Bitmap Img = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					BitmapData ImgData = Img.LockBits(new Rectangle(0, 0, Img.Width, Img.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
					Marshal.Copy(Out, 0, ImgData.Scan0, Out.Length);
					Img.UnlockBits(ImgData);
					MyTex.Image = Img;
					MyTex.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);

					Texture Texture = null;
					if (Create_DX_Texture) {
						Texture = new Texture(Device, Width, Height, 1, Usage.None, Microsoft.DirectX.Direct3D.Format.A8R8G8B8, Pool.Managed);
						GraphicsStream pData = Texture.LockRectangle(0, LockFlags.None);
						pData.Write(Out, 0, Out.Length);
						Texture.UnlockRectangle(0);
					}

					var _with13 = MyTex;
					if (Create_DX_Texture)
						_with13.Texture = Texture;

					_with13.Name = Texture_Name;
					_with13.Has_Alpha = Check_Alpha(Out);
					_with13.Offset = Data_Offset;
					_with13.Format = Format;
					Model_Texture.Add(MyTex);
				}
			} else {
				string File_Magic = null;
				for (int i = 0; i <= 1; i++) {
					File_Magic += Strings.Chr(Data[i]);
				}
				int BCH_Table_Offset = File_Magic == "PT" ? 4 : 8;
				int BCH_Offset = Common.Read32(Data, BCH_Table_Offset);

				int Header_Offset = Common.Read32(Data, BCH_Offset + 8);
				if (Header_Offset == 0x44)
					Version = BCH_Version.ORAS;
				else
					Version = BCH_Version.XY;

				int Texture_Description_Length = 0;
				int Desc_Texture_Pointer = 0;
				int Desc_Texture_Format = 0;
				if (Version == BCH_Version.XY) {
					Texture_Description_Length = 0x20;
					Desc_Texture_Pointer = 8;
					Desc_Texture_Format = 0x10;
				} else if (Version == BCH_Version.ORAS) {
					Texture_Description_Length = 0x30;
					Desc_Texture_Pointer = 0x10;
					Desc_Texture_Format = 0x18;
				}

				Model_Texture = new List<OhanaTexture>();
				while (BCH_Offset > 0) {
					if (BCH_Offset == Data.Length)
						break; // TODO: might not be correct. Was : Exit While
					string Magic = null;
					for (int i = 0; i <= 2; i++) {
						Magic += Strings.Chr(Data[BCH_Offset + i]);
					}

					if (Magic == "BCH") {
						int Texture_Names_Offset = BCH_Offset + Common.Read32(Data, BCH_Offset + 0xc);
						int Description_Offset = BCH_Offset + Common.Read32(Data, BCH_Offset + 0x10);
						int Data_Offset = BCH_Offset + Common.Read32(Data, BCH_Offset + 0x14);
						int Texture_Count = 0;
						if (Version == BCH_Version.XY) {
							Texture_Count = Common.Read32(Data, BCH_Offset + 0x60);
						} else if (Version == BCH_Version.ORAS) {
							Texture_Count = Common.Read32(Data, BCH_Offset + 0x6c);
						}

						string[] Texture_Names = new string[Texture_Count];
						int Tmp = 0;
						for (int i = 0; i <= Texture_Count - 1; i++) {
							string Str = null;
							do {
								int Value = Data[Texture_Names_Offset + Tmp];
								Tmp += 1;
								if (Value != 0)
									Str += Strings.Chr(Value);
								else
									break; // TODO: might not be correct. Was : Exit Do
							} while (true);
							Texture_Names[i] = Str;
						}

						int Index = 0;
						while (Index < Texture_Count) {
							if (Common.Read32(Data, Description_Offset + Desc_Texture_Pointer - Texture_Description_Length) != Common.Read32(Data, Description_Offset + Desc_Texture_Pointer)) {
								int Width = Common.Read16(Data, Description_Offset + 2);
								int Height = Common.Read16(Data, Description_Offset);
								int Format = Data[Description_Offset + Desc_Texture_Format];
								int Texture_Data_Offset = Data_Offset + Common.Read32(Data, Description_Offset + Desc_Texture_Pointer);
								if (Width + Height == 0)
									break; // TODO: might not be correct. Was : Exit While
								byte[] Out = Convert_Texture(Data, Texture_Data_Offset, Format, Width, Height);

								OhanaTexture MyTex = new OhanaTexture();
								Bitmap Img = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
								BitmapData ImgData = Img.LockBits(new Rectangle(0, 0, Img.Width, Img.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
								Marshal.Copy(Out, 0, ImgData.Scan0, Out.Length);
								Img.UnlockBits(ImgData);
								MyTex.Image = Img;
								MyTex.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);

								if (File_Magic == "PT") {
									Out = Mirror_Texture(Out, Width, Height);
									Width *= 2;
								}

								Texture Texture = null;
								if (Create_DX_Texture) {
									Texture = new Texture(Device, Width, Height, 1, Usage.None, Microsoft.DirectX.Direct3D.Format.A8R8G8B8, Pool.Managed);
									GraphicsStream pData = Texture.LockRectangle(0, LockFlags.None);
									pData.Write(Out, 0, Out.Length);
									Texture.UnlockRectangle(0);
								}

								var _with12 = MyTex;
								if (Create_DX_Texture)
									_with12.Texture = Texture;
								_with12.Name = Texture_Names[Index];
								_with12.Has_Alpha = Check_Alpha(Out);
								_with12.Offset = Texture_Data_Offset;
								_with12.Format = Format;
								Model_Texture.Add(MyTex);

								Index += 1;
							}

							Description_Offset += Texture_Description_Length;
						}
					}

					if (File_Magic == "PT") {
						if (BCH_Table_Offset < 8)
							BCH_Table_Offset += 4;
						else
							break; // TODO: might not be correct. Was : Exit While
					} else {
						BCH_Table_Offset += 0x10;
					}

					BCH_Offset = Common.Read32(Data, BCH_Table_Offset);
				}
			}
		}
		private void Load_BCH_Textures(byte[] Data, int Count, int BCH_Offset, int Header_Offset, int Data_Offset, int Description_Offset, int Texture_Names_Offset, int BCH_Texture_Table, BCH_Version Version)
		{
			Model_Texture = new List<OhanaTexture>();
			for (var Index = 0; Index <= Count - 1; Index++) {
				int Name_Offset = Texture_Names_Offset + Common.Read32(Data, (Header_Offset + Common.Read32(Data, BCH_Texture_Table + (Index * 4))) + 0x1c);
				string Texture_Name = null;
				do {
					int Value = Data[Name_Offset];
					Name_Offset += 1;
					if (Value != 0)
						Texture_Name += Strings.Chr(Value);
					else
						break; // TODO: might not be correct. Was : Exit Do
				} while (true);

				int Texture_Description = Description_Offset + Common.Read32(Data, Header_Offset + Common.Read32(Data, BCH_Texture_Table + (Index * 4)));
				int Height = Common.Read16(Data, Texture_Description);
				int Width = Common.Read16(Data, Texture_Description + 2);
				int Texture_Offset = Data_Offset + Common.Read32(Data, Texture_Description + Version == BCH_Version.XY ? 8 : 0x10);
				int Texture_Format = Common.Read32(Data, Texture_Description + Version == BCH_Version.XY ? 0x10 : 0x18);
				byte[] Out = Convert_Texture(Data, Texture_Offset, Texture_Format, Width, Height);

				OhanaTexture MyTex = new OhanaTexture();
				Bitmap Img = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				BitmapData ImgData = Img.LockBits(new Rectangle(0, 0, Img.Width, Img.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
				Marshal.Copy(Out, 0, ImgData.Scan0, Out.Length);
				Img.UnlockBits(ImgData);
				MyTex.Image = Img;
				MyTex.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);

				Texture Texture = null;
				Texture = new Texture(Device, Width, Height, 1, Usage.None, Microsoft.DirectX.Direct3D.Format.A8R8G8B8, Pool.Managed);
				GraphicsStream pData = Texture.LockRectangle(0, LockFlags.None);
				pData.Write(Out, 0, Out.Length);
				Texture.UnlockRectangle(0);

				var _with14 = MyTex;
				_with14.Texture = Texture;
				_with14.Name = Texture_Name;
				_with14.Has_Alpha = Check_Alpha(Out);
				_with14.Offset = Texture_Offset + BCH_Offset;
				_with14.Format = Texture_Format;
				Model_Texture.Add(MyTex);
			}
		}

		private byte[] Convert_Texture(byte[] Data, int Texture_Data_Offset, int Format, int Width, int Height, bool Linear = false)
		{
			byte[] Out = new byte[(Width * Height * 4)];
			int Offset = Texture_Data_Offset;
			bool Low_High_Toggle = false;

			//ETC1 (iPACKMAN)
			if (Format == 12 | Format == 13) {
				byte[] Temp_Buffer = new byte[((Width * Height) / 2)];
				byte[] Alphas = new byte[Temp_Buffer.Length];
				if (Format == 12) {
					Buffer.BlockCopy(Data, Offset, Temp_Buffer, 0, Temp_Buffer.Length);
					for (int j = 0; j <= Alphas.Length - 1; j++) {
						Alphas[j] = 0xff;
					}
				} else {
					int k = 0;
					for (int j = 0; j <= (Width * Height) - 1; j++) {
						Buffer.BlockCopy(Data, Offset + j + 8, Temp_Buffer, k, 8);
						Buffer.BlockCopy(Data, Offset + j, Alphas, k, 8);
						k += 8;
						j += 15;
					}
				}
				byte[] Temp_2 = ETC1_Decompress(Temp_Buffer, Alphas, Width, Height);

				//Os tiles com compressão ETC1 no 3DS estão embaralhados
				int[] Tile_Scramble = Get_ETC1_Scramble(Width, Height);

				int i = 0;
				for (int Tile_Y = 0; Tile_Y <= (Height / 4) - 1; Tile_Y++) {
					for (int Tile_X = 0; Tile_X <= (Width / 4) - 1; Tile_X++) {
						int TX = Tile_Scramble[i] % (Width / 4);
						int TY = (Tile_Scramble[i] - TX) / (Width / 4);
						for (int Y = 0; Y <= 3; Y++) {
							for (int X = 0; X <= 3; X++) {
								int Out_Offset = ((Tile_X * 4) + X + (((Height - 1) - ((Tile_Y * 4) + Y)) * Width)) * 4;
								int Image_Offset = ((TX * 4) + X + (((TY * 4) + Y) * Width)) * 4;

								Out[Out_Offset] = Temp_2[Image_Offset];
								Out[Out_Offset + 1] = Temp_2[Image_Offset + 1];
								Out[Out_Offset + 2] = Temp_2[Image_Offset + 2];
								Out[Out_Offset + 3] = Temp_2[Image_Offset + 3];
							}
						}
						i += 1;
					}
				}
			} else {
				for (int Tile_Y = 0; Tile_Y <= (Height / 8) - 1; Tile_Y++) {
					for (int Tile_X = 0; Tile_X <= (Width / 8) - 1; Tile_X++) {
						for (int i = 0; i <= 63; i++) {
							int X = Tile_Order[i] % 8;
							int Y = (Tile_Order[i] - X) / 8;
							int Out_Offset = ((Tile_X * 8) + X + (((Height - 1) - ((Tile_Y * 8) + Y)) * Width)) * 4;
							switch (Format) {
								case 0:
                                    {
                                        //R8G8B8A8
                                        Buffer.BlockCopy(Data, Offset + 1, Out, Out_Offset, 3);
                                        Out[Out_Offset + 3] = Data[Offset];
                                        Offset += 4;
                                        break;
                                    }
								case 1:
                                    {
                                        //R8G8B8 (sem transparência)
                                        Buffer.BlockCopy(Data, Offset, Out, Out_Offset, 3);
                                        Out[Out_Offset + 3] = 0xff;
                                        Offset += 3;
                                        break;
                                    }
								case 2:
                                    {
                                        //R5G5B5A1
                                        int Pixel_Data = Common.Read16(Data, Offset);
                                        Out[Out_Offset + 2] = Convert.ToByte(((Pixel_Data >> 11) & 0x1f) * 8);
                                        Out[Out_Offset + 1] = Convert.ToByte(((Pixel_Data >> 6) & 0x1f) * 8);
                                        Out[Out_Offset] = Convert.ToByte(((Pixel_Data >> 1) & 0x1f) * 8);
                                        Out[Out_Offset + 3] = Convert.ToByte((Pixel_Data & 1) * 0xff);
                                        Offset += 2;
                                        break;
                                    }
								case 3:
                                    {
                                        //R5G6B5
                                        int Pixel_Data = Common.Read16(Data, Offset);
                                        Out[Out_Offset + 2] = Convert.ToByte(((Pixel_Data >> 11) & 0x1f) * 8);
                                        Out[Out_Offset + 1] = Convert.ToByte(((Pixel_Data >> 5) & 0x3f) * 4);
                                        Out[Out_Offset] = Convert.ToByte(((Pixel_Data) & 0x1f) * 8);
                                        Out[Out_Offset + 3] = 0xff;
                                        Offset += 2;
                                        break;
                                    }
								case 4:
                                    {
                                        //R4G4B4A4
                                        int Pixel_Data = Common.Read16(Data, Offset);
                                        Out[Out_Offset + 2] = Convert.ToByte(((Pixel_Data >> 12) & 0xf) * 0x11);
                                        Out[Out_Offset + 1] = Convert.ToByte(((Pixel_Data >> 8) & 0xf) * 0x11);
                                        Out[Out_Offset] = Convert.ToByte(((Pixel_Data >> 4) & 0xf) * 0x11);
                                        Out[Out_Offset + 3] = Convert.ToByte((Pixel_Data & 0xf) * 0x11);
                                        Offset += 2;
                                        break;
                                    }
								case 5:
                                    {
                                        //L8A8
                                        byte Pixel_Data = Data[Offset + 1];
                                        Out[Out_Offset] = Pixel_Data;
                                        Out[Out_Offset + 1] = Pixel_Data;
                                        Out[Out_Offset + 2] = Pixel_Data;
                                        Out[Out_Offset + 3] = Data[Offset];
                                        Offset += 2;
                                        break;
                                    }
								case 6:
                                    {
                                        //HILO8
                                        break;
                                    }
								case 7:
                                    {
                                        //L8
                                        Out[Out_Offset] = Data[Offset];
                                        Out[Out_Offset + 1] = Data[Offset];
                                        Out[Out_Offset + 2] = Data[Offset];
                                        Out[Out_Offset + 3] = 0xff;
                                        Offset += 1;
                                        break;
                                    }
								case 8:
                                    {
                                        //A8
                                        Out[Out_Offset] = 0xff;
                                        Out[Out_Offset + 1] = 0xff;
                                        Out[Out_Offset + 2] = 0xff;
                                        Out[Out_Offset + 3] = Data[Offset];
                                        Offset += 1;
                                        break;
                                    }
								case 9:
                                    {
                                        //L4A4
                                        int Luma = Data[Offset] & 0xf;
                                        int Alpha = (Data[Offset] & 0xf0) >> 4;
                                        Out[Out_Offset] = Convert.ToByte((Luma << 4) + Luma);
                                        Out[Out_Offset + 1] = Convert.ToByte((Luma << 4) + Luma);
                                        Out[Out_Offset + 2] = Convert.ToByte((Luma << 4) + Luma);
                                        Out[Out_Offset + 3] = Convert.ToByte((Alpha << 4) + Alpha);
                                        break;
                                    }
								case 10:
                                    {
                                        //L4
                                        int Pixel_Data = 0;
                                        if (Low_High_Toggle)
                                        {
                                            Pixel_Data = Data[Offset] & 0xf;
                                            Offset += 1;
                                        }
                                        else
                                        {
                                            Pixel_Data = (Data[Offset] & 0xf0) >> 4;
                                        }
                                        Out[Out_Offset] = Convert.ToByte(Pixel_Data * 0x11);
                                        Out[Out_Offset + 1] = Convert.ToByte(Pixel_Data * 0x11);
                                        Out[Out_Offset + 2] = Convert.ToByte(Pixel_Data * 0x11);
                                        Out[Out_Offset + 3] = 0xff;
                                        Low_High_Toggle = !Low_High_Toggle;
                                        break;
                                    }
							}
						}
					}
				}
			}

			return Out;
		}
		#endregion

		#region "Texture inserter/ETC1 Compressor"
		public void Insert_Texture(string File_Name, int LstIndex, bool Show_Warning = true)
		{
			int Offset = Model_Texture[LstIndex].Offset;
			int Format = Model_Texture[LstIndex].Format;

			Bitmap Img = new Bitmap(File_Name);
			if ((Img.Width != Model_Texture[LstIndex].Image.Width) | (Img.Height != Model_Texture[LstIndex].Image.Height)) {
				if (Show_Warning)
					MessageBox.Show("Images need to have the same resolution!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			BitmapData ImgData = Img.LockBits(new Rectangle(0, 0, Img.Width, Img.Height), ImageLockMode.ReadOnly, Img.PixelFormat);
			byte[] Data = new byte[(ImgData.Height * ImgData.Stride)];
			Marshal.Copy(ImgData.Scan0, Data, 0, Data.Length);
			Img.UnlockBits(ImgData);

			int BPP = 24;
			if (Img.PixelFormat == PixelFormat.Format32bppArgb)
				BPP = 32;

			byte[] Out_Data = null;

			switch (Format) {
				case 12:
				case 13:
                    {
                        //Os tiles com compressão ETC1 no 3DS estão embaralhados
                        byte[] Out = new byte[(Img.Width * Img.Height * 4)];
                        int[] Tile_Scramble = Get_ETC1_Scramble(Img.Width, Img.Height);

                        int i = 0;
                        for (int Tile_Y = 0; Tile_Y <= (Img.Height / 4) - 1; Tile_Y++)
                        {
                            for (int Tile_X = 0; Tile_X <= (Img.Width / 4) - 1; Tile_X++)
                            {
                                int TX = Tile_Scramble[i] % (Img.Width / 4);
                                int TY = (Tile_Scramble[i] - TX) / (Img.Width / 4);
                                for (int Y = 0; Y <= 3; Y++)
                                {
                                    for (int X = 0; X <= 3; X++)
                                    {
                                        int Out_Offset = ((TX * 4) + X + ((((TY * 4) + Y)) * Img.Width)) * 4;
                                        int Image_Offset = ((Tile_X * 4) + X + (((Tile_Y * 4) + Y) * Img.Width)) * (BPP / 8);

                                        Out[Out_Offset] = Data[Image_Offset + 2];
                                        Out[Out_Offset + 1] = Data[Image_Offset + 1];
                                        Out[Out_Offset + 2] = Data[Image_Offset];
                                        if (BPP == 32)
                                            Out[Out_Offset + 3] = Data[Image_Offset + 3];
                                        else
                                            Out[Out_Offset + 3] = 0xff;
                                    }
                                }
                                i += 1;
                            }
                        }


                        Out_Data = new byte[((Img.Width * Img.Height) / (Format == 12 ? 2 : 1))];
                        int Out_Data_Offset = 0;

                        for (int Tile_Y = 0; Tile_Y <= (Img.Height / 4) - 1; Tile_Y++)
                        {
                            for (int Tile_X = 0; Tile_X <= (Img.Width / 4) - 1; Tile_X++)
                            {
                                bool Flip = false;
                                bool Difference = false;
                                int Block_Top = 0;
                                int Block_Bottom = 0;

                                //Teste do Difference Bit
                                int Diff_Match_V = 0;
                                int Diff_Match_H = 0;
                                for (int Y = 0; Y <= 3; Y++)
                                {
                                    for (int X = 0; X <= 1; X++)
                                    {
                                        int Image_Offset_1 = ((Tile_X * 4) + X + (((Tile_Y * 4) + Y) * Img.Width)) * 4;
                                        int Image_Offset_2 = ((Tile_X * 4) + (2 + X) + (((Tile_Y * 4) + Y) * Img.Width)) * 4;

                                        byte Bits_R1 = Convert.ToByte(Out[Image_Offset_1] & 0xf8);
                                        byte Bits_G1 = Convert.ToByte(Out[Image_Offset_1 + 1] & 0xf8);
                                        byte Bits_B1 = Convert.ToByte(Out[Image_Offset_1 + 2] & 0xf8);

                                        byte Bits_R2 = Convert.ToByte(Out[Image_Offset_2] & 0xf8);
                                        byte Bits_G2 = Convert.ToByte(Out[Image_Offset_2 + 1] & 0xf8);
                                        byte Bits_B2 = Convert.ToByte(Out[Image_Offset_2 + 2] & 0xf8);

                                        if ((Bits_R1 == Bits_R2) & (Bits_G1 == Bits_G2) & (Bits_B1 == Bits_B2))
                                            Diff_Match_V += 1;
                                    }
                                }
                                for (int Y = 0; Y <= 1; Y++)
                                {
                                    for (int X = 0; X <= 3; X++)
                                    {
                                        int Image_Offset_1 = ((Tile_X * 4) + X + (((Tile_Y * 4) + Y) * Img.Width)) * 4;
                                        int Image_Offset_2 = ((Tile_X * 4) + X + (((Tile_Y * 4) + (2 + Y)) * Img.Width)) * 4;

                                        byte Bits_R1 = Convert.ToByte(Out[Image_Offset_1] & 0xf8);
                                        byte Bits_G1 = Convert.ToByte(Out[Image_Offset_1 + 1] & 0xf8);
                                        byte Bits_B1 = Convert.ToByte(Out[Image_Offset_1 + 2] & 0xf8);

                                        byte Bits_R2 = Convert.ToByte(Out[Image_Offset_2] & 0xf8);
                                        byte Bits_G2 = Convert.ToByte(Out[Image_Offset_2 + 1] & 0xf8);
                                        byte Bits_B2 = Convert.ToByte(Out[Image_Offset_2 + 2] & 0xf8);

                                        if ((Bits_R1 == Bits_R2) & (Bits_G1 == Bits_G2) & (Bits_B1 == Bits_B2))
                                            Diff_Match_H += 1;
                                    }
                                }
                                //Difference + Flip
                                if (Diff_Match_H == 8)
                                {
                                    Difference = true;
                                    Flip = true;
                                    //Difference
                                }
                                else if (Diff_Match_V == 8)
                                {
                                    Difference = true;
                                    //Individual
                                }
                                else
                                {
                                    int Test_R1 = 0;
                                    int Test_G1 = 0;
                                    int Test_B1 = 0;
                                    int Test_R2 = 0;
                                    int Test_G2 = 0;
                                    int Test_B2 = 0;
                                    for (int Y = 0; Y <= 1; Y++)
                                    {
                                        for (int X = 0; X <= 1; X++)
                                        {
                                            int Image_Offset_1 = ((Tile_X * 4) + X + (((Tile_Y * 4) + Y) * Img.Width)) * 4;
                                            int Image_Offset_2 = ((Tile_X * 4) + (2 + X) + (((Tile_Y * 4) + (2 + Y)) * Img.Width)) * 4;

                                            Test_R1 += Out[Image_Offset_1];
                                            Test_G1 += Out[Image_Offset_1 + 1];
                                            Test_B1 += Out[Image_Offset_1 + 2];

                                            Test_R2 += Out[Image_Offset_2];
                                            Test_G2 += Out[Image_Offset_2 + 1];
                                            Test_B2 += Out[Image_Offset_2 + 2];
                                        }
                                    }

                                    Test_R1 /= 8;
                                    Test_G1 /= 8;
                                    Test_B1 /= 8;

                                    Test_R2 /= 8;
                                    Test_G2 /= 8;
                                    Test_B2 /= 8;

                                    int Test_Luma_1 = Convert.ToInt32(0.299f * Test_R1 + 0.587f * Test_G1 + 0.114f * Test_B1);
                                    int Test_Luma_2 = Convert.ToInt32(0.299f * Test_R2 + 0.587f * Test_G2 + 0.114f * Test_B2);
                                    int Test_Flip_Diff = Math.Abs(Test_Luma_1 - Test_Luma_2);
                                    if (Test_Flip_Diff > 48)
                                        Flip = true;
                                }

                                int Avg_R1 = 0;
                                int Avg_G1 = 0;
                                int Avg_B1 = 0;
                                int Avg_R2 = 0;
                                int Avg_G2 = 0;
                                int Avg_B2 = 0;

                                //Primeiro, cálcula a média de cores de cada bloco
                                if (Flip)
                                {
                                    for (int Y = 0; Y <= 1; Y++)
                                    {
                                        for (int X = 0; X <= 3; X++)
                                        {
                                            int Image_Offset_1 = ((Tile_X * 4) + X + (((Tile_Y * 4) + Y) * Img.Width)) * 4;
                                            int Image_Offset_2 = ((Tile_X * 4) + X + (((Tile_Y * 4) + (2 + Y)) * Img.Width)) * 4;

                                            Avg_R1 += Out[Image_Offset_1];
                                            Avg_G1 += Out[Image_Offset_1 + 1];
                                            Avg_B1 += Out[Image_Offset_1 + 2];

                                            Avg_R2 += Out[Image_Offset_2];
                                            Avg_G2 += Out[Image_Offset_2 + 1];
                                            Avg_B2 += Out[Image_Offset_2 + 2];
                                        }
                                    }
                                }
                                else
                                {
                                    for (int Y = 0; Y <= 3; Y++)
                                    {
                                        for (int X = 0; X <= 1; X++)
                                        {
                                            int Image_Offset_1 = ((Tile_X * 4) + X + (((Tile_Y * 4) + Y) * Img.Width)) * 4;
                                            int Image_Offset_2 = ((Tile_X * 4) + (2 + X) + (((Tile_Y * 4) + Y) * Img.Width)) * 4;

                                            Avg_R1 += Out[Image_Offset_1];
                                            Avg_G1 += Out[Image_Offset_1 + 1];
                                            Avg_B1 += Out[Image_Offset_1 + 2];

                                            Avg_R2 += Out[Image_Offset_2];
                                            Avg_G2 += Out[Image_Offset_2 + 1];
                                            Avg_B2 += Out[Image_Offset_2 + 2];
                                        }
                                    }
                                }

                                Avg_R1 /= 8;
                                Avg_G1 /= 8;
                                Avg_B1 /= 8;

                                Avg_R2 /= 8;
                                Avg_G2 /= 8;
                                Avg_B2 /= 8;

                                if (Difference)
                                {
                                    //+============+
                                    //| Difference |
                                    //+============+
                                    if ((Avg_R1 & 7) > 3) { Avg_R1 = Clip(Avg_R1 + 8); Avg_R2 = Clip(Avg_R2 + 8); }
                                    if ((Avg_G1 & 7) > 3) { Avg_G1 = Clip(Avg_G1 + 8); Avg_G2 = Clip(Avg_G2 + 8); }
                                    if ((Avg_B1 & 7) > 3) { Avg_B1 = Clip(Avg_B1 + 8); Avg_B2 = Clip(Avg_B2 + 8); }

                                    Block_Top = (Avg_R1 & 0xf8) | (((Avg_R2 - Avg_R1) / 8) & 7);
                                    Block_Top = Block_Top | (((Avg_G1 & 0xf8) << 8) | ((((Avg_G2 - Avg_G1) / 8) & 7) << 8));
                                    Block_Top = Block_Top | (((Avg_B1 & 0xf8) << 16) | ((((Avg_B2 - Avg_B1) / 8) & 7) << 16));

                                    //Vamos ter certeza de que os mesmos valores obtidos pelo descompressor serão usados na comparação (modo Difference)
                                    Avg_R1 = Block_Top & 0xf8;
                                    Avg_G1 = (Block_Top & 0xf800) >> 8;
                                    Avg_B1 = (Block_Top & 0xf80000) >> 16;

                                    int R = Signed_Byte(Convert.ToByte(Avg_R1 >> 3)) + (Signed_Byte(Convert.ToByte((Block_Top & 7) << 5)) >> 5);
                                    int G = Signed_Byte(Convert.ToByte(Avg_G1 >> 3)) + (Signed_Byte(Convert.ToByte((Block_Top & 0x700) >> 3)) >> 5);
                                    int B = Signed_Byte(Convert.ToByte(Avg_B1 >> 3)) + (Signed_Byte(Convert.ToByte((Block_Top & 0x70000) >> 11)) >> 5);

                                    Avg_R2 = R;
                                    Avg_G2 = G;
                                    Avg_B2 = B;

                                    Avg_R1 = Avg_R1 + (Avg_R1 >> 5);
                                    Avg_G1 = Avg_G1 + (Avg_G1 >> 5);
                                    Avg_B1 = Avg_B1 + (Avg_B1 >> 5);

                                    Avg_R2 = (Avg_R2 << 3) + (Avg_R2 >> 2);
                                    Avg_G2 = (Avg_G2 << 3) + (Avg_G2 >> 2);
                                    Avg_B2 = (Avg_B2 << 3) + (Avg_B2 >> 2);
                                }
                                else
                                {
                                    //+============+
                                    //| Individual |
                                    //+============+
                                    if ((Avg_R1 & 0xf) > 7)
                                        Avg_R1 = Clip(Avg_R1 + 0x10);
                                    if ((Avg_G1 & 0xf) > 7)
                                        Avg_G1 = Clip(Avg_G1 + 0x10);
                                    if ((Avg_B1 & 0xf) > 7)
                                        Avg_B1 = Clip(Avg_B1 + 0x10);
                                    if ((Avg_R2 & 0xf) > 7)
                                        Avg_R2 = Clip(Avg_R2 + 0x10);
                                    if ((Avg_G2 & 0xf) > 7)
                                        Avg_G2 = Clip(Avg_G2 + 0x10);
                                    if ((Avg_B2 & 0xf) > 7)
                                        Avg_B2 = Clip(Avg_B2 + 0x10);

                                    Block_Top = ((Avg_R2 & 0xf0) >> 4) | (Avg_R1 & 0xf0);
                                    Block_Top = Block_Top | (((Avg_G2 & 0xf0) << 4) | ((Avg_G1 & 0xf0) << 8));
                                    Block_Top = Block_Top | (((Avg_B2 & 0xf0) << 12) | ((Avg_B1 & 0xf0) << 16));

                                    //Vamos ter certeza de que os mesmos valores obtidos pelo descompressor serão usados na comparação (modo Individual)
                                    Avg_R1 = (Avg_R1 & 0xf0) + ((Avg_R1 & 0xf0) >> 4);
                                    Avg_G1 = (Avg_G1 & 0xf0) + ((Avg_G1 & 0xf0) >> 4);
                                    Avg_B1 = (Avg_B1 & 0xf0) + ((Avg_B1 & 0xf0) >> 4);

                                    Avg_R2 = (Avg_R2 & 0xf0) + ((Avg_R2 & 0xf0) >> 4);
                                    Avg_G2 = (Avg_G2 & 0xf0) + ((Avg_G2 & 0xf0) >> 4);
                                    Avg_B2 = (Avg_B2 & 0xf0) + ((Avg_B2 & 0xf0) >> 4);
                                }

                                if (Flip)
                                    Block_Top = Block_Top | 0x1000000;
                                if (Difference)
                                    Block_Top = Block_Top | 0x2000000;

                                //Seleciona a melhor tabela para ser usada nos blocos
                                int Mod_Table_1 = 0;
                                int[] Min_Diff_1 = new int[8];
                                for (int a = 0; a <= 7; a++)
                                {
                                    Min_Diff_1[a] = 0;
                                }
                                for (int Y = 0; Y <= (Flip ? 1 : 3); Y++)
                                {
                                    for (int X = 0; X <= (Flip ? 3 : 1); X++)
                                    {
                                        int Image_Offset = ((Tile_X * 4) + X + (((Tile_Y * 4) + Y) * Img.Width)) * 4;
                                        int Luma = Convert.ToInt32(0.299f * Out[Image_Offset] + 0.587f * Out[Image_Offset + 1] + 0.114f * Out[Image_Offset + 2]);

                                        for (int a = 0; a <= 7; a++)
                                        {
                                            int Optimal_Diff = 255 * 4;
                                            for (int b = 0; b <= 3; b++)
                                            {
                                                int CR = Clip(Avg_R1 + Modulation_Table[a, b]);
                                                int CG = Clip(Avg_G1 + Modulation_Table[a, b]);
                                                int CB = Clip(Avg_B1 + Modulation_Table[a, b]);

                                                int Test_Luma = Convert.ToInt32(0.299f * CR + 0.587f * CG + 0.114f * CB);
                                                int Diff = Math.Abs(Luma - Test_Luma);
                                                if (Diff < Optimal_Diff)
                                                    Optimal_Diff = Diff;
                                            }
                                            Min_Diff_1[a] += Optimal_Diff;
                                        }
                                    }
                                }

                                int Temp_1 = 255 * 8;
                                for (int a = 0; a <= 7; a++)
                                {
                                    if (Min_Diff_1[a] < Temp_1)
                                    {
                                        Temp_1 = Min_Diff_1[a];
                                        Mod_Table_1 = a;
                                    }
                                }

                                int Mod_Table_2 = 0;
                                int[] Min_Diff_2 = new int[8];
                                for (int a = 0; a <= 7; a++)
                                {
                                    Min_Diff_2[a] = 0;
                                }
                                for (int Y = Flip ? 2 : 0; Y <= 3; Y++)
                                {
                                    for (int X = Flip ? 0 : 2; X <= 3; X++)
                                    {
                                        int Image_Offset = ((Tile_X * 4) + X + (((Tile_Y * 4) + Y) * Img.Width)) * 4;
                                        int Luma = Convert.ToInt32(0.299f * Out[Image_Offset] + 0.587f * Out[Image_Offset + 1] + 0.114f * Out[Image_Offset + 2]);

                                        for (int a = 0; a <= 7; a++)
                                        {
                                            int Optimal_Diff = 255 * 4;
                                            for (int b = 0; b <= 3; b++)
                                            {
                                                int CR = Clip(Avg_R2 + Modulation_Table[a, b]);
                                                int CG = Clip(Avg_G2 + Modulation_Table[a, b]);
                                                int CB = Clip(Avg_B2 + Modulation_Table[a, b]);

                                                int Test_Luma = Convert.ToInt32(0.299f * CR + 0.587f * CG + 0.114f * CB);
                                                int Diff = Math.Abs(Luma - Test_Luma);
                                                if (Diff < Optimal_Diff)
                                                    Optimal_Diff = Diff;
                                            }
                                            Min_Diff_2[a] += Optimal_Diff;
                                        }
                                    }
                                }

                                int Temp_2 = 255 * 8;
                                for (int a = 0; a <= 7; a++)
                                {
                                    if (Min_Diff_2[a] < Temp_2)
                                    {
                                        Temp_2 = Min_Diff_2[a];
                                        Mod_Table_2 = a;
                                    }
                                }

                                Block_Top = Block_Top | (Mod_Table_1 << 29);
                                Block_Top = Block_Top | (Mod_Table_2 << 26);

                                //Seleciona o melhor valor da tabela que mais se aproxima com a cor original
                                for (int Y = 0; Y <= (Flip ? 1 : 3); Y++)
                                {
                                    for (int X = 0; X <= (Flip ? 3 : 1); X++)
                                    {
                                        int Image_Offset = ((Tile_X * 4) + X + (((Tile_Y * 4) + Y) * Img.Width)) * 4;
                                        int Luma = Convert.ToInt32(0.299f * Out[Image_Offset] + 0.587f * Out[Image_Offset + 1] + 0.114f * Out[Image_Offset + 2]);

                                        int Col_Diff = 255;
                                        int Pix_Table_Index = 0;
                                        for (int b = 0; b <= 3; b++)
                                        {
                                            int CR = Clip(Avg_R1 + Modulation_Table[Mod_Table_1, b]);
                                            int CG = Clip(Avg_G1 + Modulation_Table[Mod_Table_1, b]);
                                            int CB = Clip(Avg_B1 + Modulation_Table[Mod_Table_1, b]);

                                            int Test_Luma = Convert.ToInt32(0.299f * CR + 0.587f * CG + 0.114f * CB);
                                            int Diff = Math.Abs(Luma - Test_Luma);
                                            if (Diff < Col_Diff)
                                            {
                                                Col_Diff = Diff;
                                                Pix_Table_Index = b;
                                            }
                                        }

                                        int Index = X * 4 + Y;
                                        if (Index < 8)
                                        {
                                            Block_Bottom = Block_Bottom | (((Pix_Table_Index & 2) >> 1) << (Index + 8));
                                            Block_Bottom = Block_Bottom | ((Pix_Table_Index & 1) << (Index + 24));
                                        }
                                        else
                                        {
                                            Block_Bottom = Block_Bottom | (((Pix_Table_Index & 2) >> 1) << (Index - 8));
                                            Block_Bottom = Block_Bottom | ((Pix_Table_Index & 1) << (Index + 8));
                                        }
                                    }
                                }

                                for (int Y = Flip ? 2 : 0; Y <= 3; Y++)
                                {
                                    for (int X = Flip ? 0 : 2; X <= 3; X++)
                                    {
                                        int Image_Offset = ((Tile_X * 4) + X + (((Tile_Y * 4) + Y) * Img.Width)) * 4;
                                        int Luma = Convert.ToInt32(0.299f * Out[Image_Offset] + 0.587f * Out[Image_Offset + 1] + 0.114f * Out[Image_Offset + 2]);

                                        int Col_Diff = 255;
                                        int Pix_Table_Index = 0;
                                        for (int b = 0; b <= 3; b++)
                                        {
                                            int CR = Clip(Avg_R2 + Modulation_Table[Mod_Table_2, b]);
                                            int CG = Clip(Avg_G2 + Modulation_Table[Mod_Table_2, b]);
                                            int CB = Clip(Avg_B2 + Modulation_Table[Mod_Table_2, b]);

                                            int Test_Luma = Convert.ToInt32(0.299f * CR + 0.587f * CG + 0.114f * CB);
                                            int Diff = Math.Abs(Luma - Test_Luma);
                                            if (Diff < Col_Diff)
                                            {
                                                Col_Diff = Diff;
                                                Pix_Table_Index = b;
                                            }
                                        }

                                        int Index = X * 4 + Y;
                                        if (Index < 8)
                                        {
                                            Block_Bottom = Block_Bottom | (((Pix_Table_Index & 2) >> 1) << (Index + 8));
                                            Block_Bottom = Block_Bottom | ((Pix_Table_Index & 1) << (Index + 24));
                                        }
                                        else
                                        {
                                            Block_Bottom = Block_Bottom | (((Pix_Table_Index & 2) >> 1) << (Index - 8));
                                            Block_Bottom = Block_Bottom | ((Pix_Table_Index & 1) << (Index + 8));
                                        }
                                    }
                                }

                                //Copia dados para a saída
                                byte[] Block = new byte[8];
                                Buffer.BlockCopy(BitConverter.GetBytes(Block_Top), 0, Block, 0, 4);
                                Buffer.BlockCopy(BitConverter.GetBytes(Block_Bottom), 0, Block, 4, 4);
                                byte[] New_Block = new byte[8];
                                for (int j = 0; j <= 7; j++)
                                {
                                    New_Block[7 - j] = Block[j];
                                }
                                if (Format == 13)
                                {
                                    byte[] Alphas = new byte[8];
                                    int Alpha_Offset = 0;
                                    for (int TX = 0; TX <= 3; TX++)
                                    {
                                        for (int TY = 0; TY <= 3; TY += 2)
                                        {
                                            int Img_Offset_1 = (Tile_X * 4 + TX + ((Tile_Y * 4 + TY) * Img.Width)) * 4;
                                            int Img_Offset_2 = (Tile_X * 4 + TX + ((Tile_Y * 4 + TY + 1) * Img.Width)) * 4;

                                            byte Alpha_1 = (byte)(Out[Img_Offset_1 + 3] >> 4);
                                            byte Alpha_2 = (byte)(Out[Img_Offset_2 + 3] >> 4);

                                            Alphas[Alpha_Offset] = (byte)(Alpha_1 | (Alpha_2 << 4));

                                            Alpha_Offset += 1;
                                        }
                                    }

                                    Buffer.BlockCopy(Alphas, 0, Out_Data, Out_Data_Offset, 8);
                                    Buffer.BlockCopy(New_Block, 0, Out_Data, Out_Data_Offset + 8, 8);
                                    Out_Data_Offset += 16;
                                }
                                else if (Format == 12)
                                {
                                    Buffer.BlockCopy(New_Block, 0, Out_Data, Out_Data_Offset, 8);
                                    Out_Data_Offset += 8;
                                }

                                Texture_Insertion_Percentage = Convert.ToSingle((Out_Data_Offset / Out_Data.Length) * 100);
                            }
                        }

                        break;
                    }
				default:
                    {
                        switch (Format)
                        {
                            case 0:
                                Out_Data = new byte[(Img.Width * Img.Height * 4)];
                                break;
                            case 1:
                                Out_Data = new byte[(Img.Width * Img.Height * 3)];
                                break;
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                Out_Data = new byte[(Img.Width * Img.Height * 2)];
                                break;
                            case 7:
                            case 8:
                                Out_Data = new byte[(Img.Width * Img.Height)];
                                break;
                        }
                        int Out_Data_Offset = 0;
                        for (int Tile_Y = 0; Tile_Y <= (Img.Height / 8) - 1; Tile_Y++)
                        {
                            for (int Tile_X = 0; Tile_X <= (Img.Width / 8) - 1; Tile_X++)
                            {
                                for (int i = 0; i <= 63; i++)
                                {
                                    int X = Tile_Order[i] % 8;
                                    int Y = (Tile_Order[i] - X) / 8;
                                    int Img_Offset = ((Tile_X * 8) + X + (((Tile_Y * 8) + Y)) * Img.Width) * (BPP / 8);
                                    switch (Format)
                                    {
                                        case 0:
                                            {
                                                //R8G8B8A8
                                                if (BPP == 32)
                                                    Out_Data[Out_Data_Offset] = Data[Img_Offset + 3];
                                                else
                                                    Out_Data[Out_Data_Offset] = 0xff;
                                                Buffer.BlockCopy(Data, Img_Offset, Out_Data, Out_Data_Offset + 1, 3);
                                                Out_Data_Offset += 4;
                                                break;
                                            }
                                        case 1:
                                            {
                                                //R8G8B8 (sem transparência)
                                                Buffer.BlockCopy(Data, Img_Offset, Out_Data, Out_Data_Offset, 3);
                                                Out_Data_Offset += 3;
                                                break;
                                            }
                                        case 2:
                                            {
                                                //R5G5B5A1
                                                Out_Data[Out_Data_Offset + 1] = Convert.ToByte((Data[Img_Offset + 1] & 0xe0) >> 5);
                                                Out_Data[Out_Data_Offset + 1] += Convert.ToByte(Data[Img_Offset + 2] & 0xf8);
                                                Out_Data[Out_Data_Offset] = Convert.ToByte((Data[Img_Offset] & 0xf8) >> 2);
                                                Out_Data[Out_Data_Offset] += Convert.ToByte((Data[Img_Offset + 1] & 0x18) << 3);
                                                if ((BPP == 32 & Data[Img_Offset + 3] == 0xff) | BPP == 24)
                                                    Out_Data[Out_Data_Offset] += Convert.ToByte(1);
                                                Out_Data_Offset += 2;
                                                break;
                                            }
                                        case 3:
                                            {
                                                //R5G6B5
                                                Out_Data[Out_Data_Offset + 1] = Convert.ToByte((Data[Img_Offset + 1] & 0xe0) >> 5);
                                                Out_Data[Out_Data_Offset + 1] += Convert.ToByte(Data[Img_Offset + 2] & 0xf8);
                                                Out_Data[Out_Data_Offset] = Convert.ToByte(Data[Img_Offset] >> 3);
                                                Out_Data[Out_Data_Offset] += Convert.ToByte((Data[Img_Offset + 1] & 0x1c) << 3);
                                                Out_Data_Offset += 2;
                                                break;
                                            }
                                        case 4:
                                            {
                                                //R4G4B4A4
                                                Out_Data[Out_Data_Offset + 1] = Convert.ToByte((Data[Img_Offset + 1] & 0xf0) >> 4);
                                                Out_Data[Out_Data_Offset + 1] += Convert.ToByte(Data[Img_Offset + 2] & 0xf0);
                                                Out_Data[Out_Data_Offset] = Convert.ToByte(Data[Img_Offset] & 0xf0);
                                                if (BPP == 32)
                                                {
                                                    Out_Data[Out_Data_Offset] += Convert.ToByte((Data[Img_Offset + 3] & 0xf0) >> 4);
                                                }
                                                else
                                                {
                                                    Out_Data[Out_Data_Offset] += Convert.ToByte(0xf);
                                                }
                                                Out_Data_Offset += 2;
                                                break;
                                            }
                                        case 5:
                                            {
                                                //L8A8
                                                byte Luma = Convert.ToByte(0.299f * Data[Img_Offset] + 0.587f * Data[Img_Offset + 1] + 0.114f * Data[Img_Offset + 2]);
                                                Out_Data[Out_Data_Offset + 1] = Luma;
                                                if (BPP == 32)
                                                    Out_Data[Out_Data_Offset] = Data[Img_Offset + 3];
                                                else
                                                    Out_Data[Out_Data_Offset] = 0xff;
                                                Out_Data_Offset += 2;
                                                break;
                                            }
                                        case 7:
                                            {
                                                //L8
                                                byte Luma = Convert.ToByte(0.299f * Data[Img_Offset] + 0.587f * Data[Img_Offset + 1] + 0.114f * Data[Img_Offset + 2]);
                                                Out_Data[Out_Data_Offset] = Luma;
                                                Out_Data_Offset += 1;
                                                break;
                                            }
                                        case 8:
                                            {
                                                //A8
                                                if (BPP == 32)
                                                {
                                                    Out_Data[Out_Data_Offset] = Data[Img_Offset + 3];
                                                }
                                                else
                                                {
                                                    Out_Data[Out_Data_Offset] = 0xff;
                                                }
                                                Out_Data_Offset += 1;
                                                break;
                                            }
                                    }

                                    Texture_Insertion_Percentage = Convert.ToSingle((Out_Data_Offset / Out_Data.Length) * 100);
                                }
                            }
                        }
                    }

					break;
			}

			byte[] Temp = Convert_Texture(Out_Data, 0, Format, Img.Width, Img.Height);
			Bitmap Img2 = new Bitmap(Img.Width, Img.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			BitmapData ImgData2 = Img2.LockBits(new Rectangle(0, 0, Img.Width, Img.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			Marshal.Copy(Temp, 0, ImgData2.Scan0, Temp.Length);
			Img2.UnlockBits(ImgData2);

			OhanaTexture[] Temp_List = Model_Texture.ToArray();
			Model_Texture = new List<OhanaTexture>();

			string Temp_File = null;
			if (Current_Texture != null) {
				Temp_File = Temp_Texture_File;
			} else if (BCH_Have_Textures) {
				Temp_File = Temp_Model_File;
			} else {
				return;
			}

			byte[] Temp_Data = File.ReadAllBytes(Temp_File);

			//Mirror
			if (Common.ReadMagic(Temp_Data, 0, 2) == "PT") {
				byte[] Temp2 = Mirror_Texture(Temp, Img.Width, Img.Height);
				Bitmap Img3 = new Bitmap(Img.Width * 2, Img.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				BitmapData ImgData3 = Img3.LockBits(new Rectangle(0, 0, Img.Width * 2, Img.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
				Marshal.Copy(Temp2, 0, ImgData3.Scan0, Temp2.Length);
				Img3.UnlockBits(ImgData2);
				Temp_List[LstIndex].Texture = Get_Texture(Img3);
			} else {
				Temp_List[LstIndex].Texture = Get_Texture(Img2);
			}
			Img2.RotateFlip(RotateFlipType.RotateNoneFlipY);

			Temp_List[LstIndex].Image = Img2;
			Model_Texture.AddRange(Temp_List);
			if (My.MyProject.Forms.FrmMain.LstTextures.SelectedIndex == LstIndex) {
				My.MyProject.Forms.FrmMain.ImgTexture.Image = Img2;
				My.MyProject.Forms.FrmMain.ImgTexture.Refresh();
			}

			Buffer.BlockCopy(Out_Data, 0, Temp_Data, Model_Texture[LstIndex].Offset, Out_Data.Length);
			File.WriteAllBytes(Temp_File, Temp_Data);

			Texture_Insertion_Percentage = 0;
		}
		#endregion

		#region "ETC1 Decompressor"
		private byte[] ETC1_Decompress(byte[] Data, byte[] Alphas, int Width, int Height)
		{
			byte[] Out = new byte[(Width * Height * 4)];
			int Offset = 0;
			for (int Y = 0; Y <= (Height / 4) - 1; Y++) {
				for (int X = 0; X <= (Width / 4) - 1; X++) {
					byte[] Block = new byte[8];
					byte[] Alphas_Block = new byte[8];
					for (int i = 0; i <= 7; i++) {
						Block[7 - i] = Data[Offset + i];
						Alphas_Block[i] = Alphas[Offset + i];
					}
					Offset += 8;
					Block = ETC1_Decompress_Block(Block);

					bool Low_High_Toggle = false;
					int Alpha_Offset = 0;
					for (int TX = 0; TX <= 3; TX++) {
						for (int TY = 0; TY <= 3; TY++) {
							int Out_Offset = (X * 4 + TX + ((Y * 4 + TY) * Width)) * 4;
							int Block_Offset = (TX + (TY * 4)) * 4;
							Buffer.BlockCopy(Block, Block_Offset, Out, Out_Offset, 3);

							int Alpha_Data = 0;
							if (Low_High_Toggle) {
								Alpha_Data = (Alphas_Block[Alpha_Offset] & 0xf0) >> 4;
								Alpha_Offset += 1;
							} else {
								Alpha_Data = Alphas_Block[Alpha_Offset] & 0xf;
							}
							Low_High_Toggle = !Low_High_Toggle;
							Out[Out_Offset + 3] = Convert.ToByte((Alpha_Data << 4) + Alpha_Data);
						}
					}
				}
			}
			return Out;
		}
		private byte[] ETC1_Decompress_Block(byte[] Data)
		{
			//Ericsson Texture Compression
			int Block_Top = Common.Read32(Data, 0);
			int Block_Bottom = Common.Read32(Data, 4);

			bool Flip = (Block_Top & 0x1000000) > 0;
			bool Difference = (Block_Top & 0x2000000) > 0;

			int R1 = 0;
			int G1 = 0;
			int B1 = 0;
			int R2 = 0;
			int G2 = 0;
			int B2 = 0;
			int R = 0;
			int G = 0;
			int B = 0;

			if (Difference) {
				R1 = Block_Top & 0xf8;
				G1 = (Block_Top & 0xf800) >> 8;
				B1 = (Block_Top & 0xf80000) >> 16;

				R = Signed_Byte(Convert.ToByte(R1 >> 3)) + (Signed_Byte(Convert.ToByte((Block_Top & 7) << 5)) >> 5);
				G = Signed_Byte(Convert.ToByte(G1 >> 3)) + (Signed_Byte(Convert.ToByte((Block_Top & 0x700) >> 3)) >> 5);
				B = Signed_Byte(Convert.ToByte(B1 >> 3)) + (Signed_Byte(Convert.ToByte((Block_Top & 0x70000) >> 11)) >> 5);

				R2 = R;
				G2 = G;
				B2 = B;

				R1 = R1 + (R1 >> 5);
				G1 = G1 + (G1 >> 5);
				B1 = B1 + (B1 >> 5);

				R2 = (R2 << 3) + (R2 >> 2);
				G2 = (G2 << 3) + (G2 >> 2);
				B2 = (B2 << 3) + (B2 >> 2);
			} else {
				R1 = Block_Top & 0xf0;
				R1 = R1 + (R1 >> 4);
				G1 = (Block_Top & 0xf000) >> 8;
				G1 = G1 + (G1 >> 4);
				B1 = (Block_Top & 0xf00000) >> 16;
				B1 = B1 + (B1 >> 4);

				R2 = (Block_Top & 0xf) << 4;
				R2 = R2 + (R2 >> 4);
				G2 = (Block_Top & 0xf00) >> 4;
				G2 = G2 + (G2 >> 4);
				B2 = (Block_Top & 0xf0000) >> 12;
				B2 = B2 + (B2 >> 4);
			}

			int Mod_Table_1 = (Block_Top >> 29) & 7;
			int Mod_Table_2 = (Block_Top >> 26) & 7;

			byte[] Out = new byte[(4 * 4 * 4)];
			if (Flip == false) {
				for (int Y = 0; Y <= 3; Y++) {
					for (int X = 0; X <= 1; X++) {
						Color Col_1 = Modify_Pixel(R1, G1, B1, X, Y, Block_Bottom, Mod_Table_1);
						Color Col_2 = Modify_Pixel(R2, G2, B2, X + 2, Y, Block_Bottom, Mod_Table_2);
						Out[(Y * 4 + X) * 4] = Col_1.R;
						Out[((Y * 4 + X) * 4) + 1] = Col_1.G;
						Out[((Y * 4 + X) * 4) + 2] = Col_1.B;
						Out[(Y * 4 + X + 2) * 4] = Col_2.R;
						Out[((Y * 4 + X + 2) * 4) + 1] = Col_2.G;
						Out[((Y * 4 + X + 2) * 4) + 2] = Col_2.B;
					}
				}
			} else {
				for (int Y = 0; Y <= 1; Y++) {
					for (int X = 0; X <= 3; X++) {
						Color Col_1 = Modify_Pixel(R1, G1, B1, X, Y, Block_Bottom, Mod_Table_1);
						Color Col_2 = Modify_Pixel(R2, G2, B2, X, Y + 2, Block_Bottom, Mod_Table_2);
						Out[(Y * 4 + X) * 4] = Col_1.R;
						Out[((Y * 4 + X) * 4) + 1] = Col_1.G;
						Out[((Y * 4 + X) * 4) + 2] = Col_1.B;
						Out[((Y + 2) * 4 + X) * 4] = Col_2.R;
						Out[(((Y + 2) * 4 + X) * 4) + 1] = Col_2.G;
						Out[(((Y + 2) * 4 + X) * 4) + 2] = Col_2.B;
					}
				}
			}

			return Out;
		}
		private Color Modify_Pixel(int R, int G, int B, int X, int Y, int Mod_Block, int Mod_Table)
		{
			int Index = X * 4 + Y;
			int Pixel_Modulation = 0;
			int MSB = Mod_Block << 1;

			if (Index < 8) {
				Pixel_Modulation = Modulation_Table[Mod_Table, ((Mod_Block >> (Index + 24)) & 1) + ((MSB >> (Index + 8)) & 2)];
			} else {
				Pixel_Modulation = Modulation_Table[Mod_Table, ((Mod_Block >> (Index + 8)) & 1) + ((MSB >> (Index - 8)) & 2)];
			}

			R = Clip(R + Pixel_Modulation);
			G = Clip(G + Pixel_Modulation);
			B = Clip(B + Pixel_Modulation);

			return Color.FromArgb(B, G, R);
		}
		private byte Clip(int Value)
		{
			if (Value > 0xff) {
				return 0xff;
			} else if (Value < 0) {
				return 0;
			} else {
				return Convert.ToByte(Value & 0xff);
			}
		}
		#endregion

		#region "Misc. functions"
		private sbyte Signed_Byte(byte Byte_To_Convert)
		{
			if ((Byte_To_Convert < 0x80))
				return Convert.ToSByte(Byte_To_Convert);
			return Convert.ToSByte(Byte_To_Convert - 0x100);
		}
		private int Signed_Short(int Short_To_Convert)
		{
			if ((Short_To_Convert < 0x8000))
				return Short_To_Convert;
			return Short_To_Convert - 0x10000;
		}

		public Texture Get_Texture(Bitmap Image)
		{
			return new Texture(Device, Image, Usage.None, Pool.Managed);
		}
		private byte[] Mirror_Texture(byte[] Data, int Width, int Height)
		{
			byte[] Out = new byte[((Width * 2) * Height * 4)];
			for (int Y = 0; Y <= Height - 1; Y++) {
				for (int X = 0; X <= Width - 1; X++) {
					int Offset = (X + (Y * Width)) * 4;
					int Offset_2 = (X + (Y * (Width * 2))) * 4;
					int Offset_3 = ((Width + (Width - X - 1)) + (Y * (Width * 2))) * 4;
					Buffer.BlockCopy(Data, Offset, Out, Offset_2, 4);
					Buffer.BlockCopy(Data, Offset, Out, Offset_3, 4);
				}
			}
			return Out;
		}
		private bool Check_Alpha(byte[] Img)
		{
			for (int Offset = 0; Offset <= Img.Length - 1; Offset += 4) {
				if (Img[Offset + 3] < 0xff)
					return true;
			}
			return false;
		}

		private int[] Get_ETC1_Scramble(int Width, int Height)
		{
			int[] Tile_Scramble = new int[((Width / 4) * (Height / 4))];
			int Base_Accumulator = 0;
			int Line_Accumulator = 0;
			int Base_Number = 0;
			int Line_Number = 0;

			for (int Tile = 0; Tile <= Tile_Scramble.Length - 1; Tile++) {
				if ((Tile % (Width / 4) == 0) & Tile > 0) {
					if (Line_Accumulator < 1) {
						Line_Accumulator += 1;
						Line_Number += 2;
						Base_Number = Line_Number;
					} else {
						Line_Accumulator = 0;
						Base_Number -= 2;
						Line_Number = Base_Number;
					}
				}

				Tile_Scramble[Tile] = Base_Number;

				if (Base_Accumulator < 1) {
					Base_Accumulator += 1;
					Base_Number += 1;
				} else {
					Base_Accumulator = 0;
					Base_Number += 3;
				}
			}

			return Tile_Scramble;
		}
		#endregion

		#endregion

		#region "Renderer"
		public void Render()
		{
			//Define a posição da "câmera"
			Device.Transform.Projection = Matrix.PerspectiveFovLH((float)(Math.PI / 4), Convert.ToSingle(SWidth / SHeight), 0.1f, 500f);
			Device.Transform.View = Matrix.LookAtLH(new Vector3(0f, 0f, 20f), new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f));

			do {
				if (Model_Object != null & Rendering) {
					Device.Clear(ClearFlags.Target, bgCol, 1f, 0);
					Device.Clear(ClearFlags.ZBuffer, bgCol, 1f, 0);
					Device.BeginScene();

					Material MyMaterial = new Material();
					MyMaterial.Diffuse = Color.White;
					MyMaterial.Ambient = Color.White;
					Device.Material = MyMaterial;

					float Pos_Y = (Max_Y_Pos / 2) + (Max_Y_Neg / 2);
					if (Pos_Y > 10f)
						Pos_Y = 0;
					Matrix Rotation_Matrix = Matrix.RotationYawPitchRoll(-Rotation.X / 200f, -Rotation.Y / 200f, 0);
					Matrix Translation_Matrix = Matrix.Translation(new Vector3(-Translation.X / 50f, (Translation.Y / 50f) - Pos_Y, Zoom));
					Device.Transform.World = Rotation_Matrix * Translation_Matrix * Matrix.Scaling(-1, 1, 1);
					//Mirror X

					if (Edit_Mode) {
						var _with15 = Model_Object[Selected_Object];
						if (_with15.Texture_ID < Model_Texture_Index.Length) {
							string Texture_Name = Model_Texture_Index[_with15.Texture_ID];
							if (Model_Texture != null) {
								foreach (OhanaTexture Current_Texture in Model_Texture) {
									if (Current_Texture.Name == Texture_Name) {
										Device.SetTexture(0, Current_Texture.Texture);
									}
								}
							}
						}

						VertexFormats Vertex_Format = VertexFormats.Position | VertexFormats.Normal | VertexFormats.Texture1 | VertexFormats.Diffuse;
						VertexBuffer VtxBuffer = new VertexBuffer(typeof(OhanaVertex), _with15.Vertice.Length, Device, Usage.None, Vertex_Format, Pool.Managed);
						VtxBuffer.SetData(_with15.Vertice, 0, LockFlags.None);
						Device.VertexFormat = Vertex_Format;
						Device.SetStreamSource(0, VtxBuffer, 0);

						if (Selected_Face > -1) {
							IndexBuffer Index_Buffer = new IndexBuffer(typeof(int), _with15.Per_Face_Index[Selected_Face].Length, Device, Usage.WriteOnly, Pool.Managed);
							Index_Buffer.SetData(_with15.Per_Face_Index[Selected_Face], 0, LockFlags.None);
							Device.Indices = Index_Buffer;

							Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _with15.Vertice.Length, 0, _with15.Per_Face_Index[Selected_Face].Length / 3);
							Index_Buffer.Dispose();
						} else {
							IndexBuffer Index_Buffer = new IndexBuffer(typeof(int), _with15.Index.Length, Device, Usage.WriteOnly, Pool.Managed);
							Index_Buffer.SetData(_with15.Index, 0, LockFlags.None);
							Device.Indices = Index_Buffer;

							Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _with15.Vertice.Length, 0, _with15.Index.Length / 3);
							Index_Buffer.Dispose();
						}

						VtxBuffer.Dispose();


						Device.SetTexture(0, null);
					} else {
						for (int Phase = 0; Phase <= 1; Phase++) {
							for (int Index = 0; Index <= Model_Object.Length - 1; Index++) {
								var _with16 = Model_Object[Index];
								bool Has_Alpha = false;

								if (_with16.Texture_ID < Model_Texture_Index.Length) {
									string Texture_Name = Model_Texture_Index[_with16.Texture_ID];
									if (Model_Texture != null) {
										foreach (OhanaTexture Current_Texture in Model_Texture) {
											if (Current_Texture.Name == Texture_Name) {
												Has_Alpha = Current_Texture.Has_Alpha;
												if (!Has_Alpha | (Has_Alpha & Phase > 0))
													Device.SetTexture(0, Current_Texture.Texture);
											}
										}
									}
								}

								if (!Has_Alpha | (Has_Alpha & Phase > 0)) {
									VertexFormats Vertex_Format = VertexFormats.Position | VertexFormats.Normal | VertexFormats.Texture1 | VertexFormats.Diffuse;
									VertexBuffer VtxBuffer = new VertexBuffer(typeof(OhanaVertex), _with16.Vertice.Length, Device, Usage.None, Vertex_Format, Pool.Managed);
									VtxBuffer.SetData(_with16.Vertice, 0, LockFlags.None);
									Device.VertexFormat = Vertex_Format;
									Device.SetStreamSource(0, VtxBuffer, 0);

									IndexBuffer Index_Buffer = new IndexBuffer(typeof(int), _with16.Index.Length, Device, Usage.WriteOnly, Pool.Managed);
									Index_Buffer.SetData(_with16.Index, 0, LockFlags.None);
									Device.Indices = Index_Buffer;

									Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _with16.Vertice.Length, 0, _with16.Index.Length / 3);

									VtxBuffer.Dispose();
									Index_Buffer.Dispose();
								}

								Device.SetTexture(0, null);
							}
						}
						// "HackyCode"
						if (Map_Properties_Mode) {
							Switch_Lighting(false);

							float Start_X = 0;
							float Start_Y = 0;
							Start_X = -360 / Load_Scale;
							Start_Y = -360 / Load_Scale;

							int Verts = 40 * 40 * 6;
							VertexBuffer Vertex_Buffer = new VertexBuffer(typeof(CustomVertex.PositionColored), Verts, Device, Usage.None, CustomVertex.PositionColored.Format, Pool.Managed);
							Microsoft.DirectX.Direct3D.CustomVertex.PositionColored[] Vertices = new CustomVertex.PositionColored[Verts];
							int i = 0;
							float Block_Size = 18 / Load_Scale;
							for (int Y = 0; Y <= 39; Y++) {
								for (int X = 0; X <= 39; X++) {
									float VX1 = Start_X + X * Block_Size;
									float VZ1 = Start_Y + Y * Block_Size;
									float VX2 = Start_X + X * Block_Size + Block_Size;
									float VZ2 = Start_Y + Y * Block_Size + Block_Size;

									uint[] v = My.MyProject.Forms.FrmMapProp.getMapVals();
									uint col = v[X + (Y * 40)];
									Color c = default(Color);
									if (col == 0x1000021) {
										c = Color.Transparent;
									} else {
										col = My.MyProject.Forms.FrmMapProp.LCG(col, 4);
										c = Color.FromArgb(0x7f, 0xff - Convert.ToByte(col & 0xff), 0xff - Convert.ToByte((col >> 8) & 0xff), 0xff - Convert.ToByte(col >> 24 & 0xff));
									}

									Vertices[i] = new Microsoft.DirectX.Direct3D.CustomVertex.PositionColored(VX1, Pos_Y, VZ2, c.ToArgb());
									Vertices[i + 1] = new Microsoft.DirectX.Direct3D.CustomVertex.PositionColored(VX2, Pos_Y, VZ2, c.ToArgb());
									Vertices[i + 2] = new Microsoft.DirectX.Direct3D.CustomVertex.PositionColored(VX1, Pos_Y, VZ1, c.ToArgb());
									Vertices[i + 3] = new Microsoft.DirectX.Direct3D.CustomVertex.PositionColored(VX1, Pos_Y, VZ1, c.ToArgb());
									Vertices[i + 4] = new Microsoft.DirectX.Direct3D.CustomVertex.PositionColored(VX2, Pos_Y, VZ1, c.ToArgb());
									Vertices[i + 5] = new Microsoft.DirectX.Direct3D.CustomVertex.PositionColored(VX2, Pos_Y, VZ2, c.ToArgb());

									i += 6;
								}
							}

							Vertex_Buffer.SetData(Vertices, 0, LockFlags.None);
							Device.VertexFormat = CustomVertex.PositionColored.Format;
							Device.SetStreamSource(0, Vertex_Buffer, 0);

							Device.DrawPrimitives(PrimitiveType.TriangleList, 0, Vertices.Length / 3);
							Vertex_Buffer.Dispose();
							Switch_Lighting(Lighting);
						}
						//End HackyCode

						if (Coll_Debug) {
							VertexBuffer Buffer = new VertexBuffer(typeof(CustomVertex.PositionOnly), Collision.Length, Device, Usage.None, CustomVertex.PositionOnly.Format, Pool.Managed);
							Buffer.SetData(Collision, 0, LockFlags.None);
							Device.VertexFormat = CustomVertex.PositionOnly.Format;
							Device.SetStreamSource(0, Buffer, 0);
							Device.DrawPrimitives(PrimitiveType.LineStrip, 0, Collision.Length - 1);
							Buffer.Dispose();
						}
					}

					Device.EndScene();
					Device.Present();
				}

				Application.DoEvents();
			} while (true);
		}

		public void Switch_Lighting(bool Enabled)
		{
			var _with17 = Device;
			if (Enabled) {
				_with17.RenderState.Lighting = true;
				_with17.RenderState.Ambient = Color.FromArgb(64, 64, 64);
				_with17.Lights[0].Type = LightType.Point;
				_with17.Lights[0].Diffuse = Color.White;
				_with17.Lights[0].Position = new Vector3(0f, 10f, 30f);
				_with17.Lights[0].Range = 520f;
				_with17.Lights[0].Attenuation0 = 2f / Load_Scale;
				_with17.Lights[0].Enabled = true;
			} else {
				_with17.RenderState.Lighting = false;
				_with17.RenderState.Ambient = Color.White;
				_with17.Lights[0].Enabled = false;
			}
		}
		#endregion

		#region "OBJ Inserter"
		public void Insert_OBJ(string File_Name)
		{
			int SelObj = Selected_Object;
			byte[] Data = File.ReadAllBytes(Temp_Model_File);
			string Obj = File.ReadAllText(File_Name);

			List<Vector3> Vertices = new List<Vector3>();
			List<Vector3> Normals = new List<Vector3>();
			List<Vector2> UVs = new List<Vector2>();

			List<Vertex_Face> Faces = new List<Vertex_Face>();

			string[] Lines = Obj.Split(Convert.ToChar(0xa));
			foreach (string ObjLine in Lines) {
				string Line = Strings.LCase(ObjLine.Trim());
				string[] Line_Params = Regex.Split(Line, "\\s+");

				switch (Line_Params[0]) {
					case "v":
					case "vn":
                        {
                            Vector3 Vector = new Vector3();
                            Vector.X = float.Parse(Line_Params[1], CultureInfo.InvariantCulture);
                            Vector.Y = float.Parse(Line_Params[2], CultureInfo.InvariantCulture);
                            Vector.Z = float.Parse(Line_Params[3], CultureInfo.InvariantCulture);
                            if (Line_Params[0] == "v")
                                Vertices.Add(Vector);
                            else
                                Normals.Add(Vector);
                            break;
                        }
					case "vt":
                        {
                            Vector2 Vector = new Vector2();
                            Vector.X = float.Parse(Line_Params[1], CultureInfo.InvariantCulture);
                            Vector.Y = float.Parse(Line_Params[2], CultureInfo.InvariantCulture);
                            UVs.Add(Vector);
                            break;
                        }
					case "f":
                        {
                            string[] Vtx_A = Line_Params[1].Split(Convert.ToChar("/"));
                            string[] Vtx_B = Line_Params[2].Split(Convert.ToChar("/"));
                            string[] Vtx_C = Line_Params[3].Split(Convert.ToChar("/"));

                            Vertex_Face Face = default(Vertex_Face);
                            Face.Vtx_A_Coord_Index = int.Parse(Vtx_A[0]) - 1;
                            if (Vtx_A.Length > 1)
                                Face.Vtx_A_UV_Index = int.Parse(Vtx_A[1]) - 1;
                            if (Vtx_A.Length > 2)
                                Face.Vtx_A_Normal_Index = int.Parse(Vtx_A[2]) - 1;

                            Face.Vtx_B_Coord_Index = int.Parse(Vtx_B[0]) - 1;
                            if (Vtx_B.Length > 1)
                                Face.Vtx_B_UV_Index = int.Parse(Vtx_B[1]) - 1;
                            if (Vtx_B.Length > 2)
                                Face.Vtx_B_Normal_Index = int.Parse(Vtx_B[2]) - 1;

                            Face.Vtx_C_Coord_Index = int.Parse(Vtx_C[0]) - 1;
                            if (Vtx_C.Length > 1)
                                Face.Vtx_C_UV_Index = int.Parse(Vtx_C[1]) - 1;
                            if (Vtx_C.Length > 2)
                                Face.Vtx_C_Normal_Index = int.Parse(Vtx_C[2]) - 1;

                            Faces.Add(Face);
                            break;
                        }
				}
			}

			var _with18 = Model_Object[SelObj];
			//Insere Faces presentes no .obj até onde der
			bool Vtx_OK = true;

			int CurrFace = 0;
			int Current_Face_Offset = _with18.Per_Face_Entry[0].Offset;
			int Face_Length = _with18.Per_Face_Entry[0].Length;

			foreach (Data_Entry Entry in _with18.Per_Face_Entry) {
				for (int i = Entry.Offset; i <= Entry.Offset + Entry.Length; i++) {
					Data[i] = 0;
				}
			}

			for (int i = 0; i <= _with18.Index.Length - 1; i++) {
				_with18.Index[i] = 0;
			}

			for (int i = 0; i <= _with18.Per_Face_Index.Count - 1; i++) {
				for (int j = 0; j <= _with18.Per_Face_Index[i].Length - 1; j++) {
					_with18.Per_Face_Index[i][j] = 0;
				}
			}

			int Face_Index = 0;
			int Per_Face_Index = 0;
			foreach (Vertex_Face Face in Faces) {
				int a = Face.Vtx_A_Coord_Index;
				int b = Face.Vtx_B_Coord_Index;
				int c = Face.Vtx_C_Coord_Index;

				if (a < _with18.Vertice.Length & b < _with18.Vertice.Length & c < _with18.Vertice.Length) {
					if (_with18.Per_Face_Entry[CurrFace].Format == 1) {
						if (a > 0xff | b > 0xff | c > 0xff) {
							while (_with18.Per_Face_Entry[CurrFace].Format == 1) {
								Face_Index += (_with18.Per_Face_Entry[CurrFace].Length - Per_Face_Index);
								CurrFace += 1;
								Per_Face_Index = 0;
								if (CurrFace < _with18.Per_Face_Entry.Count) {
									Current_Face_Offset = _with18.Per_Face_Entry[CurrFace].Offset;
									Face_Length = _with18.Per_Face_Entry[CurrFace].Length;
								} else {
									break; // TODO: might not be correct. Was : Exit For
								}
							}

							//16 bits
							Data[Current_Face_Offset] = Convert.ToByte(a & 0xff);
							Data[Current_Face_Offset + 1] = Convert.ToByte((a & 0xff00) >> 8);
							Data[Current_Face_Offset + 2] = Convert.ToByte(b & 0xff);
							Data[Current_Face_Offset + 3] = Convert.ToByte((b & 0xff00) >> 8);
							Data[Current_Face_Offset + 4] = Convert.ToByte(c & 0xff);
							Data[Current_Face_Offset + 5] = Convert.ToByte((c & 0xff00) >> 8);
							Current_Face_Offset += 6;
						} else {
							//8 bits
							Data[Current_Face_Offset] = Convert.ToByte(a & 0xff);
							Data[Current_Face_Offset + 1] = Convert.ToByte(b & 0xff);
							Data[Current_Face_Offset + 2] = Convert.ToByte(c & 0xff);
							Current_Face_Offset += 3;
						}
					} else {
						//16 bits
						Data[Current_Face_Offset] = Convert.ToByte(a & 0xff);
						Data[Current_Face_Offset + 1] = Convert.ToByte((a & 0xff00) >> 8);
						Data[Current_Face_Offset + 2] = Convert.ToByte(b & 0xff);
						Data[Current_Face_Offset + 3] = Convert.ToByte((b & 0xff00) >> 8);
						Data[Current_Face_Offset + 4] = Convert.ToByte(c & 0xff);
						Data[Current_Face_Offset + 5] = Convert.ToByte((c & 0xff00) >> 8);
						Current_Face_Offset += 6;
					}

					//Injeta vertices
					Vtx_OK = Vtx_OK & Inject_Vertice(Data, a, SelObj, Vertices[a]);
					Vtx_OK = Vtx_OK & Inject_Vertice(Data, b, SelObj, Vertices[b]);
					Vtx_OK = Vtx_OK & Inject_Vertice(Data, c, SelObj, Vertices[c]);

					if (Face.Vtx_A_UV_Index < UVs.Count) {
						Inject_UV(Data, a, SelObj, UVs[Face.Vtx_A_UV_Index]);
						Inject_UV(Data, b, SelObj, UVs[Face.Vtx_B_UV_Index]);
						Inject_UV(Data, c, SelObj, UVs[Face.Vtx_C_UV_Index]);
					}

					if (Face.Vtx_A_Normal_Index < Normals.Count) {
						Inject_Normal(Data, a, SelObj, Normals[Face.Vtx_A_Normal_Index]);
						Inject_Normal(Data, b, SelObj, Normals[Face.Vtx_B_Normal_Index]);
						Inject_Normal(Data, c, SelObj, Normals[Face.Vtx_C_Normal_Index]);
					}

					//Atualiza modelo com novas faces
					Model_Object[SelObj].Index[Face_Index] = a;
					Model_Object[SelObj].Index[Face_Index + 1] = b;
					Model_Object[SelObj].Index[Face_Index + 2] = c;

					Model_Object[SelObj].Per_Face_Index[CurrFace][Per_Face_Index] = a;
					Model_Object[SelObj].Per_Face_Index[CurrFace][Per_Face_Index + 1] = b;
					Model_Object[SelObj].Per_Face_Index[CurrFace][Per_Face_Index + 2] = c;

					Face_Index += 3;
					Per_Face_Index += 3;

					if (Current_Face_Offset - _with18.Per_Face_Entry[CurrFace].Offset >= Face_Length) {
						CurrFace += 1;
						Per_Face_Index = 0;
						if (CurrFace < _with18.Per_Face_Entry.Count) {
							Current_Face_Offset = _with18.Per_Face_Entry[CurrFace].Offset;
							Face_Length = _with18.Per_Face_Entry[CurrFace].Length;
						} else {
							break; // TODO: might not be correct. Was : Exit For
						}
					}
				}
			}

			if (!Vtx_OK & Face_Index < _with18.Index.Length) {
				MessageBox.Show("The inserted object have too much faces and vertices." + Environment.NewLine + "Try limiting it to the original counts.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			} else if (Face_Index / 3 < Faces.Count) {
				MessageBox.Show("The inserted object have more faces than the original one." + Environment.NewLine + "Some faces couldn't be added.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			} else if (!Vtx_OK) {
				MessageBox.Show("The inserted object have more vertices than the original one." + Environment.NewLine + "Some vertices couldn't be added.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			File.WriteAllBytes(Temp_Model_File, Data);
		}
		private bool Inject_Vertice(byte[] Data, int Index, int SelObj, Vector3 Vertice)
		{
			int Offset = Model_Object[SelObj].Vertex_Entry.Offset + (Index * Model_Object[SelObj].Vertex_Entry.Format);
			if (Offset - Model_Object[SelObj].Vertex_Entry.Offset >= Model_Object[SelObj].Vertex_Entry.Length)
				return false;

			byte[] X_Bytes = BitConverter.GetBytes(Vertice.X);
			byte[] Y_Bytes = BitConverter.GetBytes(Vertice.Y);
			byte[] Z_Bytes = BitConverter.GetBytes(Vertice.Z);

			Buffer.BlockCopy(X_Bytes, 0, Data, Offset, 4);
			Buffer.BlockCopy(Y_Bytes, 0, Data, Offset + 4, 4);
			Buffer.BlockCopy(Z_Bytes, 0, Data, Offset + 8, 4);

			var _with19 = Model_Object[SelObj].Vertice[Index];
			_with19.X = Vertice.X / Load_Scale;
			_with19.Y = Vertice.Y / Load_Scale;
			_with19.Z = Vertice.Z / Load_Scale;

			return true;
		}
		private void Inject_UV(byte[] Data, int Index, int SelObj, Vector2 UV)
		{
			int Offset = Model_Object[SelObj].Vertex_Entry.Offset + (Index * Model_Object[SelObj].Vertex_Entry.Format);
			if (Offset - Model_Object[SelObj].Vertex_Entry.Offset >= Model_Object[SelObj].Vertex_Entry.Length)
				return;

			byte[] U_Bytes = BitConverter.GetBytes(UV.X);
			byte[] V_Bytes = BitConverter.GetBytes(UV.Y);

			switch (Model_Object[SelObj].Vertex_Entry.Format) {
				case 0x14:
				case 0x18:
				case 0x1c:
					Buffer.BlockCopy(U_Bytes, 0, Data, Offset + 12, 4);
					Buffer.BlockCopy(V_Bytes, 0, Data, Offset + 16, 4);
					break;
				case 0x20:
				case 0x24:
				case 0x28:
				case 0x2c:
				case 0x30:
				case 0x34:
				case 0x38:
					Buffer.BlockCopy(U_Bytes, 0, Data, Offset + 24, 4);
					Buffer.BlockCopy(V_Bytes, 0, Data, Offset + 28, 4);
					break;
			}

			Model_Object[SelObj].Vertice[Index].U = UV.X;
			Model_Object[SelObj].Vertice[Index].V = UV.Y;
		}
		private void Inject_Normal(byte[] Data, int Index, int SelObj, Vector3 Normal)
		{
			int Offset = Model_Object[SelObj].Vertex_Entry.Offset + (Index * Model_Object[SelObj].Vertex_Entry.Format);
			if (Offset - Model_Object[SelObj].Vertex_Entry.Offset >= Model_Object[SelObj].Vertex_Entry.Length)
				return;

			byte[] NX_Bytes = BitConverter.GetBytes(Normal.X);
			byte[] NY_Bytes = BitConverter.GetBytes(Normal.Y);
			byte[] NZ_Bytes = BitConverter.GetBytes(Normal.Z);

			switch (Model_Object[SelObj].Vertex_Entry.Format) {
				case 0x20:
				case 0x24:
				case 0x28:
				case 0x2c:
				case 0x30:
				case 0x34:
				case 0x38:
					Buffer.BlockCopy(NX_Bytes, 0, Data, Offset + 12, 4);
					Buffer.BlockCopy(NY_Bytes, 0, Data, Offset + 16, 4);
					Buffer.BlockCopy(NZ_Bytes, 0, Data, Offset + 20, 4);
					break;
			}

			var _with20 = Model_Object[SelObj].Vertice[Index];
			_with20.NX = Normal.X / Load_Scale;
			_with20.NY = Normal.Y / Load_Scale;
			_with20.NZ = Normal.Z / Load_Scale;
		}
		#endregion

		public List<string> getProps()
		{
			List<string> list = new List<string>();
			string mapProperties = Ohana3DS.My.Resources.Resources.MapProperties;
			int num = 0;
			foreach (string str in mapProperties.Split(new char[] { Environment.NewLine[0] }, (StringSplitOptions)num)) {
				list.Add(str.Substring(str.IndexOf(",") + 1));
			}
			return list;
		}

	}
}
