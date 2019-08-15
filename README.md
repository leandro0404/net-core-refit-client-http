# net-core-refit-client-http
Demo para modelo básico do uso da lib Refit  e Refit.HttpClientFactory demonstrando  boas práticas


* pacotes usados  { o resto é firula para injeção de dependencias do net core e boas práticas }

  <PackageReference Include="Refit" Version="4.7.9" />
  <PackageReference Include="Refit.HttpClientFactory" Version="4.7.9" />

*  entity
```
 public class Post
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
    
```
* interface
```
 public interface IPostService
    {
        [Get("/posts")]
        Task<IEnumerable<Post>> Get();
        [Get("/posts/{id}")]
        Task<Post> GetById(int id);
        [Post("/posts")]
        Task<Post> Created(Post post);
        [Put("/posts/{id}")]
        Task<Post> Update(int id, Post post);
        [Delete("/posts/{id}")]
        Task Remove(int id);
    }

```
* service
```
public class PostService : IPostService
    {
        private readonly IPostService _client;
        public PostService(IPostService client)
        {
            _client = client;
        }
        public Task<IEnumerable<Post>> Get()
        {
            return _client.Get();
        }
        public Task<Post> GetById(int id)
        {
            return _client.GetById(id);
        }
        public Task<Post> Created(Post post)
        {
            return _client.Created(post);
        }
        public Task<Post> Update(int id, Post post)
        {
            return _client.Update(id, post);
        }
        public Task Remove(int id)
        {
            return _client.Remove(id);
        }
    }
    
```
* registre as dependências e seja feliz !!!! 
 Lembrando que você pode registrar vários serviços em endpoints diferentes.
EX:
```
 private static IServiceCollection ConfigureServices( this IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();

            //Refit injection http client
            services.AddRefitClient<IPostService>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetValue<string>(AppSettingsUrl));
                //c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("scheme and token");
            });
        }
```
