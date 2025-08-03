using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Represents a skeletal animation for a model.
    /// 
    /// This structure holds the animation data for a skinned model,
    /// including per-frame bone transformation poses.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ModelAnimation
    {
        /// <summary>
        /// Number of bones in the skeleton affected by this animation
        /// </summary>
        public int BoneCount;

        /// <summary>
        /// Total number of frames in the animation sequence
        /// </summary>
        public int FrameCount;

        /// <summary>
        /// Array of bone metadata (name, parent index, etc.) that defines the skeleton hierarchy
        /// </summary>
        public BoneInfo* Bones;

        /// <summary>
        /// 2D array of transformation matrices: [frame][bone].
        /// Each matrix represents the pose of a bone in a specific frame, typically in local space.
        /// </summary>
        public Matrix4x4** FramePoses;

        /// <summary>
        /// Name identifier for the animation (e.g., "Walk", "Jump", etc.)
        /// </summary>
        public fixed sbyte Name[32];
    }
}
