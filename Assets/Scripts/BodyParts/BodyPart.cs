using Items;
using UnityEngine;

namespace BodyParts
{
	public class BodyPart : IBodyPart
	{
		public BodyPartType BodyPartType { get; }
		public IItem EquippedItem { get; private set; }
		private readonly SpriteRenderer spriteRenderer;
		
		public BodyPart(BodyPartType type, SpriteRenderer spriteRenderer, IItem startingItem)
		{
			BodyPartType = type;
			this.spriteRenderer = spriteRenderer;
			EquipItem(startingItem);
		}
		
		public void EquipItem(IItem item)
		{
			if (EquippedItem != null) EquippedItem.Equipped = false;
			spriteRenderer.sprite = item.Sprite;
			EquippedItem = item;
			EquippedItem.Equipped = true;
		}
	}
}