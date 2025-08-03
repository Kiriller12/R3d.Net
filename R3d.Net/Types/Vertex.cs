﻿using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Represents a vertex and all its attributes for a mesh
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Vertex
    {
        /// <summary>
        /// The 3D position of the vertex in object space
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The 2D texture coordinates (UV) for mapping textures
        /// </summary>
        public Vector2 Texcoord;

        /// <summary>
        /// The normal vector used for lighting calculations
        /// </summary>
        public Vector3 Normal;

        /// <summary>
        /// Vertex color, typically RGBA
        /// </summary>
        public Color Color;

        /// <summary>
        /// The tangent vector, used in normal mapping (often with a handedness in w)
        /// </summary>
        public Vector4 Tangent;

        /// <summary>
        /// Indices of up to 4 bones that influence this vertex (for GPU skinning)
        /// </summary>
        public fixed int BoneIds[4];

        /// <summary>
        /// Corresponding bone weights (should sum to 1.0). Defines the influence of each bone
        /// </summary>
        public fixed float Weights[4];
    }
}
