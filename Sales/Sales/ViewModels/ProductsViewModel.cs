namespace Sales.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Common.Models;
    using Sales.Helpers;
    using Services;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private bool isRefreshing;

        /* a cada propiedad que se quiera que se refresque se le debe 
         * crear su atributo privado este debe ir en minúscula la propiedad en mayúscula
         * si se quiere que la lista se pinte en listview debe ser de tipo ObservableCollection
         * para que refresque se debe en la propiedad colocar en los get y set el sig codigo
         */
        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ProductsViewModel()
        {
            //en el constructor colocar la instancia para consumir los servicios API
            this.apiService = new ApiService();

            //ahora debo cargar los productos
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
            }
            //el metodo GetList() recibe como parametros: UrlBase,prefix, controller
            //este metodo se le envia el modelo que sea para consumir la API
            //Tomamos la url del diccionario de recursos de la vista
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.GetList<Product>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            /*En este punto tenemos devuelto una lista en objeto response que devuelve el JSON
             * es necesario hacerle cast porque devuelve un object
             */
            var list = (List<Product>)response.Result;

            //ahora convertimos la List a ObservableCollection
            this.Products = new ObservableCollection<Product>(list);
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
    }
}
