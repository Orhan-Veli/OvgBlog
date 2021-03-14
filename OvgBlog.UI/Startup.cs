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
            services.AddControllersWithViews();
            services.AddSingleton<IArticleService, ArticleService>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ICommentService, CommentService>();
            services.AddSingleton<ITagService, TagService>();
            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton<IEntityRepository<User>, EntityRepository<User, OvgBlogContext>>();
            services.AddSingleton<IEntityRepository<Article>, EntityRepository<Article, OvgBlogContext>>();
            services.AddSingleton<IEntityRepository<Category>, EntityRepository<Category, OvgBlogContext>>();
            services.AddSingleton<IEntityRepository<Tag>, EntityRepository<Tag, OvgBlogContext>>();
            services.AddSingleton<IEntityRepository<Comment>, EntityRepository<Comment, OvgBlogContext>>();
            services.AddSingleton<IEntityRepository<ArticleTagRelation>, EntityRepository<ArticleTagRelation, OvgBlogContext>>();
            services.AddSingleton<IEntityRepository<ArticleCategoryRelation>, EntityRepository<ArticleCategoryRelation, OvgBlogContext>>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc().AddFluentValidation();


            services.AddTransient<IValidator<ArticleDetailViewModel>, ArticleDetailViewModelValidator>();
            services.AddTransient<IValidator<ArticleListViewModel>, ArticleListViewModelValidator>();
            services.AddTransient<IValidator<CategoryListViewModel>, CategoryListViewModelValidator>();
            services.AddTransient<IValidator<CommentViewModel>, CommentViewModelValidator>();
            services.AddTransient<IValidator<CategoryAddViewModel>, CategoryAddViewModelValidator>();
            services.AddTransient<IValidator<ArticleViewModel>, ArticleViewModelValidator>();
            services.AddTransient<IValidator<TagViewModel>, TagViewModelValidator>();
            services.AddTransient<IValidator<SendEmailViewModel>, SendEmailViewModelValidator>();
          

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
        {

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
            app.UseHttpsRedirection();
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
