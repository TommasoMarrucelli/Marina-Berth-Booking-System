using System;
using System.Collections;
using System.Collections.Generic;

namespace Marina_Berth
{

	[Serializable]
	public class RecordList : IEnumerable<RecordNode>
	{
		private RecordNode start, end;

		public RecordList()
		{
			start = null;   // both set to NULL
			end = null;
		}

		public void addRecordAtEnd(RecordNode current)
		{

			if (end == null)       // if list is empty
			{
				start = current;               // change start
				end = current;                        // and end
			}
			else
			{
				end.setNext(current);           // change end's next
				end = current;                  // change end}
			}
		}

		public IEnumerator<RecordNode> GetEnumerator()
		{
			RecordNode current = start;
			while (current != null)
			{
				yield return current;
				current = current.getNext();
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count() //it starts to count from 1
		{
			RecordNode current;
			int i = 1;

			if (start != null)
			{
				current = start;
				while (current != null)
				{
					i++;
					current = current.getNext();
				}

			}

			return i;
		}


		public int EmptyCheck()
		{
			RecordNode current;
			int i = 0;

			if (start != null)
			{
				current = start;
				while (current != null)
				{
					i++;
					current = current.getNext();
				}

			}

			return i;
		}

		public void removeRecordAt(int position) // the user insert the records position to eleminate the record
		{

			RecordNode previous = null;
			RecordNode current = start;

			while (current != null) // navigate down the list till the end
			{
				RecordNode next = current.getNext();

				if (position == current.getPosition()) // when the position of the current record is the same of the one inserted by the user, the current record is deleted
				{
					if (previous == null)
					{
						start = next;
					}
					else
					{
						previous.setNext(next);
					}
				}

				previous = current;
				current = next;
			}


		}

		public void reorderList() // when a record is deleted, the positions of all other records are reassigned. In this way the enumeration is continous, without holes.
		{
			RecordNode current;

			if (start != null)
			{
				current = start;
				int i = 1;

				while (current != null)
				{
					current.setPosition(i);
					i++;
					current = current.getNext();
				}
			}
		}

	}
}
