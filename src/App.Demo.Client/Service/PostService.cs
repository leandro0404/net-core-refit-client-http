using App.Demo.Client.Entities;
using App.Demo.Client.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Demo.Client.Service
{
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
}
