using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace R3d.Net.Types
{
    /// <summary>
    /// Represents a complete 3D model with meshes and materials.
    /// 
    /// Contains multiple meshes and their associated materials, along with bounding information
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Model
    {
        /// <summary>
        /// Array of meshes composing the model
        /// </summary>
        public Mesh* Meshes;

        /// <summary>
        /// Array of materials used by the model
        /// </summary>
        public Material* Materials;

        /// <summary>
        /// Array of material indices, one per mesh
        /// </summary>
        public int* MeshMaterials;

        /// <summary>
        /// Number of meshes
        /// </summary>
        public int MeshCount;

        /// <summary>
        /// Number of materials
        /// </summary>
        public int MaterialCount;

        /// <summary>
        /// Axis-Aligned Bounding Box encompassing the whole model
        /// </summary>
        public BoundingBox Aabb;

        /// <summary>
        /// Array of offset (inverse bind) matrices, one per bone.
        /// 
        /// Transforms mesh-space vertices to bone space. Used in skinning
        /// </summary>
        public Matrix4x4* BoneOffsets;

        /// <summary>
        /// Bones information (skeleton). Defines the hierarchy and names of bones
        /// </summary>
        public BoneInfo* Bones;

        /// <summary>
        /// Number of bones
        /// </summary>
        public int BoneCount;

        /// <summary>
        /// Pointer to the currently assigned animation for this model (optional)
        /// </summary>
        public ModelAnimation* Anim;

        /// <summary>
        /// Current animation frame index. Used for sampling bone poses from the animation
        /// </summary>
        public int AnimFrame;
    }
}
