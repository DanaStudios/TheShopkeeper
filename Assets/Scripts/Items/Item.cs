using System;
using BodyParts;
using UnityEngine;

namespace Items
{
	[Serializable]
	public class Item : IItem
	{
		[field: SerializeField] public BodyPartType BodyPartType { get; private set; }
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public string Description { get; private set; }
		[field: SerializeField] public Sprite Sprite { get; private set; }
		[field: SerializeField] public int Cost { get; private set; }
		[field: SerializeField] public int MaxStackCount { get; private set; }
		
		public bool Equipped { get; set; }

		public Item(BodyPartType bodyPartType, string name, string description, Sprite sprite, int cost,
			int maxStackCount)
		{
			BodyPartType = bodyPartType;
			Name = name;
			Description = description;
			Sprite = sprite;
			Cost = cost;
			MaxStackCount = maxStackCount;
		}
	}
}