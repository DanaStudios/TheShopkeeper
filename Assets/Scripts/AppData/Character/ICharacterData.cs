using UnityEngine;

namespace AppData.Character
{
	public interface ICharacterData
	{
		string Name { get; }
		Texture2D ProfilePicture { get; }
	}
}