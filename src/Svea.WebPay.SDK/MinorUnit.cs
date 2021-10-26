namespace Svea.WebPay.SDK
{
	using System;
	using System.Globalization;
	using System.Text.Json.Serialization;

	public class MinorUnit : IEquatable<MinorUnit>, IComparable<MinorUnit>, IComparable
	{
		private readonly decimal _decimalValue;
		private readonly long _inLowestMonetaryUnit;

		[JsonConstructor]
		public MinorUnit(decimal value)
		{
			_decimalValue = value;
			_inLowestMonetaryUnit = GetMonetaryUnit();
		}

		public MinorUnit(int value)
		{
			var convertedAmount = Convert.ToDecimal(value);
			_decimalValue = convertedAmount;
			_inLowestMonetaryUnit = GetMonetaryUnit();
		}


		public MinorUnit(long value)
		{
			var convertedAmount = Convert.ToDecimal(value);
			_decimalValue = convertedAmount;
			_inLowestMonetaryUnit = GetMonetaryUnit();
		}

		/// <summary>
		/// Gets the amount in a format suitable for api requests.
		/// </summary>
		public long InLowestMonetaryUnit => _inLowestMonetaryUnit;

		/// <summary>
		/// Returns the original amount passed in the constructor.
		/// </summary>
		/// <param name="value">The <seealso cref="MinorUnit"/> you want converted.</param>
		public static implicit operator decimal(MinorUnit value)
		{
			return value._decimalValue;
		}

        /// <summary>
        /// Converts a <seealso cref="decimal"/> to a <seealso cref="MinorUnit"/>
        /// </summary>
        /// <param name="value">The <seealso cref="decimal"/> you want converted.</param>
        public static implicit operator MinorUnit(decimal value)
        {
            return new MinorUnit(value);
        }

        /// <summary>
        /// Converts a <seealso cref="long"/> to a <seealso cref="MinorUnit"/>
        /// </summary>
        /// <param name="value">The <seealso cref="long"/> you want converted.</param>
        public static implicit operator MinorUnit(long value)
        {
            return new MinorUnit(value);
        }

        /// <summary>
        /// Adds the amounts in two <seealso cref="MinorUnit"/> instances together.
        /// </summary>
        /// <returns>A new <seealso cref="MinorUnit"/> with the amounts added together.</returns>
        public static MinorUnit operator +(MinorUnit a, MinorUnit b)
		{
			return new MinorUnit(a._decimalValue + b._decimalValue);
		}

		/// <summary>
		/// Subtracts two amounts in <seealso cref="MinorUnit"/> from each other.
		/// </summary>
		/// <param name="a">The left side parameter of the - operator.</param>
		/// <param name="b">The right side parameter of the - operator.</param>
		/// <returns>A new <seealso cref="MinorUnit"/> with the new sum.</returns>
		public static MinorUnit operator -(MinorUnit a, MinorUnit b)
		{
			return new MinorUnit(a._decimalValue - b._decimalValue);
		}

		/// <summary>
		/// Divides two amounts in <seealso cref="MinorUnit"/> from each other.
		/// </summary>
		/// <param name="a">The left side parameter of the / operator.</param>
		/// <param name="b">The right side parameter of the / operator.</param>
		/// <returns>A new <seealso cref="MinorUnit"/> with the new sum.</returns>
		public static MinorUnit operator /(MinorUnit a, MinorUnit b)
		{
			return new MinorUnit(a._decimalValue / b._decimalValue);
		}

		/// <summary>
		/// Multiplies two amounts in <seealso cref="MinorUnit"/> from each other.
		/// </summary>
		/// <param name="a">The left side parameter of the * operator.</param>
		/// <param name="b">The right side parameter of the * operator.</param>
		/// <returns>A new <seealso cref="MinorUnit"/> with the new sum.</returns>
		public static MinorUnit operator *(MinorUnit a, MinorUnit b)
		{
			return new MinorUnit(a._decimalValue * b._decimalValue);
		}

		/// <summary>
		/// Converts a <seealso cref="int"/> to a <seealso cref="MinorUnit"/>.
		/// </summary>
		/// <param name="valueInLowestMonetaryUnit">The amount.</param>
		/// <returns>A new <seealso cref="MinorUnit"/> based on the <paramref name="valueInLowestMonetaryUnit"/>.</returns>
		public static MinorUnit FromLowestMonetaryUnit(int valueInLowestMonetaryUnit)
		{
			var convertedAmount = (decimal)valueInLowestMonetaryUnit / 100;
			return new MinorUnit(convertedAmount);
		}


		/// <summary>
		/// Converts a <seealso cref="long"/> to a <seealso cref="MinorUnit"/>.
		/// </summary>
		/// <param name="valueInLowestMonetaryUnit">The amount.</param>
		/// <returns>A new <seealso cref="MinorUnit"/> based on the <paramref name="valueInLowestMonetaryUnit"/>.</returns>
		public static MinorUnit FromLowestMonetaryUnit(long valueInLowestMonetaryUnit)
		{
			var convertedAmount = (decimal)valueInLowestMonetaryUnit / 100;
			return new MinorUnit(convertedAmount);
		}

		public override string ToString()
		{
			return ToString("N2");
		}

		public string ToString(string format)
		{
			return _decimalValue.ToString(format, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns><inheritdoc/></returns>
		public int CompareTo(object obj)
		{
			if (obj is null)
			{
				return 1;
			}

			if (ReferenceEquals(this, obj))
			{
				return 0;
			}

			if (!(obj is MinorUnit))
			{
				throw new ArgumentException($"Object must be of type {nameof(MinorUnit)}");
			}

			return CompareTo((MinorUnit)obj);
		}


		public int CompareTo(MinorUnit other)
		{
			if (ReferenceEquals(this, other))
			{
				return 0;
			}

			if (other is null)
			{
				return 1;
			}

			return _decimalValue.CompareTo(other._decimalValue);
		}


		public bool Equals(MinorUnit other)
		{
			if (other is null)
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return _decimalValue == other._decimalValue;
		}

		public override bool Equals(object obj)
		{
			if (obj is null)
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return obj.GetType() == GetType() && Equals((MinorUnit)obj);
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns><inheritdoc/></returns>
		public override int GetHashCode()
		{
			return _decimalValue.GetHashCode();
		}


		/// <summary>
		/// Checks that two <seealso cref="MinorUnit"/> are equal.
		/// </summary>
		/// <returns>true if equal, false otherwise.</returns>
		public static bool operator ==(MinorUnit left, MinorUnit right)
		{
			if (left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		/// <summary>
		/// Checks that two <seealso cref="MinorUnit"/> are not equal.
		/// </summary>
		/// <returns>true if not equal, false otherwise.</returns>
		public static bool operator !=(MinorUnit left, MinorUnit right)
		{
			if (left is null)
			{
				return !(right is null);
			}

			return !left.Equals(right);
		}


		/// <summary>
		/// Checks if the first <seealso cref="MinorUnit"/> is smaller than the second.
		/// </summary>
		/// <param name="left">The first <seealso cref="MinorUnit"/> to check.</param>
		/// <param name="right">The second <seealso cref="MinorUnit"/> to check.</param>
		/// <returns>true if <paramref name="left"/> is smaller, false otherwise.</returns>
		public static bool operator <(MinorUnit left, MinorUnit right)
		{
			return left is null ? !(right is null) : left.CompareTo(right) < 0;
		}

		/// <summary>
		/// Checks if the <paramref name="left"/> is smaller or equal to <paramref name="right"/>.
		/// </summary>
		/// <param name="left">The first <seealso cref="MinorUnit"/> to check.</param>
		/// <param name="right">The second <seealso cref="MinorUnit"/> to check.</param>
		/// <returns>true if <paramref name="left"/> is smaller or equal to <paramref name="right"/>.</returns>
		public static bool operator <=(MinorUnit left, MinorUnit right)
		{
			return left is null || left.CompareTo(right) <= 0;
		}

		/// <summary>
		/// Checks if <paramref name="left"/> is larger than <paramref name="right"/>.
		/// </summary>
		/// <param name="left">The first <paramref name="left"/> to check.</param>
		/// <param name="right">The second <paramref name="right"/> to check.</param>
		/// <returns>true if <paramref name="left"/> is larger than <paramref name="right"/>, false otherwise.</returns>
		public static bool operator >(MinorUnit left, MinorUnit right)
		{
			return !(left is null) && left.CompareTo(right) > 0;
		}

		/// <summary>
		/// Checks if <paramref name="left"/> is larger than, or equal to<paramref name="right"/>.
		/// </summary>
		/// <param name="left">The first <paramref name="left"/> to check.</param>
		/// <param name="right">The second <paramref name="right"/> to check.</param>
		/// <returns>true if <paramref name="left"/> is larger than or equal to <paramref name="right"/>, false otherwise.</returns>
		public static bool operator >=(MinorUnit left, MinorUnit right)
		{
			return left is null ? right is null : left.CompareTo(right) >= 0;
		}

		private long GetMonetaryUnit()
		{
			var monetaryUnit = _decimalValue * 100;
			var longMinorUnit = Convert.ToInt64(monetaryUnit);
			return longMinorUnit;
		}
	}
}
