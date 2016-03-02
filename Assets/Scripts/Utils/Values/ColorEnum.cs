using System;
using System.Reflection;
using UnityEngine;

public class ColorEnum {
    public static Color getColorValue(Enum value)
    {
        Type type = value.GetType();
        FieldInfo fi = type.GetField(value.ToString());
        ColorValue[] attributes = fi.GetCustomAttributes(typeof(ColorValue), false) as ColorValue[];

        if (attributes != null)
            return attributes[0].Value;

        return new Color(0f,0f,0f,0f);
    }
}
