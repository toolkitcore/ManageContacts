namespace ManageContacts.Shared.Configurations;

public class JwtSetting
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiredMinute { get; set; }
}