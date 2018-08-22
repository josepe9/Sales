namespace Sales.Infrastructure
{
    using ViewModels;

    /* clase para usar el modelo locator + mvvm
     * sirve para instanciar la viewmodel
     */
    public class InstanceLocator
    {
        // este es un objeto de la mainviewmodel que todas las páginas van a bindar
        //el binding principal va contra el main
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
