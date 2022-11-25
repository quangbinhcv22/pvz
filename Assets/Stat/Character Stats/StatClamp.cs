using System;
using Kryz.CharacterStats;
using UnityEngine;

namespace Character_Stats
{
    [Serializable]
    public class StatClamp : CharacterStat
    {
        public StatClamp() : base()
        {
        }

        public StatClamp(float baseValue) : base(baseValue)
        {
        }

        public StatClamp(float baseValue, float disableValue) : base(baseValue, disableValue)
        {
        }


        private bool usingMinStat = false;
        private bool usingMaxStat = false;


        private float minNumber;

        public float MinNumber
        {
            get => minNumber;
            set
            {
                minNumber = value;
                usingMinStat = false;
            }
        }


        protected CharacterStat minStat;

        public CharacterStat MinStat
        {
            get => minStat;
            set
            {
                minStat = value;
                usingMinStat = true;
            }
        }


        private float maxNumber;

        public float MaxNumber
        {
            get => maxNumber;
            set
            {
                maxNumber = value;
                usingMaxStat = false;
            }
        }


        protected CharacterStat maxStat;

        public CharacterStat MaxStat
        {
            get => maxStat;
            set
            {
                maxStat = value;
                usingMaxStat = true;
            }
        }


        public float MinFinal => usingMinStat && minStat != null ? minStat.Value : minNumber;
        public float MaxFinal => usingMaxStat && maxStat != null ? maxStat.Value : maxNumber;


        public override float HandleFinalValue(float rawValue)
        {
            Debug.Log($"{usingMaxStat} - {maxStat} - {maxStat.Value}");
            
            
            return Mathf.Clamp(rawValue, MinFinal, MaxFinal);
        }
    }
}