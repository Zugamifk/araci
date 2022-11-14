using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class RandomMapping
{
    static byte[] _buffer = new byte[8];
    static ulong _wBase = 0x2545F4914F6CDD1DUL, _xBase = 1181783497276652981UL, _yBase = 5932975029725095861UL, _zBase = 805073914753922748UL;
    static ulong _w = _wBase, _x = _xBase, _y = _yBase, _z = _zBase;
    static ulong _seed;

    public static Vector2 Vector2(int x, int y)
    {
        SetSeed((uint)x.GetHashCode());
        float xf = RandomFloat();
        SetSeed((uint)y.GetHashCode());
        float yf = RandomFloat();
        return new Vector2(xf, yf);
    }

    public static float RandomFloat()
    {
        FillBuffer(4);
        return BitConverter.ToSingle(_buffer);
    }

    public static int RandomInt()
    {
        FillBuffer(4);
        return BitConverter.ToInt32(_buffer);
    }

    public static ulong RandomIntLong()
    {
        FillBuffer(8);
        return BitConverter.ToUInt64(_buffer);
    }

    static void SetSeed(uint seed)
    {
        SetSeed(ref _x, ref _xBase, seed);
        SetSeed(ref _y, ref _yBase, seed);
        SetSeed(ref _z, ref _zBase, seed);
        SetSeed(ref _w, ref _wBase, seed);
    }

    static void SetSeed(ref ulong currentSeed, ref ulong baseSeed, uint seed)
    {
        currentSeed = (baseSeed ^ seed) ^ (seed << 17);
    }

    static unsafe void FillBuffer(uint length)
    {
        ulong x = _x, y = _y, z = _z, w = _w; 
        fixed (byte* pbytes = _buffer)
        {
            ulong* pbuf = (ulong*)(pbytes);
            ulong* pend = (ulong*)(pbytes + length);
            while (pbuf < pend)
            {
                ulong tx = x ^ (x << 11);
                ulong ty = y ^ (y << 11);
                ulong tz = z ^ (z << 11);
                ulong tw = w ^ (w << 11);
                *(pbuf++) = x = w ^ (w >> 19) ^ (tx ^ (tx >> 8));
                *(pbuf++) = y = x ^ (x >> 19) ^ (ty ^ (ty >> 8));
                *(pbuf++) = z = y ^ (y >> 19) ^ (tz ^ (tz >> 8));
                *(pbuf++) = w = z ^ (z >> 19) ^ (tw ^ (tw >> 8));
            }
        }
        _x = x; _y = y; _z = z; _w = w;
    }
}
