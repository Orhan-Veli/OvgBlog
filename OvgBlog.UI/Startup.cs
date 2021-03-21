using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OvgBlog.Business.Abstract;
using OvgBlog.Business.Services;
using OvgBlog.DAL.Abstract;
using OvgBlog.DAL.Concrete;
using OvgBlog.DAL.Data;
using OvgBlog.DAL;
using OvgBlog.UI.Models;
using OvgBlog.UI.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OvgBlog.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {          
 

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc().AddFluentValidation();

            services.AddScoped<IEntityRepository<User>, EntityRepository<User>>();
            services.AddScoped<IEntityRepository<Article>, EntityRepository<Article>>();
            services.AddScoped<IEntityRepository<Category>, EntityRepository<Category>>();
            services.AddScoped<IEntityRepository<Tag>, EntityRepository<Tag>>();
            services.AddScoped<IEntityRepository<Comment>, EntityRepository<Comment>>();
            services.AddScoped<IEntityRepository<Contact>, EntityRepository<Contact>>();
            services.AddScoped<IEntityRepository<ArticleTagRelation>, EntityRepository<ArticleTagRelation>>();
            services.AddScoped<IEntityRepository<ArticleCategoryRelation>, EntityRepository<ArticleCategoryRelation>>();

            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactService, ContactService>();


            services.AddDbContextPool<OvgBlogContext>(x => x.UseSqlServer(Configuration["ConnectionStrings"].ToString()));           
            

            services.AddTransient<IValidator<ArticleDetailViewModel>, ArticleDetailViewModelValidator>();
            services.AddTransient<IValidator<ArticleListViewModel>, ArticleListViewModelValidator>();
            services.AddTransient<IValidator<CategoryListViewModel>, CategoryListViewModelValidator>();
            services.AddTransient<IValidator<CommentViewModel>, CommentViewModelValidator>();
            services.AddTransient<IValidator<CategoryAddViewModel>, CategoryAddViewModelValidator>();
            services.AddTransient<IValidator<ArticleViewModel>, ArticleViewModelValidator>();
            services.AddTransient<IValidator<TagViewModel>, TagViewModelValidator>();
            services.AddTransient<IValidator<SendEmailViewModel>, SendEmailViewModelValidator>();
            services.AddTransient<IValidator<ContactListViewModel>, ContactListViewModelValidator>();
          

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = "AUTHCOOKIE";
                options.LoginPath = "/Login";
            });
            services.AddRazorPages();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {       app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
