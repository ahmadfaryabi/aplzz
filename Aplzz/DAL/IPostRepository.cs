using Aplzz.Models;

namespace Aplzz.DAL;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAll();
    Task<Post?> GetPostById(int id);
    Task Create(Post post);
    Task Update(Post post);
    Task<bool> Delete(int id);
    Task<bool> AddComment(Comment comment);
    Task<bool> AddLike(Like like);
    Task<bool> RemoveLike(int postId, int userId);
    Task<int> GetLikeCount(int postId);
    Task<bool> HasUserLikedPost(int postId, int userId);
}
