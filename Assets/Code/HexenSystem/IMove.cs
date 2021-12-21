using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DAE.HexenSystem
{
	interface IMove<TPosition> where TPosition : MonoBehaviour, ITile
	{

		bool CanExecute(Piece<TPosition> piece);
		
		void Execute(Piece<TPosition> piece, TPosition position);

		List<TPosition> Positions(Piece<TPosition> piece);
	}
}
