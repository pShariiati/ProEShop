using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class CommentReportService : CustomGenericService<CommentReport>, ICommentReportService
{
    private readonly DbSet<CommentReport> _commentReports;
    private readonly IMapper _mapper;

    public CommentReportService(
        IUnitOfWork uow,
        IMapper mapper) : base(uow)
    {
        _mapper = mapper;
        _commentReports = uow.Set<CommentReport>();
    }
}