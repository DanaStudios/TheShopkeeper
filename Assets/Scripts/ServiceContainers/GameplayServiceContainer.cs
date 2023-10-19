using Animators;
using AppData.Player;
using Physics.Bodies;
using Player;
using UnityEngine;

namespace ServiceContainers
{
	[DefaultExecutionOrder(-9999)]
	public class GameplayServiceContainer : MonoBehaviour
	{
		[SerializeField] private PlayerData playerData;
		[SerializeField] private PlayerControllerBehaviour playerPrefab;

		private void Awake()
		{
			var playerController = Instantiate(playerPrefab);
			var rb = playerController.transform.GetComponentInChildren<Rigidbody2D>();
			var animator = playerController.transform.GetComponentInChildren<Animator>();
			var playerBody = new PlayerBody(rb);
			var playerAnimator = new CharacterAnimator(animator);
			playerController.Inject(playerData, playerBody, playerAnimator);
		}
	}
}