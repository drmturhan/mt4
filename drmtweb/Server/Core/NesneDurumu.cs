namespace DrMturhan.Server.Core
{
    public enum NesneDurumu
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
    public interface IDurumuOlanNesne
    {
        NesneDurumu Durum { get; set; }
    }
}
