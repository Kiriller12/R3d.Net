using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Represents an interpolation curve composed of keyframes.
    /// 
    /// This structure contains an array of keyframes and metadata about the array, such as the current number of keyframes
    /// and the allocated capacity. The keyframes define a curve that can be used for smooth interpolation between values
    /// over a normalized time range (0.0 to 1.0)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct InterpolationCurve
    {
        /// <summary>
        /// Dynamic array of keyframes defining the interpolation curve
        /// </summary>
        public Keyframe* Keyframes;

        /// <summary>
        /// Allocated size of the keyframes array
        /// </summary>
        public uint Capacity;

        /// <summary>
        /// Current number of keyframes in the array
        /// </summary>
        public uint Count;
    }
}
