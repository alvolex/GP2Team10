using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Vector3SerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        Vector3 v3 = (Vector3)obj;
        info.AddValue("x",v3.x);
        info.AddValue("y",v3.y);
        info.AddValue("z",v3.z);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        Vector3 v3 = (Vector3)obj;
        v3.x = (float) info.GetValue("x", typeof(float));
        v3.y = (float) info.GetValue("y", typeof(float));
        v3.z = (float) info.GetValue("z", typeof(float));
        obj = v3;
        return obj;
    }
}

public class QuaternionSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        Quaternion quaternion = (Quaternion)obj;
        info.AddValue("x",quaternion.x);
        info.AddValue("y",quaternion.y);
        info.AddValue("z",quaternion.z);
        info.AddValue("w",quaternion.w);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        Quaternion quaternion = (Quaternion)obj;
        quaternion.x = (float) info.GetValue("x", typeof(float));
        quaternion.y = (float) info.GetValue("y", typeof(float));
        quaternion.z = (float) info.GetValue("z", typeof(float));
        quaternion.w = (float) info.GetValue("w", typeof(float));
        obj = quaternion;
        return obj;
    }
}
