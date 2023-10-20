using System;
using UnityEngine;

namespace AppData.Shopkeeper
{
	[Serializable]
	public class ShopkeeperData : IShopkeeperData
	{
		 [field: SerializeField] public string Name { get; private set; }
		 [field: SerializeField] public string DialogueText { get; private set; }
		 [field: SerializeField] public Texture2D ProfilePicture { get; private set; }
	}
}