using UnityEngine;

namespace SCS.Mio
{
    [CreateAssetMenu(fileName = "Character", menuName = "Mio/CharacterData/Default")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private string CharacterName;

        [SerializeField] private float HealthModifier = 1.0f;
        [SerializeField] private float Level;
        [SerializeField] private int BaseHealth;

        [SerializeField] private Stat<int> CurrentExp;
        [SerializeField] private Stat<int> Strength;
        [SerializeField] private Stat<int> Dexterity;
        [SerializeField] private Stat<int> Intelligence;
        [SerializeField] private Stat<int> Vitality;

    
        private Stat<int> _health;
        public Stat<int> Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
            }
        }

        private void Awake()
        {
            CalculateHealth();
        }


        private void CalculateHealth()
        {
            float addedMax = Level * (Vitality.Current * HealthModifier);
            float max = BaseHealth + addedMax;

            _health.Max = (int)max;

            _health.Current = Health.Max;
        }

        public void IncreaseCurrentHealth(int byValue)
        {
            _health.Current = this._health.Current + byValue;
            if (_health.Current > _health.Max) _health.Current = _health.Max;
        }

        public void ReduceCurrentHealth(int byValue)
        {
            _health.Current = this._health.Current - byValue;
            if (_health.Current < _health.Min) _health.Current = _health.Min;
        }
    }

}