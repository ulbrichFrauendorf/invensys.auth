namespace invensys.auth.application.Common.Encryption;

internal class KeyInfo
{
    public byte[] Key { get; }
    public byte[] Iv { get; }
    
    private string KeyString => Convert.ToBase64String(Key);
    private string IvString => Convert.ToBase64String(Iv);

    public KeyInfo(string key, string iv)
    {
        Key = Convert.FromBase64String(key);
        Iv = Convert.FromBase64String(iv);
    }

    public KeyInfo(byte[] key, byte[] iv)
    {
        Key = key;
        Iv = iv;
    }

    public bool HasValues()
    {
        return !string.IsNullOrWhiteSpace(KeyString) && !string.IsNullOrWhiteSpace(IvString);
    }
}