using UnityEngine;

namespace AppData.Shopkeeper
{
	public interface IShopkeeperData
	{
		string Name { get; }
		string DialogueText { get; }
		Texture2D ProfilePicture { get; }
	}
}