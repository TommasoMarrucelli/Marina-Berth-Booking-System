using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Marina_Berth
{
	[Serializable]
	class DeleteRecords
	{
		public static void DeletingMenu()
		{

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("DELETE A RECORD" + Environment.NewLine);

			bool repeat;

			do
			{
				Console.WriteLine("Choose a deleting option" + Environment.NewLine); // The user can choose to search for name of the owner an of the boat
				Console.ForegroundColor = ConsoleColor.White;						 // or select a boat from the display of all records
				Console.WriteLine("1 - Search a record by the owner and the boat names" + Environment.NewLine + "2 - Display all records");

				string choice;
				do
				{
					choice = Console.ReadLine();
					repeat = MarinaTools.checkIntInput(choice);
				} while (repeat == true);

				int userChoice = int.Parse(choice);

				switch (userChoice)
				{
					case 1:
						deleteByNames();
						break;

					case 2:
						deleteByDisplay();
						break;

					default:
						Console.WriteLine("Insert a valid option.");
						repeat = true;
						break;
				}


			} while (repeat == true);

		}


		public static void deleteByNames()
		{

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("DELETE A RECORD" + Environment.NewLine);

			//Call a method to read from the list of all records
			RecordList recordsList = MarinaTools.readList();


			Console.WriteLine("In order to delete your booking we need you to input some information: " + Environment.NewLine);
			Console.ForegroundColor = ConsoleColor.White;

			bool repeat;
			string oName, bName;


			do
			{

				Console.WriteLine("Name of the owner > ");
				oName = Console.ReadLine();
				repeat = MarinaTools.checkStringInput(oName);
			} while (repeat == true);

			do
			{
				Console.WriteLine("Name of the boat > ");
				bName = Console.ReadLine();
				repeat = MarinaTools.checkStringInput(bName);
			} while (repeat == true);

			//find the records that contain the inserted owner name and owner boat
			IEnumerable<RecordNode> delList = recordsList.Where(record => (record.getName() == oName) && (record.getBoat() == bName));

			if (delList.Count() != 0)
			{

				Console.WriteLine(String.Join(Environment.NewLine, delList));
	

				Console.WriteLine("Insert the position number that you want to delete >");

				string inputNumber;

				do
				{
					inputNumber = Console.ReadLine();   
					
				}while (repeat == true);

				int delNumber = int.Parse(inputNumber);

				finalDelCheck(delNumber, recordsList);
			}
			else
			{
				Console.WriteLine("The entered details have 0 matches. Press enter to go back to the main menu.");
				MarinaTools.goBackMainMenu();
			}

		}

		public static void deleteByDisplay()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("DELETE A RECORD" + Environment.NewLine);

			//Call a method to read from the list of all records
			RecordList recordsList = MarinaTools.readList();

			Console.ForegroundColor = ConsoleColor.White;
			//Print all the records
			Console.WriteLine(String.Join(Environment.NewLine + Environment.NewLine, recordsList));

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(Environment.NewLine +"Insert the position number that you want to delete >");
			Console.ForegroundColor = ConsoleColor.White;

			string inputNumber;
			bool repeat;

			do
			{
				inputNumber = Console.ReadLine();               
				repeat = MarinaTools.checkIntInput(inputNumber);   

			} while (repeat == true);

			int delNumber = int.Parse(inputNumber);

			finalDelCheck(delNumber, recordsList);

		}


		public static void finalDelCheck(int number, RecordList list)  // the user is asked to confirm the deleting
		{
			bool repeat = false;
			do
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("Are you sure to delete record number " + number + " ? Typet Y to confirm or N to go back to the main menu.");
				Console.ForegroundColor = ConsoleColor.White;
				string inputDecision = Console.ReadLine();
				//converting every input to low case to avoid errors due to input format 
				string lowDecision = inputDecision.ToLower(new CultureInfo("en-US", false));
				char decision = lowDecision[0];

				switch (decision)
				{
					case 'n':
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("No record has been deleted. Press enter to go back to the main menu. Thank you");
						MarinaTools.goBackMainMenu();
						break;

					case 'y':

						DeleteRecords.deleteRecord(number, list);

						break;

					default:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("You did not insert a valid option. Try again");
						Console.ForegroundColor = ConsoleColor.White;
						repeat = true;
						break;
				}
			}
			while (repeat == true);
		}

		public static void deleteRecord(int number, RecordList list) // removes a record from the list and displays the boats that have to be moved in the holding bay
		{

			int count =list.Count();
			int movments = count - number -1;
			IEnumerable<RecordNode> movList = list.Where(record => (record.getPosition() > number));

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("DELETE A RECORD" + Environment.NewLine);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("The record has been succesfully deleted." + Environment.NewLine);

			if (movList.Count() != 0)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("In order to leave access out to that boat, you have to move in the holding bay the following " + movments + " boat/s:" + Environment.NewLine);
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine(String.Join(Environment.NewLine, movList));
			}
			else
			{
				Console.WriteLine("There is no need to move any other boat to leave access out to this one.");
			}
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(Environment.NewLine + "Press enter to go back to the main menu.");

			list.removeRecordAt(number);

			if (list.EmptyCheck() != 0) // check if the list is empty
			{

				MarinaTools.writeList(list);
			}
			else
			{
				MarinaTools.writeEmptyList();   //if the last record has been removed, clear all the text in the file. Using the binary formatter in this case, leaves some strings in the file that interfer with the creation of new records.
			}


			MarinaTools.goBackMainMenu();

		}
	}
}
