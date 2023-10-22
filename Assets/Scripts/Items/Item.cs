using System;
using UnityEngine;

namespace Items
{
	[Serializable]
	public class Item : IItem
	{
		[field: SerializeField] public ItemType ItemType { get; private set; }
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public string Description { get; private set; }
		[field: SerializeField] public Texture2D Icon { get; private set; }
		[field: SerializeField] public int Cost { get; private set; }
		[field: SerializeField] public int MaxStackCount { get; private set; }
	}
}