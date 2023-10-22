using Extensions;
using Player;
using Shopkeeper;
using UnityEngine;

namespace Interactions
{
	public class PlayerInteractionBehaviour : MonoBehaviour, IPlayerInteraction
	{
		public bool IsInteracting => !enabled;
		public IShopkeeper LastInteracted { get; private set; }
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
			var closest = transform.FindClosest<IShopkeeper>(radius, hitColliders);
			if (closest == null) return;
			enabled = false;
			LastInteracted = closest;
			closest.OnInteract(player,() =>
			{
				closest = null;
				enabled = true;
			});
		}
	}
}