using System;

using CodeBlaze.Detris.Input;

using IngameDebugConsole;

using UnityEngine;

namespace CodeBlaze.Detris.Debug {

    public class DebugController : MonoBehaviour {

        [SerializeField] private SwipeInputDetector _swipeInputDetector;
        [SerializeField] private GameObject _debugConsole;

        private void Awake() {
#if DEVELOPMENT_BUILD
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
#if DEVELOPMENT_BUILD
            DebugLogPopup.OnToggle -= DebugLogPopupOnToggle;
#endif
        }

    }

}