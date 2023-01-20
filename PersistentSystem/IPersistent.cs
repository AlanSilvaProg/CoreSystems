namespace CoreSystems.PersistentSystem
{
    public interface IPersistent
    {
        void Save<T>(T information, string saveFileName);
        T Load<T>(string saveFileName);
        void DeleteSave(string saveFileName);
    }
}