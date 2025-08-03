using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Represents a keyframe in an interpolation curve.
    /// 
    /// A keyframe contains two values: the time at which the keyframe occurs and the value of the interpolation at that time.
    /// The time is normalized between 0.0 and 1.0, where 0.0 represents the start of the curve and 1.0 represents the end
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Keyframe
    {
        /// <summary>
        /// Normalized time of the keyframe, ranging from 0.0 to 1.0
        /// </summary>
        public float Time;

        /// <summary>
        /// The value of the interpolation at this keyframe
        /// </summary>
        public float Value;
    }
}
