using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.HexagonalSystem
{
	// TODO: Convert to Monobehaviour.
	/// <summary>
	/// Implementation following the guidelines at https://www.redblobgames.com/grids/hexagons/implementation.html
	/// </summary>
	[Serializable]
	public class Hexagon : IEquatable<Hexagon>
	{
		#region Properties
		public int Q { get; }
		public int R { get; }
		public int S { get; }

		public float Width
		{
			get { return Mathf.Sqrt(3) * _size; }
		}

		public float Height
		{
			get { return 2 * _size; }
		}
		#endregion

		#region Fields
		private static Vector3Int[] _directions =
	{
		new Vector3Int(1, 0, -1),
		new Vector3Int(1, -1, 0),
		new Vector3Int(0, -1, 1),
		new Vector3Int(-1, 0, 1),
		new Vector3Int(-1, 1, 0),
		new Vector3Int(0, 1, -1),
	};

		private readonly float _size = 1f;
		#endregion

		#region Constructors
		public Hexagon(int q, int r, int s)
		{
			if (q + r + s != 0)
				throw new Exception("Hexagon coordinates don't add up to 0.");

			Q = q;
			R = r;
			S = s;
		}
		#endregion

		#region Equality Methods
		public static bool operator ==(Hexagon a, Hexagon b)
		{
			return a.Q == b.Q && a.R == b.R && a.S == b.S;
		}

		public static bool operator !=(Hexagon a, Hexagon b)
		{
			return !(a == b);
		}

		public bool Equals(Hexagon other)
		{
			return this == other;
		}

		public override bool Equals(object o)
		{
			if (o == null)
				return false;

			Hexagon hexagon = o as Hexagon;

			return Equals(hexagon);
		}

		public override int GetHashCode()
		{
			int hashCode = -1013937445;
			hashCode = hashCode * -1521134295 + Q.GetHashCode();
			hashCode = hashCode * -1521134295 + R.GetHashCode();
			hashCode = hashCode * -1521134295 + S.GetHashCode();
			return hashCode;
		}
		#endregion

		#region Arithmetic Methos
		public static Hexagon Add(Hexagon a, Hexagon b)
		{
			return new Hexagon(a.Q + b.Q, a.R + b.R, a.S + b.S);
		}

		public static Hexagon Add(Hexagon a, Vector3Int vector)
		{
			return Add(a, new Hexagon(vector.x, vector.y, vector.z));
		}

		public static Hexagon Subtract(Hexagon a, Hexagon b)
		{
			return new Hexagon(a.Q - b.Q, a.R - b.R, a.S - b.S);
		}

		public static Hexagon Subtract(Hexagon a, Vector3Int vector)
		{
			return Subtract(a, new Hexagon(vector.x, vector.y, vector.z));
		}

		public static Hexagon Multiply(Hexagon a, int k)
		{
			return new Hexagon(a.Q * k, a.R * k, a.S * k);
		}
		#endregion

		#region Distance Methods
		public static int Length(Hexagon hexagon)
		{
			return Mathf.RoundToInt(Math.Abs(hexagon.Q) + Math.Abs(hexagon.R) + Math.Abs(hexagon.S) / 2);
		}

		public int Length()
		{
			return Length(this);
		}

		public static int Distance(Hexagon a, Hexagon b)
		{
			return Length(Subtract(a, b));
		}
		#endregion

		#region Neighbor Methods
		public Vector3Int Direction(int direction /* 0 to 5 */)
		{
			if (direction < 0 && direction >= 6)
				throw new ArgumentOutOfRangeException("Direction should be between 0 to 5.");

			return _directions[direction];
		}

		public static Hexagon Neighbour(Hexagon hexagon, int direction)
		{
			return Add(hexagon, hexagon.Direction(direction));
		}
		#endregion

		#region Position Conversion
		public Vector3 ToWorldPosition()
		{
			float x = _size * Mathf.Sqrt(3) * Q + Mathf.Sqrt(3) / 2 * R;
			float y = _size * 3 / 2 * R;

			return new Vector3(x, 0, y);
		}
		#endregion

		#region Convertors
		public Vector3Int ToVector3Int()
		{
			return new Vector3Int(Q, R, S);
		}
		#endregion
	} 
}