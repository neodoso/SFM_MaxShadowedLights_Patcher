using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFM_MaxShadowedLights_Patcher.Functions
{
    public static class HexString_ToBytes
    {

		private readonly static Dictionary<char, byte> hexmap = new Dictionary<char, byte>()
		{
			{ 'a', 0xA },{ 'b', 0xB },{ 'c', 0xC },{ 'd', 0xD },
			{ 'e', 0xE },{ 'f', 0xF },{ 'A', 0xA },{ 'B', 0xB },
			{ 'C', 0xC },{ 'D', 0xD },{ 'E', 0xE },{ 'F', 0xF },
			{ '0', 0x0 },{ '1', 0x1 },{ '2', 0x2 },{ '3', 0x3 },
			{ '4', 0x4 },{ '5', 0x5 },{ '6', 0x6 },{ '7', 0x7 },
			{ '8', 0x8 },{ '9', 0x9 }
		};

		/// <summary>
		/// converts hexadecimal string to byte array
		/// </summary>
		/// <param name="hex">hex code as string</param>
		public static byte[] ToBytes(this string hex)
		{
			if (string.IsNullOrWhiteSpace(hex))
				MessageBox.Show("(ToBytes) Error: hex code cannot be null/empty/whitespace");

			if (hex.Length % 2 != 0)
				MessageBox.Show("(ToBytes) Error: hex code must have an even number of characters");

			bool startsWithHexStart = hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase);

			if (startsWithHexStart && hex.Length == 2)
				MessageBox.Show("(ToBytes) Error: There are no characters in the hex string");


			int startIndex = startsWithHexStart ? 2 : 0;

			byte[] bytesArr = new byte[(hex.Length - startIndex) / 2];

			char left;
			char right;

			try
			{
				int x = 0;
				for (int i = startIndex; i < hex.Length; i += 2, x++)
				{
					left = hex[i];
					right = hex[i + 1];
					bytesArr[x] = (byte)((hexmap[left] << 4) | hexmap[right]);
				}
				return bytesArr;
			}
			catch (KeyNotFoundException)
			{
				throw new FormatException("Hex string has non-hex character");
			}
		}

		/// <summary>
		/// returns true if string is hexadecimal
		/// </summary>
		/// <param name="test"></param>
		/// <returns></returns>
		private static bool OnlyHexInString(string inputString)
		{
			return System.Text.RegularExpressions.Regex.IsMatch(inputString, @"\A\b[0-9a-fA-F]+\b\Z");
		}

	}
}
