namespace DAE.ReplaySystem
{
	public interface IReplayCommand
	{
		void Forward();

		void Backward();
	}
}