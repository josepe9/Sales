namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common.Models;
    using Newtonsoft.Json;

    /* los using colocarlos denbtro de los namespaces
     * colocar primero los system
     * luego los demás ordenados alfabeticamente
     */
    public class ApiService
    {
        /*devolver una lista de T que asume las veces de cualquier modelo
         * debe consumir el servicio de la api desde la url dada
         * que se divide así: urlBase = https://salesapiservices.azurewebsites.net
         * prefix = /api
         * cotroller = /products
         * donde la url completa es https://salesapiservices.azurewebsites.net/api/products
        */
        public async Task<Response> GetList<T>(string urlBase,string prefix, string controller)
        {
            try
            {
                /*consumir un servicio result
                 * 1. paso crear un cliente HttpClient, sirve para hacer la comunicacion
                 * 2. cargarle la direccion base 
                 * 3. concatenar el prefijo y el controlador
                 */
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                //leer tipo string json
                var answer = await response.Content.ReadAsStringAsync();
                //si no conectó
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                //pasar de strin a objeto se llama descerializar
                //pasar de objeto a string se llama serializar
                //convertir el string del json en una lista de objetos
                //desserializar una lista de un modelo en este caso de T para que quede generico
                //desserializamos la variable answer que es donde está el Json en string
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
