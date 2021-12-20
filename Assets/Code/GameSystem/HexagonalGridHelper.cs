using DAE.HexenSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DAE.GameSystem
{
	[CreateAssetMenu(menuName = "DAE/Grid Helper")]
	public class HexagonalGridHelper : ScriptableObject
	{
		#region Inspector Fields
		public int GridRadius = 3;
		public HexagonTile HexagonPrefab = null;

		[Space]
		public Piece<HexagonTile> PlayerPiecePrefab = null;
		public Piece<HexagonTile> EnemyPiecePrefab = null;
		#endregion
	} 
}
