using System;
using Transactions;

namespace Shopkeeper
{
	public interface IShopkeeper : IBuyer, ISeller
	{ 
		void OnInteract(IBuyer player, Action action);
	}
}