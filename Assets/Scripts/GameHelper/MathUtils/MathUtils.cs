using System;
using System.Collections.Generic;
using System.Text;

public class MathUtils
{

    public static int get32UID()
    {
        return BitConverter.ToInt32(Encoding.UTF8.GetBytes(System.Guid.NewGuid().ToString()), 0);
    }

    public static long get64UID()
    {
        return BitConverter.ToInt64(Encoding.UTF8.GetBytes(System.Guid.NewGuid().ToString()), 0);
    }


}

