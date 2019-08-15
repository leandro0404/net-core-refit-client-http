using App.Demo.Client.Entities;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Demo.Client.Interfaces
{
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
}
