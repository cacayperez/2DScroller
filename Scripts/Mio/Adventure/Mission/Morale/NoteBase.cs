using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SCS.Mio.Scene;

namespace SCS.Mio.Mission.Morale
{
    public enum ENoteState
    {
        Disabled,
        Enabled,
        Hit_OK,
        Hit_Good,
        Hit_Great,
        Miss
    }
    public interface INoteBase
    {
        public bool IsHittable { get; }
        public Scene.SceneRhythmHandler RhythmHandler { get; set; }
        public void Initialize(KoreographyEvent koreoEvent, FPositionData positionData);
        public void TakeHit();
    }

    public interface INoteVisual
    {
        public float Modifier { get; set; }
        public void Begin();
        public void End();
    }

    public class NoteBase : MonoBehaviour, INoteBase
    {
        #region private variables
        [SerializeField] private NoteRing _ring;
        [SerializeField] private NoteBody _body;
        private bool _isHit = false;
        private KoreographyEvent _trackedEvent;
        private Scene.SceneRhythmHandler _rhythmHandler;
        private float _curveValue;
        private int _noteTime;
        private int _curTime;
        private int _sampleTime;
        private ENoteState _state;
        private int _positionIndex;
        #endregion private variables

        #region getters/setters
        public bool IsHittable => CheckIfHittable();
        public SceneRhythmHandler RhythmHandler { get { return _rhythmHandler; } set { _rhythmHandler = value; } }
        #endregion getters/setters

        #region private functions
        private bool CheckIfHittable()
        {
            int hitWindow = _rhythmHandler.HitWindowSampleWidth;
            if (_sampleTime <= hitWindow) return true;
            else return false;
        }

        private void Update()
        {
            if (_trackedEvent == null) return;
            if (_rhythmHandler == null) return;

            UpdateSampleTimes();
            UpdateCurveValue();
            UpdateVisuals();
        }

        private void UpdateSampleTimes()
        {
            _noteTime = _trackedEvent.StartSample;
            _curTime = _rhythmHandler.DelayedSampleTime;
            _sampleTime = Mathf.Abs(_noteTime - _curTime);
        }

        private void UpdateCurveValue()
        {
            if (_trackedEvent.HasCurvePayload() == false) return;
            _curveValue = _trackedEvent.GetValueOfCurveAtTime(_curTime);

            if (_curveValue.Equals(1.0f))
            {
                StopVisuals();
                _trackedEvent = null;
                
                return;
            }

            HandleOnCurveChange();
        }

        private void UpdateVisuals()
        {
            if(_body != null)
            _body.Modifier = _curveValue;
        
            if(_ring != null)
            _ring.Modifier = _curveValue;
        }

        private void InitializeVisuals()
        {
            _isHit = false;
            _body?.Begin();
            _ring?.Begin();
        }
        private void StopVisuals()
        {
            _body?.End();
            _ring?.End();

            ProcessCombo();
            _rhythmHandler.FreePosition(_positionIndex);
            gameObject.SetActive(false);
        }

        private void HandleOnCurveChange()
        {
            float val = 2 - _curveValue;
            _ring.transform.localScale = new Vector3(val, val, 1);
        }

        public void ProcessCombo()
        {
            if(_isHit == true)
            {
                _rhythmHandler.ComboCounter.IncrementCombo();
            } else
            {
                _rhythmHandler.ComboCounter.ResetCombo();
            }
        }
        #endregion private functions

        #region public functions

        public virtual void Initialize(KoreographyEvent koreoEvent, FPositionData positionData)
        {
            _trackedEvent = koreoEvent;
            transform.position = positionData.position;
            _positionIndex = positionData.key;
            InitializeVisuals();
            gameObject.SetActive(true);
            _state = ENoteState.Enabled;
        }

        public void TakeHit()
        {
            _ring?.End();
            _isHit = true;
            ProcessCombo();
        }
        #endregion public functions
    }
}