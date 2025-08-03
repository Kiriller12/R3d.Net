namespace R3d.Net.Types
{
    /// <summary>
    /// Defines billboard modes for 3D objects
    /// 
    /// This enumeration defines how a 3D object aligns itself relative to the camera.
    /// It provides options to disable billboarding or to enable specific modes of alignment.
    /// </summary>
    public enum BillboardMode
    {
        /// <summary>
        /// Billboarding is disabled; the object retains its original orientation
        /// </summary>
        Disabled,

        /// <summary>
        /// Full billboarding; the object fully faces the camera, rotating on all axes
        /// </summary>
        Front,

        /// <summary>
        /// Y-axis constrained billboarding; the object rotates only around the Y-axis,
        /// keeping its "up" orientation fixed. This is suitable for upright objects like characters or signs
        /// </summary>
        YAxis
    }
}