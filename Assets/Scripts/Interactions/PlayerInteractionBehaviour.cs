using Player;
using Shopkeeper;
using UnityEngine;

namespace Interactions
{
	public class PlayerInteractionBehaviour : MonoBehaviour, IPlayerInteraction
	{
		public bool IsInteracting => !enabled;
		private IPlayer player;
		private float radius;
		private Collider2D[] hitColliders;
		const int maxColliders = 10;

		public void Inject(IPlayer behaviour, float interactionRadius)
		{
			player = behaviour;
			radius = interactionRadius;
			hitColliders = new Collider2D[maxColliders];
		}

		private void Update()
		{
			if (!player.PressedInteract) return;
			var interactable = FindClosestInteractable();
			if (interactable == null) return;
			enabled = false;
			interactable.OnInteract(player,() =>
			{
				interactable = null;
				enabled = true;
			});
		}
		
		private IShopkeeper FindClosestInteractable()
		{
			var count = Physics2D.OverlapCircleNonAlloc(transform.position, radius, hitColliders);

			IShopkeeper closest = null;
			var closestDistance = float.MaxValue;
			
			for (var i = 0; i < count; i++)
			{
				var interactable = hitColliders[i].GetComponent<IShopkeeper>();
				if (interactable == null) continue;
				var distance = Vector2.Distance(transform.position, hitColliders[i].transform.position);
				if (!(distance < closestDistance)) continue;
				closestDistance = distance;
				closest = interactable;
			}
    
			return closest;
		}

	}
}