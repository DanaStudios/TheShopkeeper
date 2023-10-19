using Interactions;
using Player;
using UnityEngine;

namespace Interactors
{
	public class PlayerInteractBehaviour : MonoBehaviour, IInteractor
	{
		private IPlayerBehaviour playerBehaviour;
		private CircleCollider2D circleCollider;
		private IInteractable interactable;

		public bool IsInteracting => !circleCollider.enabled;

		public void Inject(IPlayerBehaviour behaviour, CircleCollider2D col)
		{
			playerBehaviour = behaviour;
			circleCollider = col;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			other.TryGetComponent(out IInteractable newInteractable);
			if (newInteractable == null) return;
			interactable = newInteractable;
		}

		private void Update()
		{
			if (!playerBehaviour.PressedInteract) return;
			if (interactable == null) return;
			circleCollider.enabled = false;
			interactable.Interact(() =>
			{
				interactable = null;
				circleCollider.enabled = true;
			});
		}
	}
}