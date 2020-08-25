namespace StankUtilities.Runtime.ScriptableObjects.Variables
{
    [System.Serializable]
    public class FloatReference : INumericalVariable<float>
    {
        public bool UseConstant = true;
        public float ConstantValue = 0.0f;
        public FloatVariable Variable;

        public FloatReference() { }

        public FloatReference(float value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public float Value
        {
            get => UseConstant || Variable == null ? ConstantValue : Variable.RuntimeValue;
        }

        public void SetValue(float value)
        {
            if (UseConstant)
                ConstantValue = value;
            else
                Variable.SetValue(value);
        }

        public void SetValue(FloatVariable value)
        {
            SetValue(value.RuntimeValue);
        }

        public void IncrementValue(float amount)
        {
            SetValue(Value + amount);
        }

        public void IncrementValue(FloatVariable amount)
        {
            SetValue(Value + amount.RuntimeValue);
        }

        public void DecrementValue(float amount)
        {
            SetValue(Value - amount);
        }

        public void DecrementValue(FloatVariable amount)
        {
            SetValue(Value - amount.RuntimeValue);
        }
    }
}
