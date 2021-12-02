using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private GameObject _hexagonPrefab = null;
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

		foreach(Hexagon hexagon in _grid.Hexagons)
		{
			Instantiate(_hexagonPrefab, hexagon.ToWorldPosition(), Quaternion.identity, transform);
		}
	}
	#endregion

	#region Methods

	#endregion
}
