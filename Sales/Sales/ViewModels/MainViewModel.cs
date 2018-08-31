/* Clase donde todos debe ir es la mas importante del proyecto
 * siempre se debe llamar así
 */
namespace Sales.ViewModels
{
    public class MainViewModel
    {
        public ProductsViewModel Products{ get; set; }

        public MainViewModel()
        {
            this.Products = new ProductsViewModel();
        }
    }
}
