using System;

namespace Interactions
{
	public interface IInteractable
	{ 
		void Interact(Action callback);
	}
}