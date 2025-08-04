using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;

namespace R3d.Net
{
    /// <summary>
    /// R3d wrapper methods
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public static unsafe partial class R3d
    {
        #region Init And Config Functions

        /// <summary>
        /// Sets a custom render target.
        /// 
        /// This function allows rendering to a custom framebuffer instead of the main one
        /// </summary>
        /// <param name="target">The custom render target</param>
        public static void SetRenderTarget(ref RenderTexture2D target)
        {
            fixed (RenderTexture2D* targetPointer = &target)
            {
                SetRenderTarget(targetPointer);
            }
        }

        /// <summary>
        /// Reverts back to rendering to the main framebuffer
        /// </summary>
        public static void SetRenderTarget()
        {
            SetRenderTarget(null);
        }

        #endregion

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
            fixed (Types.Material* materialPointer = &material)
            {
                DrawMesh(meshPointer, materialPointer, transform);
            }
        }

        /// <summary>
        /// Draws a mesh with a specified material and transformation.
        /// 
        /// This function renders a mesh with the provided transformation matrix.
        /// The default material will be used
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

        /// <summary>
        /// Draws a mesh with instancing support.
        ///
        /// This function renders a mesh multiple times with different transformation matrices for each instance
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="material">A reference to the material to apply to the mesh</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstanced(ref Types.Mesh mesh, ref Types.Material material, Span<Matrix4x4> instanceTransforms,
            int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Types.Material* materialPointer = &material)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawMeshInstanced(meshPointer, materialPointer, transformsPointer, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support.
        ///
        /// This function renders a mesh multiple times with different transformation matrices for each instance.
        /// The default material will be used
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstanced(ref Types.Mesh mesh, Span<Matrix4x4> instanceTransforms, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawMeshInstanced(meshPointer, null, transformsPointer, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support and different colors per instance.
        ///
        /// This function renders a mesh multiple times with different transformation matrices
        /// and different colors for each instance
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="material">A reference to the material to apply to the mesh</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceColors">Array of colors for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedEx(ref Types.Mesh mesh, ref Types.Material material, Span<Matrix4x4> instanceTransforms,
            Span<Color> instanceColors, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Types.Material* materialPointer = &material)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawMeshInstancedEx(meshPointer, materialPointer, transformsPointer, colorsPointer, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support and different colors per instance.
        ///
        /// This function renders a mesh multiple times with different transformation matrices
        /// and different colors for each instance.
        /// The default material will be used
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceColors">Array of colors for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedEx(ref Types.Mesh mesh, Span<Matrix4x4> instanceTransforms, Span<Color> instanceColors,
            int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawMeshInstancedEx(meshPointer, null, transformsPointer, colorsPointer, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support and different colors per instance.
        ///
        /// This function renders a mesh multiple times with different transformation matrices for each instance
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="material">A reference to the material to apply to the mesh</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedEx(ref Types.Mesh mesh, ref Types.Material material, Span<Matrix4x4> instanceTransforms,
             int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Types.Material* materialPointer = &material)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawMeshInstancedEx(meshPointer, materialPointer, transformsPointer, null, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support and different colors per instance.
        ///
        /// This function renders a mesh multiple times with different transformation matrices for each instance.
        /// The default material will be used
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedEx(ref Types.Mesh mesh, Span<Matrix4x4> instanceTransforms, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawMeshInstancedEx(meshPointer, null, transformsPointer, null, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a mesh multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same mesh
        /// and material
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="material">A reference to the material to apply to the mesh</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Will be transformed by the global matrix if necessary</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Reference to an array of colors for each instance, allowing unique colors</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedPro(ref Types.Mesh mesh, ref Types.Material material, ref BoundingBox globalAabb,
            Matrix4x4 globalTransform, Span<Matrix4x4> instanceTransforms, int transformsStride, Span<Color> instanceColors,
            int colorsStride, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Types.Material* materialPointer = &material)
            fixed (BoundingBox* globalAabbPointer = &globalAabb)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawMeshInstancedPro(meshPointer, materialPointer, globalAabbPointer, globalTransform, transformsPointer,
                    transformsStride, colorsPointer, colorsStride, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a mesh multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same mesh
        /// and material.
        /// The default material will be used
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Will be transformed by the global matrix if necessary</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Reference to an array of colors for each instance, allowing unique colors</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedPro(ref Types.Mesh mesh, ref BoundingBox globalAabb, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, Span<Color> instanceColors, int colorsStride, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (BoundingBox* globalAabbPointer = &globalAabb)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawMeshInstancedPro(meshPointer, null, globalAabbPointer, globalTransform, transformsPointer,
                    transformsStride, colorsPointer, colorsStride, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a mesh multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same mesh
        /// and material.
        /// Culling is disabled
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="material">A reference to the material to apply to the mesh</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Reference to an array of colors for each instance, allowing unique colors</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedPro(ref Types.Mesh mesh, ref Types.Material material, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, Span<Color> instanceColors, int colorsStride, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Types.Material* materialPointer = &material)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawMeshInstancedPro(meshPointer, materialPointer, null, globalTransform, transformsPointer,
                    transformsStride, colorsPointer, colorsStride, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support, a global transformation per instance.
        ///
        /// This function renders a mesh multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same mesh
        /// and material
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="material">A reference to the material to apply to the mesh</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Will be transformed by the global matrix if necessary</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedPro(ref Types.Mesh mesh, ref Types.Material material, ref BoundingBox globalAabb,
            Matrix4x4 globalTransform, Span<Matrix4x4> instanceTransforms, int transformsStride, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Types.Material* materialPointer = &material)
            fixed (BoundingBox* globalAabbPointer = &globalAabb)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawMeshInstancedPro(meshPointer, materialPointer, globalAabbPointer, globalTransform, transformsPointer,
                    transformsStride, null, 0, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a mesh multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same mesh
        /// and material.
        /// The default material will be used.
        /// Culling is disabled
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Reference to an array of colors for each instance, allowing unique colors</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedPro(ref Types.Mesh mesh, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, Span<Color> instanceColors, int colorsStride, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawMeshInstancedPro(meshPointer, null, null, globalTransform, transformsPointer,
                    transformsStride, colorsPointer, colorsStride, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support, a global transformation per instance.
        ///
        /// This function renders a mesh multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same mesh
        /// and material.
        /// Culling is disabled
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="material">A reference to the material to apply to the mesh</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedPro(ref Types.Mesh mesh, ref Types.Material material, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Types.Material* materialPointer = &material)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawMeshInstancedPro(meshPointer, materialPointer, null, globalTransform, transformsPointer,
                    transformsStride, null, 0, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support, a global transformation per instance.
        ///
        /// This function renders a mesh multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same mesh
        /// and material.
        /// The default material will be used
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Will be transformed by the global matrix if necessary</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedPro(ref Types.Mesh mesh, ref BoundingBox globalAabb, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (BoundingBox* globalAabbPointer = &globalAabb)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawMeshInstancedPro(meshPointer, null, globalAabbPointer, globalTransform, transformsPointer,
                    transformsStride, null, 0, instanceCount);
            }
        }

        /// <summary>
        /// Draws a mesh with instancing support, a global transformation per instance.
        ///
        /// This function renders a mesh multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same mesh
        /// and material.
        /// The default material will be used.
        /// Culling is disabled
        /// </summary>
        /// <param name="mesh">A reference to the mesh to render</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawMeshInstancedPro(ref Types.Mesh mesh, Matrix4x4 globalTransform, Span<Matrix4x4> instanceTransforms,
            int transformsStride, int instanceCount)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawMeshInstancedPro(meshPointer, null, null, globalTransform, transformsPointer,
                    transformsStride, null, 0, instanceCount);
            }
        }

        /// <summary>
        /// Draws a model at a specified position and scale.
        ///
        /// This function renders a model at the given position with the specified scale factor
        /// </summary>
        /// <param name="model">A reference to the model to render</param>
        /// <param name="position">The position to place the model at</param>
        /// <param name="scale">The scale factor to apply to the model</param>
        public static void DrawModel(ref Types.Model model, Vector3 position, float scale)
        {
            fixed (Types.Model* modelPointer = &model)
            {
                DrawModel(modelPointer, position, scale);
            }
        }

        /// <summary>
        /// Draws a model with advanced transformation options.
        ///
        /// This function renders a model with a specified position, rotation axis, rotation
        /// angle, and scale. It provides more control over how the model is transformed before
        /// rendering
        /// </summary>
        /// <param name="model">A reference to the model to render</param>
        /// <param name="position">The position to place the model at</param>
        /// <param name="rotationAxis">The axis of rotation for the model</param>
        /// <param name="rotationAngle">The angle to rotate the model</param>
        /// <param name="scale">The scale factor to apply to the model</param>
        public static void DrawModelEx(ref Types.Model model, Vector3 position, Vector3 rotationAxis, float rotationAngle, Vector3 scale)
        {
            fixed (Types.Model* modelPointer = &model)
            {
                DrawModelEx(modelPointer, position, rotationAxis, rotationAngle, scale);
            }
        }

        /// <summary>
        /// Draws a model using a transformation matrix.
        ///
        /// This function renders a model using a custom transformation matrix, allowing full control
        /// over the model's position, rotation, scale, and skew. It is intended for advanced rendering
        /// scenarios where a single matrix defines the complete transformation
        /// </summary>
        /// <param name="model">A reference to the model to render</param>
        /// <param name="transform">A transformation matrix that defines how to position, rotate, and scale the model</param>
        public static void DrawModelPro(ref Types.Model model, Matrix4x4 transform)
        {
            fixed (Types.Model* modelPointer = &model)
            {
                DrawModelPro(modelPointer, transform);
            }
        }

        /// <summary>
        /// Draws a sprite at a specified position.
        ///
        /// This function renders a sprite in 3D space at the given position.
        /// It supports negative scaling to flip the sprite
        /// </summary>
        /// <param name="sprite">A reference to the sprite to render</param>
        /// <param name="position">The position to place the sprite at</param>
        public static void DrawSprite(ref Sprite sprite, Vector3 position)
        {
            fixed (Sprite* spritePointer = &sprite)
            {
                DrawSprite(spritePointer, position);
            }
        }

        /// <summary>
        /// Draws a sprite with size and rotation options.
        ///
        /// This function allows rendering a sprite with a specified size and rotation.
        /// It supports negative size values for flipping the sprite
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render</param>
        /// <param name="position">The position to place the sprite at</param>
        /// <param name="size">The size of the sprite (negative values flip the sprite)</param>
        /// <param name="rotation">The rotation angle in degrees</param>
        public static void DrawSpriteEx(ref Sprite sprite, Vector3 position, Vector2 size, float rotation)
        {
            fixed (Sprite* spritePointer = &sprite)
            {
                DrawSpriteEx(spritePointer, position, size, rotation);
            }
        }

        /// <summary>
        /// Draws a sprite with full transformation control.
        ///
        /// This function provides advanced transformation options, allowing
        /// customization of size, rotation axis, and rotation angle.
        /// It supports all billboard modes, or can be drawn without billboarding
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render</param>
        /// <param name="position">The position to place the sprite at</param>
        /// <param name="size">The size of the sprite (negative values flip the sprite)</param>
        /// <param name="rotationAxis">The axis around which the sprite rotates</param>
        /// <param name="rotationAngle">The angle to rotate the sprite around the given axis</param>
        public static void DrawSpritePro(ref Sprite sprite, Vector3 position, Vector2 size, Vector3 rotationAxis, float rotationAngle)
        {
            fixed (Sprite* spritePointer = &sprite)
            {
                DrawSpritePro(spritePointer, position, size, rotationAxis, rotationAngle);
            }
        }

        /// <summary>
        /// Draws a 3D sprite with instancing support.
        ///
        /// This function renders a 3D sprite multiple times with different transformation matrices
        /// for each instance
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawSpriteInstanced(ref Sprite sprite, Span<Matrix4x4> instanceTransforms, int instanceCount)
        {
            fixed (Sprite* spritePointer = &sprite)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawSpriteInstanced(spritePointer, transformsPointer, instanceCount);
            }
        }

        /// <summary>
        /// Draws a 3D sprite with instancing support and different colors per instance.
        ///
        /// This function renders a 3D sprite multiple times with different transformation matrices
        /// and different colors for each instance
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceColors">Array of colors for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawSpriteInstancedEx(ref Sprite sprite, Span<Matrix4x4> instanceTransforms, Span<Color> instanceColors, int instanceCount)
        {
            fixed (Sprite* spritePointer = &sprite)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawSpriteInstancedEx(spritePointer, transformsPointer, colorsPointer, instanceCount);
            }
        }

        /// <summary>
        /// Draws a 3D sprite with instancing support and different colors per instance.
        ///
        /// This function renders a 3D sprite multiple times with different transformation matrices
        /// and different colors for each instance
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawSpriteInstancedEx(ref Sprite sprite, Span<Matrix4x4> instanceTransforms, int instanceCount)
        {
            fixed (Sprite* spritePointer = &sprite)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawSpriteInstancedEx(spritePointer, transformsPointer, null, instanceCount);
            }
        }

        /// <summary>
        /// Draws a 3D sprite with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a 3D sprite multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same sprite
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Will be transformed by the global matrix if provided</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Pointer to an array of colors for each instance, allowing unique colors</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawSpriteInstancedPro(ref Sprite sprite, ref BoundingBox globalAabb, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, Span<Color> instanceColors, int colorsStride, int instanceCount)
        {
            fixed (Sprite* spritePointer = &sprite)
            fixed (BoundingBox* globalAabbPointer = &globalAabb)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawSpriteInstancedPro(spritePointer, globalAabbPointer, globalTransform, transformsPointer, transformsStride,
                    colorsPointer, colorsStride, instanceCount);
            }
        }

        /// <summary>
        /// Draws a 3D sprite with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a 3D sprite multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same sprite.
        /// Culling is disabled
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Pointer to an array of colors for each instance, allowing unique colors</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawSpriteInstancedPro(ref Sprite sprite, Matrix4x4 globalTransform, Span<Matrix4x4> instanceTransforms,
            int transformsStride, Span<Color> instanceColors, int colorsStride, int instanceCount)
        {
            fixed (Sprite* spritePointer = &sprite)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawSpriteInstancedPro(spritePointer, null, globalTransform, transformsPointer, transformsStride,
                    colorsPointer, colorsStride, instanceCount);
            }
        }



        /// <summary>
        /// Draws a 3D sprite with instancing support, a global transformation per instance.
        ///
        /// This function renders a 3D sprite multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same sprite
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Will be transformed by the global matrix if provided</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawSpriteInstancedPro(ref Sprite sprite, ref BoundingBox globalAabb, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, int instanceCount)
        {
            fixed (Sprite* spritePointer = &sprite)
            fixed (BoundingBox* globalAabbPointer = &globalAabb)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawSpriteInstancedPro(spritePointer, globalAabbPointer, globalTransform, transformsPointer, transformsStride,
                    null, 0, instanceCount);
            }
        }

        /// <summary>
        /// Draws a 3D sprite with instancing support, a global transformation per instance.
        ///
        /// This function renders a 3D sprite multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same sprite.
        /// Culling is disabled
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawSpriteInstancedPro(ref Sprite sprite, Matrix4x4 globalTransform, Span<Matrix4x4> instanceTransforms,
            int transformsStride, int instanceCount)
        {
            fixed (Sprite* spritePointer = &sprite)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawSpriteInstancedPro(spritePointer, null, globalTransform, transformsPointer, transformsStride, null, 0, instanceCount);
            }
        }

        /// <summary>
        /// Renders the current state of a CPU-based particle system.
        ///
        /// This function draws the particles of a CPU-simulated particle system
        /// in their current state. It does not modify the simulation or update
        /// particle properties such as position, velocity, or lifetime
        /// </summary>
        /// <param name="system">A pointer to the `ParticleSystem` to be rendered. The particle system must be properly initialized and updated. to the desired state before calling this function</param>
        /// <param name="mesh">A pointer to the mesh used to represent each particle</param>
        /// <param name="material">A pointer to the material applied to the particle mesh</param>
        public static void DrawParticleSystem(ref ParticleSystem system, ref Types.Mesh mesh, ref Types.Material material)
        {
            fixed (ParticleSystem* systemPointer = &system)
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Types.Material* materialPointer = &material)
            {
                DrawParticleSystem(systemPointer, meshPointer, materialPointer);
            }
        }

        /// <summary>
        /// Renders the current state of a CPU-based particle system.
        ///
        /// This function draws the particles of a CPU-simulated particle system
        /// in their current state. It does not modify the simulation or update
        /// particle properties such as position, velocity, or lifetime.
        /// The default material will be used
        /// </summary>
        /// <param name="system">A pointer to the `ParticleSystem` to be rendered. The particle system must be properly initialized and updated. to the desired state before calling this function</param>
        /// <param name="mesh">A pointer to the mesh used to represent each particle</param>
        public static void DrawParticleSystem(ref ParticleSystem system, ref Types.Mesh mesh)
        {
            fixed (ParticleSystem* systemPointer = &system)
            fixed (Types.Mesh* meshPointer = &mesh)
            {
                DrawParticleSystem(systemPointer, meshPointer, null);
            }
        }

        /// <summary>
        /// Renders the current state of a CPU-based particle system with a global transformation.
        ///
        /// This function is similar to `DrawParticleSystem`, but it applies an additional
        /// global transformation to all particles. This is useful for rendering particle effects
        /// in a transformed space (e.g., attached to a moving object)
        /// </summary>
        /// <param name="system">A pointer to the `ParticleSystem` to be rendered. The particle system must be properly initialized and updated. to the desired state before calling this function</param>
        /// <param name="mesh">A pointer to the mesh used to represent each particle</param>
        /// <param name="material">A pointer to the material applied to the particle mesh</param>
        /// <param name="transform">A transformation matrix applied to all particles</param>
        public static void DrawParticleSystemEx(ref ParticleSystem system, ref Types.Mesh mesh, ref Types.Material material, Matrix4x4 transform)
        {
            fixed (ParticleSystem* systemPointer = &system)
            fixed (Types.Mesh* meshPointer = &mesh)
            fixed (Types.Material* materialPointer = &material)
            {
                DrawParticleSystemEx(systemPointer, meshPointer, materialPointer, transform);
            }
        }

        /// <summary>
        /// Renders the current state of a CPU-based particle system with a global transformation.
        ///
        /// This function is similar to `DrawParticleSystem`, but it applies an additional
        /// global transformation to all particles. This is useful for rendering particle effects
        /// in a transformed space (e.g., attached to a moving object).
        /// The default material will be used
        /// </summary>
        /// <param name="system">A pointer to the `ParticleSystem` to be rendered. The particle system must be properly initialized and updated. to the desired state before calling this function</param>
        /// <param name="mesh">A pointer to the mesh used to represent each particle</param>
        /// <param name="transform">A transformation matrix applied to all particles</param>
        public static void DrawParticleSystemEx(ref ParticleSystem system, ref Types.Mesh mesh, Matrix4x4 transform)
        {
            fixed (ParticleSystem* systemPointer = &system)
            fixed (Types.Mesh* meshPointer = &mesh)
            {
                DrawParticleSystemEx(systemPointer, meshPointer, null, transform);
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
        /// <param name="mesh">Reference to the mesh structure to be freed</param>
        public static void UnloadMesh(ref Types.Mesh mesh)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            {
                UnloadMesh(meshPointer);
            }
        }


        /// <summary>
        /// Upload a mesh to GPU memory.
        ///
        /// This function uploads a mesh's vertex and (optional) index data to the GPU.
        /// It creates and configures a VAO, VBO, and optionally an EBO if indices are provided.
        /// All vertex attributes are interleaved in a single VBO
        ///
        /// This function must only be called once per mesh. For updates, use UpdateMesh()
        /// </summary>
        /// <param name="mesh">Pointer to the mesh structure containing vertex and index data</param>
        /// <param name="dynamic">If true, allocates buffers with GL_DYNAMIC_DRAW for later updates. If false, uses GL_STATIC_DRAW for optimized static meshes</param>
        /// <returns>true if upload succeeded, false on error (e.g. invalid input or already uploaded)</returns>
        public static bool UploadMesh(ref Types.Mesh mesh, bool dynamic)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            {
                return UploadMesh(meshPointer, dynamic);
            }
        }

        /// <summary>
        /// Update an already uploaded mesh on the GPU.
        ///
        /// This function updates the GPU-side data of a mesh previously uploaded with UploadMesh().
        /// It replaces the vertex buffer contents using glBufferSubData.
        /// If index data is present, it also updates or creates the index buffer (EBO).
        ///
        /// This function assumes the mesh was uploaded with the `dynamic` flag set to true
        /// </summary>
        /// <param name="mesh">Pointer to the mesh structure with updated vertex and/or index data</param>
        /// <returns>true if update succeeded, false on error (e.g. mesh not uploaded or invalid data)</returns>
        public static bool UpdateMesh(ref Types.Mesh mesh)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            {
                return UpdateMesh(meshPointer);
            }
        }

        /// <summary>
        /// Recalculate the bounding box of a mesh.
        ///
        /// Computes and updates the axis-aligned bounding box (AABB) of the mesh
        /// by examining all vertex positions. This is useful after mesh deformation
        /// or when the bounding box needs to be refreshed
        /// </summary>
        /// <param name="mesh">Pointer to the mesh structure whose bounding box will be updated</param>
        public static void UpdateMeshBoundingBox(ref Types.Mesh mesh)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            {
                UpdateMeshBoundingBox(meshPointer);
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
        /// <param name="material">Reference to the material structure to be unloaded</param>
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
