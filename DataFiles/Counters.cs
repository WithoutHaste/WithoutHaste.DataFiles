using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithoutHaste.DataFiles
{
	/// <summary>
	/// Counts using an arbitrary set of digits.
	/// </summary>
	/// <remarks>
	/// Using "integers" as an analogy, Counter values cannot be negative.
	/// </remarks>
	public abstract class ArbitraryCounter
	{
		/// <summary>Counter can never decrement below the minimum value.</summary>
		public string MINIMUM_VALUE { get { return VALID_CHARACTERS[0].ToString(); } }

		/// <summary>Current display value.</summary>
		public string Value { get { return String.Join("", value); } }

		/// <summary>Current internal value.</summary>
		protected List<char> value;

		/// <summary>All valid characters, in order from smallest to largest.</summary>
		protected readonly char[] VALID_CHARACTERS;

		/// <summary>Initialize at MINIMUM_VALUE.</summary>
		public ArbitraryCounter(char[] validCharacters)
		{
			VALID_CHARACTERS = validCharacters;
			value = new List<char>() { VALID_CHARACTERS[0] };
		}

		/// <summary></summary>
		public void SetValue(string start)
		{
			foreach(char c in start)
			{
				if(!VALID_CHARACTERS.Contains(c))
				{
					throw new ArgumentException("Start value can only contain VALID_CHARACTERS.");
				}
			}

			value = start.Select(c => c).ToList();
		}

		/// <summary></summary>
		protected void SetValue(List<char> start)
		{
			foreach(char c in start)
			{
				if(!VALID_CHARACTERS.Contains(c))
				{
					throw new ArgumentException("Start value can only contain VALID_CHARACTERS.");
				}
			}

			value = start.Select(c => c).ToList();
		}

		/// <summary>Increment a value.</summary>
		protected void Increment(List<char> value, int delta)
		{
			if(delta < 0)
			{
				Decrement(value, (delta * -1));
				return;
			}

			while(delta > 0)
			{
				int placeIndex = value.Count - 1;
				while(placeIndex > -1 && value[placeIndex] == VALID_CHARACTERS.Last())
				{
					placeIndex--;
				}
				if(placeIndex == -1)
				{
					for(int i = 0; i < value.Count; i++)
					{
						value[i] = VALID_CHARACTERS.First();
					}
					value.Insert(0, VALID_CHARACTERS.First());
				}
				else
				{
					int validCharacterIndex = Array.IndexOf(VALID_CHARACTERS, value[placeIndex]);
					value[placeIndex] = VALID_CHARACTERS[validCharacterIndex + 1];
					for(int i = placeIndex + 1; i < value.Count; i++)
					{
						value[i] = VALID_CHARACTERS.First();
					}
				}

				delta--;
			}
		}

		/// <summary>Increment a value.</summary>
		protected void Decrement(List<char> value, int delta)
		{
			if(delta < 0)
			{
				Increment(value, delta * -1);
				return;
			}

			while(delta > 0)
			{
				if(value.Count == 1 && value[0] == VALID_CHARACTERS.First())
					return;

				int placeIndex = value.Count - 1;
				while(placeIndex > -1 && value[placeIndex] == VALID_CHARACTERS.First())
				{
					placeIndex--;
				}
				if(placeIndex == -1)
				{
					for(int i = 0; i < value.Count; i++)
					{
						value[i] = VALID_CHARACTERS.Last();
					}
					value.RemoveAt(0);
				}
				else
				{
					int validCharacterIndex = Array.IndexOf(VALID_CHARACTERS, value[placeIndex]);
					value[placeIndex] = VALID_CHARACTERS[validCharacterIndex - 1];
					for(int i = placeIndex + 1; i < value.Count; i++)
					{
						value[i] = VALID_CHARACTERS.Last();
					}
				}

				delta--;
			}
		}

		/// <summary>Returns an independent copy of the value.</summary>
		protected List<char> CopyValue()
		{
			return value.Select(c => c).ToList();
		}
	}

	/// <summary>
	/// Counts "A", "B", "C", ..., "Z", "AA", "AB", ..., "AZ", "BA", "BB", ...
	/// </summary>
	public sealed class AlphabetCounter : ArbitraryCounter
	{
		/// <summary>
		/// Valid characters.
		/// </summary>
		public static readonly char[] CHARACTERS = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
		
		/// <inheritdoc/>
		public AlphabetCounter() : base(CHARACTERS)
		{
		}
		
		/// <summary>Increment counter.</summary>
		public static AlphabetCounter operator +(AlphabetCounter counter, int delta)
		{
			List<char> newValue = counter.CopyValue();
			counter.Increment(newValue, delta);
			AlphabetCounter newCounter = new AlphabetCounter();
			newCounter.SetValue(newValue);
			return newCounter;
		}

		/// <summary>Decrement counter.</summary>
		public static AlphabetCounter operator -(AlphabetCounter counter, int delta)
		{
			List<char> newValue = counter.CopyValue();
			counter.Decrement(newValue, delta);
			AlphabetCounter newCounter = new AlphabetCounter();
			newCounter.SetValue(newValue);
			return newCounter;
		}

		/// <summary>Increment counter by 1.</summary>
		public static AlphabetCounter operator ++(AlphabetCounter counter)
		{
			return counter + 1;
		}

		/// <summary>Decrement counter by 1.</summary>
		public static AlphabetCounter operator --(AlphabetCounter counter)
		{
			return counter - 1;
		}
	}
}
