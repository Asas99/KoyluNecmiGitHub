using Unity.VisualScripting;

public interface IKarakterMekanikleri
{
    //Script nameholder
}
public interface IHareket
{
    public void Y�r�();
}
public interface IAraba
{
    bool ArabadaM� {get; set;}
    bool BinebilirMi {get; set; }

    public void ArabayaBin�n();
}
