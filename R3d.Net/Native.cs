using R3d.Net.Types;
using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;

namespace R3d.Net
{
    /// <summary>
    /// R3d native imported methods
    /// </summary>
    /// <remarks>
    /// Should be actual as of 
    /// <see href="https://github.com/Bigfoot71/r3d/blob/2d896490bc922ddeaa4682367ba5c82f39726963/include/r3d.h">
    /// 2d896490bc922ddeaa4682367ba5c82f39726963
    /// </see>
    /// </remarks>
    [SuppressUnmanagedCodeSecurity]
    public static unsafe partial class R3d
    {
        /// <summary>
        /// Native dll file name
        /// </summary>
        const string NativeDll = "r3d";

        #region Init And Config Functions

        /// <summary>
        /// Initializes the rendering engine.
        /// 
        /// This function sets up the internal rendering system with the provided resolution
        /// and state flags, which define the internal behavior. These flags can be modified
        /// later via SetState
        /// </summary>
        /// <param name="resWidth">Width of the internal resolution</param>
        /// <param name="resHeight">Height of the internal resolution</param>
        /// <param name="flags">Flags indicating internal behavior (modifiable via SetState)</param>
        [DllImport(NativeDll, EntryPoint = "R3D_Init", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Init(int resWidth, int resHeight, ConfigFlag flags);

        /// <summary>
        /// Closes the rendering engine and deallocates all resources.
        ///
        /// This function shuts down the rendering system and frees all allocated memory,
        /// including the resources associated with the created lights
        /// </summary>
        [DllImport(NativeDll, EntryPoint = "R3D_Close", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Close();

        /// <summary>
        /// Checks if a specific internal state flag is set
        /// </summary>
        /// <param name="flag">The state flag to check</param>
        /// <returns>True if the flag is set, false otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_HasState", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool HasState(ConfigFlag flag);

        /// <summary>
        /// Sets internal state flags for the rendering engine
        /// </summary>
        /// <param name="flags">The flags to set</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetState", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetState(ConfigFlag flags);

        /// <summary>
        /// Clears specific internal state flags.
        /// 
        /// This function clears one or more previously set state flags, modifying the
        /// behavior of the rendering engine accordingly
        /// </summary>
        /// <param name="flags">The flags to clear</param>
        [DllImport(NativeDll, EntryPoint = "R3D_ClearState", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClearState(ConfigFlag flags);

        /// <summary>
        /// Gets the current internal resolution.
        /// 
        /// This function retrieves the current internal resolution being used by the
        /// rendering engine
        /// </summary>
        /// <param name="width">Width of the internal resolution</param>
        /// <param name="height">Height of the internal resolution</param>
        [DllImport(NativeDll, EntryPoint = "R3D_GetResolution", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetResolution(out int width, out int height);

        /// <summary>
        /// Updates the internal resolution.
        /// 
        /// This function changes the internal resolution of the rendering engine. Note that
        /// this process destroys and recreates all framebuffers, which may be a slow operation
        /// </summary>
        /// <param name="width">The new width for the internal resolution</param>
        /// <param name="height">The new height for the internal resolution</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UpdateResolution", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdateResolution(int width, int height);

        /// <summary>
        /// Sets a custom render target.
        /// 
        /// This function allows rendering to a custom framebuffer instead of the main one.
        /// Passing `NULL` will revert back to rendering to the main framebuffer
        /// </summary>
        /// <param name="target">The custom render target (can be NULL to revert to the default framebuffer)</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetRenderTarget", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRenderTarget(RenderTexture2D* target);

        /// <summary>
        /// Defines the bounds of the scene for directional light calculations.
        /// 
        /// This function sets the scene bounds used to determine which areas should be illuminated.
        /// by directional lights. It is the user's responsibility to calculate and provide the
        /// correct bounds
        /// </summary>
        /// <param name="sceneBounds">The bounding box defining the scene's dimensions</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetSceneBounds", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSceneBounds(BoundingBox sceneBounds);

        /// <summary>
        /// Sets the default texture filtering mode.
        /// 
        /// This function defines the default texture filter that will be applied to all subsequently 
        /// loaded textures, including those used in materials, sprites, and other resources.
        /// 
        /// If a trilinear or anisotropic filter is selected, mipmaps will be automatically generated 
        /// for the textures, but they will not be generated when using nearest or bilinear filtering.
        /// 
        /// The default texture filter mode is `Trilinear`
        /// </summary>
        /// <param name="filter">The texture filtering mode to be applied by default</param>

        [DllImport(NativeDll, EntryPoint = "R3D_SetTextureFilter", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetTextureFilter(TextureFilter filter);

        #endregion

        #region Drawing Functions

        /// <summary>
        /// Begins a rendering session for a 3D camera.
        /// 
        /// This function starts a rendering session, preparing the engine to handle subsequent
        /// draw calls using the provided camera settings
        /// </summary>
        /// <param name="camera">The camera to use for rendering the scene</param>

        [DllImport(NativeDll, EntryPoint = "R3D_Begin", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Begin(Camera3D camera);

        /// <summary>
        /// Ends the current rendering session.
        /// 
        /// This function signals the end of a rendering session, at which point the engine
        /// will process all necessary render passes and output the final result to the main
        /// or custom framebuffer
        /// </summary>

        [DllImport(NativeDll, EntryPoint = "R3D_End", CallingConvention = CallingConvention.Cdecl)]
        public static extern void End();

        /// <summary>
        /// Draws a mesh with a specified material and transformation.
        /// 
        /// This function renders a mesh with the provided material and transformation matrix
        /// </summary>
        /// <param name="mesh">A pointer to the mesh to render. Cannot be NULL</param>
        /// <param name="material">A pointer to the material to apply to the mesh. Can be NULL, default material will be used</param>
        /// <param name="transform">The transformation matrix to apply to the mesh</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawMesh", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawMesh(Types.Mesh* mesh, Types.Material* material, Matrix4x4 transform);

        /// <summary>
        /// Draws a mesh with instancing support.
        ///
        /// This function renders a mesh multiple times with different transformation matrices
        /// for each instance
        /// </summary>
        /// <param name="mesh">A pointer to the mesh to render. Cannot be NULL</param>
        /// <param name="material">A pointer to the material to apply to the mesh. Can be NULL, default material will be used</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance. Cannot be NULL</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawMeshInstanced", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawMeshInstanced(Types.Mesh* mesh, Types.Material* material, Matrix4x4* instanceTransforms, int instanceCount);

        /// <summary>
        /// Draws a mesh with instancing support and different colors per instance.
        ///
        /// This function renders a mesh multiple times with different transformation matrices
        /// and different colors for each instance
        /// </summary>
        /// <param name="mesh">A pointer to the mesh to render. Cannot be NULL</param>
        /// <param name="material">A pointer to the material to apply to the mesh. Can be NULL, default material will be used</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance. Cannot be NULL</param>
        /// <param name="instanceColors">Array of colors for each instance. Can be NULL if no per-instance colors are needed</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawMeshInstancedEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawMeshInstancedEx(Types.Mesh* mesh, Types.Material* material, Matrix4x4* instanceTransforms, Color* instanceColors,
            int instanceCount);

        /// <summary>
        /// Draws a mesh with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a mesh multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same mesh
        /// and material
        /// </summary>
        /// <param name="mesh">A pointer to the mesh to render. Cannot be NULL</param>
        /// <param name="material">A pointer to the material to apply to the mesh. Can be NULL, default material will be used</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Can be NULL to disable culling. Will be transformed by the global matrix if necessary</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations. Cannot be NULL</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Pointer to an array of colors for each instance, allowing unique colors. Can be NULL if no per-instance colors are needed</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawMeshInstancedPro", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawMeshInstancedPro(Types.Mesh* mesh, Types.Material* material, BoundingBox* globalAabb, Matrix4x4 globalTransform,
            Matrix4x4* instanceTransforms, int transformsStride, Color* instanceColors, int colorsStride, int instanceCount);

        /// <summary>
        /// Draws a model at a specified position and scale.
        ///
        /// This function renders a model at the given position with the specified scale factor
        /// </summary>
        /// <param name="model">A pointer to the model to render</param>
        /// <param name="position">The position to place the model at</param>
        /// <param name="scale">The scale factor to apply to the model</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawModel", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawModel(Types.Model* model, Vector3 position, float scale);

        /// <summary>
        /// Draws a model with advanced transformation options.
        ///
        /// This function renders a model with a specified position, rotation axis, rotation
        /// angle, and scale. It provides more control over how the model is transformed before
        /// rendering
        /// </summary>
        /// <param name="model">A pointer to the model to render</param>
        /// <param name="position">The position to place the model at</param>
        /// <param name="rotationAxis">The axis of rotation for the model</param>
        /// <param name="rotationAngle">The angle to rotate the model</param>
        /// <param name="scale">The scale factor to apply to the model</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawModelEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawModelEx(Types.Model* model, Vector3 position, Vector3 rotationAxis, float rotationAngle, Vector3 scale);

        /// <summary>
        /// Draws a model using a transformation matrix.
        ///
        /// This function renders a model using a custom transformation matrix, allowing full control
        /// over the model's position, rotation, scale, and skew. It is intended for advanced rendering
        /// scenarios where a single matrix defines the complete transformation
        /// </summary>
        /// <param name="model">A pointer to the model to render</param>
        /// <param name="transform">A transformation matrix that defines how to position, rotate, and scale the model</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawModelPro", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawModelPro(Types.Model* model, Matrix4x4 transform);

        /// <summary>
        /// Draws a model with instancing support.
        ///
        /// This function renders a model multiple times with different transformation matrices
        /// for each instance.
        /// </summary>
        /// <param name="model">A pointer to the model to render. Cannot be NULL</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance. Cannot be NULL</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawModelInstanced", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawModelInstanced(Types.Model* model, Matrix4x4* instanceTransforms, int instanceCount);

        /// <summary>
        /// Draws a model with instancing support and different colors per instance.
        ///
        /// This function renders a model multiple times with different transformation matrices
        /// and different colors for each instance.
        /// </summary>
        /// <param name="model">A pointer to the model to render. Cannot be NULL</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance. Cannot be NULL</param>
        /// <param name="instanceColors">Array of colors for each instance. Can be NULL if no per-instance colors are needed</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawModelInstancedEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawModelInstancedEx(Types.Model* model, Matrix4x4* instanceTransforms, Color* instanceColors, int instanceCount);

        /// <summary>
        /// Draws a model with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a model multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same model.
        /// </summary>
        /// <param name="model">A pointer to the model to render. Cannot be NULL</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Can be NULL to disable culling. Will be transformed by the global matrix if necessary</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations. Cannot be NULL</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Pointer to an array of colors for each instance, allowing unique colors. Can be NULL if no per-instance colors are needed</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawModelInstancedPro", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawModelInstancedPro(Types.Model* model, BoundingBox* globalAabb, Matrix4x4 globalTransform,
            Matrix4x4* instanceTransforms, int transformsStride, Color* instanceColors, int colorsStride, int instanceCount);


        /// <summary>
        /// Draws a sprite at a specified position.
        ///
        /// This function renders a sprite in 3D space at the given position.
        /// It supports negative scaling to flip the sprite
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render</param>
        /// <param name="position">The position to place the sprite at</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawSprite", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawSprite(Sprite* sprite, Vector3 position);

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
        [DllImport(NativeDll, EntryPoint = "R3D_DrawSpriteEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawSpriteEx(Sprite* sprite, Vector3 position, Vector2 size, float rotation);

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
        [DllImport(NativeDll, EntryPoint = "R3D_DrawSpritePro", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawSpritePro(Sprite* sprite, Vector3 position, Vector2 size, Vector3 rotationAxis, float rotationAngle);

        /// <summary>
        /// Draws a 3D sprite with instancing support.
        ///
        /// This function renders a 3D sprite multiple times with different transformation matrices
        /// for each instance
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render. Cannot be NULL</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance. Cannot be NULL</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawSpriteInstanced", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawSpriteInstanced(Sprite* sprite, Matrix4x4* instanceTransforms, int instanceCount);

        /// <summary>
        /// Draws a 3D sprite with instancing support and different colors per instance.
        ///
        /// This function renders a 3D sprite multiple times with different transformation matrices
        /// and different colors for each instance
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render. Cannot be NULL</param>
        /// <param name="instanceTransforms">Array of transformation matrices for each instance. Cannot be NULL</param>
        /// <param name="instanceColors">Array of colors for each instance. Can be NULL if no per-instance colors are needed</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawSpriteInstancedEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawSpriteInstancedEx(Sprite* sprite, Matrix4x4* instanceTransforms, Color* instanceColors, int instanceCount);

        /// <summary>
        /// Draws a 3D sprite with instancing support, a global transformation, and different colors per instance.
        ///
        /// This function renders a 3D sprite multiple times using instancing, with a global transformation
        /// applied to all instances, and individual transformation matrices and colors for each instance.
        /// Each instance can have its own position, rotation, scale, and color while sharing the same sprite
        /// </summary>
        /// <param name="sprite">A pointer to the sprite to render. Cannot be NULL</param>
        /// <param name="globalAabb">Optional bounding box encompassing all instances, in local space. Used for frustum culling. Can be NULL to disable culling. Will be transformed by the global matrix if provided</param>
        /// <param name="globalTransform">The global transformation matrix applied to all instances</param>
        /// <param name="instanceTransforms">Pointer to an array of transformation matrices for each instance, allowing unique transformations. Cannot be NULL</param>
        /// <param name="transformsStride">The stride (in bytes) between consecutive transformation matrices in the array. Set to 0 if the matrices are tightly packed (stride equals sizeof(Matrix4x4)). If matrices are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceColors">Pointer to an array of colors for each instance, allowing unique colors. Can be NULL if no per-instance colors are needed</param>
        /// <param name="colorsStride">The stride (in bytes) between consecutive colors in the array. Set to 0 if the colors are tightly packed (stride equals sizeof(Color)). If colors are embedded in a struct, set to the size of the struct or the actual byte offset between elements</param>
        /// <param name="instanceCount">The number of instances to render. Must be greater than 0</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawSpriteInstancedPro", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawSpriteInstancedPro(Sprite* sprite, BoundingBox* globalAabb, Matrix4x4 globalTransform, Matrix4x4* instanceTransforms,
            int transformsStride, Color* instanceColors, int colorsStride, int instanceCount);

        /// <summary>
        /// Renders the current state of a CPU-based particle system.
        ///
        /// This function draws the particles of a CPU-simulated particle system
        /// in their current state. It does not modify the simulation or update
        /// particle properties such as position, velocity, or lifetime
        /// </summary>
        /// <param name="system">A pointer to the `ParticleSystem` to be rendered. The particle system must be properly initialized and updated. to the desired state before calling this function</param>
        /// <param name="mesh">A pointer to the mesh used to represent each particle. Cannot be NULL</param>
        /// <param name="material">A pointer to the material applied to the particle mesh. Can be NULL, default material will be used</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawParticleSystem", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawParticleSystem(ParticleSystem* system, Types.Mesh* mesh, Types.Material* material);

        /// <summary>
        /// Renders the current state of a CPU-based particle system with a global transformation.
        ///
        /// This function is similar to `DrawParticleSystem`, but it applies an additional
        /// global transformation to all particles. This is useful for rendering particle effects
        /// in a transformed space (e.g., attached to a moving object)
        /// </summary>
        /// <param name="system">A pointer to the `ParticleSystem` to be rendered. The particle system must be properly initialized and updated. to the desired state before calling this function</param>
        /// <param name="mesh">A pointer to the mesh used to represent each particle. Cannot be NULL</param>
        /// <param name="material">A pointer to the material applied to the particle mesh. Can be NULL, default material will be used</param>
        /// <param name="transform">A transformation matrix applied to all particles</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawParticleSystemEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawParticleSystemEx(ParticleSystem* system, Types.Mesh* mesh, Types.Material* material, Matrix4x4 transform);

        #endregion

        #region Mesh Functions

        /// <summary>
        /// Generate a polygon mesh with specified number of sides.
        ///
        /// Creates a regular polygon mesh centered at the origin in the XY plane.
        /// The polygon is generated with vertices evenly distributed around a circle
        /// </summary>
        /// <param name="sides">Number of sides for the polygon (minimum 3)</param>
        /// <param name="radius">Radius of the circumscribed circle</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated polygon mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshPoly", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshPoly(int sides, float radius, CBool upload);

        /// <summary>
        /// Generate a plane mesh with specified dimensions and resolution.
        /// 
        /// Creates a flat plane mesh in the XZ plane, centered at the origin.
        /// The mesh can be subdivided for higher resolution or displacement mapping
        /// </summary>
        /// <param name="width">Width of the plane along the X axis</param>
        /// <param name="length">Length of the plane along the Z axis</param>
        /// <param name="resX">Number of subdivisions along the X axis</param>
        /// <param name="resZ">Number of subdivisions along the Z axis</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated plane mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshPlane", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshPlane(float width, float length, int resX, int resZ, CBool upload);

        /// <summary>
        /// Generate a cube mesh with specified dimensions.
        ///
        /// Creates a cube mesh centered at the origin with the specified width, height, and length.
        /// Each face consists of two triangles with proper normals and texture coordinates
        /// </summary>
        /// <param name="width">Width of the cube along the X axis</param>
        /// <param name="height">Height of the cube along the Y axis</param>
        /// <param name="length">Length of the cube along the Z axis</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated cube mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshCube", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshCube(float width, float height, float length, CBool upload);

        /// <summary>
        /// Generate a sphere mesh with specified parameters.
        /// 
        /// Creates a UV sphere mesh centered at the origin using latitude-longitude subdivision.
        /// Higher ring and slice counts produce smoother spheres but with more vertices
        /// </summary>
        /// <param name="radius">Radius of the sphere</param>
        /// <param name="rings">Number of horizontal rings (latitude divisions)</param>
        /// <param name="slices">Number of vertical slices (longitude divisions)</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated sphere mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshSphere", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshSphere(float radius, int rings, int slices, CBool upload);

        /// <summary>
        /// Generate a hemisphere mesh with specified parameters.
        ///
        /// Creates a half-sphere mesh (dome) centered at the origin, extending upward in the Y axis.
        /// Uses the same UV sphere generation technique as GenMeshSphere but only the upper half
        /// </summary>
        /// <param name="radius">Radius of the hemisphere</param>
        /// <param name="rings">Number of horizontal rings (latitude divisions)</param>
        /// <param name="slices">Number of vertical slices (longitude divisions)</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated hemisphere mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshHemiSphere", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshHemiSphere(float radius, int rings, int slices, CBool upload);

        /// <summary>
        /// Generate a cylinder mesh with specified parameters.
        ///
        /// Creates a cylinder mesh centered at the origin, extending along the Y axis.
        /// The cylinder includes both top and bottom caps and smooth side surfaces
        /// </summary>
        /// <param name="radius">Radius of the cylinder base</param>
        /// <param name="height">Height of the cylinder along the Y axis</param>
        /// <param name="slices">Number of radial subdivisions around the cylinder</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated cylinder mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshCylinder", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshCylinder(float radius, float height, int slices, CBool upload);

        /// <summary>
        /// Generate a cone mesh with specified parameters.
        ///
        /// Creates a cone mesh with its base centered at the origin and apex pointing upward along the Y axis.
        /// The cone includes a circular base and smooth tapered sides
        /// </summary>
        /// <param name="radius">Radius of the cone base</param>
        /// <param name="height">Height of the cone along the Y axis</param>
        /// <param name="slices">Number of radial subdivisions around the cone base</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated cone mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshCone", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshCone(float radius, float height, int slices, CBool upload);

        /// <summary>
        /// Generate a torus mesh with specified parameters.
        ///
        /// Creates a torus (donut shape) mesh centered at the origin in the XZ plane.
        /// The torus is defined by a major radius (distance from center to tube center)
        /// and a minor radius (tube thickness)
        /// </summary>
        /// <param name="radius">Major radius of the torus (distance from center to tube center)</param>
        /// <param name="size">Minor radius of the torus (tube thickness/radius)</param>
        /// <param name="radSeg">Number of segments around the major radius</param>
        /// <param name="sides">Number of sides around the tube cross-section</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated torus mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshTorus", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshTorus(float radius, float size, int radSeg, int sides, CBool upload);

        /// <summary>
        /// Generate a trefoil knot mesh with specified parameters.
        ///
        /// Creates a trefoil knot mesh, which is a mathematical knot shape.
        /// Similar to a torus but with a twisted, knotted topology
        /// </summary>
        /// <param name="radius">Major radius of the knot</param>
        /// <param name="size">Minor radius (tube thickness) of the knot</param>
        /// <param name="radSeg">Number of segments around the major radius</param>
        /// <param name="sides">Number of sides around the tube cross-section</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated trefoil knot mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshKnot", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshKnot(float radius, float size, int radSeg, int sides, CBool upload);

        /// <summary>
        /// Generate a terrain mesh from a heightmap image.
        ///
        /// Creates a terrain mesh by interpreting the brightness values of a heightmap image
        /// as height values. The resulting mesh represents a 3D terrain surface
        /// </summary>
        /// <param name="heightmap">Image containing height data (grayscale values represent elevation)</param>
        /// <param name="size">3D vector defining the terrain dimensions (width, max height, depth)</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated heightmap terrain mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshHeightmap", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshHeightmap(Image heightmap, Vector3 size, CBool upload);

        /// <summary>
        /// Generate a voxel-style mesh from a cubicmap image.
        ///
        /// Creates a mesh composed of cubes based on a cubicmap image, where each pixel
        /// represents the presence or absence of a cube at that position. Useful for
        /// creating voxel-based or block-based geometry
        /// </summary>
        /// <param name="cubicmap">Image where pixel values determine cube placement</param>
        /// <param name="cubeSize">3D vector defining the size of each individual cube</param>
        /// <param name="upload">If true, automatically uploads the mesh to GPU memory</param>
        /// <returns>Generated cubicmap mesh structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GenMeshCubicmap", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Mesh GenMeshCubicmap(Image cubicmap, Vector3 cubeSize, CBool upload);

        /// <summary>
        /// Free mesh data from both RAM and VRAM.
        /// 
        /// Releases all memory associated with a mesh, including vertex data in RAM
        /// and GPU buffers (VAO, VBO, EBO) if the mesh was uploaded to VRAM.
        /// After calling this function, the mesh should not be used
        /// </summary>
        /// <param name="mesh">Pointer to the mesh structure to be freed</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UnloadMesh", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnloadMesh(Types.Mesh* mesh);

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
        [DllImport(NativeDll, EntryPoint = "R3D_UploadMesh", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool UploadMesh(Types.Mesh* mesh, CBool dynamic);

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
        [DllImport(NativeDll, EntryPoint = "R3D_UpdateMesh", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool UpdateMesh(Types.Mesh* mesh);

        /// <summary>
        /// Recalculate the bounding box of a mesh.
        ///
        /// Computes and updates the axis-aligned bounding box (AABB) of the mesh
        /// by examining all vertex positions. This is useful after mesh deformation
        /// or when the bounding box needs to be refreshed
        /// </summary>
        /// <param name="mesh">Pointer to the mesh structure whose bounding box will be updated</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UpdateMeshBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdateMeshBoundingBox(Types.Mesh* mesh);

        #endregion

        #region Material Functions

        /// <summary>
        /// Get the default material configuration.
        /// 
        /// Returns a default material with standard properties and default textures.
        /// This material can be used as a fallback or starting point for custom materials
        /// </summary>
        /// <returns>Default material structure with standard properties</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetDefaultMaterial", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Material GetDefaultMaterial();

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
        [DllImport(NativeDll, EntryPoint = "R3D_UnloadMaterial", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnloadMaterial(Types.Material* material);

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
        [DllImport(NativeDll, EntryPoint = "R3D_LoadModel", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Model LoadModel(sbyte* filePath);

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
        /// <param name="data">Pointer to the memory buffer containing the model data</param>
        /// <param name="size">Size of the data buffer in bytes</param>
        /// <returns>Loaded model structure containing meshes and materials</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_LoadModelFromMemory", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Model LoadModelFromMemory(sbyte* fileType, void* data, uint size);

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
        /// <param name="mesh">Pointer to the mesh to be wrapped in a model structure</param>
        /// <returns>Model structure containing the specified mesh</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_LoadModelFromMesh", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.Model LoadModelFromMesh(Types.Mesh* mesh);

        /// <summary>
        /// Unload a model and optionally its materials.
        ///
        /// Frees all memory associated with a model, including its meshes.
        /// Materials can be optionally unloaded as well
        /// </summary>
        /// <param name="model">Pointer to the model structure to be unloaded</param>
        /// <param name="unloadMaterials">If true, also unloads all materials associated with the model. Set to false if textures are still being used elsewhere to avoid freeing shared resources</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UnloadModel", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnloadModel(Types.Model* model, CBool unloadMaterials);

        /// <summary>
        /// Update the bounding box of a model.
        ///
        /// Recalculates the axis-aligned bounding box (AABB) of the entire model
        /// by examining all meshes within the model. Optionally updates individual
        /// mesh bounding boxes as well
        /// </summary>
        /// <param name="model">Pointer to the model structure whose bounding box will be updated</param>
        /// <param name="updateMeshBoundingBoxes">If true, also updates the bounding box of each individual mesh within the model before calculating the model's overall bounding box</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UpdateModelBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdateModelBoundingBox(Types.Model* model, CBool updateMeshBoundingBoxes);

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
        /// <returns>Pointer to a dynamically allocated array of ModelAnimation. NULL on failure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_LoadModelAnimations", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.ModelAnimation* LoadModelAnimations(sbyte* fileName, out int animCount, int targetFrameRate);

        /// <summary>
        /// Loads model animations from memory data in a supported format (e.g., GLTF, IQM).
        ///
        /// This function parses animation data from the given memory buffer and returns an array
        /// of ModelAnimation structs. The caller is responsible for freeing the returned data
        /// using UnloadModelAnimations()
        /// </summary>
        /// <param name="fileType">File format hint (e.g., "gltf", "iqm", ".gltf"). The leading dot is optional</param>
        /// <param name="data">Pointer to the model data in memory</param>
        /// <param name="size">Size of the data buffer in bytes</param>
        /// <param name="animCount">An integer that will receive the number of animations loaded</param>
        /// <param name="targetFrameRate">Desired frame rate (FPS) to sample the animation at. For example, 30 or 60</param>
        /// <returns>Pointer to a dynamically allocated array of ModelAnimation. NULL on failure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_LoadModelAnimationsFromMemory", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.ModelAnimation* LoadModelAnimationsFromMemory(sbyte* fileType, void* data, uint size, out int animCount, int targetFrameRate);

        /// <summary>
        /// Frees memory allocated for model animations.
        ///
        /// This should be called after you're done using animations loaded via LoadModelAnimations()
        /// </summary>
        /// <param name="animations">Pointer to the animation array to free</param>
        /// <param name="animCount">Number of animations in the array</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UnloadModelAnimations", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnloadModelAnimations(Types.ModelAnimation* animations, int animCount);

        /// <summary>
        /// Finds and returns a pointer to a named animation within the array.
        ///
        /// Searches the given array of animations for one that matches the specified name
        /// </summary>
        /// <param name="animations">Array of animations to search</param>
        /// <param name="animCount">Number of animations in the array</param>
        /// <param name="name">Name of the animation to find (case-sensitive)</param>
        /// <returns>Pointer to the matching animation, or NULL if not found</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetModelAnimation", CallingConvention = CallingConvention.Cdecl)]
        public static extern Types.ModelAnimation* GetModelAnimation(Types.ModelAnimation* animations, int animCount, sbyte* name);

        /// <summary>
        /// Logs the names of all animations in the array (for debugging or inspection).
        ///
        /// Prints the animation names (and possibly other info) to the standard output or debug console
        /// </summary>
        /// <param name="animations">Array of animations to list</param>
        /// <param name="animCount">Number of animations in the array</param>
        [DllImport(NativeDll, EntryPoint = "R3D_ListModelAnimations", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ListModelAnimations(Types.ModelAnimation* animations, int animCount);

        /// <summary>
        /// Sets the scaling factor applied to models on loading.
        ///
        /// The functions sets the scaling factor to be used when loading models. This value
        /// is only applied to models loaded after this value is set
        /// </summary>
        /// <param name="value">Scaling factor to be used (i.e. 0.01 for meters to centimeters)</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetModelImportScale", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetModelImportScale(float value);

        #endregion

        #region Lights Config Functions

        /// <summary>
        /// Creates a new light of the specified type.
        /// 
        /// This function creates a light of the given type. The light must be destroyed
        /// manually when no longer needed by calling `DestroyLight`
        /// </summary>
        /// <param name="type">The type of light to create (directional, spot or omni-directional)</param>
        /// <returns>The ID of the created light</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_CreateLight", CallingConvention = CallingConvention.Cdecl)]
        public static extern uint CreateLight(LightType type);

        /// <summary>
        /// Destroys the specified light.
        ///
        /// This function deallocates the resources associated with the light and makes
        /// the light ID invalid. It must be called after the light is no longer needed
        /// </summary>
        /// <param name="id">The ID of the light to destroy</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DestroyLight", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroyLight(uint id);

        /// <summary>
        /// Checks if a light exists.
        ///
        /// This function checks if the specified light ID is valid and if the light exists
        /// </summary>
        /// <param name="id">The ID of the light to check</param>
        /// <returns>True if the light exists, false otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_IsLightExist", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool IsLightExist(uint id);

        /// <summary>
        /// Gets the type of a light.
        ///
        /// This function returns the type of the specified light (directional, spot or omni-directional)
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The type of the light</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightType", CallingConvention = CallingConvention.Cdecl)]
        public static extern LightType GetLightType(uint id);

        /// <summary>
        /// Checks if a light is active.
        ///
        /// This function checks whether the specified light is currently active (enabled or disabled)
        /// </summary>
        /// <param name="id">The ID of the light to check</param>
        /// <returns>True if the light is active, false otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_IsLightActive", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool IsLightActive(uint id);

        /// <summary>
        /// Toggles the state of a light (active or inactive).
        ///
        /// This function toggles the state of the specified light, turning it on if it is off,
        /// or off if it is on
        /// </summary>
        /// <param name="id">The ID of the light to toggle</param>
        [DllImport(NativeDll, EntryPoint = "R3D_ToggleLight", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ToggleLight(uint id);


        /// <summary>
        /// Sets the active state of a light.
        /// 
        /// This function allows manually turning a light on or off by specifying its active state
        /// </summary>
        /// <param name="id">The ID of the light to set the active state for</param>
        /// <param name="active">True to activate the light, false to deactivate it</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightActive", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightActive(uint id, CBool active);

        /// <summary>
        /// Gets the color of a light.
        ///
        /// This function retrieves the color of the specified light as a `Color` structure
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The color of the light as a `Color` structure</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern Color GetLightColor(uint id);

        /// <summary>
        /// Gets the color of a light as a `Vector3`.
        ///
        /// This function retrieves the color of the specified light as a `Vector3`, where each
        /// component (x, y, z) represents the RGB values of the light
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The color of the light as a `Vector3`</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightColorV", CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 GetLightColorV(uint id);

        /// <summary>
        /// Sets the color of a light.
        ///
        /// This function sets the color of the specified light using a `Color` structure
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="color">The new color to set for the light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightColor(uint id, Color color);

        /// <summary>
        /// Sets the color of a light using a `Vector3`.
        ///
        /// This function sets the color of the specified light using a `Vector3`, where each
        /// component (x, y, z) represents the RGB values of the light
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="color">The new color to set for the light as a `Vector3`</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightColorV", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightColorV(uint id, Vector3 color);

        /// <summary>
        /// Gets the position of a light.
        ///
        /// This function retrieves the position of the specified light.
        /// Only applicable to spot lights or omni-lights
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The position of the light as a `Vector3`</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightPosition", CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 GetLightPosition(uint id);

        /// <summary>
        /// Sets the position of a light.
        ///
        /// This function sets the position of the specified light.
        /// Only applicable to spot lights or omni-lights
        /// </summary>
        /// <remarks>
        /// Has no effect for directional lights.
        /// If called on a directional light,
        /// a warning will be logged
        /// </remarks>
        /// <param name="id">The ID of the light</param>
        /// <param name="position">The new position to set for the light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightPosition", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightPosition(uint id, Vector3 position);

        /// <summary>
        /// Gets the direction of a light.
        ///
        /// This function retrieves the direction of the specified light.
        /// Only applicable to directional lights or spot lights
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The direction of the light as a `Vector3`</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightDirection", CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 GetLightDirection(uint id);

        /// <summary>
        /// Sets the direction of a light.
        /// 
        /// This function sets the direction of the specified light.
        /// Only applicable to directional lights or spot lights
        /// </summary>
        /// <remarks>
        /// Has no effect for omni-directional lights.
        /// If called on an omni-directional light,
        /// a warning will be logged
        /// </remarks>
        /// <param name="id">The ID of the light</param>
        /// <param name="direction">The new direction to set for the light. The vector is automatically normalized</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightDirection", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightDirection(uint id, Vector3 direction);

        /// <summary>
        /// Sets the position and direction of a light to look at a target point.
        /// 
        /// This function sets both the position and the direction of the specified light,
        /// causing it to "look at" a given target point
        /// </summary>
        /// <remarks>
        /// For directional lights, only the direction is updated (position is ignored).
        /// For omni-directional lights, only the position is updated (direction is not calculated).
        /// For spot lights, both position and direction are set accordingly.
        /// This function does NOT emit any warning or log message
        /// </remarks>
        /// <param name="id">The ID of the light</param>
        /// <param name="position">The position to set for the light</param>
        /// <param name="target">The point the light should look at</param>
        [DllImport(NativeDll, EntryPoint = "R3D_LightLookAt", CallingConvention = CallingConvention.Cdecl)]
        public static extern void LightLookAt(uint id, Vector3 position, Vector3 target);

        /// <summary>
        /// Gets the energy level of a light.
        ///
        /// This function retrieves the energy level (intensity) of the specified light.
        /// Energy typically affects the brightness of the light
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The energy level of the light</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightEnergy", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetLightEnergy(uint id);

        /// <summary>
        /// Sets the energy level of a light.
        ///
        /// This function sets the energy (intensity) of the specified light.
        /// A higher energy value will result in a brighter light
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="energy">The new energy value to set for the light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightEnergy", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightEnergy(uint id, float energy);

        /// <summary>
        /// Gets the specular intensity of a light.
        ///
        /// This function retrieves the current specular intensity of the specified light.
        /// Specular intensity affects how shiny surfaces appear when reflecting the light
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The current specular intensity of the light</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightSpecular", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetLightSpecular(uint id);

        /// <summary>
        /// Sets the specular intensity of a light.
        ///
        /// This function sets the specular intensity of the specified light.
        /// Higher specular values result in stronger and sharper highlights on reflective surfaces
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="specular">The new specular intensity value to set for the light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightSpecular", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightSpecular(uint id, float specular);

        /// <summary>
        /// Gets the range of a light.
        ///
        /// This function retrieves the range of the specified light, which determines how far the light can affect.
        /// Only applicable to spot lights or omni-lights
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The range of the light</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightRange", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetLightRange(uint id);

        /// <summary>
        /// Sets the range of a light.
        ///
        /// This function sets the range of the specified light.
        /// The range determines how far the light can illuminate the scene before it fades out.
        /// Only applicable to spot lights or omni-lights
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="range">The new range to set for the light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightRange", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightRange(uint id, float range);

        /// <summary>
        /// Gets the attenuation factor of a light.
        ///
        /// This function retrieves the attenuation factor of the specified light.
        /// Attenuation controls how the intensity of a light decreases with distance.
        /// Only applicable to spot lights or omni-lights
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The attenuation factor of the light</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightAttenuation", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetLightAttenuation(uint id);

        /// <summary>
        /// Sets the attenuation factor of a light.
        ///
        /// This function sets the attenuation factor of the specified light.
        /// A higher attenuation value causes the light to lose intensity more quickly as the distance increases.
        /// For a realistic effect, an attenuation factor of 2.0f is typically used.
        /// Only applicable to spot lights or omni-lights
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="attenuation">The new attenuation factor to set for the light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightAttenuation", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightAttenuation(uint id, float attenuation);

        /// <summary>
        /// Gets the inner cutoff angle of a spotlight.
        ///
        /// This function retrieves the inner cutoff angle of a spotlight.
        /// The inner cutoff defines the cone of light where the light is at full intensity
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The inner cutoff angle in degrees of the spotlight</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightInnerCutOff", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetLightInnerCutOff(uint id);

        /// <summary>
        /// Sets the inner cutoff angle of a spotlight.
        ///
        /// This function sets the inner cutoff angle of a spotlight.
        /// The inner cutoff angle defines the cone where the light is at full intensity.
        /// Anything outside this cone starts to fade
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="degrees">The new inner cutoff angle in degrees</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightInnerCutOff", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightInnerCutOff(uint id, float degrees);

        /// <summary>
        /// Gets the outer cutoff angle of a spotlight.
        ///
        /// This function retrieves the outer cutoff angle of a spotlight.
        /// The outer cutoff defines the outer boundary of the light's cone, where the light starts to fade
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The outer cutoff angle in degrees of the spotlight</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightOuterCutOff", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetLightOuterCutOff(uint id);

        /// <summary>
        /// Sets the outer cutoff angle of a spotlight.
        ///
        /// This function sets the outer cutoff angle of a spotlight.
        /// The outer cutoff defines the boundary of the light's cone where the light intensity starts to gradually decrease
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="degrees">The new outer cutoff angle in degrees</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetLightOuterCutOff", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetLightOuterCutOff(uint id, float degrees);

        #endregion

        #region Shadow Config Functions

        /// <summary>
        /// Enables shadow casting for a light and sets the resolution of its shadow map.
        /// 
        /// This function enables shadow casting for a specified light and allocates a shadow map with the specified resolution.
        /// Shadows can be rendered from the light based on this shadow map
        /// </summary>
        /// <param name="id">The ID of the light for which shadows should be enabled</param>
        /// <param name="resolution">The resolution of the shadow map to be used by the light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_EnableShadow", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableShadow(uint id, int resolution);

        /// <summary>
        /// Disables shadow casting for a light and optionally destroys its shadow map.
        ///
        /// This function disables shadow casting for the specified light and optionally frees the memory
        /// used by its shadow map. If `destroyMap` is true, the shadow map will be destroyed, otherwise,
        /// the map will be retained but the light will no longer cast shadows
        /// </summary>
        /// <param name="id">The ID of the light for which shadows should be disabled</param>
        /// <param name="destroyMap">Whether or not to destroy the shadow map associated with the light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DisableShadow", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DisableShadow(uint id, CBool destroyMap);

        /// <summary>
        /// Checks if shadow casting is enabled for a light.
        ///
        /// This function checks if shadow casting is currently enabled for the specified light
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>True if shadow casting is enabled, false otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_IsShadowEnabled", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool IsShadowEnabled(uint id);

        /// <summary>
        /// Checks if a light has an associated shadow map.
        ///
        /// This function checks if the specified light has a shadow map allocated for it
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>True if the light has a shadow map, false otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_HasShadowMap", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool HasShadowMap(uint id);

        /// <summary>
        /// Gets the shadow map update mode of a light.
        ///
        /// This function retrieves the current mode for updating the shadow map of a light. The mode can be:
        /// 1. Interval: Updates the shadow map at a fixed interval.
        /// 2. Continuous: Updates the shadow map continuously.
        /// 3. Manual: Updates the shadow map manually (via explicit function calls)
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The shadow map update mode</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetShadowUpdateMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern ShadowUpdateMode GetShadowUpdateMode(uint id);

        /// <summary>
        /// Sets the shadow map update mode of a light.
        ///
        /// This function sets the mode for updating the shadow map of the specified light.
        /// The update mode controls when and how often the shadow map is refreshed
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="mode">The update mode to set for the shadow map (Interval, Continuous, or Manual)</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetShadowUpdateMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetShadowUpdateMode(uint id, ShadowUpdateMode mode);

        /// <summary>
        /// Gets the frequency of shadow map updates for the interval update mode.
        ///
        /// This function retrieves the frequency (in milliseconds) at which the shadow map should be updated when
        /// the interval update mode is enabled. This function is only relevant if the shadow map update mode is set
        /// to "Interval"
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The frequency in milliseconds at which the shadow map is updated</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetShadowUpdateFrequency", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetShadowUpdateFrequency(uint id);

        /// <summary>
        /// Sets the frequency of shadow map updates for the interval update mode.
        ///
        /// This function sets the frequency (in milliseconds) at which the shadow map should be updated when
        /// the interval update mode is enabled. This function is only relevant if the shadow map update mode is set
        /// to "Interval"
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="msec">The frequency in milliseconds at which to update the shadow map</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetShadowUpdateFrequency", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetShadowUpdateFrequency(uint id, int msec);

        /// <summary>
        /// Forces an immediate update of the shadow map during the next rendering pass.
        ///
        /// This function forces the shadow map of the specified light to be updated during the next call to `End`.
        /// This is primarily used for the manual update mode, but may also work for the interval mode
        /// </summary>
        /// <param name="id">The ID of the light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UpdateShadowMap", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdateShadowMap(uint id);

        /// <summary>
        /// Retrieves the softness factor used to simulate penumbra in shadows.
        ///
        /// This function returns the current softness factor for the specified light's shadow map.
        /// A higher softness value will produce softer shadow edges, simulating a broader penumbra,
        /// while a lower value results in sharper shadows
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The softness factor currently set for the shadow (typically in the range [0.0, 1.0])</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetShadowSoftness", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetShadowSoftness(uint id);

        /// <summary>
        /// Sets the softness factor used to simulate penumbra in shadows.
        ///
        /// This function adjusts the softness of the shadow edges for the specified light.
        /// Increasing the softness value creates more diffuse, penumbra-like shadows
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="softness">The softness factor to apply (typically in the range [0.0, 1.0])</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetShadowSoftness", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetShadowSoftness(uint id, float softness);

        /// <summary>
        /// Gets the shadow bias of a light.
        ///
        /// This function retrieves the shadow bias value for the specified light. The shadow bias helps prevent shadow artifacts,
        /// such as shadow acne, by slightly offsetting the depth comparisons used in shadow mapping
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <returns>The shadow bias value</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetShadowBias", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetShadowBias(uint id);

        /// <summary>
        /// Sets the shadow bias of a light.
        ///
        /// This function sets the shadow bias value for the specified light. Adjusting the shadow bias can help avoid shadow
        /// artifacts such as shadow acne by modifying the depth comparisons used in shadow mapping
        /// </summary>
        /// <param name="id">The ID of the light</param>
        /// <param name="value">The shadow bias value to set</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetShadowBias", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetShadowBias(uint id, float value);


        #endregion

        #region Light Helper Functions

        /// <summary>
        /// Returns the bounding box encompassing the light's area of influence.
        ///
        /// This function computes the axis-aligned bounding box (AABB) that encloses the
        /// volume affected by the specified light, based on its type:
        ///
        /// - For spotlights, the bounding box encloses the light cone.
        /// - For omni-directional lights, it encloses a sphere representing the light's range.
        /// - For directional lights, it returns an infinite bounding box to represent global influence.
        ///
        /// This bounding box is primarily useful for spatial partitioning, culling, or visual debugging
        /// </summary>
        /// <param name="light">The light for which to compute the bounding box</param>
        /// <returns>A BoundingBox struct that encloses the light's influence volume</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetLightBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern BoundingBox GetLightBoundingBox(uint light);

        /// <summary>
        /// Draws the area of influence of the light in 3D space.
        ///
        /// This function visualizes the area affected by a light in 3D space.
        /// It draws the light's influence, such as the cone for spotlights or the volume for omni-lights.
        /// This function is only relevant for spotlights and omni-lights
        /// </summary>
        /// <remarks>
        /// This function should be called while using the default 3D rendering mode of raylib, 
        /// not with R3D's rendering mode. It uses raylib's 3D drawing functions to render the light's shape
        /// </remarks>
        /// <param name="id">The ID of the light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawLightShape", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawLightShape(uint id);

        #endregion

        #region Particle System Functions

        /// <summary>
        /// Loads a particle emitter system for the CPU.
        ///
        /// This function initializes a particle emitter system on the CPU with a specified maximum
        /// number of particles. It prepares the necessary data structures and allocates memory
        /// for managing the particles
        /// </summary>
        /// <param name="maxParticles">The maximum number of particles the system can handle at once. This value determines the memory allocation and performance constraints</param>
        /// <returns>A newly initialized `ParticleSystem` structure. The caller is responsible for properly managing and freeing the system when no longer needed</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_LoadParticleSystem", CallingConvention = CallingConvention.Cdecl)]
        public static extern ParticleSystem LoadParticleSystem(int maxParticles);

        /// <summary>
        /// Unloads the particle emitter system and frees allocated memory.
        ///
        /// This function deallocates the memory used by the particle emitter system and clears the associated resources.
        /// It should be called when the particle system is no longer needed to prevent memory leaks
        /// </summary>
        /// <param name="system">A pointer to the `ParticleSystem` to be unloaded</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UnloadParticleSystem", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnloadParticleSystem(ParticleSystem* system);

        /// <summary>
        /// Emits a particle in the particle system.
        ///
        /// This function triggers the emission of a new particle in the particle system. It handles the logic of adding a new
        /// particle to the system and initializing its properties based on the current state of the system
        /// </summary>
        /// <param name="system">A pointer to the `ParticleSystemCPU` where the particle will be emitted</param>
        /// <returns>`true` if the particle was successfully emitted, `false` if the system is at full capacity and cannot emit more particles</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_EmitParticle", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool EmitParticle(ParticleSystem* system);

        /// <summary>
        /// Updates the particle emitter system by advancing particle positions.
        ///
        /// This function updates the positions and properties of particles in the system based on the elapsed time. It handles
        /// simulation of particle movement, gravity, and other physics-based calculations
        /// </summary>
        /// <param name="system">A pointer to the `ParticleSystem` to be updated</param>
        /// <param name="deltaTime">The time elapsed since the last update (in seconds)</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UpdateParticleSystem", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdateParticleSystem(ParticleSystem* system, float deltaTime);

        /// <summary>
        /// Computes and updates the AABB (Axis-Aligned Bounding Box) of a particle system.
        ///
        /// This function simulates the particle system to estimate the region of space it occupies.
        /// It considers particle positions at mid-life and end-of-life to approximate the AABB,
        /// which is then stored in the system's `aabb` field. This is useful for enabling frustum culling,
        /// especially when the bounds are not known beforehand
        /// </summary>
        /// <param name="system">Pointer to the `ParticleSystem` to update</param>
        [DllImport(NativeDll, EntryPoint = "R3D_CalculateParticleSystemBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CalculateParticleSystemBoundingBox(ParticleSystem* system);

        #endregion

        #region Sprites Functions

        /// <summary>
        /// Load a sprite from a texture.
        ///
        /// This function creates a `Sprite` using the provided texture.
        /// The texture will be used as the albedo of the sprite's material.
        /// The default billboard mode applied to the material is `BillboardMode.YAxis`
        /// </summary>
        /// <remarks>
        /// The lifetime of the provided texture is managed by the caller
        /// </remarks>
        /// <param name="texture">The `Texture2D` to be used for the sprite</param>
        /// <param name="xFrameCount">The number of frames in the horizontal direction</param>
        /// <param name="yFrameCount">The number of frames in the vertical direction</param>
        /// <returns>A `Sprite` object initialized with the given texture and frame configuration</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_LoadSprite", CallingConvention = CallingConvention.Cdecl)]
        public static extern Sprite LoadSprite(Texture2D texture, int xFrameCount, int yFrameCount);

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
        /// <param name="sprite">A pointer to the `Sprite` to be unloaded</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UnloadSprite", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnloadSprite(Sprite* sprite);

        /// <summary>
        /// Updates the animation of a sprite.
        ///
        /// This function updates the current frame of the sprite's animation based on the provided speed. The animation frames are read from
        /// right to left, advancing the cursor to the next row after completing a line
        /// </summary>
        /// <remarks>
        /// The `speed` parameter can be calculated as the number of frames per second multiplied by `GetFrameTime()`
        /// </remarks>
        /// <param name="sprite">A pointer to the `Sprite` to update</param>
        /// <param name="speed">The speed at which the animation progresses, in frames per second</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UpdateSprite", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdateSprite(Sprite* sprite, float speed);

        /// <summary>
        /// Updates the animation of a sprite with specified frame boundaries.
        ///
        /// This function updates the current frame of the sprite's animation while restricting it between `firstFrame` and `lastFrame`.
        /// This is useful for spritesheets containing multiple animations
        /// </summary>
        /// <remarks>
        /// The frames are read from right to left, and the cursor moves to the next row after completing a line.
        /// The `speed` parameter can be calculated as the number of frames per second multiplied by `GetFrameTime()`
        /// </remarks>
        /// <param name="sprite">A pointer to the `Sprite` to update</param>
        /// <param name="firstFrame">The first frame of the animation loop</param>
        /// <param name="lastFrame">The last frame of the animation loop</param>
        /// <param name="speed">The speed at which the animation progresses, in frames per second</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UpdateSpriteEx", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UpdateSpriteEx(Sprite* sprite, int firstFrame, int lastFrame, float speed);

        #endregion

        #region Interpolation Curves Functions

        /// <summary>
        /// Loads an interpolation curve with a specified initial capacity.
        ///
        /// This function initializes an interpolation curve with the given capacity. The capacity represents the initial size of
        /// the memory allocated for the curve. You can add keyframes to the curve using `AddKeyframe`. If adding a keyframe
        /// exceeds the initial capacity, the system will automatically reallocate memory and double the initial capacity
        /// </summary>
        /// <param name="capacity">The initial capacity (size) of the interpolation curve before a reallocation occurs</param>
        /// <returns>An initialized interpolation curve with the specified capacity</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_LoadInterpolationCurve", CallingConvention = CallingConvention.Cdecl)]
        public static extern InterpolationCurve LoadInterpolationCurve(int capacity);

        /// <summary>
        /// Unloads the interpolation curve and frees the allocated memory.
        ///
        /// This function deallocates the memory associated with the interpolation curve and clears any keyframes stored in it.
        /// It should be called when the curve is no longer needed to avoid memory leaks
        /// </summary>
        /// <param name="curve">The interpolation curve to be unloaded</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UnloadInterpolationCurve", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnloadInterpolationCurve(InterpolationCurve curve);

        /// <summary>
        /// Adds a keyframe to the interpolation curve.
        ///
        /// This function adds a keyframe to the given interpolation curve at a specific time and value. If the addition of the
        /// keyframe requires reallocating memory and the reallocation fails, the previously allocated memory and keyframes are
        /// preserved, but the new keyframe is not added
        /// </summary>
        /// <param name="curve">A pointer to the interpolation curve to which the keyframe will be added</param>
        /// <param name="time">The time at which the keyframe will be added</param>
        /// <param name="value">The value associated with the keyframe</param>
        /// <returns>`true` if the keyframe was successfully added, or `false` if the reallocation failed</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_AddKeyframe", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool AddKeyframe(InterpolationCurve* curve, float time, float value);

        /// <summary>
        /// Evaluates the interpolation curve at a specific time.
        ///
        /// This function evaluates the value of the interpolation curve at a given time. The curve will interpolate between
        /// keyframes based on the time provided
        /// </summary>
        /// <param name="curve">The interpolation curve to be evaluated</param>
        /// <param name="time">The time at which to evaluate the curve</param>
        /// <returns>The value of the curve at the specified time</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_EvaluateCurve", CallingConvention = CallingConvention.Cdecl)]
        public static extern float EvaluateCurve(InterpolationCurve curve, float time);

        #endregion

        #region Background And Ambient Config Functions

        /// <summary>
        /// Sets the background color when no skybox is enabled.
        ///
        /// This function defines the background color to be used when no skybox is active.
        /// The color will be used for the clear color of the scene
        /// </summary>
        /// <param name="color">The color to set as the background color</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetBackgroundColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetBackgroundColor(Color color);

        /// <summary>
        /// Sets the ambient light color when no skybox is enabled.
        ///
        /// This function defines the ambient light color to be used when no skybox is active.
        /// It affects the overall lighting of the scene when no skybox is present
        /// </summary>
        /// <param name="color">The color to set for ambient light</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetAmbientColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAmbientColor(Color color);

        /// <summary>
        /// Enables a skybox for the scene.
        ///
        /// This function enables a skybox in the scene, replacing the default background with
        /// a 3D environment. The skybox is defined by the specified skybox asset
        /// </summary>
        /// <param name="skybox">The skybox to enable</param>
        [DllImport(NativeDll, EntryPoint = "R3D_EnableSkybox", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableSkybox(Skybox skybox);

        /// <summary>
        /// Disables the skybox in the scene.
        ///
        /// This function disables the skybox, reverting back to the default background
        /// color (or no background if none is set). It should be called to remove the skybox
        /// from the scene
        /// </summary>
        [DllImport(NativeDll, EntryPoint = "R3D_DisableSkybox", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DisableSkybox();

        /// <summary>
        /// Sets the rotation of the skybox.
        ///
        /// This function allows you to specify the rotation of the skybox along the
        /// pitch, yaw, and roll axes, which allows the skybox to be rotated in the scene
        /// </summary>
        /// <param name="pitch">The rotation angle around the X-axis (in degrees)</param>
        /// <param name="yaw">The rotation angle around the Y-axis (in degrees)</param>
        /// <param name="roll">The rotation angle around the Z-axis (in degrees)</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetSkyboxRotation", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSkyboxRotation(float pitch, float yaw, float roll);

        /// <summary>
        /// Gets the current rotation of the skybox.
        ///
        /// This function returns the current rotation of the skybox as a vector containing
        /// the pitch, yaw, and roll values in degrees
        /// </summary>
        /// <returns>A vector containing the current pitch, yaw, and roll of the skybox</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetSkyboxRotation", CallingConvention = CallingConvention.Cdecl)]
        public static extern Vector3 GetSkyboxRotation();

        /// <summary>
        /// Sets the intensity scaling values used for the environment's skybox.
        ///
        /// This function controls the intensity of both the rendered skybox as well as
        /// the light that is generated from the skybox
        /// </summary>
        /// <param name="background">The intensity of the skybox rendered as the background. A value of 0.0 will disable rendering the skybox but. allow any generated lighting to still be applied</param>
        /// <param name="ambient">The intensity of ambient light produced by the skybox</param>
        /// <param name="reflection">The intensity of reflections of the skybox in reflective materials</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetSkyboxIntensity", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSkyboxIntensity(float background, float ambient, float reflection);

        /// <summary>
        /// Gets the intensity scaling values used for the environment's skybox.
        ///
        /// This function returns the intensity values for the rendered skybox as well
        /// the light that is generated from the skybox
        /// </summary>
        /// <param name="background">Pointer to store the intensity value for the rendered skybox</param>
        /// <param name="ambient">Pointer to store the intensity value for ambient light produced by the skybox</param>
        /// <param name="reflection">Pointer to store the intensity value for reflections from the skybox</param>
        [DllImport(NativeDll, EntryPoint = "R3D_GetSkyboxIntensity", CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetSkyboxIntensity(out float background, out float ambient, out float reflection);

        #endregion

        #region SSAO Config Functions

        /// <summary>
        /// Enables or disables Screen Space Ambient Occlusion (SSAO).
        ///
        /// This function toggles the SSAO effect. When enabled, SSAO enhances the realism
        /// of the scene by simulating ambient occlusion, darkening areas where objects
        /// are close together or in corners
        /// </summary>
        /// <param name="enabled">Whether to enable or disable SSAO</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetSSAO", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSSAO(CBool enabled);

        /// <summary>
        /// Gets the current state of SSAO.
        ///
        /// This function checks if SSAO is currently enabled or disabled
        /// </summary>
        /// <returns>True if SSAO is enabled, false otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetSSAO", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool GetSSAO();

        /// <summary>
        /// Sets the radius for SSAO effect.
        ///
        /// This function sets the radius used by the SSAO effect to calculate occlusion.
        /// A higher value will affect a larger area around each pixel, while a smaller value
        /// will create sharper and more localized occlusion
        /// </summary>
        /// <param name="value">The radius value to set for SSAO</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetSSAORadius", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSSAORadius(float value);

        /// <summary>
        /// Gets the current SSAO radius.
        ///
        /// This function retrieves the current radius value used by the SSAO effect
        /// </summary>
        /// <returns>The radius value for SSAO</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetSSAORadius", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetSSAORadius();

        /// <summary>
        /// Sets the bias for SSAO effect.
        ///
        /// This function sets the bias used by the SSAO effect to adjust how much occlusion
        /// is applied to the scene. A higher value can reduce artifacts, but may also
        /// result in less pronounced ambient occlusion
        /// </summary>
        /// <param name="value">The bias value for SSAO</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetSSAOBias", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSSAOBias(float value);

        /// <summary>
        /// Gets the current SSAO bias.
        ///
        /// This function retrieves the current bias value used by the SSAO effect
        /// </summary>
        /// <returns>The SSAO bias value</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetSSAOBias", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetSSAOBias();

        /// <summary>
        /// Sets the number of blur iterations for the SSAO effect.
        ///
        /// This function sets the number of blur iterations applied to the SSAO effect.
        /// By default, one iteration is performed, using a total of 12 samples for the
        /// Gaussian blur. Increasing the number of iterations results in a smoother
        /// ambient occlusion but may impact performance
        /// </summary>
        /// <param name="value">The number of blur iterations for SSAO</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetSSAOIterations", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSSAOIterations(int value);

        /// <summary>
        /// Gets the current number of blur iterations for the SSAO effect.
        ///
        /// This function retrieves the current number of blur iterations applied to the SSAO effect
        /// </summary>
        /// <returns>The number of blur iterations for SSAO</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetSSAOIterations", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetSSAOIterations();

        #endregion

        #region Bloom Config Functions

        /// <summary>
        /// Sets the bloom mode.
        ///
        /// This function configures the bloom effect mode, which determines how the bloom
        /// effect is applied to the rendered scene
        /// </summary>
        /// <param name="mode">The bloom mode to set</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetBloomMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetBloomMode(Bloom mode);

        /// <summary>
        /// Gets the current bloom mode.
        ///
        /// This function retrieves the bloom mode currently applied to the scene
        /// </summary>
        /// <returns>The current bloom mode</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetBloomMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern Bloom GetBloomMode();

        /// <summary>
        /// Sets the bloom intensity.
        ///
        /// This function controls the strength of the bloom effect. Higher values result
        /// in a more intense glow effect on bright areas of the scene
        /// </summary>
        /// <param name="value">The intensity value for bloom</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetBloomIntensity", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetBloomIntensity(float value);

        /// <summary>
        /// Gets the current bloom intensity.
        ///
        /// This function retrieves the intensity value of the bloom effect
        /// </summary>
        /// <returns>The current bloom intensity</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetBloomIntensity", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetBloomIntensity();

        /// <summary>
        /// Sets the bloom filter radius.
        ///
        /// Controls the radius of the blur filter applied during the upscaling stage
        /// of the bloom effect. A larger radius results in a wider glow around bright
        /// objects, creating a softer and more diffuse bloom. A value of 0 disables
        /// the filtering effect, preserving sharp bloom highlights
        /// </summary>
        /// <param name="value">The radius of the bloom filter (in pixels or arbitrary units depending on implementation)</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetBloomFilterRadius", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetBloomFilterRadius(int value);

        /// <summary>
        /// Gets the current bloom filter radius.
        ///
        /// Retrieves the current radius used for the bloom filter. This value determines
        /// how far the glow effect extends around bright areas in the scene
        /// </summary>
        /// <returns>The current bloom filter radius</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetBloomFilterRadius", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetBloomFilterRadius();

        /// <summary>
        /// Sets the bloom brightness threshold.
        ///
        /// Controls the brightness cutoff used during the downsampling stage of the
        /// bloom effect. If the color channel with the brightest value is below the
        /// set threshold the pixel will not be included in the bloom effect
        /// </summary>
        /// <param name="value">The lowest value to be included the bloom effect (in color value depending on implementation)</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetBloomThreshold", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetBloomThreshold(float value);

        /// <summary>
        /// Gets the bloom brightness threshold.
        ///
        /// Retrieves the current brightness cutoff used for the bloom effect. This value
        /// determines if a pixel will be included in the bloom effect based on it's brightness
        /// </summary>
        /// <returns>The current bloom brightness cutoff threshold</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetBloomThreshold", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetBloomThreshold();

        /// <summary>
        /// Sets the bloom brightness threshold's softness.
        ///
        /// Controls the softness of the cutoff between being include or excluded in the
        /// bloom effect. A value of 0 will result in a hard transition between being
        /// included or excluded, while larger values will give an increasingly
        /// softer transition
        /// </summary>
        /// <param name="value">The value of of the bloom brightness threshold's softness</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetBloomSoftThreshold", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetBloomSoftThreshold(float value);

        /// <summary>
        /// Gets the current bloom brightness threshold's softness.
        ///
        /// Retrieves the softness of the brightness cutoff for the bloom effect.
        /// This value determines the softness of the transition between being
        /// included or excluded in the bloom effect
        /// </summary>
        /// <returns>The current bloom brightness threshold's softness</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetBloomSoftThreshold", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetBloomSoftThreshold();

        #endregion

        #region Fog Config Functions

        /// <summary>
        /// Sets the fog mode.
        ///
        /// This function defines the type of fog effect applied to the scene.
        /// Different modes may provide linear, exponential, or volumetric fog effects
        /// </summary>
        /// <param name="mode">The fog mode to set</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetFogMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetFogMode(Fog mode);

        /// <summary>
        /// Gets the current fog mode.
        ///
        /// This function retrieves the fog mode currently applied to the scene
        /// </summary>
        /// <returns>The current fog mode</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetFogMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern Fog GetFogMode();

        /// <summary>
        /// Sets the color of the fog.
        ///
        /// This function defines the color of the fog effect applied to the scene.
        /// The fog color blends with objects as they are affected by fog
        /// </summary>
        /// <param name="color">The color to set for the fog</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetFogColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetFogColor(Color color);

        /// <summary>
        /// Gets the current fog color.
        ///
        /// This function retrieves the color currently used for the fog effect
        /// </summary>
        /// <returns>The current fog color</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetFogColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern Color GetFogColor();

        /// <summary>
        /// Sets the start distance of the fog.
        ///
        /// This function defines the distance from the camera at which fog begins to appear.
        /// Objects closer than this distance will not be affected by fog
        /// </summary>
        /// <param name="value">The start distance for the fog effect</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetFogStart", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetFogStart(float value);

        /// <summary>
        /// Gets the current fog start distance.
        ///
        /// This function retrieves the distance at which the fog begins to be applied
        /// </summary>
        /// <returns>The current fog start distance</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetFogStart", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetFogStart();

        /// <summary>
        /// Sets the end distance of the fog.
        ///
        /// This function defines the distance from the camera at which fog reaches full intensity.
        /// Objects beyond this distance will be completely covered by fog
        /// </summary>
        /// <param name="value">The end distance for the fog effect</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetFogEnd", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetFogEnd(float value);

        /// <summary>
        /// Gets the current fog end distance.
        ///
        /// This function retrieves the distance at which the fog is fully applied
        /// </summary>
        /// <returns>The current fog end distance</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetFogEnd", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetFogEnd();

        /// <summary>
        /// Sets the density of the fog.
        ///
        /// This function controls how thick the fog appears. Higher values result in
        /// denser fog, making objects fade out more quickly
        /// </summary>
        /// <param name="value">The density of the fog (higher values increase fog thickness)</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetFogDensity", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetFogDensity(float value);

        /// <summary>
        /// Gets the current fog density.
        ///
        /// This function retrieves the current density of the fog
        /// </summary>
        /// <returns>The current fog density</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetFogDensity", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetFogDensity();

        #endregion

        #region Tonemap Config Functions

        /// <summary>
        /// Sets the tonemapping mode.
        ///
        /// This function defines the tonemapping algorithm applied to the final rendered image.
        /// Different tonemap modes affect color balance, brightness compression, and overall
        /// scene appearance
        /// </summary>
        /// <param name="mode">The tonemap mode to set</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetTonemapMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetTonemapMode(Tonemap mode);

        /// <summary>
        /// Gets the current tonemapping mode.
        ///
        /// This function retrieves the tonemap mode currently applied to the scene
        /// </summary>
        /// <returns>The current tonemap mode</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetTonemapMode", CallingConvention = CallingConvention.Cdecl)]
        public static extern Tonemap GetTonemapMode();

        /// <summary>
        /// Sets the exposure level for tonemapping.
        ///
        /// This function adjusts the exposure level used in tonemapping, affecting
        /// the overall brightness of the rendered scene
        /// </summary>
        /// <param name="value">The exposure value (higher values make the scene brighter)</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetTonemapExposure", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetTonemapExposure(float value);

        /// <summary>
        /// Gets the current tonemap exposure level.
        ///
        /// This function retrieves the current exposure setting used in tonemapping
        /// </summary>
        /// <returns>The current tonemap exposure value</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetTonemapExposure", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetTonemapExposure();

        /// <summary>
        /// Sets the white point for tonemapping.
        ///
        /// This function defines the reference white level, which determines how bright
        /// areas of the scene are mapped to the final output
        /// </summary>
        /// <param name="value">The white point value</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetTonemapWhite", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetTonemapWhite(float value);

        /// <summary>
        /// Gets the current tonemap white point.
        ///
        /// This function retrieves the white point setting used in tonemapping
        /// </summary>
        /// <returns>The current tonemap white value</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetTonemapWhite", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetTonemapWhite();

        #endregion

        #region Color Adjustment Functions

        /// <summary>
        /// Sets the global brightness adjustment.
        ///
        /// This function controls the brightness of the final rendered image.
        /// Higher values make the image brighter, while lower values darken it
        /// </summary>
        /// <param name="value">The brightness adjustment value</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetBrightness", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetBrightness(float value);

        /// <summary>
        /// Gets the current brightness level.
        ///
        /// This function retrieves the brightness setting applied to the scene
        /// </summary>
        /// <returns>The current brightness value</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetBrightness", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetBrightness();

        /// <summary>
        /// Sets the global contrast adjustment.
        ///
        /// This function controls the contrast of the final rendered image.
        /// Higher values increase the difference between dark and bright areas
        /// </summary>
        /// <param name="value">The contrast adjustment value</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetContrast", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetContrast(float value);

        /// <summary>
        /// Gets the current contrast level.
        ///
        /// This function retrieves the contrast setting applied to the scene
        /// </summary>
        /// <returns>The current contrast value</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetContrast", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetContrast();

        /// <summary>
        /// Sets the global saturation adjustment.
        ///
        /// This function controls the color intensity of the final rendered image.
        /// Higher values make colors more vibrant, while lower values desaturate them
        /// </summary>
        /// <param name="value">The saturation adjustment value</param>
        [DllImport(NativeDll, EntryPoint = "R3D_SetSaturation", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetSaturation(float value);

        /// <summary>
        /// Gets the current saturation level.
        ///
        /// This function retrieves the saturation setting applied to the scene
        /// </summary>
        /// <returns>The current saturation value</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetSaturation", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetSaturation();

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
        [DllImport(NativeDll, EntryPoint = "R3D_LoadSkybox", CallingConvention = CallingConvention.Cdecl)]
        public static extern Skybox LoadSkybox(sbyte* fileName, CubemapLayout layout);

        /// <summary>
        /// Loads a skybox from an image in memory.
        ///
        /// This function loads a skybox cubemap from an image already loaded in memory,
        /// using a specified cubemap layout to map the six faces
        /// </summary>
        /// <param name="image">The source image in memory</param>
        /// <param name="layout">The cubemap layout format</param>
        /// <returns>The loaded skybox object</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_LoadSkyboxFromMemory", CallingConvention = CallingConvention.Cdecl)]
        public static extern Skybox LoadSkyboxFromMemory(Image image, CubemapLayout layout);

        /// <summary>
        /// Loads a skybox from a panorama texture file.
        ///
        /// This function loads a skybox from a panorama (equirectangular) texture file,
        /// and converts it into a cubemap with the specified resolution
        /// </summary>
        /// <param name="fileName">The path to the panorama texture file</param>
        /// <param name="size">The resolution of the generated cubemap (e.g., 512, 1024)</param>
        /// <returns>The loaded skybox object</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_LoadSkyboxPanorama", CallingConvention = CallingConvention.Cdecl)]
        public static extern Skybox LoadSkyboxPanorama(sbyte* fileName, int size);

        /// <summary>
        /// Loads a skybox from a panorama image in memory.
        ///
        /// This function loads a skybox from a panorama (equirectangular) image already loaded in memory,
        /// and converts it into a cubemap with the specified resolution
        /// </summary>
        /// <param name="image">The panorama image in memory</param>
        /// <param name="size">The resolution of the generated cubemap (e.g., 512, 1024)</param>
        /// <returns>The loaded skybox object</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_LoadSkyboxPanoramaFromMemory", CallingConvention = CallingConvention.Cdecl)]
        public static extern Skybox LoadSkyboxPanoramaFromMemory(Image image, int size);

        /// <summary>
        /// Unloads a skybox and frees its resources.
        ///
        /// This function removes a previously loaded skybox from memory.
        /// It should be called when the skybox is no longer needed to prevent memory leaks
        /// </summary>
        /// <param name="sky">The skybox to unload</param>
        [DllImport(NativeDll, EntryPoint = "R3D_UnloadSkybox", CallingConvention = CallingConvention.Cdecl)]
        public static extern void UnloadSkybox(Skybox sky);

        #endregion

        #region Frustum Test Functions

        /// <summary>
        /// Checks if a point is inside the view frustum.
        ///
        /// Tests whether a 3D point lies within the camera's frustum by checking against all six planes.
        /// Call this only between `Begin` and `End`.
        ///
        /// Useful when automatic frustum culling is disabled and you're using a custom spatial structure
        /// (e.g., octree, BVH, etc.)
        /// </summary>
        /// <remarks>
        /// This performs an exact plane-point test. Slower than bounding box tests.
        /// Frustum culling may incorrectly discard objects casting visible shadows
        /// </remarks>
        /// <param name="position">The 3D point to test</param>
        /// <returns>`true` if inside the frustum, `false` otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_IsPointInFrustum", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool IsPointInFrustum(Vector3 position);

        /// <summary>
        /// Checks if a sphere is inside the view frustum.
        ///
        /// Tests whether a sphere intersects the camera's frustum using plane-sphere tests.
        /// Call this only between `Begin` and `End`.
        ///
        /// Useful when managing visibility manually
        /// </summary>
        /// <remarks>
        /// More accurate but slower than bounding box approximations.
        /// May cause visual issues with shadow casters being culled too early
        /// </remarks>
        /// <param name="position">The center of the sphere</param>
        /// <param name="radius">The sphere's radius (must be positive)</param>
        /// <returns>`true` if at least partially inside the frustum, `false` otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_IsSphereInFrustum", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool IsSphereInFrustum(Vector3 position, float radius);

        /// <summary>
        /// Checks if an AABB is inside the view frustum.
        ///
        /// Determines whether an axis-aligned bounding box intersects the frustum.
        /// Call between `Begin` and `End`.
        ///
        /// For use in custom culling strategies or spatial partitioning systems
        /// </summary>
        /// <remarks>
        /// Exact but more costly than AABB pre-tests.
        /// May prematurely cull objects casting visible shadows
        /// </remarks>
        /// <param name="aabb">The bounding box to test</param>
        /// <returns>`true` if at least partially inside the frustum, `false` otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_IsAABBInFrustum", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool IsAABBInFrustum(BoundingBox aabb);

        /// <summary>
        /// Checks if an OBB is inside the view frustum.
        ///
        /// Tests an oriented bounding box (transformed AABB) for frustum intersection.
        /// Must be called between `Begin` and `End`.
        ///
        /// Use this for objects with transformations when doing manual culling
        /// </summary>
        /// <remarks>
        /// More expensive than AABB checks due to matrix operations.
        /// May incorrectly cull shadow casters.
        /// </remarks>
        /// <param name="aabb">Local-space bounding box</param>
        /// <param name="transform">World-space transform matrix</param>
        /// <returns>`true` if the transformed box intersects the frustum, `false` otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_IsOBBInFrustum", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool IsOBBInFrustum(BoundingBox aabb, Matrix4x4 transform);

        /// <summary>
        /// Fast pre-filtering test for point inside frustum bounding box.
        ///
        /// Performs an AABB check using the frustum's bounding volume.
        /// Useful for quick rejection before precise tests
        /// </summary>
        /// <remarks>
        /// May return false positives, never false negatives.
        /// Only checks against a loose AABB, not actual frustum planes
        /// </remarks>
        /// <param name="position">The 3D point to test</param>
        /// <returns>`true` if inside the frustum AABB, `false` otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_IsPointInFrustumBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool IsPointInFrustumBoundingBox(Vector3 position);

        /// <summary>
        /// Fast pre-filtering test for sphere inside frustum bounding box.
        ///
        /// Performs a quick check using the frustum's AABB to approximate intersection
        /// </summary>
        /// <remarks>
        /// Faster but less accurate than full frustum testing.
        /// May produce false positives
        /// </remarks>
        /// <param name="position">The center of the sphere</param>
        /// <param name="radius">Radius of the sphere</param>
        /// <returns>`true` if possibly intersecting the frustum AABB, `false` otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_IsSphereInFrustumBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool IsSphereInFrustumBoundingBox(Vector3 position, float radius);

        /// <summary>
        /// Fast pre-filtering test for AABB inside frustum bounding box.
        ///
        /// Performs a bounding box vs bounding box intersection to quickly eliminate non-visible objects.
        /// Useful as an initial coarse check before calling full frustum tests
        /// </summary>
        /// <remarks>
        /// False positives possible, but never false negatives.
        /// Does not use actual frustum planes.
        /// No OBB variant exists due to computational cost
        /// </remarks>
        /// <param name="aabb">The bounding box to test</param>
        /// <returns>`true` if intersecting the frustum AABB, `false` otherwise</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_IsAABBInFrustumBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        public static extern CBool IsAABBInFrustumBoundingBox(BoundingBox aabb);

        #endregion

        #region Default Texture Retrieval Functions

        /// <summary>
        /// Retrieves a default white texture.
        ///
        /// This texture is fully white (1,1,1,1), useful for default material properties
        /// </summary>
        /// <returns>A white texture</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetWhiteTexture", CallingConvention = CallingConvention.Cdecl)]
        public static extern Texture2D GetWhiteTexture();

        /// <summary>
        /// Retrieves a default black texture.
        ///
        /// This texture is fully black (0,0,0,1), useful for masking or default values
        /// </summary>
        /// <returns>A black texture</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetBlackTexture", CallingConvention = CallingConvention.Cdecl)]
        public static extern Texture2D GetBlackTexture();

        /// <summary>
        /// Retrieves a default normal map texture.
        ///
        /// This texture represents a neutral normal map (0.5, 0.5, 1.0), which applies no normal variation
        /// </summary>
        /// <returns>A neutral normal texture</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetNormalTexture", CallingConvention = CallingConvention.Cdecl)]
        public static extern Texture2D GetNormalTexture();

        #endregion

        #region Render Texture Retrieval Functions

        /// <summary>
        /// Retrieves the final scene color buffer.
        ///
        /// This texture stores the final rendered scene as a 24-bit RGB buffer
        /// </summary>
        /// <returns>The final color buffer texture</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetBufferColor", CallingConvention = CallingConvention.Cdecl)]
        public static extern Texture2D GetBufferColor();

        /// <summary>
        /// Retrieves the buffer containing the scene's normal data.
        ///
        /// This texture stores octahedral-compressed normals using two 16-bit per-channel RG components
        /// </summary>
        /// <remarks>
        /// You can find the decoding functions in the embedded shaders, such as 'screen/lighting.fs.glsl'
        /// </remarks>
        /// <returns>The normal buffer texture</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetBufferNormal", CallingConvention = CallingConvention.Cdecl)]
        public static extern Texture2D GetBufferNormal();

        /// <summary>
        /// Retrieves the final depth buffer.
        ///
        /// This texture contains the depth stored in 24 bits and a stencil buffer where each value is 0 or 1, indicating the presence of geometry.
        /// It is useful for post-processing effects outside of R3D
        /// </summary>
        /// <remarks>
        /// If you modify the texture parameters to sample the stencil instead of the depth,
        /// make sure to reset the parameters afterward
        /// </remarks>
        /// <returns>The final depth buffer texture</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetBufferDepth", CallingConvention = CallingConvention.Cdecl)]
        public static extern Texture2D GetBufferDepth();

        #endregion

        #region Camera Matrices Retrieval Functions

        /// <summary>
        /// Retrieves the view matrix.
        ///
        /// This matrix represents the camera's transformation from world space to view space.
        /// It is updated at the last call to 'Begin'
        /// </summary>
        /// <returns>The current view matrix</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetMatrixView", CallingConvention = CallingConvention.Cdecl)]
        public static extern Matrix4x4 GetMatrixView();

        /// <summary>
        /// Retrieves the inverse view matrix.
        ///
        /// This matrix transforms coordinates from view space back to world space.
        /// It is updated at the last call to 'Begin'
        /// </summary>
        /// <returns>The current inverse view matrix</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetMatrixInvView", CallingConvention = CallingConvention.Cdecl)]
        public static extern Matrix4x4 GetMatrixInvView();

        /// <summary>
        /// Retrieves the projection matrix.
        ///
        /// This matrix defines the transformation from view space to clip space.
        /// It is updated at the last call to 'Begin'
        /// </summary>
        /// <returns>The current projection matrix</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetMatrixProjection", CallingConvention = CallingConvention.Cdecl)]
        public static extern Matrix4x4 GetMatrixProjection();

        /// <summary>
        /// Retrieves the inverse projection matrix.
        ///
        /// This matrix transforms coordinates from clip space back to view space.
        /// It is updated at the last call to 'Begin'
        /// </summary>
        /// <returns>The current inverse projection matrix</returns>
        [DllImport(NativeDll, EntryPoint = "R3D_GetMatrixInvProjection", CallingConvention = CallingConvention.Cdecl)]
        public static extern Matrix4x4 GetMatrixInvProjection();

        #endregion

        #region Debug Buffer Rendering Functions

        /// <summary>
        /// Renders the internal albedo buffer to the screen.
        ///
        /// This function displays the albedo (diffuse) buffer as a 2D texture.
        /// It must be called outside of `Begin` and `End`
        /// </summary>
        /// <param name="x">X position to draw the buffer</param>
        /// <param name="y">Y position to draw the buffer</param>
        /// <param name="w">Width of the drawn buffer</param>
        /// <param name="h">Height of the drawn buffer</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawBufferAlbedo", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawBufferAlbedo(float x, float y, float w, float h);

        /// <summary>
        /// Renders the internal emission buffer to the screen.
        ///
        /// Displays the emission buffer, which contains emissive lighting data.
        /// Must be called outside of `Begin` and `End`
        /// </summary>
        /// <param name="x">X position to draw the buffer</param>
        /// <param name="y">Y position to draw the buffer</param>
        /// <param name="w">Width of the drawn buffer</param>
        /// <param name="h">Height of the drawn buffer</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawBufferEmission", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawBufferEmission(float x, float y, float w, float h);

        /// <summary>
        /// Renders the internal normal buffer to the screen.
        ///
        /// Displays the normal buffer, showing world-space normal data as colors.
        /// Must be called outside of `Begin` and `End`
        /// </summary>
        /// <param name="x">X position to draw the buffer</param>
        /// <param name="y">Y position to draw the buffer</param>
        /// <param name="w">Width of the drawn buffer</param>
        /// <param name="h">Height of the drawn buffer</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawBufferNormal", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawBufferNormal(float x, float y, float w, float h);

        /// <summary>
        /// Renders the ORM (Occlusion, Roughness, Metalness) buffer to the screen.
        ///
        /// Displays the ORM buffer, where each channel stores different material properties:
        /// - Red: Ambient occlusion
        /// - Green: Roughness
        /// - Blue: Metalness
        ///
        /// Must be called outside of `Begin` and `End`
        /// </summary>
        /// <param name="x">X position to draw the buffer</param>
        /// <param name="y">Y position to draw the buffer</param>
        /// <param name="w">Width of the drawn buffer</param>
        /// <param name="h">Height of the drawn buffer</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawBufferORM", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawBufferOrm(float x, float y, float w, float h);

        /// <summary>
        /// Renders the SSAO (Screen Space Ambient Occlusion) buffer to the screen.
        ///
        /// Displays the SSAO buffer, showing ambient occlusion data in grayscale.
        /// Must be called outside of `Begin` and `End`
        /// </summary>
        /// <param name="x">X position to draw the buffer</param>
        /// <param name="y">Y position to draw the buffer</param>
        /// <param name="w">Width of the drawn buffer</param>
        /// <param name="h">Height of the drawn buffer</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawBufferSSAO", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawBufferSsao(float x, float y, float w, float h);

        /// <summary>
        /// Renders the bloom buffer to the screen.
        ///
        /// Displays the bloom effect buffer, showing the extracted bright areas after blur processing.
        /// Must be called outside of `Begin` and `End`
        /// </summary>
        /// <param name="x">X position to draw the buffer</param>
        /// <param name="y">Y position to draw the buffer</param>
        /// <param name="w">Width of the drawn buffer</param>
        /// <param name="h">Height of the drawn buffer</param>
        [DllImport(NativeDll, EntryPoint = "R3D_DrawBufferBloom", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DrawBufferBloom(float x, float y, float w, float h);

        #endregion
    }
}
