using System;
using Interactions;
using Transactions;

namespace Shopkeeper
{
	public interface IShopkeeper
	{ 
		void OnInteract(IBuyer buyer, Action callback);
	}
}