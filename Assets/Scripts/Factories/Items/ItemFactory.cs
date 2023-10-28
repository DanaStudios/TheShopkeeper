using Items;

namespace Factories.Items
{
	public class ItemFactory : IFactory<IItem>
	{
		private readonly Item item;

		public ItemFactory(Item item)
		{
			this.item = item;
		}
		
		public IItem Create()
		{
			return new Item(item.BodyPartType, item.Name, item.Description, item.Sprite, item.Cost, item.MaxStackCount);
		}
	}
}