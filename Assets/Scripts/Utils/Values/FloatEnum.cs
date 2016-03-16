using System;
using System.Reflection;

public class FloatEnum {
    public static float getFloatValue(Enum value)
    {
        Type type = value.GetType();
        FieldInfo fi = type.GetField(value.ToString());
        FloatValue[] attributes = fi.GetCustomAttributes(typeof(FloatValue), false) as FloatValue[];

        if (attributes != null)
            return attributes[0].Value;

        return 0f;
    }

}
