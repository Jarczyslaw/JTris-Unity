using UnityEngine;
using System.Collections;
using System;

public class Vector3i
{

    public int x, y, z;

    public static Vector3i operator +(Vector3i lhs, Vector3i rhs)
    {
        return new Vector3i(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
    }

    public static Vector3i operator +(Vector3i lhs, int rhs)
    {
        return new Vector3i(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
    }

    public static Vector3i operator *(Vector3i lhs, int rhs)
    {
        return new Vector3i(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
    }

    public static Vector3 operator *(Vector3i lhs, float rhs)
    {
        return new Vector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
    }

    public static Vector3i operator -(Vector3i arg)
    {
        return new Vector3i(-arg.x, -arg.y, -arg.z);
    }

    public Vector3i()
    {
        Set(0, 0, 0);
    }

    public Vector3i(int x, int y, int z)
    {
        Set(x, y, z);
    }

    public Vector3i(float x, float y, float z)
    {
        Set(x, y, z);
    }

    public Vector3i(Vector3 v)
    {
        Set(v.x, v.y, v.z);
    }

    public void Set(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void Set(Vector3i source)
    {
        Set(source.x, source.y, source.z);
    }

    public void Set(float x, float y, float z)
    {
        this.x = Mathf.RoundToInt(x);
        this.y = Mathf.RoundToInt(y);
        this.z = Mathf.RoundToInt(z);
    }

    public void Set(Vector3 v)
    {
        Set(v.x, v.y, v.z);
    }

    public Vector3 ToVector3()
    {
        return new Vector3((float)this.x, (float)this.y, (float)this.z);
    }

    public Vector3 Inverted()
    {
        return new Vector3(1.0f / this.x, 1.0f / this.y, 1.0f / this.z);
    }

    public Vector3i Negated()
    {
        return new Vector3i(-this.x, -this.y, -this.z);
    }

    public static Vector3i Clone(Vector3i source)
    {
        Vector3i newVect = new Vector3i(source.x, source.y, source.z);
        return newVect;
    }

    public bool Compare(Vector3i v)
    {
        if (x != v.x)
            return false;
        if (y != v.y)
            return false;
        if (z != v.z)
            return false;
        return true;
    }
}
