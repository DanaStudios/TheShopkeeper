using UnityEngine.UIElements;

namespace Screens
{
	public class ShopScreen : IScreen
	{
		public UIDocument UIDocument { get; }
		public bool Visible => UIDocument.enabled;
		public void Show() => UIDocument.enabled = true;
		public void Hide() => UIDocument.enabled = false;
		
		public ShopScreen(UIDocument uiDocument)
		{
			UIDocument = uiDocument;
			UIDocument.enabled = false;
		}
	}
}