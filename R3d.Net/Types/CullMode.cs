namespace R3d.Net.Types
{
    /// <summary>
    /// Cull modes for rendering
    /// </summary>
    public enum CullMode
    {
        /// <summary>
        /// Disable culling
        /// </summary>
        None,

        /// <summary>
        /// Cull back-facing geometry
        /// </summary>
        Back,

        /// <summary>
        /// Cull front-facing geometry
        /// </summary>
        Front
    }
}