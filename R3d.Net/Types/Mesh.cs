using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Represents a mesh with its geometry data and GPU buffers.
    /// 
    /// Contains vertex/index data, GPU buffer handles, and bounding volume
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Mesh
    {
        /// <summary>
        /// Pointer to the array of vertices
        /// </summary>
        public Vertex* Vertices;

        /// <summary>
        /// Pointer to the array of indices
        /// </summary>
        public uint* Indices;

        /// <summary>
        /// Number of vertices
        /// </summary>
        public int VertexCount;

        /// <summary>
        /// Number of indices
        /// </summary>
        public int IndexCount;

        /// <summary>
        /// Vertex Buffer Object (GPU handle)
        /// </summary>
        public uint Vbo;

        /// <summary>
        /// Element Buffer Object (GPU handle)
        /// </summary>
        public uint Ebo;

        /// <summary>
        /// Vertex Array Object (GPU handle)
        /// </summary>
        public uint Vao;

        /// <summary>
        /// Cached animation matrices for all passes
        /// </summary>
        public Matrix4x4* BoneMatrices;

        /// <summary>
        /// Axis-Aligned Bounding Box in local space
        /// </summary>
        public BoundingBox Aabb;
    }
}
