namespace ModelosTienda
{
    public class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var rutaCategorias = "api/Categorias";

            httpClient.BaseAddress = new Uri("https://localhost:7116/");


            //#### PRUEBA DE CRUD ####

            //Lectura de datos
            var response = httpClient.GetAsync("api/Categorias").Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var categorias = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelosTienda.ApiResult<List<ModelosTienda.Categoria>>>(json);

            //Insercion de datos
            var nuevaCategoria = new ModelosTienda.Categoria()
            {
                Id = 0,
                Nombre = "Laptop"
            };

            //Invocar el serico web para insettar la nueva especie
            var categoriaJson = Newtonsoft.Json.JsonConvert.SerializeObject(nuevaCategoria);
            var content = new StringContent(categoriaJson, System.Text.Encoding.UTF8, "application/json");
            response = httpClient.PostAsync(rutaCategorias, content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            //Deserializar la respuesta
            var categoriaCreada = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelosTienda.ApiResult<ModelosTienda.Categoria>>(json);

            //Actualizacion de datos
            categoriaCreada.Data.Nombre = "Laptop actualizado";
            categoriaJson = Newtonsoft.Json.JsonConvert.SerializeObject(categoriaCreada.Data);
            content = new StringContent(categoriaJson, System.Text.Encoding.UTF8, "application/json");
            response = httpClient.PutAsync($"{rutaCategorias}/{categoriaCreada.Data.Id}", content).Result;
            json = response.Content.ReadAsStringAsync().Result;

            var categoriaActualizada = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelosTienda.ApiResult<ModelosTienda.Categoria>>(json);

            //Eliminar datos
            response = httpClient.DeleteAsync($"{rutaCategorias}/{categoriaCreada.Data.Id}").Result;
            json = response.Content.ReadAsStringAsync().Result;
            var categoriaEliminada = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelosTienda.ApiResult<ModelosTienda.Categoria>>(json);

            Console.WriteLine(json);
            Console.ReadLine();
        }
    }
}