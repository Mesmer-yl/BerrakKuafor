using EntityLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstracts
{
    public interface IPostService
    {
        Task CreatePostAsync(PostAddVM _postAddVM);
        Task<(List<PostVM>?,bool)> GetPostsByUserAsync(string userName);
        (List<PostVM>?, bool) GetPostsByHairdresser(int hairdresserId);
        (List<PostVM>?, bool) GetAllPosts();
        void UpdatePost(PostUpdateVM _postUpdateVM);
        bool DeletePost(int postId);
        List<HairdresserForPostVM> GetHairdressers();
    }
}
