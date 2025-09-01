using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Data;

namespace OvgBlog.DAL.Concrete;

public class CategoryRepository(OvgBlogContext context) : EntityRepository<Category>(context), ICategoryRepository
{
    
}