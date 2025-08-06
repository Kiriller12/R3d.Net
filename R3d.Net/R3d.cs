using R3d.Net.Types;
using Raylib_cs;
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
        /// Draws a model with instancing support.
        ///
        /// This function renders a model multiple times with different transformation matrices
        /// for each instance
        /// </summary>
        /// <param name="model">A reference to the model to render</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawModelInstanced(ref Types.Model model, Span<Matrix4x4> instanceTransforms, int instanceCount)
        {
            fixed (Types.Model* modelPointer = &model)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawModelInstanced(modelPointer, transformsPointer, instanceCount);
            }
        }

        /// <summary>
        /// Draws a model with instancing support and different colors per instance.
        ///
        /// This function renders a model multiple times with different transformation matrices
        /// and different colors for each instance
        /// </summary>
        /// <param name="model">A reference to the model to render</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance</param>
        /// <param name="instanceColors">Array of colors for each instance</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawModelInstancedEx(ref Types.Model model, Span<Matrix4x4> instanceTransforms,
            Span<Color> instanceColors, int instanceCount)
        {
            fixed (Types.Model* modelPointer = &model)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawModelInstancedEx(modelPointer, transformsPointer, colorsPointer, instanceCount);
            }
        }

        /// <summary>
        /// Draws a model with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a model multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same model
        /// </summary>
        /// <param name="model">A pointer to the model to render</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Will be transformed by the global matrix if necessary</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Pointer to an array of colors for each instance, allowing unique colors</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawModelInstancedPro(ref Types.Model model, ref BoundingBox globalAabb, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, Span<Color> instanceColors, int colorsStride, int instanceCount)
        {
            fixed (Types.Model* modelPointer = &model)
            fixed (BoundingBox* globalAabbPointer = &globalAabb)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawModelInstancedPro(modelPointer, globalAabbPointer, globalTransform, transformsPointer,
                    transformsStride, colorsPointer, colorsStride, instanceCount);
            }
        }

        /// <summary>
        /// Draws a model with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a model multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same model.
        /// Culling is disabled
        /// </summary>
        /// <param name="model">A pointer to the model to render</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Pointer to an array of colors for each instance, allowing unique colors</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawModelInstancedPro(ref Types.Model model, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, Span<Color> instanceColors, int colorsStride, int instanceCount)
        {
            fixed (Types.Model* modelPointer = &model)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            fixed (Color* colorsPointer = instanceColors)
            {
                DrawModelInstancedPro(modelPointer, null, globalTransform, transformsPointer, transformsStride,
                    colorsPointer, colorsStride, instanceCount);
            }
        }

        /// <summary>
        /// Draws a model with instancing support, a global transformation per instance.
        ///
        /// This function renders a model multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same model
        /// </summary>
        /// <param name="model">A pointer to the model to render</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Will be transformed by the global matrix if necessary</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawModelInstancedPro(ref Types.Model model, ref BoundingBox globalAabb, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, int instanceCount)
        {
            fixed (Types.Model* modelPointer = &model)
            fixed (BoundingBox* globalAabbPointer = &globalAabb)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawModelInstancedPro(modelPointer, globalAabbPointer, globalTransform, transformsPointer,
                    transformsStride, null, 0, instanceCount);
            }
        }

        /// <summary>
        /// Draws a model with instancing support, a global transformation per instance.
        ///
        /// This function renders a model multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same model.
        /// Culling is disabled
        /// </summary>
        /// <param name="model">A pointer to the model to render</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        public static void DrawModelInstancedPro(ref Types.Model model, Matrix4x4 globalTransform,
            Span<Matrix4x4> instanceTransforms, int transformsStride, int instanceCount)
        {
            fixed (Types.Model* modelPointer = &model)
            fixed (Matrix4x4* transformsPointer = instanceTransforms)
            {
                DrawModelInstancedPro(modelPointer, null, globalTransform, transformsPointer, transformsStride,
                    null, 0, instanceCount);
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
        /// <param name="sprite">A reference to the sprite to render</param>
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
        /// <param name="sprite">A reference to the sprite to render</param>
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
        /// <param name="sprite">A reference to the sprite to render</param>
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
        /// <param name="sprite">A reference to the sprite to render</param>
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
        /// <param name="sprite">A reference to the sprite to render</param>
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
        /// <param name="sprite">A reference to the sprite to render</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Will be transformed by the global matrix if provided</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Reference to an array of colors for each instance, allowing unique colors</param>
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
        /// <param name="sprite">A reference to the sprite to render</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Reference to an array of colors for each instance, allowing unique colors</param>
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
        /// <param name="sprite">A reference to the sprite to render</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Will be transformed by the global matrix if provided</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
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
        /// <param name="sprite">A reference to the sprite to render</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Reference to an array of transformation matrices for each instance, allowing unique transformations</param>
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
        /// <param name="system">A reference to the `ParticleSystem` to be rendered. The particle system must be properly initialized and updated. to the desired state before calling this function</param>
        /// <param name="mesh">A reference to the mesh used to represent each particle</param>
        /// <param name="material">A reference to the material applied to the particle mesh</param>
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
        /// <param name="system">A reference to the `ParticleSystem` to be rendered. The particle system must be properly initialized and updated. to the desired state before calling this function</param>
        /// <param name="mesh">A reference to the mesh used to represent each particle</param>
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
        /// <param name="system">A reference to the `ParticleSystem` to be rendered. The particle system must be properly initialized and updated. to the desired state before calling this function</param>
        /// <param name="mesh">A reference to the mesh used to represent each particle</param>
        /// <param name="material">A reference to the material applied to the particle mesh</param>
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
        /// <param name="system">A reference to the `ParticleSystem` to be rendered. The particle system must be properly initialized and updated. to the desired state before calling this function</param>
        /// <param name="mesh">A reference to the mesh used to represent each particle</param>
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
        /// <param name="mesh">Reference to the mesh structure containing vertex and index data</param>
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
        /// <param name="mesh">Reference to the mesh structure with updated vertex and/or index data</param>
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
        /// <param name="mesh">Reference to the mesh structure whose bounding box will be updated</param>
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

        #region Model Functions

        /// <summary>
        /// Load a 3D model from a file.
        ///
        /// Loads a 3D model from the specified file path. Supports various 3D file formats
        /// and automatically parses meshes, materials, and texture references
        /// </summary>
        /// <param name="filePath">Path to the 3D model file to load</param>
        /// <returns>Loaded model structure containing meshes and materials</returns>
        public static Types.Model LoadModel(string filePath)
        {
            using var filePathBuffer = filePath.ToAnsiBuffer();

            return LoadModel(filePathBuffer.AsPointer());
        }

        /// <summary>
        /// Load a 3D model from memory buffer.
        ///
        /// Loads a 3D model from a memory buffer containing the file data.
        /// Useful for loading models from embedded resources or network streams
        /// </summary>
        /// <remarks>
        /// External dependencies (e.g., textures or linked resources) are not supported.
        /// The model data must be fully self-contained. Use embedded formats like .glb to ensure compatibility
        /// </remarks>
        /// <param name="fileType">String indicating the file format (e.g., "obj", "fbx", "gltf")</param>
        /// <param name="data">Reference to the memory buffer containing the model data</param>
        /// <param name="size">Size of the data buffer in bytes</param>
        /// <returns>Loaded model structure containing meshes and materials</returns>
        public static Types.Model LoadModelFromMemory(string fileType, byte[] data, uint size)
        {
            using var fileTypeBuffer = fileType.ToAnsiBuffer();
            fixed (byte* dataPointer = data)
            {
                return LoadModelFromMemory(fileTypeBuffer.AsPointer(), dataPointer, size);
            }
        }

        /// <summary>
        /// Create a model from a single mesh.
        ///
        /// Creates a model structure containing a single mesh with a default material.
        /// This is useful for procedurally generated meshes or simple geometry
        /// </summary>
        /// <remarks>
        /// The model's bounding box calculation assumes that the mesh's
        /// bounding box has already been computed. Call UpdateMeshBoundingBox()
        /// on the mesh before using this function if needed
        /// </remarks>
        /// <param name="mesh">Reference to the mesh to be wrapped in a model structure</param>
        /// <returns>Model structure containing the specified mesh</returns>
        public static Types.Model LoadModelFromMesh(ref Types.Mesh mesh)
        {
            fixed (Types.Mesh* meshPointer = &mesh)
            {
                return LoadModelFromMesh(meshPointer);
            }
        }

        /// <summary>
        /// Unload a model and optionally its materials.
        ///
        /// Frees all memory associated with a model, including its meshes.
        /// Materials can be optionally unloaded as well
        /// </summary>
        /// <param name="model">Reference to the model structure to be unloaded</param>
        /// <param name="unloadMaterials">If true, also unloads all materials associated with the model. Set to false if textures are still being used elsewhere to avoid freeing shared resources</param>
        public static void UnloadModel(ref Types.Model model, bool unloadMaterials)
        {
            fixed (Types.Model* modelPointer = &model)
            {
                UnloadModel(modelPointer, unloadMaterials);
            }
        }

        /// <summary>
        /// Update the bounding box of a model.
        ///
        /// Recalculates the axis-aligned bounding box (AABB) of the entire model
        /// by examining all meshes within the model. Optionally updates individual
        /// mesh bounding boxes as well
        /// </summary>
        /// <param name="model">Reference to the model structure whose bounding box will be updated</param>
        /// <param name="">. individual mesh within the model before calculating the model's overall bounding box</param>
        public static void UpdateModelBoundingBox(ref Types.Model model, bool updateMeshBoundingBoxes)
        {
            fixed (Types.Model* modelPointer = &model)
            {
                UpdateModelBoundingBox(modelPointer, updateMeshBoundingBoxes);
            }
        }

        /// <summary>
        /// Loads model animations from a supported file format (e.g., GLTF, IQM).
        ///
        /// This function parses animation data from the given model file and returns an array
        /// of ModelAnimation structs. The caller is responsible for freeing the returned data
        /// using UnloadModelAnimations()
        /// </summary>
        /// <param name="fileName">Path to the model file containing animation(s)</param>
        /// <param name="animCount">An integer that will receive the number of animations loaded</param>
        /// <param name="targetFrameRate">Desired frame rate (FPS) to sample the animation at. For example, 30 or 60</param>
        /// <returns>A dynamically allocated array of ModelAnimation. NULL on failure</returns>
        public static Span<Types.ModelAnimation> LoadModelAnimations(string fileName, int targetFrameRate)
        {
            using var fileNameBuffer = fileName.ToAnsiBuffer();
            var array = LoadModelAnimations(fileNameBuffer.AsPointer(), out var animCount, targetFrameRate);

            return new Span<Types.ModelAnimation>(array, animCount);
        }

        /// <summary>
        /// Loads model animations from memory data in a supported format (e.g., GLTF, IQM).
        ///
        /// This function parses animation data from the given memory buffer and returns an array
        /// of ModelAnimation structs. The caller is responsible for freeing the returned data
        /// using UnloadModelAnimations()
        /// </summary>
        /// <param name="fileType">File format hint (e.g., "gltf", "iqm", ".gltf"). The leading dot is optional</param>
        /// <param name="data">Reference to the model data in memory</param>
        /// <param name="targetFrameRate">Desired frame rate (FPS) to sample the animation at. For example, 30 or 60</param>
        /// <returns>A dynamically allocated array of ModelAnimation. NULL on failure</returns>
        public static Span<Types.ModelAnimation> LoadModelAnimationsFromMemory(string fileType, byte[] data, int targetFrameRate)
        {
            using var fileTypeBuffer = fileType.ToAnsiBuffer();
            fixed (byte* dataPointer = data)
            {
                var array = LoadModelAnimationsFromMemory(fileTypeBuffer.AsPointer(), dataPointer, (uint)data.Length, out var animCount, targetFrameRate);

                return new Span<Types.ModelAnimation>(array, animCount);
            }
        }

        /// <summary>
        /// Frees memory allocated for model animations.
        ///
        /// This should be called after you're done using animations loaded via LoadModelAnimations()
        /// </summary>
        /// <param name="animations">Animation array to free</param>
        public static void UnloadModelAnimations(Span<Types.ModelAnimation> animations)
        {
            fixed (Types.ModelAnimation* animationsPointer = animations)
            {
                UnloadModelAnimations(animationsPointer, animations.Length);
            }
        }

        /// <summary>
        /// Finds and returns a reference to a named animation within the array.
        ///
        /// Searches the given array of animations for one that matches the specified name
        /// </summary>
        /// <param name="animations">Array of animations to search</param>
        /// <param name="name">Name of the animation to find (case-sensitive)</param>
        /// <returns>A reference to matching animation, or NULL if not found</returns>
        public static ref Types.ModelAnimation GetModelAnimation(Span<Types.ModelAnimation> animations, string name)
        {
            using var nameBuffer = name.ToAnsiBuffer();
            fixed (Types.ModelAnimation* animationsPointer = animations)
            {
                var animationPointer = GetModelAnimation(animationsPointer, animations.Length, nameBuffer.AsPointer());
                var index = (animationPointer - animationsPointer) / sizeof(Types.ModelAnimation);

                return ref animations[(int)index];
            }
        }

        /// <summary>
        /// Logs the names of all animations in the array (for debugging or inspection).
        ///
        /// Prints the animation names (and possibly other info) to the standard output or debug console
        /// </summary>
        /// <param name="animations">Array of animations to list</param>
        public static void ListModelAnimations(Span<Types.ModelAnimation> animations)
        {
            fixed (Types.ModelAnimation* animationsPointer = animations)
            {
                ListModelAnimations(animationsPointer, animations.Length);
            }
        }

        #endregion

        #region Particle System Functions

        /// <summary>
        /// Unloads the particle emitter system and frees allocated memory.
        ///
        /// This function deallocates the memory used by the particle emitter system and clears the associated resources.
        /// It should be called when the particle system is no longer needed to prevent memory leaks
        /// </summary>
        /// <param name="system">A reference to the `ParticleSystem` to be unloaded</param>
        public static void UnloadParticleSystem(ref ParticleSystem system)
        {
            fixed (ParticleSystem* systemPointer = &system)
            {
                UnloadParticleSystem(systemPointer);
            }
        }

        /// <summary>
        /// Emits a particle in the particle system.
        ///
        /// This function triggers the emission of a new particle in the particle system. It handles the logic of adding a new
        /// particle to the system and initializing its properties based on the current state of the system
        /// </summary>
        /// <param name="system">A reference to the `ParticleSystemCPU` where the particle will be emitted</param>
        /// <returns>`true` if the particle was successfully emitted, `false` if the system is at full capacity and cannot emit more particles</returns>
        public static bool EmitParticle(ref ParticleSystem system)
        {
            fixed (ParticleSystem* systemPointer = &system)
            {
                return EmitParticle(systemPointer);
            }
        }

        /// <summary>
        /// Updates the particle emitter system by advancing particle positions.
        ///
        /// This function updates the positions and properties of particles in the system based on the elapsed time. It handles
        /// simulation of particle movement, gravity, and other physics-based calculations
        /// </summary>
        /// <param name="system">A reference to the `ParticleSystem` to be updated</param>
        /// <param name="deltaTime">The time elapsed since the last update (in seconds)</param>
        public static void UpdateParticleSystem(ref ParticleSystem system, float deltaTime)
        {
            fixed (ParticleSystem* systemPointer = &system)
            {
                UpdateParticleSystem(systemPointer, deltaTime);
            }
        }

        /// <summary>
        /// Computes and updates the AABB (Axis-Aligned Bounding Box) of a particle system.
        ///
        /// This function simulates the particle system to estimate the region of space it occupies.
        /// It considers particle positions at mid-life and end-of-life to approximate the AABB,
        /// which is then stored in the system's `aabb` field. This is useful for enabling frustum culling,
        /// especially when the bounds are not known beforehand
        /// </summary>
        /// <param name="system">A reference to the `ParticleSystem` to update</param>
        public static void CalculateParticleSystemBoundingBox(ref ParticleSystem system)
        {
            fixed (ParticleSystem* systemPointer = &system)
            {
                CalculateParticleSystemBoundingBox(systemPointer);
            }
        }

        #endregion

        #region Sprites Functions

        /// <summary>
        /// Unload a sprite and free associated resources.
        ///
        /// This function releases the resources allocated for a `Sprite`.
        /// It should be called when the sprite is no longer needed
        /// </summary>
        /// <remarks>
        /// This function only unloads non-default textures from the sprite's material,
        /// so make sure these textures are not shared with other material instances elsewhere
        /// </remarks>
        /// <param name="sprite">A reference to the `Sprite` to be unloaded</param>
        public static void UnloadSprite(ref Sprite sprite)
        {
            fixed (Sprite* spritePointer = &sprite)
            {
                UnloadSprite(spritePointer);
            }
        }

        /// <summary>
        /// Updates the animation of a sprite.
        ///
        /// This function updates the current frame of the sprite's animation based on the provided speed. The animation frames are read from
        /// right to left, advancing the cursor to the next row after completing a line
        /// </summary>
        /// <remarks>
        /// The `speed` parameter can be calculated as the number of frames per second multiplied by `GetFrameTime()`
        /// </remarks>
        /// <param name="sprite">A reference to the `Sprite` to update</param>
        /// <param name="speed">The speed at which the animation progresses, in frames per second</param>
        public static void UpdateSprite(ref Sprite sprite, float speed)
        {
            fixed (Sprite* spritePointer = &sprite)
            {
                UpdateSprite(spritePointer, speed);
            }
        }

        /// <summary>
        /// Updates the animation of a sprite with specified frame boundaries.
        ///
        /// This function updates the current frame of the sprite's animation while restricting it between `firstFrame` and `lastFrame`.
        /// This is useful for spritesheets containing multiple animations
        /// </summary>
        /// <remarks>
        /// The frames are read from right to left, and the cursor moves to the next row after completing a line.
        /// <remarks>
        /// The `speed` parameter can be calculated as the number of frames per second multiplied by `GetFrameTime()`
        /// </remarks>
        /// <param name="sprite">A reference to the `Sprite` to update</param>
        /// <param name="firstFrame">The first frame of the animation loop</param>
        /// <param name="lastFrame">The last frame of the animation loop</param>
        /// <param name="speed">The speed at which the animation progresses, in frames per second</param>
        public static void UpdateSpriteEx(ref Sprite sprite, int firstFrame, int lastFrame, float speed)
        {
            fixed (Sprite* spritePointer = &sprite)
            {
                UpdateSpriteEx(spritePointer, firstFrame, lastFrame, speed);
            }
        }

        #endregion

        #region Interpolation Curves Functions

        /// <summary>
        /// Adds a keyframe to the interpolation curve.
        ///
        /// This function adds a keyframe to the given interpolation curve at a specific time and value. If the addition of the
        /// keyframe requires reallocating memory and the reallocation fails, the previously allocated memory and keyframes are
        /// preserved, but the new keyframe is not added
        /// </summary>
        /// <param name="curve">A reference to the interpolation curve to which the keyframe will be added</param>
        /// <param name="time">The time at which the keyframe will be added</param>
        /// <param name="value">The value associated with the keyframe</param>
        /// <returns>`true` if the keyframe was successfully added, or `false` if the reallocation failed</returns>
        public static bool AddKeyframe(ref InterpolationCurve curve, float time, float value)
        {
            fixed (InterpolationCurve* curvePointer = &curve)
            {
                return AddKeyframe(curvePointer, time, value);
            }
        }

        #endregion

        #region Skybox Loading Functions

        /// <summary>
        /// Loads a skybox from a texture file.
        ///
        /// This function loads a skybox cubemap from a texture file using a specified cubemap layout.
        /// The layout defines how the six faces of the cubemap are arranged within the texture
        /// </summary>
        /// <param name="fileName">The path to the texture file</param>
        /// <param name="layout">The cubemap layout format</param>
        /// <returns>The loaded skybox object</returns>
        public static Skybox LoadSkybox(string fileName, CubemapLayout layout)
        {
            using var fileNameBuffer = fileName.ToAnsiBuffer();

            return LoadSkybox(fileNameBuffer.AsPointer(), layout);
        }

        /// <summary>
        /// Loads a skybox from a panorama texture file.
        ///
        /// This function loads a skybox from a panorama (equirectangular) texture file,
        /// and converts it into a cubemap with the specified resolution
        /// </summary>
        /// <param name="fileName">The path to the panorama texture file</param>
        /// <param name="size">The resolution of the generated cubemap (e.g., 512, 1024)</param>
        /// <returns>The loaded skybox object</returns>
        public static Skybox LoadSkyboxPanorama(string fileName, int size)
        {
            using var fileNameBuffer = fileName.ToAnsiBuffer();

            return LoadSkyboxPanorama(fileNameBuffer.AsPointer(), size);
        }

        #endregion
    }
}
