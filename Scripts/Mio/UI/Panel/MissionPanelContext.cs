using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;


namespace SCS.Mio
{
    public class MissionPanelContext: PanelContext
    {
        private MissionData SelectedMission;
        public Text TitleText;
        public Text DescriptionText;
        public Text StartButtonText;

        private LocalizedStringTable _table = new LocalizedStringTable { TableReference = "Adventure" };


        public override void LoadContext()
        {
            if(_table !=null)
            {
                Debug.Log(name + " Loaded");
                _table.TableChanged += LoadStrings;
            }
            
        }

        public override void UnloadContext()
        {
            if (_table != null)
            {
                Debug.Log(name + " Unloaded");
                _table.TableChanged -= LoadStrings;
            }
            Clear();
        }

        public void OnSelectMission(MissionData mission)
        {
            SelectedMission = mission;
            Debug.Log(mission.name);
        }

        private void LoadStrings(StringTable table)
        {
            if(SelectedMission != null)
            {
                Debug.Log(SelectedMission.ToString());
                TitleText.text = LocalizationManager.GetLocalizedString(table, SelectedMission.GeneralInformation.Title);
                DescriptionText.text = LocalizationManager.GetLocalizedString(table, SelectedMission.GeneralInformation.Description);
            }
            
        }

        private void Clear()
        {
            SelectedMission = null;
            TitleText.text = 
                DescriptionText.text =  Constants.MissingString;
        }

        public void StartMission()
        {
            //GameController.Instance.StartMission(SelectedMission);
        }
    }

}