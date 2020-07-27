namespace Roxy.Lib
{
    public interface IConfigPanel
    {
        void PopulateControls(byte[] configBytes);

        byte[] GetConfigBytes();

        void PopulateRgbControls(byte[] configBytes);

        byte[] GetRgbConfigBytes();

        void PopulateKeyMappingControls(byte[] configBytes);

        byte[] GetKeyMappingBytes();

        int GetComboBoxIndex(sbyte sens);

        sbyte GetByteFromComboBox(int index);
    }
}
