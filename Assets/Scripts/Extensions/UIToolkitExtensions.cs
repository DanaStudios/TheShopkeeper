using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Extensions
{
	public static class UIToolkitExtensions
	{
		public static VisualElement CreateVisualElement(string className)
		{
			var visualElement = new VisualElement();
			visualElement.AddToClassList(className);
			return visualElement;
		}
		
		public static Button CreateButton(string text, string className, VisualElement parent = null, 
			Action onClicked = null)
		{
			var button = new Button
			{
				text = text
			};
		    
			button.AddToClassList(className);
			button.SetParent(parent);
			if (onClicked != null) button.clicked += onClicked;
			return button;	
		}

		public static Label CreateLabel(string text, string className, VisualElement parent = null)
		{
			var label = new Label
			{
				text = text
			};
			
			label.AddToClassList(className);
			label.SetParent(parent);
			return label;
		}

		public static Image CreateImage(Texture texture, string className, VisualElement parent = null)
		{
			var image = new Image
			{
				image = texture
			};
			
			image.AddToClassList(className);
			image.SetParent(parent);
			return image;
		}

		public static T Find<T>(this VisualElement element, string selector) where T : class
		{
			var match = element.Q<VisualElement>(selector);
			return match as T;
		}
		
		public static void SetPickingModeRecursively(this VisualElement element, PickingMode mode)
		{
			element.pickingMode = mode;
			foreach (var child in element.Children())
			{
				SetPickingModeRecursively(child, mode);
			}
		}

		private static void SetParent(this VisualElement element, VisualElement parent)
		{
			parent?.Add(element);
		}
	}
}