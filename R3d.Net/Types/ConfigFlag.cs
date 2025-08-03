namespace R3d.Net.Types
{
    /// <summary>
    /// Flags to configure the rendering engine behavior.
    /// 
    /// These flags control various aspects of the rendering pipeline
    /// </summary>
    [Flags]
    public enum ConfigFlag : uint
    {
        /// <summary>
        /// No special rendering flags
        /// </summary>
        None = 0,

        /// <summary>
        /// Enables Fast Approximate Anti-Aliasing (FXAA)
        /// </summary>
        Fxaa = 1 << 0,

        /// <summary>
        /// Uses linear filtering when blitting the final image
        /// </summary>
        Linear = 1 << 1,

        /// <summary>
        /// Maintains the aspect ratio of the internal resolution when blitting the final image
        /// </summary>
        AspectKeep = 1 << 2,

        /// <summary>
        /// Performs a stencil test on each rendering pass affecting geometry
        /// </summary>
        StencilTest = 1 << 3,

        /// <summary>
        /// Performs a depth pre-pass before forward rendering, improving desktop GPU performance but unnecessary on mobile
        /// </summary>
        DepthPrepass = 1 << 4,

        /// <summary>
        /// Use 8-bit precision for the normals buffer (deferred); default is 16-bit float
        /// </summary>
        Normals8Bit = 1 << 5,

        /// <summary>
        /// Used to force forward rendering for opaque objects, useful for tile-based devices.
        /// Be careful, this flag should not be set when rendering, or you may get incorrect sorting of draw calls
        /// </summary>
        ForceForward = 1 << 6,

        /// <summary>
        /// Disables internal frustum culling. Manual culling is allowed,
        /// but may break shadow visibility if objects casting shadows are skipped
        /// </summary>
        NoFrustrumCulling = 1 << 7,

        /// <summary>
        /// Back-to-front sorting of transparent objects for correct blending of non-discarded fragments.
        /// Be careful, in 'force forward' mode this flag will also sort opaque objects in 'near-to-far' but in the same sorting pass
        /// </summary>
        TransparentSorting = 1 << 8,

        /// <summary>
        /// Front-to-back sorting of opaque objects to optimize depth testing at the cost of additional sorting.
        /// Please note, in 'force forward' mode this flag has no effect, see transparent sorting
        /// </summary>
        OpaqueSorting = 1 << 9,

        /// <summary>
        /// Use 32-bit HDR formats like R11G11B10F for intermediate color buffers instead of full 16-bit floats.
        /// Saves memory and bandwidth
        /// </summary>
        LowPrecisionBuffers = 1 << 10
    }
}
