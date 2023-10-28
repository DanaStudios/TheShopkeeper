namespace Factories
{
	public interface IFactory<out T>
	{
		T Create();
	}
}