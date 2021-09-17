using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.Post;

namespace CustomerAddress.Business.Abstract
{
    public interface IPostService
    {

        public Task<int> PostStatusUpdateByPostId(int postId);
        public Task<List<PostListDto>> GetPosts();
        public Task<PostListDto> GetPostsById(int id);
        public Task<PostListDto> GetPostsByPostCode(string postCode);
        public Task<List<PostListNameDto>> GetPostsWithNames();

        public Task<List<PostListNameDto>> PostListByPostStatusAndBranchId(byte postStatus, int branchId);

        public Task<int> AddPostWithName(PostAddNameDto postAddNameDto);
        public Task<List<PostListNameDto>> PostListByPostStatus(byte postStatus);
        /// </summary>
        /// 
        public bool AnySenderIds(int senderId);

        public bool AnyReceivedIds(int receivedId);


        public bool AnyCompanyBranchIds(int companyBranchId);


        public bool AnyEmployeeIds(int employeeId);
       

        public Task<int> AddPost(PostAddDto postAddDto);


        /// <summary>
        /// ogrenci guncelleme servisi
        /// </summary>
        //public Task<int> UpdatePost(int id, PostUpdateDto postUpdateDto);
        public Task<int> UpdatePost(int id, PostUpdateDto postUpdateDto);
        public Task<int> UpdatePostByPostCode(string PostCode, PostUpdateDto postUpdateDto);

        /// <summary>
        /// ogrenci silme servisi
        /// </summary>
        public Task<int> DeletePost(int id);
        public Task<int> DeletePostByPostCode(string PostCode);
    }
}
