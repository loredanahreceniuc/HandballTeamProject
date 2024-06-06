using exp.NET6.Infrastructure.Entities;
using exp.NET6.Infrastructure.Repositories.StaffCategories;
using exp.NET6.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Services.DBServices.StaffService
{
    public class StaffService: IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IGenericService _genericService;

        public StaffService(IStaffRepository staffRepository, IGenericService genericService)
        {
            _staffRepository = staffRepository;
            _genericService= genericService;
        }

        public async Task<Pagination<StaffViewModel>>GetAllStaff(string? role, int pageNumber, int pageSize)
        {
            var pagination = new Pagination<StaffViewModel>();
            var staff = _staffRepository.GetAllQuerable();

            if (!String.IsNullOrWhiteSpace(role))
            {
                staff = staff.Where(x => x.Role == role);
            }

            var itemCount = await staff.CountAsync();
            pagination.PageDetails = new PageDetails()
            {
                TotalItemCount = itemCount,
                PageSize = pageSize,
                TotalPageCount = (int)Math.Ceiling((double)itemCount / pageSize),
            };

            pagination.Items = await staff.Skip(pageSize * (pageNumber - 1)).Take(pageSize).Select(x => new StaffViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDate = x.BirthDate.ToString("yyyy-MM-dd"),
                Role = x.Role,
                ImgBase64 = _genericService.GetImageFormat(x.ImgUrl),
            }).ToListAsync();
            return pagination;
        }

        public async Task<StaffViewModel>GetStaff(int id)
        {
            var Staff = await _staffRepository.Get(id);

            if(Staff == null)
            {
                throw new KeyNotFoundException("This staff does not exist!");
            }
            var returnStaff = new StaffViewModel()
            {
                Id = Staff.Id,
                FirstName = Staff.FirstName,
                LastName = Staff.LastName,
                BirthDate = Staff.BirthDate.ToString("yyyy-MM-dd"),
                Role = Staff.Role,
                ImgBase64 = _genericService.GetImageFormat(Staff.ImgUrl),
            };
            return returnStaff;
        }

        public async Task CreateStaff(CreateStaffViewModel createStaff)
        {
            var addStaff = new staff()
            {
               
                FirstName = createStaff.FirstName,
                LastName = createStaff.LastName,
                BirthDate = createStaff.BirthDate,
                Role = createStaff.Role,
                ImgUrl = _genericService.GetImagePath(createStaff.ImgBase64, null, "staff"),
            };
            await _staffRepository.Add(addStaff);
        }

        public async Task UpdateStaff(int id, UpdateStaffViewModel updateStaff)
        {
            var staff= await _staffRepository.Get(id);
            if(staff == null)
            {
                throw new KeyNotFoundException("This staff does not exist");
            }
            
             staff.FirstName = updateStaff.FirstName;
            staff.LastName = updateStaff.LastName;
            staff.BirthDate = updateStaff.BirthDate;
            staff.Role = updateStaff.Role;
            staff.ImgUrl = _genericService.GetImagePath(updateStaff.ImgBase64, staff.ImgUrl, "staff");
            await _staffRepository.Update(staff);
            
        }

        public async Task DeleteStaffPhysical(int id)
        {
            var staff= await _staffRepository.Get(id);
            if(staff == null)
            {
                throw new KeyNotFoundException("This staff does not exist");
                    
            }

            await _staffRepository.DeleteVirtual(id);
        }

        public async Task DeleteStaffVirtual(int id)
        {
            var staff = await _staffRepository.Get(id);

            if(staff == null)
            {
                throw new KeyNotFoundException("This staff does not exist");
            }

            staff.IsDeleted = true;

            await _staffRepository.Update(staff);
        }
    }
}
