using System;

namespace Marina_Berth
{
	[Serializable]
	public class RecordNode
	{

		private string ownerName;
		private string boatName;
		private char boatType;
		private double boatLength;
		private int boatPosition;
		private string rentEnd;
		private RecordNode next;
		private double bookingPrice;
		public static double currentLength;
		public static double currentPrice;
		public static string currentEnd;


		public RecordNode()
		{ }

		public RecordNode(string owner, string boat, char type, int position)
		{
			ownerName = owner;
			boatName = boat;
			boatType = type;
			boatPosition = position;
			boatLength = currentLength;
			bookingPrice = currentPrice;
			rentEnd = currentEnd;
		}

		public RecordNode(double length, double price, string end)
		{
			currentLength = length;
			currentPrice = price;
			currentEnd = end;
		}

		public double getLength()
		{ return boatLength; }

		public double getPrice()
		{ return bookingPrice; }

		public string getName()
		{ return ownerName; }

		public string getBoat()
		{ return boatName; }

		
		

		public void setNext(RecordNode nextNode)
		{
			next = nextNode;  // change next node
		}
	
		public RecordNode getNext()
		{ return next; }

		public int getPosition()
		{ return boatPosition; }

		public void setPosition(int position)
		{ boatPosition = position; }

		
		public override string ToString()
		{ return "Position: " + boatPosition + " || Name: " + ownerName + " ||  boat: " + boatName + " || type: " + boatType + " || length: " + boatLength + "m" + " || price: " + bookingPrice + "$" + "|| end: " + rentEnd; }
	}
}
