public interface IPersistence
{
    void Save<T>(T information, string saveFileName);
    T Load<T>(string saveFileName);
    void DeleteSave(string saveFileName);
}