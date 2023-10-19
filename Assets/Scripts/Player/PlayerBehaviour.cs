using Animators;
using AppData.Player;
using Interactors;
using Physics.Bodies;
using UnityEngine;

namespace Player
{
	public class PlayerBehaviour : MonoBehaviour, IPlayerBehaviour
	{
		private IPlayerData playerData;
		private IBody playerBody;
		private IAnimator playerAnimator;
		private IInteractor playerInteractor;
		
		private Vector2 movementDirection;
		public bool PressedInteract => Input.GetKeyDown(KeyCode.E);

		public void Inject(IPlayerData data, IBody body, IAnimator animator, IInteractor interactor)
		{
			playerData = data;
			playerBody = body;
			playerAnimator = animator;
			playerInteractor = interactor;
		}

		private void Update() => ProcessInput();
		private void FixedUpdate() => Move();
		private void LateUpdate() => UpdateAnimation();

		private void ProcessInput()
		{
			movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		}

		private void Move()
		{
			if (playerInteractor.IsInteracting) return;
			playerBody.SetVelocity(movementDirection * playerData.MoveSpeed);
		}

		private void UpdateAnimation()
		{
			var animHash = playerBody.Velocity == Vector2.zero ? "Idle" : "Walk";
			playerAnimator.PlayAnimation(Animator.StringToHash(animHash));
		}
	}
}