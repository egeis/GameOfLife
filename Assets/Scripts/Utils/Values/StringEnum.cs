using System;
using System.Reflection;

public class StringEnum
{
    public static string getStringValue(Enum value)
    {
        Type type = value.GetType();
        FieldInfo fi = type.GetField(value.ToString());
        StringValue[] attributes = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Value;
        
        return null;
    }
	
}
