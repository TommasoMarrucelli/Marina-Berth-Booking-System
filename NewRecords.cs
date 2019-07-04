using System;
using System.Globalization;


namespace Marina_Berth
{

	public class NewRecords 
	{

		public static void recordValidation() // the boat measures are checked and the rental price is offered
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("BOOKING PAGE" + Environment.NewLine);
			Console.ForegroundColor = ConsoleColor.White;

			bool repeat;  //variable used to control the following loops

			string inputLength, inputDraft;

			do
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("Insert the boat length in meters > ");
				inputLength = Console.ReadLine();
				//check if the input is valid or restart the loop
				repeat = MarinaTools.checkDoubleInput(inputLength);
			}
			while (repeat == true);

			
			double length= double.Parse(inputLength.Trim());

			//check that the boat is not too long
			MarinaTools.checkBoatLength(length);

			do
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("Insert the boat draft in meters > ");
				inputDraft = Console.ReadLine();
				repeat = MarinaTools.checkDoubleInput(inputDraft);
			}
			while (repeat == true);

			double draft = double.Parse(inputDraft.Trim());

			//check that the boat is not to deep
			MarinaTools.checkBoatDraft(draft);

			// check if there is space available
			double spaceAvailable = MarinaTools.spaceAvailable();

			if (length > spaceAvailable)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("We are sorry but there is no space available for your boat. Press enter to go back to the main menu");
				MarinaTools.goBackMainMenu();

			}

			//calculation of the rent price for the chosen period 
			string inputMonths;

			do
			{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Input the duration of your stay in months >");
			Console.ForegroundColor = ConsoleColor.White;
			inputMonths = Console.ReadLine();
			repeat = MarinaTools.checkIntInput(inputMonths);

			} while(repeat == true);
			
			int months = int.Parse(inputMonths.Trim());
			double price = months * length * 10; // formula to calculate the rental price
			Console.WriteLine("The price of the stay is " + price + "$");

			DateTime now = DateTime.Now;
			string rentEnd = now.AddMonths(months).ToString();
			
			//user can decide to accept or deny the offer
			do
			{
				string inputDecision;

				do //validate the input
				{
					Console.WriteLine("Enter Y to confirm the booking or N to reject it >");
					inputDecision = Console.ReadLine();
					repeat = MarinaTools.checkStringInput(inputDecision); 
				} while (repeat == true);


				string lowDecision = inputDecision.ToLower(new CultureInfo("en-US", false));
					char decision = lowDecision[0];


				switch (decision)
				{
					case 'n':
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("No record has been booked. Press enter to go back to the main menu. Thank you");
						MarinaTools.goBackMainMenu();
						break;

					case 'y':
						RecordNode lenghtAndPriceAndDate = new RecordNode(length, price, rentEnd);
						newRecord();
						break;

					default:
						Console.WriteLine("You did not insert a valid option. Try again");
						repeat = true;
						break;
				}
			}
			while (repeat == true);
		}
		


		public static void newRecord() // here is where the user input the information for the new object and where I create the record objects and the list
		{

			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();

			// user inputs info for his booking record
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("In order to complete the booking you have to to input some information: " + Environment.NewLine);
			Console.ForegroundColor = ConsoleColor.White;
			bool repeat;
			string oName, bName;
			char bType;
			
			

			do
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("Name of the owner > ");
				oName = (Console.ReadLine()).Trim();
				repeat = MarinaTools.checkStringInput(oName);
			} while (repeat == true);

			do
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("Name of the boat > ");
				bName = (Console.ReadLine()).Trim();
				repeat = MarinaTools.checkStringInput(bName);
			} while (repeat == true);


			string inputType;
			
			do
			{
				Console.WriteLine("input the type of the boat: N for narrow, S for sailing, M for motor > ");
				inputType = Console.ReadLine();
				repeat = MarinaTools.checkBoatType(inputType);  //method that validates the user input for the boat type

			} while (repeat == true);

			string lowInputType = inputType.ToLower(new CultureInfo("en-US", false));
			bType = lowInputType[0];


			//reads the list from the file or creates a new one
			RecordList recordsList = MarinaTools.readList();

			//counts the number of records in the list in order to assign a position to the current object
			int bPosition = recordsList.Count();

			//Creates an object containing all the customer information
			RecordNode record = new RecordNode(oName, bName, bType, bPosition);

			//adds the current object at the end of the list
			recordsList.addRecordAtEnd(record);


			//Serializes and rewrite the list in the file of booking
			MarinaTools.writeList(recordsList);

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Your booking is confirmed. Press enter to go back to the main menu.");
			MarinaTools.goBackMainMenu();

		}

	}

}


