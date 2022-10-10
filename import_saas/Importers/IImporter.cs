namespace import_saas.Importers;

public interface IImporter
{
    string Name { get; }
    void Execute();
}