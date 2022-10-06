namespace ppedv.Fuhrparkverwaltung.Model.Contracts
{
    public interface IDevice
    {
        void Init(string initCode);

        event Action<string> Overheating;
    }
}
