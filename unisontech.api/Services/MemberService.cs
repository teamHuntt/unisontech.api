using unisontech.api.Common;
using unisontech.api.Models;
using unisontech.api.Models.DTOs;
using unisontech.api.Repository;

namespace unisontech.api.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<ApiResponse<List<MemberResponseDto>>> GetAllAsync()
        {
            var members = await _memberRepository.GetAllAsync();

            var result = members.Select(m => new MemberResponseDto
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                MembershipDate = m.MembershipDate
            }).ToList();

            return ApiResponse<List<MemberResponseDto>>.Ok(result);
        }

        public async Task<ApiResponse<MemberResponseDto>> GetByIdAsync(Guid id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member is null)
                return ApiResponse<MemberResponseDto>.Fail($"Member with ID {id} not found.");

            var result = new MemberResponseDto
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                MembershipDate = member.MembershipDate
            };

            return ApiResponse<MemberResponseDto>.Ok(result);
        }

        public async Task<ApiResponse<MemberResponseDto>> CreateAsync(MemberRequestDto dto)
        {
            bool isExists = await _memberRepository.IsEmailExistsAsync(dto.Email);
            if (isExists)
                return ApiResponse<MemberResponseDto>.Fail($"Member with email {dto.Email} already exists.");

            var member = new Member
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                MembershipDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                IsEmailVerified = false,
            };

            int created = await _memberRepository.CreateAsync(member);
            if (created > 0)
            {
                var result = new MemberResponseDto
                {
                    Id = member.Id,
                    Name = member.Name,
                    Email = member.Email,
                    Phone = member.Phone,
                    MembershipDate = member.MembershipDate
                };
                return ApiResponse<MemberResponseDto>.Ok(result, "Member created successfully.");
            }

            return ApiResponse<MemberResponseDto>.Fail("Failed to create member.");
        }

        public async Task<ApiResponse<MemberResponseDto>> UpdateAsync(Guid id, MemberRequestDto dto)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member is null)
                return ApiResponse<MemberResponseDto>.Fail($"Member with ID {id} not found.");

            bool isExists = await _memberRepository.IsEmailExistsAsync(dto.Email);
            if (isExists && member.Email != dto.Email)
                return ApiResponse<MemberResponseDto>.Fail($"Email {dto.Email} is already taken.");

            member.Name = dto.Name;
            member.Email = dto.Email;
            member.Phone = dto.Phone;
            member.IsActive = dto.IsActive;

            int updated = await _memberRepository.UpdateAsync(member);
            if (updated > 0)
            {
                var result = new MemberResponseDto
                {
                    Id = member.Id,
                    Name = member.Name,
                    Email = member.Email,
                    Phone = member.Phone,
                    MembershipDate = member.MembershipDate
                };
                return ApiResponse<MemberResponseDto>.Ok(result, "Member updated successfully.");
            }

            return ApiResponse<MemberResponseDto>.Fail("Failed to update member.");
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member is null)
                return ApiResponse<bool>.Fail($"Member with ID {id} not found.");

            int deleted = await _memberRepository.DeleteAsync(member);
            if (deleted > 0)
                return ApiResponse<bool>.Ok(true, "Member deleted successfully.");

            return ApiResponse<bool>.Fail("Failed to delete member.");
        }
    }
}
