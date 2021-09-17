using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Post;
using CustomerAddress.Business.Validations.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PostListDto>>> GetPost()
        {

            try
            {
                var postList = await _postService.GetPosts();
                foreach (var item in postList)
                {
                    if (item.PostStatus == "1")
                    {
                        item.PostStatus = "KARGO YOLDA";
                    }
                    else if (item.PostStatus == "2")
                    {
                        item.PostStatus = "KARGO TESLİM EDİLDİ";
                    }
                    else
                    {
                        item.PostStatus = "BÖYLE KARGO YOK";
                    }
                }
                return Ok(postList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// <summary>


        ///////////////////////////
        ///
        [HttpGet("GetPostsById/{postID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PostListDto>>> GetPostsById(int postID)
        {

            try
            {
                var postObject = await _postService.GetPostsById(postID);
                return Ok(postObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }///////////////////////////
        ///
        [HttpGet("GetPostsByPostCode/{PostCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PostListDto>>> GetPostsByPostCode(string PostCode)
        {

            try
            {
                var postObject = await _postService.GetPostsByPostCode(PostCode);
                return Ok(postObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        ///////////////////////////
        ///
        [HttpGet("GetPostsWithNames")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PostListNameDto>>> GetPostsWithNames()
        {

            try
            {
                var postObject = await _postService.GetPostsWithNames();
                foreach (var item in postObject)
                {
                    if (item.PostStatus == "1")
                    {
                        item.PostStatus = "KARGO YOLDA";
                    }
                    else if (item.PostStatus == "2")
                    {
                        item.PostStatus = "KARGO TESLİM EDİLDİ";
                    }
                    else
                    {
                        item.PostStatus = "BÖYLE KARGO YOK";
                    }
                }
                return Ok(postObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        ///////////////////////////
        ///PostListByPostStatusAndBranchId
        ///
        [HttpGet("GetPostsByPostStatus /{postStatus}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PostListNameDto>>> GetPostsByPostStatus(byte postStatus)
        {

            try
            {
                var postObject = await _postService.PostListByPostStatus(postStatus);
                
                foreach (var item in postObject)
                {
                    if (item.PostStatus == "1")
                    {
                        item.PostStatus = "KARGO YOLDA";
                    }
                    else if (item.PostStatus == "2")
                    {
                        item.PostStatus = "KARGO TESLİM EDİLDİ";
                    }
                    else
                    {
                        item.PostStatus = "BÖYLE KARGO YOK";
                    }
                }
                return Ok(postObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        } ///////////////////////////
        ///PostListByPostStatusAndBranchId
        ///
        [HttpGet("PostListByPostStatusAndBranchId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PostListNameDto>>> PostListByPostStatusAndBranchId(byte postStatus, int branchId)
        {

                PostGetBranchStatusDto postGetBranchStatusDto = new PostGetBranchStatusDto();
                postGetBranchStatusDto.BranchId = branchId;
                postGetBranchStatusDto.PostStatus = postStatus;

                var validator = new PostGetByBranchStatusValidator(_postService);
                var validationResults = validator.Validate(postGetBranchStatusDto);
                var list = new List<string>();

                if (!validationResults.IsValid)
                {
                    foreach (var error in validationResults.Errors)
                    {
                        list.Add(error.ErrorMessage);
                    }
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
                try
                {

                    var postObject = await _postService.PostListByPostStatusAndBranchId(postGetBranchStatusDto.PostStatus, postGetBranchStatusDto.BranchId);
                    foreach (var item in postObject)
                    {
                        if (item.PostStatus == "1")
                        {
                            item.PostStatus = "KARGO YOLDA";
                        }
                        else if (item.PostStatus == "2")
                        {
                            item.PostStatus = "KARGO TESLİM EDİLDİ";
                        }
                        else
                        {
                            item.PostStatus = "BÖYLE KARGO YOK";
                        }
                    }
                    return Ok(postObject);


                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

          }
        ///////////////////////////////
            /// <summary>
            /// 

            /// ///////////////////////////

        [HttpPost("AddPost")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddPost(PostAddDto postAddDto)
        {
            var validator = new PostAddValidator(_postService);
            var validationResults = validator.Validate(postAddDto);
            var list = new List<string>();

            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {

                var result = await _postService.AddPost(postAddDto);
                if (result == -2)
                {
                    list.Add("Böyle bir postnumber zaten mevcut tekrar giriniz");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
                else
                {
                    list.Add("Ekleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        ///// ///////////////////////////

        [HttpPost("AddPostWithName")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddPostWithName(PostAddNameDto postAddNameDto)
        {
            var validator = new PostAddWithNameValidator(_postService);
            var validationResults = validator.Validate(postAddNameDto);
            var list = new List<string>();

            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {

                var result = await _postService.AddPostWithName(postAddNameDto);
                if (result == -2)
                {
                    list.Add("Böyle bir postnumber zaten mevcut tekrar giriniz");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
                else
                {
                    list.Add("Ekleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //////////////////////////////////
        ///
        [HttpPut("UpdatePost/{postID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdatePost(int postID, [FromBody] PostUpdateDto postUpdateDto)
        {
            var list = new List<string>();
            var validator = new PostUpdateValidator(_postService);
            var validationResults = validator.Validate(postUpdateDto);

            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                int result = await _postService.UpdatePost(postID, postUpdateDto);
                if (result > 0)
                {
                    list.Add("Güncelleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{postID} nolu branş bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Bu POSTA KODU MEVCUT mevcut.");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return BadRequest();
        }
       
        [HttpPut("UpdatePostStatusById/{postId}")]
        public async Task<ActionResult<string>> UpdatePostStatusById(int postId)
        {
            var list = new List<string>();
            var result = await _postService.PostStatusUpdateByPostId(postId);
            if (result > 0)
            {
                list.Add("İşlem Başarılı");
                return Ok(new { code = StatusCode(1000), message = list, type = "success" });
            }
            else
            {
                list.Add($"{postId} nolu postakodu bulunamadı");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }

        }


        //////////////////////////////////
        ///
        [HttpPut("UpdatePostByPostCode/{postCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdatePostByPostCode(string postCode, [FromBody] PostUpdateDto postUpdateDto)
        {
            var list = new List<string>();
            var validator = new PostUpdateValidator(_postService);
            var validationResults = validator.Validate(postUpdateDto);

            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                int result = await _postService.UpdatePostByPostCode(postCode, postUpdateDto);
                if (result > 0)
                {
                    list.Add("Güncelleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{postCode} nolu postakodu bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Bu POSTA KODU MEVCUT mevcut.");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return BadRequest();
        }

        /// //////////////

        [HttpDelete("DeletePost/{postID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeletePost(int postID)
        {
            var list = new List<string>();
            try
            {
                int result = await _postService.DeletePost(postID);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{postID} id branş bulunamadı. Silme işlemi başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else
                {
                    return BadRequest();
                }



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// //////////////

        [HttpDelete("DeletePostByPostCode/{postCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeletePostByPostCode(string postCode)
        {
            var list = new List<string>();
            try
            {
                int result = await _postService.DeletePostByPostCode(postCode);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{postCode} Postcode bulunamadı. Silme işlemi başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else
                {
                    return BadRequest();
                }



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}
