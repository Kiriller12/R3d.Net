namespace R3d.Net.Types
{
    /// <summary>
    /// Tone mapping modes.
    /// 
    /// Controls how high dynamic range (HDR) colors are mapped to low dynamic range (LDR) for display
    /// </summary>
    public enum Tonemap
    {
        /// <summary>
        /// Simple linear mapping of HDR valuesSimple linear mapping of HDR values
        /// </summary>
        Linear,

        /// <summary>
        /// Reinhard tone mapping, a balanced method for compressing HDR values
        /// </summary>
        Reinhard,

        /// <summary>
        /// Filmic tone mapping, mimicking the response of photographic film
        /// </summary>
        Filmic,

        /// <summary>
        /// ACES tone mapping, a high-quality cinematic rendering technique
        /// </summary>
        Aces,

        /// <summary>
        /// AGX tone mapping, a modern technique designed to preserve both highlight and shadow details for HDR rendering
        /// </summary>
        Agx,

        /// <summary>
        /// Number of tone mapping modes (used internally)
        /// </summary>
        Count
    }
}
