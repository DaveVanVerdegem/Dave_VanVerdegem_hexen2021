using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.HexagonalSystem
{
	[CreateAssetMenu(menuName = "DAE/Grid Helper")]
	public class HexagonalGridHelper : ScriptableObject
	{
		#region Inspector Fields
		public int GridRadius = 3;
		public HexagonTile HexagonPrefab = null;
		#endregion
	} 
}
