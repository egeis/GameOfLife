public class FloatValue : System.Attribute
{
    private float _value;

    public FloatValue(float value)
    {
        _value = value;
    }

    public float Value
    {
        get { return _value; }
    }
}