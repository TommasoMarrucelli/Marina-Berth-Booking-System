using System;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Marina_Berth
{
	class MarinaTools
	{
		public static void goBackMainMenu()
		{
			string pressEnter = Console.ReadLine();
			if (pressEnter != null)
			{
				Home.mainMenu();
			}
		}

		public static RecordList readList() // Read a list from a file or create a new one
		{
			string path = "Marina Berth Records.bin";

			try
			{
				using (Stream stream = File.Open(path, FileMode.Open))
				{
					var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

					RecordList recordsList = (RecordList)bformatter.Deserialize(stream);
					return recordsList;
				}
			}
			catch
			{
				RecordList recordsList = new RecordList();
				return recordsList;
			}
		}

		public static void writeList(RecordList list) // Write a list in the file
		{
			string path = "Marina Berth Records.bin";
			using (Stream stream = File.Open(path, FileMode.Create))
			{

				var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

				list.reorderList(); //reorder the list, in case a record has been deleted

				bformatter.Serialize(stream, list);

			}
		}

		public static void writeEmptyList() //clear all the text in the file. It is used when the last record on the list is deleted.
		{
			string path = "Marina Berth Records.bin";

			File.WriteAllText(path, String.Empty);

		}

		public static bool checkInput(string input) // check if the input is empty
		{
			if (string.IsNullOrEmpty(input))
			{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Empty input, please try again." + Environment.NewLine);
					Console.ForegroundColor = ConsoleColor.White;
					bool repeat = true;
					return repeat;
			}
			else
			{
				bool repeat = false;
				return repeat;
			}
		}

		public static bool checkDoubleInput(string input) // Validate double inputs
		{
			string trimInput = input.Trim();

			if (!checkInput(trimInput))
			{
				try
				{
					double number = double.Parse(trimInput);

					if (number != 0)// the user can't insert 0
					{
						bool repeat = false;
						return repeat;
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("A value of 0 is not accepted." + Environment.NewLine);
						Console.ForegroundColor = ConsoleColor.White;
						bool repeat = true;
						return repeat;
					}
				}
				catch
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("You can insert only numbers." + Environment.NewLine);
					Console.ForegroundColor = ConsoleColor.White;
					bool repeat = true;
					return repeat;
				}
			}
			else
			{
				bool repeat = true;
				return repeat;
			}
		}

		public static bool checkIntInput(string input) // Validate int inputs
		{
			string trimInput = input.Trim();

			if (!checkInput(trimInput))
			{
				try
				{
					int number = int.Parse(trimInput);

					if (number != 0) // the user can't insert 0
					{
						bool repeat = false;
						return repeat;
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("A value of 0 is not accepted." + Environment.NewLine);
						Console.ForegroundColor = ConsoleColor.White;
						bool repeat = true;
						return repeat;
					}
				}
				catch
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("You can insert only numbers." + Environment.NewLine);
					bool repeat = true;
					return repeat;
				}
			}
			else
			{
				bool repeat = true;
				return repeat;
			}
		}

		public static bool checkStringInput(string input) //validate string inputs
		{
			string trimInput = input.Trim();
			if (!checkInput(trimInput))
			{
				if (trimInput.All(Char.IsLetter))
				{

					bool repeat = false;
					return repeat;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("You can insert only letters." + Environment.NewLine);
					Console.ForegroundColor = ConsoleColor.White;
					bool repeat = true;
					return repeat;
				}
			}
			else
			{
				bool repeat = true;
				return repeat;
			}
		}

		public static double spaceAvailable() // returns the space available in the marina berth
		{
			int maxSpace = 150;
			var recordList = readList();

			double sum = 0;

			foreach (var item in recordList)
			{
				sum = sum + item.getLength();

			}

			double spaceAvailable = maxSpace - sum;
			return spaceAvailable;
		}

		public static void checkBoatLength(double lenght) //check the lenght of the boat and if it is too big go back to the main menu
		{
			int maxLength = 15;

			
			if (lenght > maxLength)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("We are sorry but the boat is too long. Press enter to go back to the main menu");
				MarinaTools.goBackMainMenu();
			}
		}

		public static void checkBoatDraft(double draft) //check the draft of the boat and if it is over tha max go back to the main menu
		{
			int maxDraft = 5;
			
			if (draft > maxDraft)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("We are sorry but the boat is too deep. Press enter to go back to the main menu");
				MarinaTools.goBackMainMenu();
			}
		}

		public static bool checkBoatType(string input) // method to validate boat type entered by the user
		{
			string trimInput = input.Trim();
			if (!checkInput(trimInput))
			{
				if (trimInput.All(Char.IsLetter))
				{
					string lowInputBoatType = input.ToLower(new CultureInfo("en-US", false));
					char boatTypeLetter = lowInputBoatType[0];

					if (boatTypeLetter == 'n' || boatTypeLetter == 's' || boatTypeLetter == 'm') // the user input has to be on of these char
					{
						bool repeat = false;
						return repeat;
					}
					else
					{

						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Insert a valid boat type. Try again." + Environment.NewLine);
						Console.ForegroundColor = ConsoleColor.White;
						bool repeat = true;
						return repeat;
					}
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("You can insert only letters." + Environment.NewLine);
					Console.ForegroundColor = ConsoleColor.White;
					bool repeat = true;
					return repeat;
				}
			}
			else
			{
				bool repeat = true;
				return repeat;
			}
		}
	}
}

	

