using UnityEngine;

namespace Animators
{
	public class CharacterAnimator : IAnimator
	{
		private readonly Animator animator;
		private const int LayerIndex = 0;
		
		private AnimatorStateInfo CurrentAnimatorState => animator.GetCurrentAnimatorStateInfo(LayerIndex);
		private bool IsAnimationPlaying(int animHash) => CurrentAnimatorState.shortNameHash == animHash;

		public CharacterAnimator(Animator animator)
		{
			this.animator = animator;
		}
		
		public void PlayAnimation(int hash)
		{
			if (IsAnimationPlaying(hash)) return;
			animator.Play(hash);
		}
	}
}