using Animators;
using AppData.Player;
using Physics.Bodies;
using UnityEngine;

namespace Player
{
	public class PlayerControllerBehaviour : MonoBehaviour
	{
		private IPlayerData playerData;
		private IBody playerBody;
		private IAnimator playerAnimator;
		private Vector2 movementDirection;

		public void Inject(IPlayerData data, IBody body, IAnimator animator)
		{
			playerData = data;
			playerBody = body;
			playerAnimator = animator;
		}

		private void Update() => ProcessInput();
		private void FixedUpdate() => Move();
		private void LateUpdate() => UpdateAnimation();

		private void ProcessInput()
		{
			movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		}
		
		private void Move() => playerBody.SetVelocity(movementDirection * playerData.MoveSpeed);

		private void UpdateAnimation()
		{
			var animHash = playerBody.Velocity == Vector2.zero ? "Idle" : "Walk";
			playerAnimator.PlayAnimation(Animator.StringToHash(animHash));
		}
	}
}