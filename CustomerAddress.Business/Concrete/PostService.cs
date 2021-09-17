using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Address;
using CustomerAddress.Business.Dtos.City;
using CustomerAddress.Business.Dtos.Customer;
using CustomerAddress.Business.Dtos.District;
using CustomerAddress.Business.Dtos.Neighborhood;
using CustomerAddress.Business.Dtos.Post;
using CustomerAddress.Business.Dtos.Street;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class PostService : IPostService
    {
        private readonly CargoDbContext _customerAddressContext;
        private readonly ICustomerService _customerService;
        private readonly IAddressService _addressService;
        private readonly ICityService _cityService;
        private readonly ITownService _townService;
        private readonly INeighborhoodService _neighborhoodService;
        private readonly IStreetService _streetService;

        public PostService(CargoDbContext customerAddressContext, ICustomerService customerService, IAddressService addressService, ICityService cityService
            , ITownService townService, INeighborhoodService neighborhoodService, IStreetService streetService)
        {
            _customerAddressContext = customerAddressContext;
            _customerService = customerService;
            _addressService = addressService;
            _cityService = cityService;
            _townService = townService;
            _neighborhoodService = neighborhoodService;
            _streetService = streetService;
        }

        public Task<List<PostListDto>> GetPosts()
        {

            return _customerAddressContext.Posts.Where(p => !p.IsDeleted).Select(p => new PostListDto
            {
                Id = p.Id,
                PostCode = p.PostCode,
                PostStatus = p.PostStatus.ToString(),
                SenderId = p.SenderId,
                ReceiverId = p.ReceiverId,
                CompanyBranchId = p.CompanyBranchId,
                EmployeeId = p.EmployeeId,
                ShippingStartDate = p.ShippingStartDate,
                ShippingFinishDate = p.ShippingFinishDate

            }).ToListAsync();
        }


        public async Task<List<PostListNameDto>> GetPostsByCompanyBranchId(int companyBranchId)
        {
            return await _customerAddressContext.Posts.Where(p => !p.IsDeleted && p.CompanyBranchId == companyBranchId)
                                                      .Select(p => new PostListNameDto
                                                      {
                                                          Id = p.Id,
                                                          PostCode = p.PostCode,
                                                          PostStatus = p.PostStatus.ToString(),

                                                          SenderName = p.SenderFK.CustomerName,
                                                          SenderSurname = p.SenderFK.CustomerSurname,
                                                          SenderAddresss = p.SenderFK.AddressFK.Description,
                                                          SenderMail = p.SenderFK.Mail,
                                                          SenderTelNumber = p.SenderFK.TelNumber,

                                                          ReceiverName = p.ReceiverFK.CustomerName,
                                                          ReceiverSurname = p.ReceiverFK.CustomerSurname,
                                                          ReceiverAddresss = p.ReceiverFK.AddressFK.Description,
                                                          ReceiverMail = p.ReceiverFK.Mail,
                                                          ReceiverTelNumber = p.ReceiverFK.TelNumber,

                                                          CompanyBranchName = p.CompanyBranchFK.BranchName,
                                                          CompanyBranchNAddress = p.CompanyBranchFK.AddressFK.Description,

                                                          EmployeeName = p.EmployeeFK.EmployeeName,
                                                          EmployeeSurname = p.EmployeeFK.EmployeeSurname,
                                                          ShippingStartDate = p.ShippingStartDate,
                                                          ShippingFinishDate = p.ShippingFinishDate
                                                      }).ToListAsync();

        }
        public async Task<List<PostListNameDto>> GetPostsWithNames()
        {
            //List<PostListNameDto> PostListNames = new List<PostListNameDto>();
            //PostListNameDto temp = new PostListNameDto();

            return await _customerAddressContext.Posts.Where(p => !p.IsDeleted).Include(p => p.SenderFK).Include(P => P.ReceiverFK)
                 .Include(P => P.CompanyBranchFK).Include(p => p.EmployeeFK).Include(p => p.SenderFK.AddressFK).Include(p => p.ReceiverFK.AddressFK).Include(p => p.CompanyBranchFK.AddressFK).Select(p => new PostListNameDto
                 {
                     Id = p.Id,
                     PostCode = p.PostCode,
                     PostStatus = p.PostStatus.ToString(),

                     SenderName = p.SenderFK.CustomerName,
                     SenderSurname = p.SenderFK.CustomerSurname,
                     SenderAddresss = p.SenderFK.AddressFK.Description,
                     SenderMail = p.SenderFK.Mail,
                     SenderTelNumber = p.SenderFK.TelNumber,

                     ReceiverName = p.ReceiverFK.CustomerName,
                     ReceiverSurname = p.ReceiverFK.CustomerSurname,
                     ReceiverAddresss = p.ReceiverFK.AddressFK.Description,
                     ReceiverMail = p.ReceiverFK.Mail,
                     ReceiverTelNumber = p.ReceiverFK.TelNumber,

                     CompanyBranchName = p.CompanyBranchFK.BranchName,
                     CompanyBranchNAddress = p.CompanyBranchFK.AddressFK.Description,

                     EmployeeName = p.EmployeeFK.EmployeeName,
                     EmployeeSurname = p.EmployeeFK.EmployeeSurname,
                     ShippingStartDate = p.ShippingStartDate,
                     ShippingFinishDate = p.ShippingFinishDate


                 }).ToListAsync();

            //foreach (var item in Posts)
            //{
            //    temp.PostCode = item.PostCode;
            //    temp.PostStatus = item.PostStatus;


            //    temp.SenderName =  _customerAddressContext.Customers.Where(p=>!p.IsDeleted && p.Id == item.SenderId).Select(p=>p.CustomerName).FirstOrDefault();
            //    temp.SenderSurname = _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.Id == item.SenderId).Select(p => p.CustomerSurname).FirstOrDefault();
            //    temp.SenderMail = _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.Id == item.SenderId).Select(p => p.Mail).FirstOrDefault();
            //    temp.SenderTelNumber = _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.Id == item.SenderId).Select(p => p.TelNumber).FirstOrDefault();
            //    var senderAddress = _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.Id==item.SenderId).Select(p => p.AddressId).FirstOrDefault();
            //    temp.SenderAddresss = _customerAddressContext.Addresses.Where(p => !p.IsDeleted &&p.Id==senderAddress).Select(p => p.Description).FirstOrDefault();

            //    temp.ReceiverName = _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.Id == item.ReceiverId).Select(p => p.CustomerName).FirstOrDefault();
            //    temp.ReceiverSurname = _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.Id == item.ReceiverId).Select(p => p.CustomerSurname).FirstOrDefault();
            //    temp.ReceiverMail = _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.Id == item.ReceiverId).Select(p => p.Mail).FirstOrDefault();
            //    temp.ReceiverTelNumber = _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.Id == item.ReceiverId).Select(p => p.TelNumber).FirstOrDefault();
            //    var receiverAddress = _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.Id == item.ReceiverId).Select(p => p.AddressId).FirstOrDefault();
            //    temp.ReceiverAddresss = _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.Id == receiverAddress).Select(p => p.Description).FirstOrDefault();

            //    temp.CompanyBranchName = _customerAddressContext.CompanyBranches.Where(p => !p.IsDeleted && p.Id == item.CompanyBranchId).Select(p => p.BranchName).FirstOrDefault();
            //    temp.CompanyBranchNAddress = _customerAddressContext.CompanyBranches.Where(p => !p.IsDeleted && p.Id == item.CompanyBranchId).Select(p => p.BranchName).FirstOrDefault();

            //    temp.EmployeeName = _customerAddressContext.Employees.Where(p => !p.IsDeleted && p.Id == item.EmployeeId).Select(p => p.EmployeeName).FirstOrDefault();
            //    temp.EmployeeSurname = _customerAddressContext.Employees.Where(p => !p.IsDeleted && p.Id == item.EmployeeId).Select(p => p.EmployeeSurname).FirstOrDefault();
            //    PostListNames.Add(temp);

            //}

            //return PostListNames;

            //return PostListNames
            // {
            //     Id = p.Id,
            //     PostCode = p.PostCode,
            //     PostStatus = p.PostStatus,
            //     SenderId = p.SenderId,
            //     ReceiverId = p.ReceiverId,
            //     CompanyBranchId = p.CompanyBranchId,
            //     EmployeeId = p.EmployeeId,

            // };

        }

        public Task<PostListDto> GetPostsById(int id)
        {
            return _customerAddressContext.Posts.Where(p => !p.IsDeleted)
           .Select(p => new PostListDto
           {
               Id = p.Id,
               PostCode = p.PostCode,
               PostStatus = p.PostStatus.ToString(),
               SenderId = p.SenderId,
               ReceiverId = p.ReceiverId,
               CompanyBranchId = p.CompanyBranchId,
               EmployeeId = p.EmployeeId,
               ShippingStartDate = p.ShippingStartDate,
               ShippingFinishDate = p.ShippingFinishDate

           }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<PostListDto> GetPostsByPostCode(string postCode)
        {
            var id = _customerAddressContext.Posts.Where(p => !p.IsDeleted && p.PostCode == postCode).Select(p => p.Id).FirstOrDefault();
            return _customerAddressContext.Posts
                                           .Where(p => !p.IsDeleted)
                                           .Select(p => new PostListDto
                                           {
                                               Id = p.Id,
                                               PostCode = p.PostCode,
                                               PostStatus = p.PostStatus.ToString(),
                                               SenderId = p.SenderId,
                                               ReceiverId = p.ReceiverId,
                                               CompanyBranchId = p.CompanyBranchId,
                                               EmployeeId = p.EmployeeId,
                                               ShippingStartDate = p.ShippingStartDate,
                                               ShippingFinishDate = p.ShippingFinishDate

                                           }).FirstOrDefaultAsync(p => p.Id == id);
        }


       


        
        

        public async Task<List<PostListNameDto>> PostListByPostStatus(byte postStatus)
        {
            //List<PostListNameDto> PostListNames = new List<PostListNameDto>();
            //PostListNameDto temp = new PostListNameDto();

            return await _customerAddressContext.Posts.Where(p => !p.IsDeleted && p.PostStatus==postStatus).Include(p => p.SenderFK).Include(P => P.ReceiverFK)
                 .Include(P => P.CompanyBranchFK).Include(p => p.EmployeeFK).Include(p => p.SenderFK.AddressFK).Include(p => p.ReceiverFK.AddressFK).Include(p => p.CompanyBranchFK.AddressFK).Select(p => new PostListNameDto
                 {
                     Id = p.Id,
                     PostCode = p.PostCode,
                     PostStatus = p.PostStatus.ToString(),

                     SenderName = p.SenderFK.CustomerName,
                     SenderSurname = p.SenderFK.CustomerSurname,
                     SenderAddresss = p.SenderFK.AddressFK.Description,
                     SenderMail = p.SenderFK.Mail,
                     SenderTelNumber = p.SenderFK.TelNumber,

                     ReceiverName = p.ReceiverFK.CustomerName,
                     ReceiverSurname = p.ReceiverFK.CustomerSurname,
                     ReceiverAddresss = p.ReceiverFK.AddressFK.Description,
                     ReceiverMail = p.ReceiverFK.Mail,
                     ReceiverTelNumber = p.ReceiverFK.TelNumber,

                     CompanyBranchName = p.CompanyBranchFK.BranchName,
                     CompanyBranchNAddress = p.CompanyBranchFK.AddressFK.Description,

                     EmployeeName = p.EmployeeFK.EmployeeName,
                     EmployeeSurname = p.EmployeeFK.EmployeeSurname,
                     ShippingStartDate = p.ShippingStartDate,
                     ShippingFinishDate = p.ShippingFinishDate,


                 }).ToListAsync();
        }


        public async Task<List<PostListNameDto>> PostListByPostStatusAndBranchId(byte postStatus, int branchId)
        {
            //List<PostListNameDto> PostListNames = new List<PostListNameDto>();
            //PostListNameDto temp = new PostListNameDto();

            return await _customerAddressContext.Posts.Where(p => !p.IsDeleted && p.PostStatus == postStatus && p.CompanyBranchId==branchId).Include(p => p.SenderFK).Include(P => P.ReceiverFK)
                 .Include(P => P.CompanyBranchFK).Include(p => p.EmployeeFK).Include(p => p.SenderFK.AddressFK).Include(p => p.ReceiverFK.AddressFK).Include(p => p.CompanyBranchFK.AddressFK).Select(p => new PostListNameDto
                 {
                     Id = p.Id,
                     PostCode = p.PostCode,
                     PostStatus = p.PostStatus.ToString(),

                     SenderName = p.SenderFK.CustomerName,
                     SenderSurname = p.SenderFK.CustomerSurname,
                     SenderAddresss = p.SenderFK.AddressFK.Description,
                     SenderMail = p.SenderFK.Mail,
                     SenderTelNumber = p.SenderFK.TelNumber,

                     ReceiverName = p.ReceiverFK.CustomerName,
                     ReceiverSurname = p.ReceiverFK.CustomerSurname,
                     ReceiverAddresss = p.ReceiverFK.AddressFK.Description,
                     ReceiverMail = p.ReceiverFK.Mail,
                     ReceiverTelNumber = p.ReceiverFK.TelNumber,

                     CompanyBranchName = p.CompanyBranchFK.BranchName,
                     CompanyBranchNAddress = p.CompanyBranchFK.AddressFK.Description,

                     EmployeeName = p.EmployeeFK.EmployeeName,
                     EmployeeSurname = p.EmployeeFK.EmployeeSurname,
                     ShippingStartDate = p.ShippingStartDate,
                     ShippingFinishDate = p.ShippingFinishDate


                 }).ToListAsync();
        }

        public bool AnySenderIds(int senderId)
        {
            return _customerAddressContext.Customers.Where(p => !p.IsDeleted).Any(p => p.Id == senderId);
        }

        public bool AnyReceivedIds(int receivedId)
        {
            return _customerAddressContext.Customers.Where(p => !p.IsDeleted).Any(p => p.Id == receivedId);
        }

        public bool AnyCompanyBranchIds(int companyBranchId)
        {
            return _customerAddressContext.CompanyBranches.Where(p => !p.IsDeleted).Any(p => p.Id == companyBranchId);
        }

        public bool AnyEmployeeIds(int employeeId)
        {
            return _customerAddressContext.Employees.Where(p => !p.IsDeleted).Any(p => p.Id == employeeId);
        }

        public async Task<int> AddPost(PostAddDto postAddDto)
        {
            var Postcode = Guid.NewGuid().ToString();
            Postcode = Postcode.Substring(0, 8);
            var result = await AnyPost(Postcode);

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var newPost = new Post
                {
                    PostCode = Postcode,
                    PostStatus = postAddDto.PostStatus,
                    SenderId = postAddDto.SenderId,
                    ReceiverId = postAddDto.ReceiverId,
                    CompanyBranchId = postAddDto.CompanyBranchId,
                    EmployeeId = postAddDto.EmployeeId,
                    ShippingStartDate = DateTime.Now.ToLocalTime(),
                    ShippingFinishDate = DateTime.Now.ToLocalTime(),

                };
                _customerAddressContext.Posts.Add(newPost);
                return await _customerAddressContext.SaveChangesAsync();

            }
        }


        public async Task<int> AddPostWithName(PostAddNameDto postAddNameDto)
        {

            var Postcode = Guid.NewGuid().ToString();
            Postcode = Postcode.Substring(0, 8);
            var result = await AnyPost(Postcode);
            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                CustomerAddDto SenderObject = new CustomerAddDto();
                CustomerAddDto ReceiverObject = new CustomerAddDto();
                AddressAddDto AddressObject = new AddressAddDto();
                CityAddDto CityObject = new CityAddDto();
                TownAddDto TownObject = new TownAddDto();
                NeighborhoodAddDto NeighborhoodObject = new NeighborhoodAddDto();
                StreetAddDto StreetObject = new StreetAddDto();
                PostAddDto PostObject = new PostAddDto();


                //////////////////////////////////////Sender///////////////////////////////////////
                if (await _cityService.AnyCity(postAddNameDto.SenderCity))
                {
                    AddressObject.CityId = _customerAddressContext.Cities.Where(p => !p.IsDeleted && p.CityName == postAddNameDto.SenderCity).Select(p => p.Id).FirstOrDefault();
                }
                else
                {
                    CityObject.CityName = postAddNameDto.SenderCity;
                    CityObject.CityRegion = postAddNameDto.SenderRegion;
                    await _cityService.AddCity(CityObject);
                    AddressObject.CityId = _customerAddressContext.Cities.Where(p => !p.IsDeleted && p.CityName == postAddNameDto.SenderCity).Select(p => p.Id).FirstOrDefault();
                }

                TownObject.CityId = AddressObject.CityId;
                TownObject.TownName = postAddNameDto.SenderTown;

                if (await _townService.AnyTown(TownObject))
                {
                    AddressObject.TownId = _customerAddressContext.Towns.Where(p => !p.IsDeleted && p.CityId == TownObject.CityId && p.TownName == TownObject.TownName).Select(p => p.Id).FirstOrDefault();
                }
                else
                {
                    await _townService.AddTown(TownObject);
                    AddressObject.TownId = _customerAddressContext.Towns.Where(p => !p.IsDeleted && p.CityId == TownObject.CityId && p.TownName == TownObject.TownName).Select(p => p.Id).FirstOrDefault();
                }

                NeighborhoodObject.TownId = AddressObject.TownId;
                NeighborhoodObject.NeighborhoodName = postAddNameDto.SenderNeighborhood;

                if (await _neighborhoodService.AnyNeighborhood(NeighborhoodObject))
                {
                    AddressObject.NeighborhoodId = _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted && NeighborhoodObject.TownId == p.TownId && p.NeighborhoodName == NeighborhoodObject.NeighborhoodName)
                        .Select(p => p.Id).FirstOrDefault();
                }
                else
                {
                    await _neighborhoodService.AddNeighborhood(NeighborhoodObject);
                    AddressObject.NeighborhoodId = _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted && NeighborhoodObject.TownId == p.TownId && p.NeighborhoodName == NeighborhoodObject.NeighborhoodName)
                        .Select(p => p.Id).FirstOrDefault();
                }

                StreetObject.NeighborhoodId = AddressObject.NeighborhoodId;
                StreetObject.StreetName = postAddNameDto.SenderStreet;

                if (await _streetService.AnyStreet(StreetObject))
                {
                    AddressObject.StreetId = _customerAddressContext.Streets.Where(p => !p.IsDeleted && p.StreetName == StreetObject.StreetName && p.NeighborhoodId == StreetObject.NeighborhoodId)
                        .Select(p => p.Id).FirstOrDefault();
                }
                else
                {
                    await _streetService.AddStreet(StreetObject);
                    AddressObject.StreetId = _customerAddressContext.Streets.Where(p => !p.IsDeleted && p.StreetName == StreetObject.StreetName && p.NeighborhoodId == StreetObject.NeighborhoodId)
                        .Select(p => p.Id).FirstOrDefault();
                }

                AddressObject.Description = postAddNameDto.SenderDescription;
                AddressObject.Title = "Sender";
                await _addressService.AddAddress(AddressObject);
                SenderObject.CustomerName = postAddNameDto.SenderName;
                SenderObject.CustomerSurname = postAddNameDto.SenderSurname;
                SenderObject.AddressId = await _addressService.GetAddressIdByAll(AddressObject);
                SenderObject.Mail = postAddNameDto.SenderMail;
                SenderObject.TelNumber = postAddNameDto.SenderTelNumber;
                SenderObject.CustomerType = true;
                await _customerService.AddCustomer(SenderObject);
                //////////////////////////RECEİVER///////////////////////////////////////
                //////////////////////////////////////////
                ///
                if (await _cityService.AnyCity(postAddNameDto.ReceiverCity))
                {
                    AddressObject.CityId = _customerAddressContext.Cities.Where(p => !p.IsDeleted && p.CityName == postAddNameDto.ReceiverCity).Select(p => p.Id).FirstOrDefault();
                }
                else
                {
                    CityObject.CityName = postAddNameDto.ReceiverCity;
                    CityObject.CityRegion = postAddNameDto.ReceiverRegion;
                    await _cityService.AddCity(CityObject);
                    AddressObject.CityId = _customerAddressContext.Cities.Where(p => !p.IsDeleted && p.CityName == postAddNameDto.ReceiverCity).Select(p => p.Id).FirstOrDefault();
                }

                TownObject.CityId = AddressObject.CityId;
                TownObject.TownName = postAddNameDto.ReceiverTown;

                if (await _townService.AnyTown(TownObject))
                {
                    AddressObject.TownId = _customerAddressContext.Towns.Where(p => !p.IsDeleted && p.CityId == TownObject.CityId && p.TownName == TownObject.TownName).Select(p => p.Id).FirstOrDefault();
                }
                else
                {
                    await _townService.AddTown(TownObject);
                    AddressObject.TownId = _customerAddressContext.Towns.Where(p => !p.IsDeleted && p.CityId == TownObject.CityId && p.TownName == TownObject.TownName).Select(p => p.Id).FirstOrDefault();
                }

                NeighborhoodObject.TownId = AddressObject.TownId;
                NeighborhoodObject.NeighborhoodName = postAddNameDto.ReceiverNeighborhood;

                if (await _neighborhoodService.AnyNeighborhood(NeighborhoodObject))
                {
                    AddressObject.NeighborhoodId = _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted && NeighborhoodObject.TownId == p.TownId && p.NeighborhoodName == NeighborhoodObject.NeighborhoodName)
                        .Select(p => p.Id).FirstOrDefault();
                }
                else
                {
                    await _neighborhoodService.AddNeighborhood(NeighborhoodObject);
                    AddressObject.NeighborhoodId = _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted && NeighborhoodObject.TownId == p.TownId && p.NeighborhoodName == NeighborhoodObject.NeighborhoodName)
                        .Select(p => p.Id).FirstOrDefault();
                }

                StreetObject.NeighborhoodId = AddressObject.NeighborhoodId;
                StreetObject.StreetName = postAddNameDto.ReceiverStreet;

                if (await _streetService.AnyStreet(StreetObject))
                {
                    AddressObject.StreetId = _customerAddressContext.Streets.Where(p => !p.IsDeleted && p.StreetName == StreetObject.StreetName && p.NeighborhoodId == StreetObject.NeighborhoodId)
                        .Select(p => p.Id).FirstOrDefault();
                }
                else
                {
                    await _streetService.AddStreet(StreetObject);
                    AddressObject.StreetId = _customerAddressContext.Streets.Where(p => !p.IsDeleted && p.StreetName == StreetObject.StreetName && p.NeighborhoodId == StreetObject.NeighborhoodId)
                        .Select(p => p.Id).FirstOrDefault();
                }
                AddressObject.Description = postAddNameDto.ReceiverDescription;
                AddressObject.Title = "Receiver";
                await _addressService.AddAddress(AddressObject);

                ReceiverObject.CustomerName = postAddNameDto.ReceiverName;
                ReceiverObject.CustomerSurname = postAddNameDto.ReceiverSurname;
                ReceiverObject.AddressId = await _addressService.GetAddressIdByAll(AddressObject);
                ReceiverObject.Mail = postAddNameDto.ReceiverMail;
                ReceiverObject.TelNumber = postAddNameDto.ReceiverTelNumber;
                ReceiverObject.CustomerType = false;
                await _customerService.AddCustomer(ReceiverObject);


                PostObject.SenderId = await _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.CustomerName == SenderObject.CustomerName && p.CustomerSurname == SenderObject.CustomerSurname &&
                                     p.AddressId == SenderObject.AddressId && p.Mail == SenderObject.Mail && p.TelNumber == SenderObject.TelNumber && p.CustomerType == SenderObject.CustomerType)
                                    .Select(p => p.Id).FirstOrDefaultAsync();
                PostObject.PostStatus = 1;
                PostObject.ReceiverId = await _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.CustomerName == ReceiverObject.CustomerName && p.CustomerSurname == ReceiverObject.CustomerSurname &&
                                      p.AddressId == ReceiverObject.AddressId && p.Mail == ReceiverObject.Mail && p.TelNumber == ReceiverObject.TelNumber && p.CustomerType == ReceiverObject.CustomerType)
                                    .Select(p => p.Id).FirstOrDefaultAsync();

                PostObject.CompanyBranchId = postAddNameDto.CompanyBranchId;
                PostObject.EmployeeId = postAddNameDto.CompanyBranchId;
                PostObject.ShippingStartDate = DateTime.Now.ToLocalTime();
                PostObject.ShippingFinishDate = DateTime.Now.ToLocalTime();
                await AddPost(PostObject);



                return await Task.FromResult(1);



                //_customerAddressContext.Customers

            }

        }

        public async Task<bool> AnyPost(string Postcode)
        {
            return await _customerAddressContext.Posts.Where(p => !p.IsDeleted).AnyAsync(p => p.PostCode == Postcode);

        }

        public async Task<int> DeletePost(int id)
        {

            var PostObject = _customerAddressContext.Posts.SingleOrDefault(p => !p.IsDeleted && p.Id == id);
            if (PostObject != null)
            {
                PostObject.IsDeleted = true;
                _customerAddressContext.Posts.Update(PostObject);
                return await _customerAddressContext.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }


        
        public async Task<int> UpdatePost(int id, PostUpdateDto postUpdateDto)
        {
            var PostCode = _customerAddressContext.Posts.Where(p => !p.IsDeleted && p.Id == id).Select(p => p.PostCode).FirstOrDefault();
            var result = await AnyPostUpdate(PostCode, id);

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var PostObject = await _customerAddressContext.Posts.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == id);

                if (PostObject == null)
                {
                    return await Task.FromResult(-1);
                }

                PostObject.PostStatus = postUpdateDto.PostStatus;
                PostObject.SenderId = postUpdateDto.SenderId;
                PostObject.ReceiverId = postUpdateDto.ReceiverId;
                PostObject.CompanyBranchId = postUpdateDto.CompanyBranchId;
                PostObject.EmployeeId = postUpdateDto.EmployeeId;
                PostObject.ShippingStartDate = postUpdateDto.ShippingStartDate;
                PostObject.ShippingFinishDate = postUpdateDto.ShippingFinishDate;



                _customerAddressContext.Posts.Update(PostObject);
                return await _customerAddressContext.SaveChangesAsync();
            }
        }

        public async Task<bool> AnyPostUpdate(string PostCode, int id)
        {

            var AnyPost = await _customerAddressContext.Posts.Where(p => !p.IsDeleted && p.Id != id).AnyAsync(p => p.PostCode == PostCode);


            if (AnyPost)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }


        }



        public async Task<int> UpdatePostByPostCode(string PostCode, PostUpdateDto postUpdateDto)
        {
            var id = _customerAddressContext.Posts.Where(p => !p.IsDeleted && p.PostCode == PostCode).Select(p => p.Id).FirstOrDefault();
            var result = await AnyPostUpdate(PostCode, id);

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var PostObject = await _customerAddressContext.Posts.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == id);

                if (PostObject == null)
                {
                    return await Task.FromResult(-1);
                }

                PostObject.PostStatus = postUpdateDto.PostStatus;
                PostObject.SenderId = postUpdateDto.SenderId;
                PostObject.ReceiverId = postUpdateDto.ReceiverId;
                PostObject.CompanyBranchId = postUpdateDto.CompanyBranchId;
                PostObject.EmployeeId = postUpdateDto.EmployeeId;
                PostObject.ShippingStartDate = postUpdateDto.ShippingStartDate;
                PostObject.ShippingFinishDate = postUpdateDto.ShippingFinishDate;



                _customerAddressContext.Posts.Update(PostObject);
                return await _customerAddressContext.SaveChangesAsync();
            }
        }

        public async Task<int> PostStatusUpdateByPostId(int postId)
        {
            //var any = _customerAddressContext.Posts.Where(p => !p.IsDeleted).AnyAsync(p => p.Id == postId);
            var result = _customerAddressContext.Posts.Where(p => !p.IsDeleted && p.Id== postId).FirstOrDefault();

            if (result != null)
            {
                result.PostStatus = 2;
                result.ShippingFinishDate = DateTime.Now.ToLocalTime();
                _customerAddressContext.Posts.Update(result);
                return await _customerAddressContext.SaveChangesAsync();
            }
            else if (result == null)
            {
                return await Task.FromResult(-1);
            }
            else
            {
                return await Task.FromResult(-10);
            }
            

        }


        public async Task<int> DeletePostByPostCode(string PostCode)
        {
            var PostObject = await _customerAddressContext.Posts.Where(p => !p.IsDeleted && p.PostCode == PostCode).SingleOrDefaultAsync();
            if (PostObject != null)
            {
                PostObject.IsDeleted = true;
                _customerAddressContext.Posts.Update(PostObject);
                return await _customerAddressContext.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }


    }
}
