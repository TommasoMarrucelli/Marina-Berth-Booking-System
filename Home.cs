using System;

namespace Marina_Berth
{
	class Home
	{
		private static void Main(string[] args)
		{
			mainMenu();
		}

		public static void mainMenu()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("This is the Marina Berth Booking System, input the number of your preferred option:" + Environment.NewLine);
			Console.ResetColor();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("1) Record a new booking");
			Console.WriteLine("2) Delete a record");
			Console.WriteLine("3) Display all records and availability");
			Console.WriteLine("4) Exit" + Environment.NewLine);

			bool repeat = false;

			do
			{
				string inputUser;
				int option;
				inputUser = Console.ReadLine();
				int.TryParse(inputUser, out option);
			
			
				switch (option)
				{
					case 1:
						Console.Clear();
						NewRecords.recordValidation();
						break;

					case 2:
						Console.Clear();
						DeleteRecords.DeletingMenu();
						break;

					case 3:
						Console.Clear();
						DisplayRecords.DisRec();
						break;

					case 4:
						Environment.Exit(0);
						break;

					default:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Input a valid option number" + Environment.NewLine);
						Console.ForegroundColor = ConsoleColor.White;
						repeat = true;
						break;

				}
			} while (repeat == true);
			
		Console.ReadLine();
			
		}

	}
}
