using Unity.VisualScripting;

public interface IKarakterMekanikleri
{
    //Script nameholder
}
public interface IHareket
{
    public void Yürü();
}
public interface IAraba
{
    bool ArabadaMı {get; set;}
    bool BinebilirMi {get; set; }

    public void ArabayaBinİn();
}
