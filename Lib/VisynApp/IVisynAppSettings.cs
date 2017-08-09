namespace Visyn.Public.VisynApp
{
    /// <summary>
    /// Interface IVisynAppSettings
    /// </summary>
    public interface IVisynAppSettings
    {
        //
        bool AreValid { get; }

        void InitializeDefaultSettings(object context);
    }
}
