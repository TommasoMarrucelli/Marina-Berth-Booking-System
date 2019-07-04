using System;

namespace Marina_Berth
{
	class DisplayRecords
	{
		public static void DisRec()
		{

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("ALL RECORDS AND AVAILABLE SPACE" + Environment.NewLine);
			Console.ForegroundColor = ConsoleColor.White;

			//check the available space in the berth
			double spaceAvailable = MarinaTools.spaceAvailable();
			Console.WriteLine("The current available space in the berth is " + spaceAvailable + "m" +  Environment.NewLine);

			Console.WriteLine("All the bookings:" + Environment.NewLine);

			Console.ForegroundColor = ConsoleColor.Yellow;

			//Call a method to read from the list of all records
			RecordList recordsList = MarinaTools.readList();

			//Print all the records
			foreach (var item in recordsList)
			{ Console.WriteLine(item + Environment.NewLine);}


			Console.WriteLine(Environment.NewLine + "Press enter to go back to the main menu");
			Console.ResetColor();
			MarinaTools.goBackMainMenu();



		}
	}
}
