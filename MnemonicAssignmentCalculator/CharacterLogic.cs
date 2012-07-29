using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MnemonicAssignmentCalculator
{
	internal class CharacterLogic
	{
		#region --Public Static Members--

		public static IList<string> FindUniqueLetters(IList<string> items)
		{
			List<char> uniqueCharacters = new List<char>();

			foreach(string item in items)
			{
				foreach (char character in item.ToLower())
				{
					if ((character != ' ') && !uniqueCharacters.Contains(character))
					{
						uniqueCharacters.Add(character);
					}
				}
			}

			IList<string> uniqueLetters = CharacterLogic.OrderByFrequency(uniqueCharacters);
			return uniqueLetters;
		}

		public static IList<string> OrderByFrequency(List<char> characters)
		{
			return characters.OrderBy(x => CharacterLogic.GetScrabbleValue(x)).Select(x => x.ToString()).Reverse().ToList();
		}

		#endregion --Public Static Members--

		#region --Private Static Members--

		private static int GetScrabbleValue(char character)
		{
			int value = 0;

			character = character.ToString().ToLower().ToCharArray()[0];

			switch (character)
			{
				case 'e':
				case 'a':
				case 'i':
				case 'o':
				case 'n':
				case 'r':
				case 't':
				case 'l':
				case 's':
				case 'u':
					value = 1;
					break;
				case 'd':
				case 'g':
					value = 2;
					break;
				case 'b':
				case 'c':
				case 'm':
				case 'p':
					value = 3;
					break;
				case 'f':
				case 'h':
				case 'v':
				case 'w':
				case 'y':
					value = 4;
					break;
				case 'k':
					value = 5;
					break;
				case 'j':
				case 'x':
					value = 8;
					break;
				case 'q':
				case 'z':
					value = 10;
					break;
			}

			return value;
		}

		#endregion --Private Static Members--
	}
}