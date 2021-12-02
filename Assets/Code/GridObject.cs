using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
	#region Inspector Fields

	#endregion

	#region Properties

	#endregion

	#region Fields
	HexagonalGrid _grid = null;
	#endregion

	#region Life Cycle
	private void Start()
	{
		_grid = new HexagonalGrid();
		_grid.GenerateGrid(new Hexagon(0, 0, 0));
	}

	private void Update()
	{
	}
	#endregion

	#region Methods

	#endregion
}
