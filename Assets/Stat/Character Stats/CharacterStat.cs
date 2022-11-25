using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kryz.CharacterStats
{
    [Serializable]
    public class CharacterStat
    {
        public float baseValue;
        public float RuntimeBaseValue
        {
            get => baseValue;
            set
            {
                baseValue = value;
                IsDirty = true;
            }
        }


        public float DisableValue;

        private bool isDirty = true;

        protected bool IsDirty
        {
            get => isDirty;
            set
            {
                isDirty = value;
                if (isDirty) OnValueChanged.Invoke();
            }
        }

        protected float lastBaseValue;

        protected float _value;

        public virtual float Value
        {
            get
            {
                if (IsDirty || lastBaseValue != baseValue)
                {
                    lastBaseValue = baseValue;
                    _value = CalculateFinalValue();
                    IsDirty = false;
                }

                return _value;
            }
        }

        public UniqueAction OnValueChanged = new();

        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        protected readonly List<Disabler> disablers;
        public readonly ReadOnlyCollection<Disabler> Disablers;


        public CharacterStat()
        {
            statModifiers = new();
            StatModifiers = statModifiers.AsReadOnly();

            disablers = new();
            Disablers = disablers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this()
        {
            this.baseValue = baseValue;
        }

        public CharacterStat(float baseValue, float disableValue) : this(baseValue)
        {
            DisableValue = disableValue;
        }


        public virtual void AddDisabler(Disabler disabler)
        {
            IsDirty = true;

            disablers.Add(disabler);
        }

        public virtual bool RemoveDisabler(Disabler disabler)
        {
            if (disablers.Remove(disabler))
            {
                IsDirty = true;
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllDisablersFromSource(object source)
        {
            int numRemovals = disablers.RemoveAll(disabler => disabler.Source == source);

            if (numRemovals > 0)
            {
                IsDirty = true;
                return true;
            }

            return false;
        }


        public virtual void AddModifier(StatModifier mod)
        {
            IsDirty = true;

            statModifiers.Add(mod);
        }

        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                IsDirty = true;
                return true;
            }

            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

            if (numRemovals > 0)
            {
                IsDirty = true;
                return true;
            }

            return false;
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0; //if (a.Order == b.Order)
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0;

            statModifiers.Sort(CompareModifierOrder);

            if (disablers.Count > 0)
            {
                finalValue = DisableValue;
            }
            else
            {
                for (int i = 0; i < statModifiers.Count; i++)
                {
                    StatModifier mod = statModifiers[i];

                    if (mod.Type == StatModType.Flat)
                    {
                        finalValue += mod.Value;
                    }
                    else if (mod.Type == StatModType.PercentAdd)
                    {
                        sumPercentAdd += mod.Value;

                        if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                        {
                            finalValue *= 1 + sumPercentAdd;
                            sumPercentAdd = 0;
                        }
                    }
                    else if (mod.Type == StatModType.PercentMult)
                    {
                        finalValue *= 1 + mod.Value;
                    }
                }
            }

            // Workaround for float calculation errors, like displaying 12.00001 instead of 12
            return (float)Math.Round(HandleFinalValue(finalValue), 4);
        }


        public virtual float HandleFinalValue(float rawValue)
        {
            return rawValue;
        }
    }
}