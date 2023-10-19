namespace Screens
{
	public interface IScreen
	{
		bool Visible { get; }
		void Show();
		void Hide();
	}
}