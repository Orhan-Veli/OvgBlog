using System.Threading;
using System.Threading.Tasks;
using OvgBlog.Business.Dto;

namespace OvgBlog.Business.Abstract;

public interface IHomeService
{
    Task<HomeDto> GetHomeAsync(CancellationToken cancellationToken);
}