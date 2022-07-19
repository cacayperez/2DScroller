using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SCS.Mio
{
    public class PauseHandler : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitMissionButton;

        private void Awake()
        {
#if UNITY_EDITOR
            if (_pauseButton == null) Debug.LogWarning("Pause Button Not set");
            if (_resumeButton == null) Debug.LogWarning("Resume Button Not set");
            if (_exitMissionButton == null) Debug.LogWarning("Resume Button Not set");
#endif

            _pauseButton.onClick.AddListener(HandlePause);
            _resumeButton.onClick.AddListener(HandleResume);
            _exitMissionButton.onClick.AddListener(HandleExitMission);
        }

        private void HandleExitMission()
        {
            GameController.Instance.ExitMission();
        }

        private void HandlePause()
        {
            GameController.Instance.PauseGame();
        }

        private void HandleResume()
        {
            GameController.Instance.ResumeGame();
        }


    }

}