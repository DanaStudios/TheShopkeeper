using System;
using Items;
using Transactions;
using UnityEngine;

namespace Commands
{
	public class SellCommand : ICommand
	{
		private readonly ISeller seller;
		private readonly IBuyer buyer;
		private readonly IItem item;
		private readonly int count;

		public SellCommand(ISeller seller, IBuyer buyer, IItem item, int count)
		{
			if (count <= 0) throw new Exception("[SellCommand] Invalid item count value!");
			
			this.seller = seller ?? throw new Exception("[SellCommand] Seller does not exist!");
			this.buyer = buyer ?? throw new Exception("[SellCommand] Buyer does not exist!");
			this.item = item ?? throw new Exception("[SellCommand] Item does not exist!");
			this.count = count;
		}
		
		public void Execute()
		{
			var totalCost = item.Cost * count;
			if (!buyer.CanAfford(totalCost))
			{
				Debug.LogWarning("[SellCommand] Buyer does not have enough gold!");
				return;
			}
			
			var items = seller.GetItems(item, count);
			buyer.Spend(totalCost);
			seller.Earn(totalCost);
			buyer.ReceiveItems(items);
			
			Debug.Log($"{buyer} bought {item.Name} (x{count}) for {totalCost} gold!");
		}

		public void Undo()
		{
			
		}
	}
}