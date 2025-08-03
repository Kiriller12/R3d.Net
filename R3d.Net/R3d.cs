using System.Numerics;
using System.Security;

namespace R3d.Net
{
    /// <summary>
    /// R3d wrapper methods
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public static unsafe partial class R3d
    {
        #region Drawing Functions

        /// <summary>
        /// Draws a mesh with a specified material and transformation.
        /// 
        /// This function renders a mesh with the provided material and transformation matrix
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="material">A reference to the material to apply to the mesh</param>
        /// <param name="transform">The transformation matrix to apply to the mesh</param>
        public static void DrawMesh(ref Types.Mesh mesh, ref Types.Material material, Matrix4x4 transform)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            {
                fixed (Types.Material* materialPointer = &material)
                {
                    DrawMesh(meshPointer, materialPointer, transform);
                }
            }
        }

        /// <summary>
        /// Draws a mesh with a specified material and transformation.
        /// 
        /// This function renders a mesh with the provided transformation matrix
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="transform">The transformation matrix to apply to the mesh</param>
        public static void DrawMesh(ref Types.Mesh mesh, Matrix4x4 transform)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            {
                DrawMesh(meshPointer, null, transform);
            }
        }

        #endregion

        #region Mesh Functions

        /// <summary>
        /// Free mesh data from both RAM and VRAM.
        /// 
        /// Releases all memory associated with a mesh, including vertex data in RAM
        /// and GPU buffers (VAO, VBO, EBO) if the mesh was uploaded to VRAM.
        /// After calling this function, the mesh should not be used
        /// </summary>
        /// <param name="mesh">Pointer to the mesh structure to be freed</param>
        public static void UnloadMesh(ref Types.Mesh mesh)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            {
                UnloadMesh(meshPointer);
            }
        }

        #endregion

        #region Material Functions

        /// <summary>
        /// Unload a material and its associated textures.
        /// 
        /// Frees all memory associated with a material, including its textures.
        /// This function will unload all textures that are not default textures.
        /// </summary>
        /// 
        /// <remarks>
        /// Only call this function if you are certain that the textures
        /// are not shared with other materials or objects, as this will permanently
        /// free the texture data
        /// </remarks>
        /// <param name="material">Pointer to the material structure to be unloaded</param>
        public static void UnloadMaterial(ref Types.Material material)
        {
            fixed (Types.Material* materialPointer = &material)
            {
                UnloadMaterial(materialPointer);
            }
        }

        #endregion
    }
}
