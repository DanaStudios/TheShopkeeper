using AppData.Character;

namespace AppData.Player
{
	public interface IPlayerData : ICharacterData
	{
		float MoveSpeed { get; }
		int StartingGold { get; }
	}
}