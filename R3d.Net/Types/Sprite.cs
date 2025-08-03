using System.Numerics;
using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Represents a 3D sprite with billboard rendering and animation support.
    /// 
    /// This structure defines a 3D sprite, which by default is rendered as a billboard around the Y-axis.
    /// The sprite supports frame-based animations and can be configured to use various billboard modes
    /// </summary>
    /// <remarks>
    /// The shadow mode does not handle transparency. If shadows are enabled, the entire quad will be rendered in the shadow map,
    /// potentially causing undesired visual artifacts for semi-transparent sprites
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Sprite
    {
        /// <summary>
        /// The material used for rendering the sprite, including its texture and shading properties
        /// </summary>
        public Material Material;

        /// <summary>
        /// The current animation frame, represented as a floating-point value to allow smooth interpolation
        /// </summary>
        public float CurrentFrame;

        /// <summary>
        /// The size of a single animation frame, in texture coordinates (width and height)
        /// </summary>
        public Vector2 FrameSize;

        /// <summary>
        /// The number of frames along the horizontal (X) axis of the texture
        /// </summary>
        public int XFrameCount;

        /// <summary>
        /// The number of frames along the vertical (Y) axis of the texture
        /// </summary>
        public int YFrameCount;
    }
}
