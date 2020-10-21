using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Manager class for the debug popup
namespace IngameDebugConsole
{
	public class DebugLogPopup : MonoBehaviour, IPointerClickHandler {
		public static event EventHandler<ToggleEventArgs> OnToggle; 
		
		private RectTransform popupTransform;

		// Dimensions of the popup divided by 2
		private Vector2 halfSize;

		// Background image that will change color to indicate an alert
		private Image backgroundImage;

		// Canvas group to modify visibility of the popup
		private CanvasGroup canvasGroup;

#pragma warning disable 0649
		[SerializeField]
		private DebugLogManager debugManager;

		[SerializeField]
		private Text newInfoCountText;
		[SerializeField]
		private Text newWarningCountText;
		[SerializeField]
		private Text newErrorCountText;
		
		[SerializeField]
		private Color alertColorWarning;
		[SerializeField]
		private Color alertColorError;
#pragma warning restore 0649

		// Number of new debug entries since the log window has been closed
		private int newInfoCount = 0, newWarningCount = 0, newErrorCount = 0;

		private Color normalColor;

		void Awake()
		{
			popupTransform = (RectTransform) transform;
			backgroundImage = GetComponent<Image>();
			canvasGroup = GetComponent<CanvasGroup>();

			normalColor = backgroundImage.color;
		}

		public void NewInfoLogArrived()
		{
			newInfoCount++;
			newInfoCountText.text = newInfoCount.ToString();

			if( newWarningCount == 0 && newErrorCount == 0 )
				backgroundImage.color = normalColor;
		}

		public void NewWarningLogArrived()
		{
			newWarningCount++;
			newWarningCountText.text = newWarningCount.ToString();

			if( newErrorCount == 0 )
				backgroundImage.color = alertColorWarning;
		}

		public void NewErrorLogArrived()
		{
			newErrorCount++;
			newErrorCountText.text = newErrorCount.ToString();

			backgroundImage.color = alertColorError;
		}

		// Popup is clicked
		public void OnPointerClick( PointerEventData data )
		{
			debugManager.ShowLogWindow();
		}

		// Hides the log window and shows the popup
		public void Show()
		{
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
			canvasGroup.alpha = 1f;

			OnToggle?.Invoke(this, new ToggleEventArgs {
				State = true
			});
		}

		// Hide the popup
		public void Hide()
		{
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
			canvasGroup.alpha = 0f;
			
			OnToggle?.Invoke(this, new ToggleEventArgs {
				State = false
			});
		}
		
		public class ToggleEventArgs : EventArgs {

			public bool State { get; set; }

		}
	}

}