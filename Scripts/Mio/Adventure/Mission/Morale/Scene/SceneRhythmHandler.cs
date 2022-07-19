using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCS.Koreo;
using SonicBloom.Koreo;

namespace SCS.Mio.Scene
{
    public class SceneRhythmHandler : KoreoRhythm
    {
        #region private variables
        [SerializeField] private SceneNodeSpawner _spawner;
        [SerializeField] private NotePositionHandler _positionHandler;
        [SerializeField] [EventID] private string _loopCheckEventID;
        private IComboCounter _comboCounter;
        private bool _showNodes = false;
        #endregion private variables

        #region public functions

        public NotePositionHandler PositionHandler { get { return _positionHandler; } }
        public IComboCounter ComboCounter { get { return _comboCounter; } }

        private void Loop(KoreographyEvent koreoEvent)
        {
            LoopTrack();
        }

        private void ShowNodes()
        {
            KoreographyTrackBase rhythmTrack = _playingKoreography.GetTrackByID(_eventID);
            List<KoreographyEvent> rawEvents = rhythmTrack.GetAllEvents();

            for (int i = 0; i < rawEvents.Count; ++i)
            {
                KoreographyEvent evt = rawEvents[i];

                _spawner.AddEvent(evt);
            }
        }

        private void ClearNodes()
        {
            _spawner.Clear();
        }

        public override void OnInitialize()
        {
            _spawner = GetComponent<SceneNodeSpawner>();
            _spawner ??= gameObject.AddComponent(typeof(SceneNodeSpawner)) as SceneNodeSpawner;
            _spawner.RhythmHandler = this;

            _positionHandler = GetComponent<NotePositionHandler>();
            _positionHandler ??= gameObject.AddComponent(typeof(NotePositionHandler)) as NotePositionHandler;

            _comboCounter = GetComponent<ComboCounter>();
            _comboCounter = gameObject.AddComponent(typeof(ComboCounter)) as ComboCounter;

            Koreographer.Instance.RegisterForEvents(_loopCheckEventID, Loop);
        }


        public void LoopTrack()
        {
            ClearNodes();
            ShowNodes();
        }

        public override void OnRestart()
        {
            LoopTrack();
        }

        public void FreePosition(int index)
        {
            _spawner.FreePosition(index);
        }

        public void ToggleNodes()
        {
            _showNodes = !_showNodes;

            if(_showNodes == true)
            {
                ShowNodes();
            }
            else
            {
                ClearNodes();
            }
        }

        #endregion public functions
    }
}
