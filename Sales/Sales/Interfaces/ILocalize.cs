namespace Sales.Interfaces
{
    using System.Globalization;
    public interface ILocalize
    {
        //Configuración del teléfono
        CultureInfo GetCurrentCultureInfo();

        //para cambiar la configuración del teléfono
        void SetLocale(CultureInfo ci);
    }
}
