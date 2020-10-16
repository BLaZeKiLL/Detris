using CodeBlaze.Detris.Input;

using IngameDebugConsole;

using UnityEngine;

namespace CodeBlaze.Detris.Console {

    public class DebugConsole : MonoBehaviour {

        [SerializeField] private SwipeInputDetector _swipeInputDetector;	
        [SerializeField] private GameObject _debugConsole;	

        private void Awake() {	
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            Instantiate(_debugConsole, transform);	

            DebugLogPopup.OnToggle += DebugLogPopupOnToggle;	
#else	
            Destroy(gameObject);
#endif	
        }	

        private void DebugLogPopupOnToggle(object sender, DebugLogPopup.ToggleEventArgs e) {	
            _swipeInputDetector.Active = e.State;	
        }	

        private void OnDestroy() {	
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            DebugLogPopup.OnToggle -= DebugLogPopupOnToggle;	
#endif	
        }

    }

}