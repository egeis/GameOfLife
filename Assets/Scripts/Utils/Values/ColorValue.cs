using UnityEngine;

public class ColorValue : System.Attribute
{
    private Color _value;

    public ColorValue(float r, float g, float b, float a)
    {
        _value = new Color(r, g, b, a);
    }

    public Color Value
    {
        get { return _value; }
    }
}