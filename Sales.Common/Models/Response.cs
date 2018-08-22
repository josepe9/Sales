namespace Sales.Common.Models
{
    //clase para que al consumir la api me de las respuestas de la acción
    public class Response
    {
        //si fue exitosa la comunicación
        public bool IsSuccess { get; set; }

        //si no es exitoso cargamos el message
        public string  Message { get; set; }

        //si es exitoso cargamos el valor a un objeto object, que sirve para cualquier
        //tipo de dato
        public object Result { get; set; }
    }
}
