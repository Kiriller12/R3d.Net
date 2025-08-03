using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Represents a particle in a 3D particle system, with properties
    /// such as position, velocity, rotation, and color modulation
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Particle
    {
        /// <summary>
        /// Duration of the particle's existence in seconds
        /// </summary>
        public float Lifetime;

        /// <summary>
        /// The particle's current transformation matrix in 3D space
        /// </summary>
        public Matrix4x4 Transform;

        /// <summary>
        /// The current position of the particle in 3D space
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The current rotation of the particle in 3D space (Euler angles)
        /// </summary>
        public Vector3 Rotation;

        /// <summary>
        /// The current scale of the particle in 3D space
        /// </summary>
        public Vector3 Scale;

        /// <summary>
        /// The current color of the particle, representing its color modulation
        /// </summary>
        public Color Color;

        /// <summary>
        /// The current velocity of the particle in 3D space
        /// </summary>
        public Vector3 Velocity;

        /// <summary>
        /// The current angular velocity of the particle in radians (Euler angles)
        /// </summary>
        public Vector3 AngularVelocity;

        /// <summary>
        /// The initial scale of the particle in 3D space
        /// </summary>
        public Vector3 BaseScale;

        /// <summary>
        /// The initial velocity of the particle in 3D space
        /// </summary>
        public Vector3 BaseVelocity;

        /// <summary>
        /// The initial angular velocity of the particle in radians (Euler angles)
        /// </summary>
        public Vector3 BaseAngularVelocity;

        /// <summary>
        /// The initial opacity of the particle, ranging from 0 (fully transparent) to 255 (fully opaque)
        /// </summary>
        public byte BaseOpacity;
    }
}
