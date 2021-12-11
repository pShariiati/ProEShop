namespace ProEShop.Common.Helpers;

public class DuplicateColumns
{
    public DuplicateColumns(bool ok = true)
    {
        Ok = ok;
    }
    public bool Ok { get; set; }
    public List<string> Columns { get; set; }
}
