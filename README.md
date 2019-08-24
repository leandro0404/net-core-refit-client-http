# net-core-refit-client-http
Demo para modelo básico do uso da lib Refit 
e Refit.HttpClientFactory utilizando boas práticas.

* o pacote precisa ser referenciado no projeto onde está a interface para que o refit consiga gerar o stub. caso contrario ocorrera erro  de não implementação da interface. (doesn't look like a Refit interface", I expect because it inherits from another interface) 

* pacotes usados  
{ o resto é firula para injeção de dependencias do net core e boas práticas }

```xml
  <PackageReference Include="Refit" Version="4.7.9" />
  <PackageReference Include="Refit.HttpClientFactory" Version="4.7.9" />
```

*  entity
```csharp
 public class Post
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
    
```
* interface
```csharp
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
```csharp
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
 Lembrando que você pode registrar 
 vários serviços em endpoints diferentes.

EX:
```csharp
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
<img src="https://github.com/leandro0404/net-core-refit-client-http/blob/master/img/console.png">
