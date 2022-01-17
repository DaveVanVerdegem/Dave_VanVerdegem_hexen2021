using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAE.StateSystem
{
	public interface IState<TState> where TState : IState<TState>
	{
		#region Properties
		StateMachine<TState> StateMachine { get; }
		#endregion

		#region Methods
		void OnEnter();
		void OnExit(); 
		#endregion
	}
}
