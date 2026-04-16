using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class PhotoRepository(AppDbContext context) : IPhotoRepository
{
    public Task<Photo?> GetPhotoById(int id)
    {
        var query = context.Photos.Where(p => p.Id == id);

        return query.IgnoreQueryFilters().FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<PhotoForApprovalDto>> GetUnapprovedPhotos()
    {
        var query = context.Photos
        .IgnoreQueryFilters()
        .Where(p => !p.IsApproved)
        .Select(p => new PhotoForApprovalDto
        {
            Id = p.Id,
            Url = p.Url,
            UserId = p.MemberId,
            IsApproved = p.IsApproved
        });

        return (await query.ToListAsync()).AsReadOnly();
    }

    public void RemovePhoto(Photo photo)
    {
        context.Photos.Remove(photo);
    }
}
