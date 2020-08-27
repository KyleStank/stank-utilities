namespace StankUtilities.Runtime.ScriptableObjects.Variables
{
    /// <summary>
    /// Responsible for creating a reference to easily access a Variable in the Unity Inspector.
    /// </summary>
    [System.Serializable]
    public abstract class Reference<T1, T2> where T1 : Variable<T2>
    {
        /// <summary>
        /// If true, the reference uses a hard-coded constant as the value. Otherwise, use the Variable's runtime value as the value.
        /// </summary>
        public bool UseConstant = true;

        /// <summary>
        /// Reference to Variable.
        /// </summary>
        public T1 Variable = null;

        [UnityEngine.SerializeField]
        private T2 m_ConstantValue = default;

        /// <summary>
        /// The value.
        /// </summary>
        public T2 Value
        {
            get => UseConstant || Variable == null ? m_ConstantValue : Variable.RuntimeValue;
            set
            {
                // Update the constant value or the Variable based on which we are using.
                if (UseConstant || Variable == null)
                    m_ConstantValue = value;
                else
                    Variable.RuntimeValue = value;
            }
        }

        /// <summary>
        /// Creates a basic Reference.
        /// </summary>
        public Reference() { }

        /// <summary>
        /// Creates a Reference with a specified constant value.
        /// </summary>
        /// <param name="value">Constant value.</param>
        public Reference(T2 value)
        {
            UseConstant = true;
            m_ConstantValue = value;
        }
    }
}
