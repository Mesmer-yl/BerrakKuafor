using DataLayer.Repos.Abstracts;
using EntityLayer.Concretes;
using EntityLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Concretes
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHairdresserRepo _hairdresserRepo;

        public PostService(IPostRepo postRepo, UserManager<AppUser> userManager, IHairdresserRepo hairdresserRepo)
        {
            _postRepo = postRepo;
            _userManager = userManager;
            _hairdresserRepo = hairdresserRepo;
        }

        public async Task CreatePostAsync(PostAddVM _postAddVM)
        {
            var currentUser = await _userManager.FindByEmailAsync(_postAddVM.UserName);
            var post = new Post()
            {
                UserId = currentUser.Id,
                HairdresserId=_postAddVM.HairdresserId,
                ImageUrl=_postAddVM.ImageUrl,
                Detail=_postAddVM.Detail,
                CurrentTime=DateTime.Now
            };

            _postRepo.Add(post);
            _postRepo.Save();
        }

        public bool DeletePost(int postId)
        {
            var post = _postRepo.GetById(postId);
            if(post == null) return false;
            _postRepo.Delete(post);
            _postRepo.Save();
            return true;
        }

        public (List<PostVM>?, bool) GetAllPosts()
        {
            var posts = _postRepo.GetAll("AppUser,Hairdresser");
            var postList = new List<PostVM>();
            foreach (var post in posts)
            {
                var postVM = new PostVM()
                {
                    Id = post.Id,
                    NameSurname = post.AppUser.NameSurname,
                    HairdresserName = post.Hairdresser.Name,
                    ImageUrl = post.ImageUrl,
                    Detail = post.Detail,
                    CurrentTime = post.CurrentTime
                };
                postList.Add(postVM);
            }
            postList = postList.OrderByDescending(post => post.CurrentTime).ToList();
            return (postList, true);
        }

        public List<HairdresserForPostVM> GetHairdressers()
        {
            var hairdressers = _hairdresserRepo.GetAll();
            var modelList=new List<HairdresserForPostVM>();
            foreach(var h in hairdressers)
            {
                var model = new HairdresserForPostVM()
                {
                    Id = h.Id,
                    HairdresserName = h.Name
                };
                modelList.Add(model);
            }
            return modelList;
        }

        public (List<PostVM>?, bool) GetPostsByHairdresser(int hairdresserId)
        {
            var hairdresser = _hairdresserRepo.GetById(hairdresserId);
            if(hairdresser == null)return (null,false);
            var posts = _postRepo.GetAllByCondition(x => x.HairdresserId == hairdresserId, "AppUser,Hairdresser");
            var postList = new List<PostVM>();
            foreach (var post in posts)
            {
                var postVM = new PostVM()
                {
                    Id = post.Id,
                    NameSurname = post.AppUser.NameSurname,
                    HairdresserName = post.Hairdresser.Name,
                    ImageUrl = post.ImageUrl,
                    Detail = post.Detail,
                    CurrentTime = post.CurrentTime
                };
                postList.Add(postVM);
            }
            postList = postList.OrderByDescending(post => post.CurrentTime).ToList();
            return (postList, true);
        }

        public async Task<(List<PostVM>?, bool)> GetPostsByUserAsync(string userName)
        {
            var currentUser = await _userManager.FindByEmailAsync(userName);
            if (currentUser == null) return (null, false);
            var posts = _postRepo.GetAllByCondition(x=>x.UserId==currentUser.Id,"AppUser,Hairdresser");
            var postList = new List<PostVM>();
            foreach (var post in posts)
            {
                var postVM = new PostVM() { 
                    Id = post.Id,
                    NameSurname = post.AppUser.NameSurname,
                    HairdresserName = post.Hairdresser.Name,
                    ImageUrl = post.ImageUrl,
                    Detail = post.Detail,
                    CurrentTime = post.CurrentTime
                };
                postList.Add(postVM);
            }
            postList = postList.OrderByDescending(post => post.CurrentTime).ToList();
            return (postList,true);
        }

        public void UpdatePost(PostUpdateVM _postUpdateVM)
        {
            var post = _postRepo.GetById(_postUpdateVM.Id);
            post.Detail = _postUpdateVM.Detail;
            _postRepo.Update(post);
            _postRepo.Save();
        }
    }
}
