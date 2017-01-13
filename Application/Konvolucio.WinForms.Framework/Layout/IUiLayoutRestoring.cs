// -----------------------------------------------------------------------
// <copyright file="IUiLayoutRestoring.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Konvolucio.WinForms.Framework
{
    /// <summary>
    /// •	Az olyan tualjodoságok amelyeknek paraméterezési sorrendje fontos 
    /// pl: a SplitContainer.SplitterDistance (mivel a SplitterDistance-et a az 
    /// szülő ablakhoz is igazítja, ezért bindingeléskor az OnPorpertyChange
    /// előbb törénk meg mint az abalk méretének véglegesre állítása, akkor az
    /// ablak méretének beállításkor a SplitterDistance elmozudl… ezzel jó sokat lehet szopni.
    /// Erre erősen ajánlott bevezetni miden olyan Form-ra és Control-ra LayoutRestore és
    /// LayoutSave metódusokat. És ezeket akkor meghívni amikor már a fő ablak Shown eseménye 
    /// elsült, így biztosítható hogy pixelre pontosan visszaállnak a vezérlők.
    /// </summary>
    public interface IUiLayoutRestoring
    {
         void LayoutSave();
         void LayoutRestore();
    }
}
