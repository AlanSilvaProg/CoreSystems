using System;

[Serializable]
public class ObservableProperty<T>
{
    private T _value;
    public T Value { get { return _value; } set { _value = value; OnValueChangedWithReturn?.Invoke(value); OnValueChanged?.Invoke(); } }

    public event Action<T> OnValueChangedWithReturn;
    public event Action OnValueChanged;
}
