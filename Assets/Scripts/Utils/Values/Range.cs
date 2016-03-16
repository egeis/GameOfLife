﻿using System;

public class Range<T> where T: IComparable<T>
{
    public T Minimum { get; set; }
    public T Maximum { get; set; }

    public override string ToString()
    {
        return String.Format("[{0} to {1}]", Minimum, Maximum);
    }

    public bool IsValid()
    {
        return Minimum.CompareTo(Maximum) <= 0;
    }

    public bool ContainsValue(T value)
    {
        return (Minimum.CompareTo(value) <= 0) && (value.CompareTo(Maximum) <= 0);
    }
}
